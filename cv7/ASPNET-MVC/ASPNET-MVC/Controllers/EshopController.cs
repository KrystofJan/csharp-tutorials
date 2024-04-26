using ASPNET_MVC.Forms;
using ASPNET_MVC.Models;
using ASPNET_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNET_MVC.Controllers;

public class EshopController : Controller {

	private readonly ProductProvider _provider;
	private readonly CartService _cartService;

	public EshopController(ProductProvider productProvider, CartService cartService) {
		_provider = productProvider;
		_cartService = cartService;
	}
	
	public IActionResult Index() {
		ViewBag.Products = _provider.List();
		return View();
	}

	public IActionResult Detail(int id) {
		Product product = _provider.Find(id);
		if (product == null) {
			return NotFound();
		}

		ViewBag.Product = product;
		return View();
	}

	public IActionResult Form() {
		ViewBag.Products = _cartService._Products;
		return View();
	}
	
	[HttpPost]
	public IActionResult Form(CartForm form) {
		ViewBag.Products = _cartService._Products;
		if (ModelState.IsValid) {
			// form je valid
			Console.WriteLine("asdasd");
		}
		
		return View();
	}

	public IActionResult Json(int limit) {
		return new JsonResult(_provider.List().Take(limit));
	}

	public override void OnActionExecuting(ActionExecutingContext context) {
		ViewBag.ProductCount = _cartService.Count;
		base.OnActionExecuting(context);
	}

	public IActionResult AddToCart(int productId/*[FromServices] CartService cartService*/) {
		_cartService.Add(_provider.Find(productId));
		return RedirectToAction("Detail", new {id = productId});
	}
}