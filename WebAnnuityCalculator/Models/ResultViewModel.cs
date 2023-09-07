namespace WebAnnuityCalculator.Models
{
    public class ResultViewModel
    {
        public List<Payment> Payments { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal Overpayment { get;  set; }

        public ResultViewModel()
        {
            Payments = new List<Payment>();
        }
    }
}
