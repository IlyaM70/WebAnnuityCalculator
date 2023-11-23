using WebAnnuityCalculator.Models;

namespace WebAnnuityCalculator.Services.CalculatorService
{
    /// <summary>
    /// Сервис для рассчета аннуитетных платежей
    /// </summary>
    public interface ICalculatorService
    {
        /// <summary>
        /// Рассчитать аннуитетные платежи
        /// </summary>
        /// <param name="loanAmount">Сумма займа</param>
        /// <param name="stepInterest">Процентная ставка за период в виде десятичной дроби</param>
        /// <param name="paymentsNumber">Количество периодов, в течение которых выплачивается кредит</param>
        /// <param name="paymentStep">Шаг платежей</param>
        /// <param name="payMonthly">Являются ли платежи ежемесячными</param>
        /// <returns></returns>
        ResultViewModel Calculate(decimal loanAmount, decimal stepInterest,
                                         int paymentsNumber, int paymentStep, bool payMonthly);
    }
}
