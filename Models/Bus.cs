namespace BusFlowApp.Models;
    
using System;
using System.Collections.Generic;

public class Bus
{
    public int Capacity { get; }
    public List<IPassenger> Passengers { get; }

    public event Action ArrivedAtStop = delegate { };
    public event Action<string> PassengerBoarded = delegate { };
    public event Action BusOvercrowded = delegate { };

    public Bus(int capacity)
    {
        Capacity = capacity;
        Passengers = new List<IPassenger>();
    }

    public void Arrive()
    {
        ArrivedAtStop.Invoke();
    }

    public void BoardPassenger(IPassenger passenger)
    {
        if (Passengers.Count >= Capacity)
        {
            BusOvercrowded.Invoke();
            return;
        }

        Passengers.Add(passenger);
        PassengerBoarded.Invoke(passenger.Name);
    }
}
