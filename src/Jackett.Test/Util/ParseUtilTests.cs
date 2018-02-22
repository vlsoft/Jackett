﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using FluentAssertions;
using Jackett.Utils;
using NUnit.Framework;

namespace JackettTest.Util
{
    [TestFixture]
    public class ParseUtilTests
    {
        private static string InvalidRssXml
        {
            get
            {
                var type = typeof(ParseUtilTests);
                using (var resourceStream = type.Assembly.GetManifestResourceStream($"{type.Namespace}.Invalid-RSS.xml"))
                using (var sr = new StreamReader(resourceStream))
                {
                    return sr.ReadToEnd();
                }
                
            }
        }

        [Test]
        public void Invalid_RSS_should_parse_after_removing_invalid_chars()
        {
            var invalidRss = InvalidRssXml;
            Action parseAction = () => XDocument.Parse(invalidRss);
            parseAction.ShouldThrow<Exception>().WithMessage("'\a', hexadecimal value 0x07, is an invalid character. Line 12, position 7.");

            var validRSs = ParseUtil.RemoveInvalidXmlChars(invalidRss);
            var rssDoc = XDocument.Parse(validRSs);
            rssDoc.Root.Should().NotBeNull();
            var description = rssDoc.Root.XPathSelectElement("//description");
            description.Value.Should().Contain("Know Your Role and Shut Your Mouth!");
        }
    }
}
