﻿using Jackett.Utils.Clients;
using NLog;
using Jackett.Services;
using Jackett.Utils;
using Jackett.Models;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Text;
using System.Globalization;
using Jackett.Models.IndexerConfig;
using System.Collections.Specialized;
using AngleSharp.Parser.Html;
using AngleSharp.Dom;
using System.Text.RegularExpressions;
using System.Web;

namespace Jackett.Indexers
{
    public class Torrentech : BaseIndexer, IIndexer
    {
        string LoginUrl { get { return SiteLink + "index.php?act=Login&CODE=01&CookieDate=1"; } }
        string IndexUrl { get { return SiteLink + "index.php"; } }

        new ConfigurationDataBasicLoginWithRSSAndDisplay configData
        {
            get { return (ConfigurationDataBasicLoginWithRSSAndDisplay)base.configData; }
            set { base.configData = value; }
        }

        public Torrentech(IIndexerManagerService i, IWebClient wc, Logger l, IProtectionService ps)
            : base(name: "Torrentech",
                   description: null,
                   link: "https://www.torrentech.org/",
                   caps: TorznabUtil.CreateDefaultTorznabTVCaps(),
                   manager: i,
                   client: wc,
                   logger: l,
                   p: ps,
                   configData: new ConfigurationDataBasicLoginWithRSSAndDisplay())
        {
            Encoding = Encoding.UTF8;
            Language = "en-us";
            Type = "private";

            AddCategoryMapping(1, TorznabCatType.AudioMP3);
            AddCategoryMapping(2, TorznabCatType.AudioLossless);
            AddCategoryMapping(3, TorznabCatType.AudioOther);
        }

        public async Task<IndexerConfigurationStatus> ApplyConfiguration(JToken configJson)
        {
            LoadValuesFromJson(configJson);

            var pairs = new Dictionary<string, string>
            {
                { "UserName", configData.Username.Value },
                { "PassWord", configData.Password.Value }
            };

            var result = await RequestLoginAndFollowRedirect(LoginUrl, pairs, null, true, null, LoginUrl, true);
            await ConfigureIfOK(result.Cookies, result.Content != null && result.Content.Contains("Logged in as: "), () =>
            {
                var errorMessage = result.Content;
                throw new ExceptionWithConfigData(errorMessage, configData);
            });
            return IndexerConfigurationStatus.RequiresTesting;
        }

        public async Task<IEnumerable<ReleaseInfo>> PerformQuery(TorznabQuery query)
        {
            var releases = new List<ReleaseInfo>();
            var searchString = query.GetQueryString();

            WebClientStringResult results = null;
            var queryCollection = new NameValueCollection();

            queryCollection.Add("act", "search");
            queryCollection.Add("forums", "all");
            queryCollection.Add("torrents", "1");
            queryCollection.Add("search_in", "titles");
            queryCollection.Add("result_type", "topics");

            // if the search string is empty use the getnew view
            if (string.IsNullOrWhiteSpace(searchString))
            {
                queryCollection.Add("CODE", "getnew");
                queryCollection.Add("active", "1");
            }
            else // use the normal search
            {
                searchString = searchString.Replace("-", " ");
                queryCollection.Add("CODE", "01");
                queryCollection.Add("keywords", searchString);
            }

            var searchUrl = IndexUrl + "?" + queryCollection.GetQueryString();
            results = await RequestStringWithCookies(searchUrl);
            if (results.IsRedirect && results.RedirectingTo.Contains("CODE=show"))
            {
                results = await RequestStringWithCookies(results.RedirectingTo);
            }
            try
            {
                string RowsSelector = "div.borderwrap:has(div.maintitle) > table > tbody > tr:has(a[href*=\"index.php?showtopic=\"])";

                var SearchResultParser = new HtmlParser();
                var SearchResultDocument = SearchResultParser.Parse(results.Content);
                var Rows = SearchResultDocument.QuerySelectorAll(RowsSelector);
                foreach (var Row in Rows)
                {
                    try
                    {
                        var release = new ReleaseInfo();

                        var StatsElements = Row.QuerySelector("td:nth-child(5)");
                        var stats = StatsElements.TextContent.Split('·');
                        if (stats.Length != 3) // not a torrent
                            continue;

                        release.Seeders = ParseUtil.CoerceInt(stats[0]);
                        release.Peers = ParseUtil.CoerceInt(stats[1]) + release.Seeders;
                        release.Grabs = ParseUtil.CoerceInt(stats[2]);

                        release.MinimumRatio = 0.51;
                        release.MinimumSeedTime = 0;

                        var qDetailsLink = Row.QuerySelector("a[onmouseover][href*=\"index.php?showtopic=\"]");
                        release.Title = qDetailsLink.TextContent;
                        release.Comments = new Uri(qDetailsLink.GetAttribute("href"));
                        release.Link = release.Comments;
                        release.Guid = release.Link;

                        release.DownloadVolumeFactor = 1;
                        release.UploadVolumeFactor = 1;

                        var id = HttpUtility.ParseQueryString(release.Comments.Query).Get("showtopic");

                        var desc = Row.QuerySelector("span.desc");
                        var forange = desc.QuerySelector("font.forange");
                        if (forange != null)
                        {
                            var DownloadVolumeFactor = forange.QuerySelector("i:contains(\"freeleech\")");
                            if (DownloadVolumeFactor != null)
                                release.DownloadVolumeFactor = 0;

                            var UploadVolumeFactor = forange.QuerySelector("i:contains(\"x upload]\")");
                            if (UploadVolumeFactor != null)
                                release.UploadVolumeFactor = ParseUtil.CoerceDouble(UploadVolumeFactor.TextContent.Split(' ')[0].Substring(1).Replace("x", ""));
                            forange.Remove();
                        }
                        var format = desc.TextContent;
                        release.Title += " [" + format + "]";

                        var preview = SearchResultDocument.QuerySelector("div#d21-tph-preview-data-" + id);
                        if (preview != null)
                        {
                            release.Description = "";
                            foreach (var e in preview.ChildNodes)
                            {
                                if (e.NodeType == NodeType.Text)
                                    release.Description += e.NodeValue;
                                else
                                    release.Description += e.TextContent + "\n";
                            }
                        }
                        release.Description = HttpUtility.HtmlEncode(release.Description.Trim());
                        release.Description = release.Description.Replace("\n", "<br>");

                        if (format.Contains("MP3"))
                            release.Category = new List<int> { TorznabCatType.AudioMP3.ID };
                        else if (format.Contains("AAC"))
                            release.Category = new List<int> { TorznabCatType.AudioOther.ID };
                        else if (format.Contains("Lossless"))
                            release.Category = new List<int> { TorznabCatType.AudioLossless.ID };
                        else
                            release.Category = new List<int> { TorznabCatType.AudioOther.ID };

                        var lastAction = Row.QuerySelector("td:nth-child(9) > span").FirstChild.NodeValue;
                        release.PublishDate = DateTimeUtil.FromUnknown(lastAction, "UK");

                        releases.Add(release);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(string.Format("{0}: Error while parsing row '{1}':\n\n{2}", ID, Row.OuterHtml, ex));
                    }
                }
            }
            catch (Exception ex)
            {
                OnParseError(results.Content, ex);
            }

            return releases;
        }

        public override async Task<byte[]> Download(Uri link)
        {
            var response = await RequestStringWithCookies(link.ToString());
            var results = response.Content;
            var SearchResultParser = new HtmlParser();
            var SearchResultDocument = SearchResultParser.Parse(results);
            var downloadSelector = "a[title=\"Download attachment\"]";
            var DlUri = SearchResultDocument.QuerySelector(downloadSelector);
            if (DlUri != null)
            {
                logger.Debug(string.Format("{0}: Download selector {1} matched:{2}", ID, downloadSelector, DlUri.OuterHtml));
                var href = DlUri.GetAttribute("href");
                link = new Uri(href);
            }
            else
            {
                logger.Error(string.Format("{0}: Download selector {1} didn't match:\n{2}", ID, downloadSelector, results));
                throw new Exception(string.Format("Download selector {0} didn't match", downloadSelector));
            }
            return await base.Download(link);
        }
    }
}

