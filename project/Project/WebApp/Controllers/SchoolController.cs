using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using WebApp.Services;
namespace WebApp.Controllers;

public class SchoolController : Controller {

	private readonly SchoolService _schoolService;
	private readonly StudyProgramService _studyProgramService;
	private readonly SchoolSearchService _schoolSearchService;
	private readonly StudyProgramSearchService _studyProgramSearchService;

	public SchoolController(SchoolService schoolService, StudyProgramService studyProgramService, SchoolSearchService schoolSearchService, StudyProgramSearchService studyProgramSearchService) {
		_schoolService = schoolService;
		_studyProgramService = studyProgramService;
		_studyProgramSearchService = studyProgramSearchService;
		_schoolSearchService = schoolSearchService;
	}

	public IActionResult Index() {
		ViewBag.Schools = _schoolSearchService.SearchedSchools;
		return View();
	}

	[HttpGet]
	public JsonResult GetSchoolsByName(string value) {
		List<School> result = new List<School>();
		if (value == "%%") {
			result.AddRange(_schoolService.GetSchools());
			_schoolSearchService.Clear();
			_schoolSearchService.Add(result);
		}
		else {
			result.AddRange(_schoolSearchService.GetSchoolsByName(value));
		}
		
		return new JsonResult(result);
	}

	public IActionResult Detail(int id) {
		if (id == 0) {
			return RedirectToAction("Index");
		}
		School s = _schoolService.GetSchoolById(id);
		ViewBag.School = s;
		ViewBag.StudyPrograms = _studyProgramSearchService.GetStudyProgramBySchoolName(s.SchoolName);
		return View();
	}
}