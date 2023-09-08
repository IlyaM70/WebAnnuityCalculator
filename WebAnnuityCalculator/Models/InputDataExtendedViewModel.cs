using System.ComponentModel.DataAnnotations;

namespace WebAnnuityCalculator.Models
{
    public class InputDataExtendedViewModel
    {
        [Required(ErrorMessage = "Введите сумму займа")]
        [Range(10000, 100000000, ErrorMessage = "Сумма займа должна быть от 10 000 до 100 000 000")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Введите cрок займа")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Введите целое число больше 0")]
        [Range(1, 100, ErrorMessage = "Cрок займа должен быть от 1 до 100 дней")]
        public int LoanTerm { get; set; }

        [Required(ErrorMessage = "Введите шаг платежа")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Введите целое число больше 0")]
        [Range(1, 100, ErrorMessage = "Шаг платежа должен быть от 1 до 100 дней")]
        public int PaymentStep { get; set; }

        [Required(ErrorMessage = "Введите ставку")]
        [Range(0.001, 1000, ErrorMessage = "Ставка должна быть от 0.001 до 1000 процентов")]
        public decimal LoanRate { get; set; }
    }
}
