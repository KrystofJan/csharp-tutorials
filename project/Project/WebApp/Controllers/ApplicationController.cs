using DatabaseHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Services;
using Models;
using WebApp.Models;

namespace WebApp.Controllers;

public class ApplicationController : Controller {
	private readonly SchoolService _schoolService;
	private readonly StudyProgramService _studyProgramService;
	private readonly ApplicationService _applicationService;

	public ApplicationController(SchoolService schoolService, StudyProgramService studyProgramService, ApplicationService applicationService) {
		_schoolService = schoolService;
		_studyProgramService = studyProgramService;
		_applicationService = applicationService;
	}

	public IActionResult Index() {
		return RedirectToAction("Form");
	}
	
	public IActionResult Detail(int id) {
		if (id == 0) {
			return RedirectToAction("Form");
		}
		Application a = _applicationService.GetApplication(id);
		ViewBag.Application = a;
		return View();
	}
	

	public IActionResult Form() {
		ViewBag.StudyPrograms = _studyProgramService.SelectedProgramsSet;
		return View();
	}

	[HttpPost]
	public IActionResult Form(ApplicationForm form) {
		if (!ModelState.IsValid) {
			return View();
		}
		Console.WriteLine("We good");
		Application a = _applicationService.CreateApplication(form);
		return RedirectToAction("Detail", new {id = a.ApplicationId});
	}

	public override void OnActionExecuting(ActionExecutingContext context) {
		
		base.OnActionExecuting(context);
	}

	[HttpPost]
	public IActionResult AddToList(int id) {
		StudyProgram sp = _studyProgramService.GetSpecificProgramById(id);
		if (_studyProgramService.ProgramCount >= 3) {
			return StatusCode(507);
		}
		_studyProgramService.Add(sp);
		Console.WriteLine(string.Join(", ", _studyProgramService.SelectedProgramsSet.Select(x => x.StudyProgramCode)));
		return Created();
	}

	// Needs to be post, due to ajax
	[HttpPost]
	public IActionResult RemoveFromList(int studyProgramId) {
		if (_studyProgramService.ProgramCount <= 0) {
			return StatusCode(507);
		}
		_studyProgramService.Remove(studyProgramId);
		Console.WriteLine(string.Join(", ", _studyProgramService.SelectedProgramsSet.Select(x => x.StudyProgramCode)));
		return Created();
	}
}