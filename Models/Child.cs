namespace BusFlowApp.Models;

public class Child : IPassenger
{
    public string Name { get; }

    public Child(string name)
    {
        Name = name;
    }
    
}