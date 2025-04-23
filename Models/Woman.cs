namespace BusFlowApp.Models;

public class Woman : IPassenger
{
    public string Name { get; }

    public Woman(string name)
    {
        Name = name;
    }
    
}
