namespace WebAnnuityCalculator.Models
{
    public class Payment
    {
        public DateOnly Date { get; set; }
        public decimal AnnuityPayment { get; set; }
        public decimal BodyPayment { get; set; }
        public decimal InterestPayment { get; set; }
        public decimal LoanBalance { get; set; }
    }
}
