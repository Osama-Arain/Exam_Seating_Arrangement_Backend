using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payday_server.Model.Report
{
    public class PayrollMods
    {
        public Guid Id { get; set; } = new Guid();
        public Guid PayrollId { get; set; }
        [ForeignKey("PayrollId")]
        public PayrollReports PayrollReports { get; set; }
        public string ModType { get; set; }
        public string ModName { get; set; }
        public Guid ModId { get; set; }
        public string ModValue { get; set; }
    }
}
