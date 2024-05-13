using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestPrep.Models;
using TestPrep.Services;

namespace TestPrep.Controllers;

public class HomeController : Controller {
	private readonly ILogger<HomeController> _logger;
	public DataExchangeService _dataExchangeService { get; set; }

	public SaveDataExchange _SaveDataExchange{ get; set; }
	public HomeController(ILogger<HomeController> logger,SaveDataExchange saveDataExchange, DataExchangeService dataExchangeService ) {
		_dataExchangeService = dataExchangeService;
		_SaveDataExchange = saveDataExchange;
		_logger = logger;
	}

	public async Task<IActionResult> Index() {
		ViewBag.ExchangeRates = await _dataExchangeService.GetDataExchange();
		return View();
	}


	public override void OnActionExecuting(ActionExecutingContext context) {
		ViewBag.ExchangeRates = _dataExchangeService._exchangeRates;
		base.OnActionExecuting(context);
	}

	[HttpPost]
	public IActionResult Index(IndexForm form) {
		form.ExchangeRate = _dataExchangeService._exchangeRates.Where(x => x.Name == form.ExchangeRateName).FirstOrDefault();	
		if (!ModelState.IsValid) {
			return View();
		}
		_SaveDataExchange.Save(form);
		
		return RedirectToAction("Summary");
	}


	public IActionResult Summary() {
		ViewBag.Exchanges = _SaveDataExchange.Get();
		return View();
	}
	
	public IActionResult Privacy() {
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() {
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}