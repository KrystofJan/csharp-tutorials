using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using Models;
namespace WebApp.Controllers;


public class ApplicationController : Controller {
	private readonly SchoolService _schoolService;
	public ApplicationController(SchoolService schoolService) {
		_schoolService = schoolService;
	}

	public IActionResult Index() {
		ViewBag.Schools = _schoolService.GetSchools();
		return View();
	}
	
	public IActionResult Form() {
    	return View();
    }
}