using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calculator_MVVM.Models;

namespace Calculator_MVVM.ViewModels;

public partial class MainWindowViewModel : ViewModelBase 
{
    private readonly CalculatorModel _calculatorModel;

    // [ObservableProperty] arka planda INotifyPropertyChanged kodlarını otomatik üretir.
    // XAML'daki TextBox'lar bu arkadaşlara Binding ile bağlanacak.
    [ObservableProperty]
    private string _primaryDisplay = "0";

    [ObservableProperty]
    private string _secondaryDisplay = "";

    public MainWindowViewModel()
    {
        _calculatorModel = new CalculatorModel();
        
        // Motorun ürettiği olaylara abone oluyoruz
        _calculatorModel.OnDisplayChanged += OnEngineDisplayChanged;
        
        // İlk ekran açılışındaki değerleri al
        OnEngineDisplayChanged();
    }

    // Motor "Değiştim!" dediğinde ViewModel kendi değişkenlerini günceller.
    // [ObservableProperty] sayesinde UI bu değişimi anında fark edip ekranı yeniler.
    private void OnEngineDisplayChanged()
    {
        PrimaryDisplay = _calculatorModel.PrimaryDisplay;
        SecondaryDisplay = _calculatorModel.SecondaryDisplay;
    }

    // --- KOMUTLAR (COMMANDS) ---
    // [RelayCommand] arka planda ICommand arayüzünü otomatik üretir (Örn: AddDigitCommand)

    [RelayCommand]
    private void AddDigit(string digitStr)
    {
        // XAML'daki butonlardan CommandParameter ile gelen değeri integer'a çevirip motora at
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