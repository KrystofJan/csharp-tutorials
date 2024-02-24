namespace ProductsApp;

public delegate int Operation(int x, int y);

public class Calculator
{
    private int x;
    private int y;

    public event EventHandler<MyEventArgs> OnSetXy;
    public event EventHandler<MyEventArgs> OnCompute;

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
        OnSetXy?.Invoke(this, new MyEventArgs());
    }

    public void Execute(Operation op)
    {
        int result = op(x, y);
        OnCompute?.Invoke(this, new MyEventArgs{Result = result});
    }
}
