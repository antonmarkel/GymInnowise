using GymInnowise.Shared.Reports.Interfaces;

namespace GymInnowise.Shared.Reports.Payment
{
    public class BillReport : IReport
    {
        public required string CustomerName { get; set; }
        public required string CustomerEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Currency { get; set; }
        public string? Notes { get; set; }
        public required string Status { get; set; }
        public DateTime DateStampUtc { get; set; }
    }
}
