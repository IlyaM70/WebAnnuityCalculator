using WebAnnuityCalculator.Models;

namespace WebAnnuityCalculator.Services.CalculatorService
{
    public interface ICalculatorService
    {
        ResultViewModel Calculate(decimal loanAmount, decimal stepInterest,
                                         int paymentsNumber, int paymentStep, bool payMonthly);
    }
}
