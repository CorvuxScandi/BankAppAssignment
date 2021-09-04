using BankApp.Enteties.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankApp.Frontend.ViewComponents
{
    public class MakeTransactionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            TransactionDTO model = new();

            return View(model);
        }
    }
}