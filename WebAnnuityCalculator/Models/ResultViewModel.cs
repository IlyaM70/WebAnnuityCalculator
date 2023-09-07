namespace WebAnnuityCalculator.Models
{
    public class ResultViewModel
    {
        public List<Payment> Payments { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TotalInterest { get;  set; }
        public decimal TotalBody { get; set; }

        public ResultViewModel()
        {
            Payments = new List<Payment>();
        }
    }
}
