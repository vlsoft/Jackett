﻿using Jackett.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackett.Models.DTO
{
    public enum ManualSearchResultIndexerStatus { Unknown = 0, Error = 1, OK = 2 };

    public class ManualSearchResultIndexer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ManualSearchResultIndexerStatus Status { get; set; }
        public int Results { get; set; }
        public string Error { get; set; }
    }

    public class ManualSearchResult
    {
        public IEnumerable<TrackerCacheResult> Results { get; set; }
        public IList<ManualSearchResultIndexer> Indexers { get; set; }
    }
}
