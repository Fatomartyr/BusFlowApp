using BusFlowApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BusFlowApp.ViewModels;

public class VisualPassenger : ObservableObject
{
    public required IPassenger Passenger { get; set; }
    public required string ImagePath { get; set; }

    private double _x;
    public double X
    {
        get => _x;
        set => SetProperty(ref _x, value);
    }

    private double _y;
    public double Y
    {
        get => _y;
        set => SetProperty(ref _y, value);
    }

}