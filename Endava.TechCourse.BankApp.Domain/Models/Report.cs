using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
    internal class Report : BaseEntity
    {
        public string SourceUserId { get; set; }
        public string SourceUserName { get; set; }
        public string Content { get; set; }
        public string DownloadLink { get; set; }
    }
}