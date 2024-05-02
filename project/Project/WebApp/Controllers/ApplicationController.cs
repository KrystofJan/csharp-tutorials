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
	
	public ApplicationController(SchoolService schoolService, StudyProgramService studyProgramService) {
		_schoolService = schoolService;
		_studyProgramService = studyProgramService;
	}

	public IActionResult Index() {
		ViewBag.Schools = _schoolService.GetSchools();
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
		Address a = new Address() {
			ApartamentNumber = form.ApartamentNumber ?? 0,
			BuildingNumber = form.BuildingNumber,
			Country = form.Country,
			City = form.City,
			Province = form.Province,
			Street = form.Street,
			PostalCode = form.PostalCode
		};
		int addressId = Database<Address>.Insert(a);
		a.AddressId = addressId;
		Student s = new Student {
			DateOfBirth = form.DateOfBirth,
			Address = a,
			Email = form.Email,
			FirstName = form.FirstName,
			LastName = form.LastName,
			Login = form.Login,
			Phone = form.Phone
		};
		int studentId = Database<Student>.Insert(s);
		s.StudentId = studentId;
		Application application = new Application {
			Student = s,
			PrimaryProgram = new StudyProgram{StudyProgramId = form.PrimaryProgramId}
		};
		
		if (form.SecondaryProgramId != null) {
			application.SecondaryProgram = new StudyProgram { StudyProgramId = (int)form.SecondaryProgramId };
		}
		
		if (form.TertiaryProgramId != null) {
			application.TertiaryProgram = new StudyProgram { StudyProgramId = (int)form.TertiaryProgramId };
		}

		int appId = Database<Application>.Insert(application);
		Console.WriteLine(appId);	
    	return View();
    }

	public override void OnActionExecuting(ActionExecutingContext context) {
		ViewBag.StudyPrograms = _studyProgramService.SelectedProgramsSet;
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