using System.ComponentModel.DataAnnotations;

namespace WebAnnuityCalculator.Models
{
    /// <summary>
    /// View Model калькулятора и раширеного калькулятора
    /// </summary>
    public class CalculatorViewModel
    {
        /// <summary>
        /// Сумма займа
        /// </summary>
        [Required(ErrorMessage = "Введите сумму займа")]
        [Range(1000, 10000000, ErrorMessage = "Сумма займа должна быть от 1 000 до 10 000 000")]
        public decimal LoanAmount { get; set; } = 1000;

        /// <summary>
        /// Cрок займа в месяцах
        /// </summary>
        [Required(ErrorMessage = "Введите cрок займа в месяцах")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Введите целое число больше 0")]
        [Range(1, 10950, ErrorMessage = "Cрок займа должен быть от 1 до 10950 дней")]
        // Максимум 30 лет
        public int LoanTermDays { get; set; } = 1;

        /// <summary>
        ///  Cрок займа в днях
        /// </summary>
        [Required(ErrorMessage = "Введите cрок займа в днях")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Введите целое число больше 0")]
        [Range(1, 360, ErrorMessage = "Cрок займа должен быть от 1 до 360  месяцев")]
        // Максимум 30 лет
        public int LoanTermMonths { get; set; } = 1;

        /// <summary>
        /// Шаг платежа в днях
        /// </summary>
        [Required(ErrorMessage = "Введите шаг платежа")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Введите целое число больше 0")]
        [Range(1, 365, ErrorMessage = "Шаг платежа должен быть от 1 до 365 дней")]
        public int PaymentStep { get; set; } = 1;

        /// <summary>
        /// Процентная ставка годовая
        /// </summary>
        [Required(ErrorMessage = "Введите ставку")]
        [Range(1, 100, ErrorMessage = "Ставка должна быть от 1 до 100 процентов")]
        public decimal LoanRateYearly { get; set; } = 1;

        /// <summary>
        /// Процентная ставка дневная,
        /// 0.00273 -  1% в год,
        /// 0.28 - 100% в год
        /// </summary>
        [Required(ErrorMessage = "Введите ставку")]
        [Range(0.00273, 0.28, ErrorMessage = "Ставка должна быть от 0.00273% (1% в год) до 0.28% (100% в год)")]

        public decimal LoanRateDayly { get; set; } = 0.00273M;

        /// <summary>
        /// Является ли расширенным калькулятором
        /// </summary>
        public bool IsExtended { get; set; } = false;
    }
}
