using System;

namespace ConsoleAPI.SessionModels
{
    public class SessionSummaryModel
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public ImageDescriptor MaximumSeverity { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public long SortableEndDateTime { get; set; }
        public int DurationSec { get; set; }
        public DateTimeOffset AddedDateTime { get; set; }
        public DateTime AddedDate { get; set; }
        public NameCaptionBadge Application { get; set; }
        public string Environment { get; set; }
        public string PromotionLevel { get; set; }
        public VersionDescriptor ApplicationVersion { get; set; }
        public EndpointDescriptor Endpoint { get; set; }
        public string DnsDomainName { get; set; }
        public string UserName { get; set; }
        public ApplicationUserDescriptor User { get; set; }
        public MessageCount MessageCount { get; set; }
        public MessageCount CriticalCount { get; set; }
        public MessageCount ErrorCount { get; set; }
        public MessageCount WarningCount { get; set; }
        public bool ShowUser { get; set; }

    }
}
