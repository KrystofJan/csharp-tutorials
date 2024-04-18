using DatabaseHandler;
using Models;


namespace Project.Services;

public class StudentService {
	public static List<Student> FindAllStudents() {
		return Database<Student>.SelectAll();
	}
}
