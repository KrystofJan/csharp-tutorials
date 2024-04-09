using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CSharp6_WPF;

public class Customer : INotifyPropertyChanged {
	private string _firstName;
	private string _lastName;
	public int Id { get; set; }

	public string FirstName {
		get => _firstName;
		set => SetField(ref _firstName, value);
	}

	public string LastName {
		get => _lastName;
		set => SetField(ref _lastName, value);
	}

	public int Age { get; set; }
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
}