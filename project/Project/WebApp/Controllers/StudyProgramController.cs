using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApp.Services;
namespace WebApp.Controllers;

public class StudyProgramController : Controller {

	private readonly StudyProgramService _studyProgramService;

	public StudyProgramController(StudyProgramService studyProgramService) {
		_studyProgramService = studyProgramService;
	}
	
	[HttpGet]
	public JsonResult GetStudyProgram(string value) {
		List<StudyProgram> studyPrograms = _studyProgramService.GetStudyProgramBySchoolName(value);
		if (studyPrograms.Count < 0) {
			return new JsonResult("Not found :(");
		}
		return new JsonResult(studyPrograms);
	}
		
	
	// [HttpPost]
	// public JsonResult GetStudyProgram(object value) {
	// 	List<StudyProgram> studyPrograms = StudyProgramService.GetStudyProgramByAnyAttribute(value);
	// 	if (studyPrograms.Count < 0) {
	// 		return new JsonResult("Not found :(");
	// 	}
	// 	return new JsonResult(studyPrograms);
	// }
	// 	
}