using System.ComponentModel;
using System.Windows.Data;

namespace Project.Utils;

public class ConcurrentBindingList<T> {
	private BindingList<T> _bindingList = new BindingList<T>();
	private object lockObj = new object();

	public ConcurrentBindingList() {
	}

	public BindingList<T> GetBindingList() {
		return _bindingList;
	}
	public void Add(T obj) {
		// lock (lockObj) {
			_bindingList.Add(obj);
		// }
	}

	public void Remove(T obj) {
		// lock (lockObj) {
			if (_bindingList.Contains(obj)) {
				_bindingList.Remove(obj);
			}
		// }
	}

	public void Clear() {
		// lock (lockObj) {
			_bindingList.Clear();
		// }
	}
	
	public T this[int i] {
		get {
			return _bindingList[i];
		}
		set {
			// lock (lockObj) {
				_bindingList[i] = value;
			// }
		}
	}
}