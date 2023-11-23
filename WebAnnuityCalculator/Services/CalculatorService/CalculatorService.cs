using WebAnnuityCalculator.Models;

namespace WebAnnuityCalculator.Services.CalculatorService
{
    public class CalculatorService : ICalculatorService
    {
        public ResultViewModel Calculate(decimal loanAmount, decimal stepInterest,
                                         int paymentsNumber, int paymentStep, bool payMonthly)
        {
            //Коэффициент аннуитета
            decimal annuityRatio = stepInterest +
                (stepInterest /
                Convert.ToDecimal((Math.Pow((1 + Convert.ToDouble(stepInterest)), paymentsNumber)) - 1));


            //Аннуитетный платёж за период
            decimal annuityPayment = annuityRatio * loanAmount;

            // Остаток задолженности 
            decimal loanBalance = loanAmount * (1 + stepInterest);
            

            ResultViewModel result = new ResultViewModel();
            List<Payment> payments = new List<Payment>();

            for (int i = 0; i < paymentsNumber; i++)
            {
                // если остаток меньше месячного платежа
                if (loanBalance < annuityPayment)
                {
                    annuityPayment = loanBalance;
                }

                Payment payment = new Payment();

                // Записать остаток долга
                loanBalance = (loanBalance - annuityPayment);
                payment.LoanBalance = loanBalance;

                //Часть выплаты, идущая на погашение процентов
                payment.InterestPayment = loanBalance * stepInterest;
                // Часть выплаты, идущая на погашение основного долга
                payment.BodyPayment = annuityPayment - payment.InterestPayment;

                
                // Начислить проценты на остаток долга
                loanBalance *= (1 + stepInterest);

                if (payMonthly)
                    payment.Date = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(i + 1));
                else
                    payment.Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(i * paymentStep));

                payment.AnnuityPayment = annuityPayment;
                payments.Add(payment);

                result.TotalPayments += payment.AnnuityPayment;
            }

            result.Payments = payments;
            result.Overpayment = result.TotalPayments - loanAmount;

            return result;
        }
    }
}
