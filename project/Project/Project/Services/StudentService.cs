using DatabaseHandler;
using Models;


namespace Project.Services;

public class StudentService {
	public static List<Student> FindAllStudents() {
		return Database<Student>.SelectAll();
	}	
	public static int CreateNewStudent(Student s) {
		int addrId = Database<Address>.Insert(s.Address);
		s.Address.AddressId = addrId;
		return Database<Student>.Insert(s);
	}

	public static void DeleteStudent(Student s) {
		Database<Student>.Delete(s);
		Database<Address>.Delete(s.Address);
	}

	public static void UpdateStudent(Student s) {
		Database<Address>.Update(s.Address);
		Database<Student>.Update(s);
	}
}
