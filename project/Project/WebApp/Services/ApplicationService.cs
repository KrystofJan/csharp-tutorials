using DatabaseHandler;
using Models;
using WebApp.Models;
namespace WebApp.Services;

public class ApplicationService {
	public ApplicationService() {
	}

	public Application GetApplication(int id) {
		return Database<Application>.SelectById(id);
	}

	public Application CreateApplication(ApplicationForm form) {
		Address a = new Address() {
			ApartamentNumber = form.ApartamentNumber,
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
			PrimaryProgram = new StudyProgram { StudyProgramId = form.PrimaryProgramId }
		};

		if (form.SecondaryProgramId != null) {
			application.SecondaryProgram = new StudyProgram { StudyProgramId = (int)form.SecondaryProgramId };
		}

		if (form.TertiaryProgramId != null) {
			application.TertiaryProgram = new StudyProgram { StudyProgramId = (int)form.TertiaryProgramId };
		}

		int appId = Database<Application>.Insert(application);
		application.ApplicationId = appId;
		return application;
	}
}