using System;

namespace BusFlowApp.Models;

using System.Collections.Generic;

public class BusStop
{
    public List<IPassenger> WaitingPassengers { get; }

    public BusStop()
    {
        WaitingPassengers = new List<IPassenger>();
    }

    public void AddPassenger(IPassenger passenger)
    {
        WaitingPassengers.Add(passenger);
    }

    public List<IPassenger> GetBoardingPassengers(int passengersToBoard)
    {
        var random = new Random();
        int countToBoard = random.Next(0, Math.Min(passengersToBoard, WaitingPassengers.Count) + 1);
        var toBoard = WaitingPassengers.GetRange(0, 
            System.Math.Min(countToBoard, WaitingPassengers.Count));
        WaitingPassengers.RemoveRange(0, toBoard.Count);
        return toBoard;
    }
}
