using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calculator_MVVM.Models;

namespace Calculator_MVVM.ViewModels;

public partial class MainWindowViewModel : ViewModelBase 
{
    public CalculatorModel Calculator { get; } = new CalculatorModel();

    [RelayCommand]
    private void AddDigit(string digitStr)
    {
        if (int.TryParse(digitStr, out int digit))
        {
            Calculator.AddDigit(digit);
        }
    }

    [RelayCommand]
    private void SetOperator(string op) => Calculator.SetOperator(op);

    [RelayCommand]
    private void Calculate() => Calculator.Calculate();

    [RelayCommand]
    private void Backspace() => Calculator.Backspace();

    [RelayCommand]
    private void Clear() => Calculator.Clear();
}