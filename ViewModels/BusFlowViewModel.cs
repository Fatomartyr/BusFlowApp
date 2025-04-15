using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BusFlowApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BusFlowApp.ViewModels;

public class BusFlowViewModel : ObservableObject
{
    private readonly Random _random = new();

    public ObservableCollection<string> Log { get; set; } = new();
    public ObservableCollection<IPassenger> Passengers { get; set; } = new();

    public ObservableCollection<VisualPassenger> VisualPassengers { get; set; } = new();

    private Bus _bus;
    private BusStop _busStop;

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
                Passengers.Remove(p);

                var visual = VisualPassengers.FirstOrDefault(v => v.Passenger == p);
                if (visual != null)
                    VisualPassengers.Remove(visual);
            }
            OnPropertyChanged(nameof(VisualPassengers));
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

    private void AddPassenger(IPassenger passenger, string imagePath)
    {
        _busStop.AddPassenger(passenger);
        Passengers.Add(passenger);

        var (x, y) = GenerateRandomPosition();
        var visual = new VisualPassenger
        {
            Passenger = passenger,
            X = x,
            Y = y,
            ImagePath = imagePath
        };

        VisualPassengers.Add(visual);
        OnPropertyChanged(nameof(VisualPassengers));
    }

    private (double x, double y) GenerateRandomPosition()
    {
        double y = _random.Next(150, 230);
        double x;
        int attempts = 0;
        do
        {
            x = _random.Next(90, 300); 
            attempts++;
        } while (VisualPassengers.Any(p => Math.Abs(p.X - x) < 25 && Math.Abs(p.Y - y) < 25) && attempts < 20);

        return (x, y);
    }

    public void AddMan(string name)
    {
        var man = new Man(name);
        AddPassenger(man, "avares://BusFlowApp/Assets/man.png");
        Log.Add($"Мужчина {name} ждет автобус.");
    }

    public void AddWoman(string name)
    {
        var woman = new Woman(name);
        AddPassenger(woman, "avares://BusFlowApp/Assets/man.png");
        Log.Add($"Женщина {name} ждет автобус.");
    }

    public void AddChild(string name)
    {
        var child = new Child(name);
        AddPassenger(child, "avares://BusFlowApp/Assets/man.png");
        Log.Add($"Ребенок {name} ждет автобус.");
    }

    public void AddDisabled(string name)
    {
        var disabled = new DisabledPerson(name);
        AddPassenger(disabled, "avares://BusFlowApp/Assets/man.png");
        Log.Add($"Инвалид {name} ждет автобус.");
    }
}
