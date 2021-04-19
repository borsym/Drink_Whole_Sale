using DrinkWholeSale.Web.Models;
using DrinkWholeSale.Web.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace DrinkWholeSale.Web.Controllers
{
	/// <summary>
	/// Vezrlő ősosztálya.
	/// </summary>
	public class BaseController : Controller
    {
	    // a logikát modell osztályok mögé rejtjük
		protected readonly ApplicationState _applicationState;
        protected readonly IDrinkWholeSaleService _service;

        public BaseController(ApplicationState accountService, IDrinkWholeSaleService service)
        {
			_applicationState = accountService;
			_service = service;
        }

		/// <summary>
		/// Egy akció meghívása után végrehajtandó metódus.
		/// </summary>
		/// <param name="context">Az akció kontextus argumentuma.</param>
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);

			// a minden oldalról elérhető információkat össze gyűjtjük
			ViewBag.Cities = _service.MainCats.ToArray();
			ViewBag.UserCount = _applicationState.UserCount;
			ViewBag.CurrentGuestName = String.IsNullOrEmpty(User.Identity.Name) ? null : User.Identity.Name;
		}
	}
}