using System.ComponentModel.DataAnnotations;

namespace WebAnnuityCalculator.Models
{
    public class CalculatorViewModel
    {
        [Required(ErrorMessage = "Введите сумму займа")]
        [Range(1000, 100000000, ErrorMessage = "Сумма займа должна быть от 1 000 до 100 000 000")]
        public decimal LoanAmount { get; set; } = 1000;

        [Required(ErrorMessage = "Введите cрок займа в месяцах")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Введите целое число больше 0")]
        [Range(1, 18250, ErrorMessage = "Cрок займа должен быть от 1 до 18 250 дней")]
        // Максимум 50 лет
        public int LoanTermDays { get; set; } = 1;

        [Required(ErrorMessage = "Введите cрок займа в днях")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Введите целое число больше 0")]
        [Range(1, 600, ErrorMessage = "Cрок займа должен быть от 1 до 600 месяцев")]
        // Максимум 50 лет
        public int LoanTermMonths { get; set; } = 1;

        [Required(ErrorMessage = "Введите шаг платежа")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Введите целое число больше 0")]
        [Range(1, 365, ErrorMessage = "Шаг платежа должен быть от 1 до 365 дней")]
        public int PaymentStep { get; set; } = 1;

        [Required(ErrorMessage = "Введите ставку")]
        [Range(0.001, 100, ErrorMessage = "Ставка должна быть от 0.001 до 100 процентов")]
        public decimal LoanRate { get; set; } = 1;

        public bool IsExtended { get; set; } = false;
    }
}
