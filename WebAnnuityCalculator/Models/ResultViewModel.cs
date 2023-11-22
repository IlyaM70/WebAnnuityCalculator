﻿namespace WebAnnuityCalculator.Models
{
    public class ResultViewModel
    {
        /// <summary>
        /// Список платежей
        /// </summary>
        public List<Payment> Payments { get; set; }

        /// <summary>
        /// Всего платежей
        /// </summary>
        public decimal TotalPayments { get; set; }

        /// <summary>
        /// Всего переплат
        /// </summary>
        public decimal Overpayment { get;  set; }

        public ResultViewModel()
        {
            Payments = new List<Payment>();
        }
    }
}
