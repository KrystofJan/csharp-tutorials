using DatabaseHandler;
using Models;


namespace Project.Services;

public class StudentService {
	public static List<Student> FindAllStudents() {
		return Database<Student>.SelectAll();
	}	
	public static int CreateNewStudent(Student s) {
		return Database<Student>.Insert(s);
	}

	public static void DeleteStudent(Student s) {
		Database<Student>.Delete(s);
	}

	public static void UpdateStudent(Student s) {
		Database<Student>.Update(s);
	}
}
