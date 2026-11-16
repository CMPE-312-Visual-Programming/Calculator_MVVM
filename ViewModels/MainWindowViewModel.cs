using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calculator_MVVM.Models;

namespace Calculator_MVVM.ViewModels;

public partial class MainWindowViewModel : ViewModelBase 
{
    private readonly CalculatorModel _calculatorModel;

    [ObservableProperty]
    private string _primaryDisplay = "0";

    [ObservableProperty]
    private string _secondaryDisplay = "";

    public MainWindowViewModel()
    {
        _calculatorModel = new CalculatorModel();
        _calculatorModel.OnDisplayChanged += OnEngineDisplayChanged;
        OnEngineDisplayChanged();
    }

    private void OnEngineDisplayChanged()
    {
        PrimaryDisplay = _calculatorModel.PrimaryDisplay;
        SecondaryDisplay = _calculatorModel.SecondaryDisplay;
    }

    [RelayCommand]
    private void AddDigit(string digitStr)
    {
        if (int.TryParse(digitStr, out int digit))
        {
            _calculatorModel.AddDigit(digit);
        }
    }

    [RelayCommand]
    private void SetOperator(string op)
    {
        _calculatorModel.SetOperator(op);
    }

    [RelayCommand]
    private void Calculate() => _calculatorModel.Calculate();

    [RelayCommand]
    private void Backspace() => _calculatorModel.Backspace();

    [RelayCommand]
    private void Clear() => _calculatorModel.Clear();
}