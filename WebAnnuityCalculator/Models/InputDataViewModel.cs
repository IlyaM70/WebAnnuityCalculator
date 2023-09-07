using System.ComponentModel.DataAnnotations;

namespace WebAnnuityCalculator.Models
{
    public class InputDataViewModel
    {
        [Required(ErrorMessage = "Введите сумму займа")]
        [Range(10000, 100000000, ErrorMessage = "Сумма займа должна быть от 10 000 до 100 000 000")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Введите cрок займа")]
        [RegularExpression("([0-9][0-9]*)",ErrorMessage ="Введите целое число больше 0")]
        [Range(1, 600, ErrorMessage = "Cрок займа должен быть от 1 до 600 месяцев")]
        public int LoanTerm { get; set; }

        [Required(ErrorMessage = "Введите ставку")]
        [Range(1, 1000, ErrorMessage = "Ставка должна быть от 1 до 1000 процентов")]
        public double LoanRate { get; set; }

    }
}
