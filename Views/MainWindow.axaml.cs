using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using BusFlowApp.ViewModels;

namespace BusFlowApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        if (DataContext is MainWindowViewModel viewModel)
        { 
            //viewModel.BusFlowViewModel.PassengersBoardedAnimation += OnPassengersBoarded;
        }
        StartBusLoop();
    }

    // private async void OnPassengersBoarded()
    // {
    //     await Task.Delay(500);
    // }
    

    private async void StartBusLoop()
    {
        if (BusPanel.RenderTransform is not TranslateTransform transform)
        {
            transform = new TranslateTransform();
            BusPanel.RenderTransform = transform;
        }

        var screenWidth = 1280;
        var stopX = 20;
        var offScreenLeft = -200;

        while (true)
        {
            await AnimateBus(transform, screenWidth, stopX, TimeSpan.FromSeconds(10));
            await Task.Delay(15000);
            await AnimateBus(transform, stopX, offScreenLeft, TimeSpan.FromSeconds(4));
            await Task.Delay(1000);
        }
    }

    private async Task AnimateBus(TranslateTransform transform, double from, double to, TimeSpan duration)
    {
        var sw = Stopwatch.StartNew();
        while (sw.Elapsed < duration)
        {
            var progress = sw.Elapsed.TotalMilliseconds / duration.TotalMilliseconds;
            progress = Math.Sin(progress * Math.PI - Math.PI / 2) / 2 + 0.5;
            transform.X = from + (to - from) * progress;
            await Task.Delay(16);
        }
        transform.X = to;
    }

    private static readonly string[] FirstNames = { "Иван", "Петя", "Алексей", "Дмитрий", "Сергей", "Виктор", "Мария", "Анна", "Юля", "Оля", "Наталия" };
    private static readonly string[] ManFirstNames = { "Иван", "Петя", "Алексей", "Дмитрий", "Сергей", "Виктор" };
    private static readonly string[] WomanFirstNames = { "Мария", "Анна", "Юля", "Оля", "Наталия" };

    private string GenerateRandomName()
    {
        var random = new Random();
        return FirstNames[random.Next(FirstNames.Length)];
    }

    private string GenerateManRandomName()
    {
        var random = new Random();
        return ManFirstNames[random.Next(ManFirstNames.Length)];
    }

    private string GenerateWomanRandomName()
    {
        var random = new Random();
        return WomanFirstNames[random.Next(WomanFirstNames.Length)];
    }

    private void AddMan_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            var name = GenerateManRandomName();
            viewModel.BusFlowViewModel.AddMan(name);
        }
    }

    private void AddWoman_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            var name = GenerateWomanRandomName();
            viewModel.BusFlowViewModel.AddWoman(name);
        }
    }

    private void AddChild_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            var name = GenerateRandomName();
            viewModel.BusFlowViewModel.AddChild(name);
        }
    }

    private void AddDisabled_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            var name = GenerateRandomName();
            viewModel.BusFlowViewModel.AddDisabled(name);
        }
    }
}
