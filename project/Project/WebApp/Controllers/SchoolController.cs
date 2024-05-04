using Microsoft.AspNetCore.Mvc;
using Models;
using WebApp.Services;
namespace WebApp.Controllers;

public class SchoolController : Controller {

	private readonly SchoolService _schoolService;
	private readonly StudyProgramService _studyProgramService;
	
	public SchoolController(SchoolService schoolService, StudyProgramService studyProgramService) {
		_schoolService = schoolService;
		_studyProgramService = studyProgramService;
	}

	public IActionResult Index() {
		ViewBag.Schools = _schoolService.GetSchools();
		return View();
	}
	
	public IActionResult Detail(int id) {
		if (id == 0) {
			return RedirectToAction("Index");
		}
		School s = _schoolService.GetSchoolById(id);
		ViewBag.School = s;
		ViewBag.StudyPrograms = _studyProgramService.GetStudyProgramBySchoolName(s.SchoolName);
		return View();
	}
}