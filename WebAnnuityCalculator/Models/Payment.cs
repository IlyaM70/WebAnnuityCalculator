namespace WebAnnuityCalculator.Models
{
    /// <summary>
    /// Аннуитетный платеж
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Сумма аннуитетного платежа
        /// </summary>
        public decimal AnnuityPayment { get; set; }

        /// <summary>
        /// Часть аннуитетного платежа, идущая на погашение основного долга
        /// </summary>
        public decimal BodyPayment { get; set; }

        /// <summary>
        /// Часть аннуитетного платежа, идущая на погашение процентов
        /// </summary>
        public decimal InterestPayment { get; set; }

        /// <summary>
        /// Остаток долга
        /// </summary>
        public decimal LoanBalance { get; set; }
    }
}
