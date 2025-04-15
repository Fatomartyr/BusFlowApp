namespace BusFlowApp.Models;

public class DisabledPerson : IPassenger
{
    public string Name { get; }

    public DisabledPerson(string name)
    {
        Name = name;
    }
}