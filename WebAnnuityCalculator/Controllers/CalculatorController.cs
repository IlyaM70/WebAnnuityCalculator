using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAnnuityCalculator.Models;
using WebAnnuityCalculator.Services.CalculatorService;

namespace WebAnnuityCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpGet]
        public IActionResult Calculator(bool isExtended = false)
        {
            CalculatorViewModel calculatorVM = new CalculatorViewModel();
            if (isExtended)
            {
                calculatorVM.IsExtended = true;
            }
            return View(calculatorVM);
        }

        [HttpPost]
        public IActionResult Calculator(CalculatorViewModel calculatorVM)
        {
            if(ModelState.IsValid)
            {
                if (calculatorVM.IsExtended)
                {
                    return RedirectToAction("ResultExtended", calculatorVM);
                }
                return RedirectToAction("Result", calculatorVM);
            }
            
            return View(calculatorVM);
        }

        public IActionResult Result(CalculatorViewModel calculatorVM)
        {
            // Сумма долга
            decimal loanAmount = calculatorVM.LoanAmount;
            // Годовая процентная ставка
            decimal yearlyInterest = calculatorVM.LoanRate / 100;
            // Месячная процентная ставка по кредиту 
            decimal mounthlyInterest = yearlyInterest / 12;
            // Количество периодов, в течение которых выплачивается кредит.
            int paymentsNumber = calculatorVM.LoanTerm;

            ResultViewModel result = 
                _calculatorService.Calculate(loanAmount,mounthlyInterest,paymentsNumber,0,true);

            return View(result);
        }

        public IActionResult ResultExtended(CalculatorViewModel calculatorVM)
        {
            // Сумма долга
            decimal loanAmount = calculatorVM.LoanAmount;
            // Дневная процентная ставка
            decimal dailyInterest = calculatorVM.LoanRate / 100;
            // Количество периодов, в течение которых выплачивается кредит.
           int paymentsNumber = calculatorVM.LoanTerm / calculatorVM.PaymentStep;
            // Ставка за период
            decimal stepInterest = dailyInterest * calculatorVM.PaymentStep;
            // Шаг платежей
            int paymentStep = calculatorVM.PaymentStep;

            ResultViewModel result = 
                _calculatorService.Calculate(loanAmount,stepInterest,paymentsNumber,paymentStep,false);

            return View("Result",result);
        }

        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}