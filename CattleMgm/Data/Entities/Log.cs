using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class Log
    {
        public long LogId { get; set; }
        public string UserId { get; set; } = null!;
        public string Ip { get; set; } = null!;
        public string HostName { get; set; } = null!;
        public string Controller { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string? ActionDescription { get; set; }
        public string? HttpMethod { get; set; }
        public string? Url { get; set; }
        public string? Developer { get; set; }
        public string? DescriptionTitle { get; set; }
        public string? Description { get; set; }
        public bool BError { get; set; }
        public string? FormContent { get; set; }
        public string? Exception { get; set; }
        public DateTime InsertedDate { get; set; }
    }
}
