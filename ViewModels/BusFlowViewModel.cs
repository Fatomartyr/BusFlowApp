using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusFlowApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BusFlowApp.ViewModels;

public class BusFlowViewModel : ObservableObject
{
    public ObservableCollection<string> Log { get; set; } = new();
    public ObservableCollection<IPassenger> Passengers { get; set; } = new();

    private Bus _bus;
    private BusStop _busStop;

    public event Action/*<List<IPassenger>>*/? PassengersBoardedAnimation;
    
    public BusFlowViewModel()
    {
        _bus = new Bus(capacity: 100);
        _busStop = new BusStop();

        _bus.ArrivedAtStop += OnBusArrived;
        _bus.PassengerBoarded += name => Log.Add($"{name} сел(а) в автобус.");
        _bus.BusOvercrowded += () => Log.Add("Автобус переполнен!");

        StartBusLoop();
    }

    private async void StartBusLoop()
    {
        while (true)
        {
            await Task.Delay(10000); 
            _bus.Arrive();
            Log.Add("Автобус прибыл на остановку");
            await Task.Delay(15000);
            var toBoard = _busStop.GetBoardingPassengers(_bus.Capacity - _bus.Passengers.Count);
            foreach (var p in toBoard)
            {
                _bus.BoardPassenger(p);
            }
          //  PassengersBoardedAnimation?.Invoke(toBoard);
            foreach (var p in toBoard)
            {
                Passengers.Remove(p);
            }
            OnPropertyChanged(nameof(Passengers));
            await Task.Delay(5000); 
            Log.Add("Автобус уехал с остановки");
            OnPropertyChanged(nameof(Log));
        }
    }

    private void OnBusArrived()
    {
        var toBoard = _busStop.GetBoardingPassengers(_bus.Capacity - _bus.Passengers.Count);
        foreach (var p in toBoard)
        {
            _bus.BoardPassenger(p);
        }
        OnPropertyChanged(nameof(Passengers));
    }

    public void AddMan(string name)
    {
        var man = new Man(name);
        _busStop.AddPassenger(man);
        Passengers.Add(man);
        Log.Add($"Мужчина {name} ждет автобус.");
    }

    public void AddWoman(string name)
    {
        var w = new Woman(name);
        _busStop.AddPassenger(w);
        Passengers.Add(w);
        Log.Add($"Женщина {name} ждет автобус.");
    }

    public void AddChild(string name)
    {
        var c = new Child(name);
        _busStop.AddPassenger(c);
        Passengers.Add(c);
        Log.Add($"Ребенок {name} ждет автобус.");
    }

    public void AddDisabled(string name)
    {
        var d = new DisabledPerson(name);
        _busStop.AddPassenger(d);
        Passengers.Add(d);
        Log.Add($"Инвалид {name} ждет автобус.");
    }
}
