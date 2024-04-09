namespace Paralelizmus;

class SimpleStack<T>
{

    private List<T> data = new List<T>();

    private object lockObject = new object();
    
    public T Top { 
        get {
            lock (lockObject) {
                int idx = data.Count - 1;
                if (idx == -1)
                {
                    throw new StackEmptyException();
                }
                return data[idx];    
            }
        } 
    }


    public bool IsEmpty
    {
        get
        {
            return data.Count == 0;
        }
    }


    public void Push(T val) {
        lock (lockObject) {
            data.Add(val);
        }
    }


    public T Pop()
    {
        lock (lockObject) {
            int idx = this.data.Count - 1;
            if (idx == -1) {
                throw new StackEmptyException();
            }

            T val = this.data[idx];
            this.data.RemoveAt(idx);
            return val;
        }
    }


    public class StackEmptyException : Exception
    {

    }
}
