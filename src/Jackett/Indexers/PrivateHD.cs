﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jackett.Models;
using Newtonsoft.Json.Linq;
using NLog;
using Jackett.Utils;
using System.Net;
using System.Net.Http;
using CsQuery;
using System.Web;
using Jackett.Services;
using Jackett.Utils.Clients;
using System.Text.RegularExpressions;
using Jackett.Models.IndexerConfig;

namespace Jackett.Indexers
{
    public class PrivateHD : AvistazTracker, IIndexer
    {
        public PrivateHD(IIndexerManagerService indexerManager, IWebClient webClient, Logger logger, IProtectionService protectionService)
            : base(name: "PrivateHD",
                desc: "BitTorrent site for High Quality, High Definition (HD) movies and TV Shows",
                link: "https://privatehd.to/",
                indexerManager: indexerManager,
                logger: logger,
                protectionService: protectionService,
                webClient: webClient
                )
        {
        }
    }
}