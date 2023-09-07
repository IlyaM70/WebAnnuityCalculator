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
            List<Payment> payments = new List<Payment>();

            decimal loanAmount = inputData.LoanAmount;
            // Годовая процентная ставка
            decimal yearlyInterest = inputData.LoanRate / 100;
            // Месячная процентная ставка по кредиту 
            decimal mounthlyInterest = yearlyInterest / 12;
            // Количество периодов, в течение которых выплачивается кредит.
            int paymentsNumber = inputData.LoanTerm;


            // Коэффициент аннуитета
            decimal annuityRatio = mounthlyInterest +
                (mounthlyInterest /
                Convert.ToDecimal((Math.Pow((1 + Convert.ToDouble(mounthlyInterest)), paymentsNumber)) - 1));


            // Ежемесячный аннуитетный платёж
            decimal annuityPayment = annuityRatio * loanAmount;

            // Остаток задолженности 
            decimal loanBalance = loanAmount*(1 + mounthlyInterest);

            ResultViewModel result = new ResultViewModel();

            for (int i = 0; i < paymentsNumber; i++)
            {
                // если остаток меньше месячного платежа
                if (loanBalance < annuityPayment)
                {
                   annuityPayment = loanBalance;
                }

                Payment payment = new Payment();
                // Начисленные проценты
                payment.InterestPayment = (loanBalance * yearlyInterest) / 12;
                // Часть выплаты, идущая на погашение основного долга
                payment.BodyPayment = annuityPayment - payment.InterestPayment;

                loanBalance = (loanBalance - annuityPayment)*(1 + mounthlyInterest);
                payment.LoanBalance = loanBalance;
                payment.Date = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(i+1));
                payment.AnnuityPayment = annuityPayment;
                payments.Add(payment);

                result.TotalPayments += payment.AnnuityPayment;
            }

            result.Payments = payments;
            result.Overpayment = result.TotalPayments - loanAmount;

            return View(result);
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