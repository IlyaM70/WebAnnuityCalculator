using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAnnuityCalculator.Models;

namespace WebAnnuityCalculator.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(InputDataViewModel inputData)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Result", inputData);
            }
            
            return View(inputData);
        }

        public IActionResult Result(InputDataViewModel inputData)
        {
            // Сумма долга
            decimal loanAmount = inputData.LoanAmount;
            // Годовая процентная ставка
            decimal yearlyInterest = inputData.LoanRate / 100;
            // Месячная процентная ставка по кредиту 
            decimal mounthlyInterest = yearlyInterest / 12;
            // Количество периодов, в течение которых выплачивается кредит.
            int paymentsNumber = inputData.LoanTerm;

            ResultViewModel result = Calculate(loanAmount,mounthlyInterest,paymentsNumber,0,true);

            return View(result);
        }

        [HttpGet]
        public IActionResult CalculatorExtended()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculatorExtended(InputDataExtendedViewModel inputData)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ResultExtended", inputData);
            }

            return View(inputData);
        }

        public IActionResult ResultExtended(InputDataExtendedViewModel inputData)
        {
            // Сумма долга
            decimal loanAmount = inputData.LoanAmount;
            // Дневная процентная ставка
            decimal dailyInterest = inputData.LoanRate / 100;
            // Количество периодов, в течение которых выплачивается кредит.
           int paymentsNumber = inputData.LoanTerm / inputData.PaymentStep;
            // Ставка за период
            decimal stepInterest = dailyInterest * inputData.PaymentStep;
            // Шаг платежей
            int paymentStep = inputData.PaymentStep;

            ResultViewModel result = Calculate(loanAmount,stepInterest,paymentsNumber,paymentStep,false);

            return View("Result",result);
        }

        private ResultViewModel Calculate(decimal loanAmount, decimal stepInterest,
                                         int paymentsNumber, int paymentStep, bool payMonthly)
        {
            // Коэффициент аннуитета
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
                // Начисленные проценты
                payment.InterestPayment = loanBalance * stepInterest;
                // Часть выплаты, идущая на погашение основного долга
                payment.BodyPayment = annuityPayment - payment.InterestPayment;
                // Записать остаток долга
                loanBalance = (loanBalance - annuityPayment);
                payment.LoanBalance = loanBalance;
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





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}