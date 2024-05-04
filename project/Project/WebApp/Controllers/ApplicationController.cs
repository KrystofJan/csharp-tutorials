using DatabaseHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Services;
using Models;
using WebApp.Models;

namespace WebApp.Controllers;

public class ApplicationController : Controller {
	private readonly StudyProgramService _studyProgramService;
	private readonly ApplicationService _applicationService;
	private readonly StudyProgramSearchService _studyProgramSearchService;

	public ApplicationController(SchoolService schoolService, StudyProgramService studyProgramService, ApplicationService applicationService, StudyProgramSearchService studyProgramSearchService) {
		_studyProgramSearchService = studyProgramSearchService;
		_studyProgramService = studyProgramService;
		_applicationService = applicationService;
		ViewBag.StudyPrograms = _studyProgramSearchService.SelectedProgramsSet;
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
		ViewBag.StudyPrograms = _studyProgramSearchService.SelectedProgramsSet;
		base.OnActionExecuting(context);
	}

	[HttpPost]
	public IActionResult AddToList(int id) {
		StudyProgram sp = _studyProgramService.GetSpecificProgramById(id);
		if (_studyProgramSearchService.ProgramCount >= 3) {
			return StatusCode(507);
		}
		_studyProgramSearchService.Add(sp);
		Console.WriteLine(string.Join(", ", _studyProgramSearchService.SelectedProgramsSet.Select(x => x.StudyProgramCode)));
		return Created();
	}

	// Needs to be post, due to ajax
	[HttpPost]
	public IActionResult RemoveFromList(int studyProgramId) {
		if (_studyProgramSearchService.ProgramCount <= 0) {
			return StatusCode(507);
		}
		_studyProgramSearchService.Remove(studyProgramId);
		Console.WriteLine(string.Join(", ", _studyProgramSearchService.SelectedProgramsSet.Select(x => x.StudyProgramCode)));
		return Created();
	}
}