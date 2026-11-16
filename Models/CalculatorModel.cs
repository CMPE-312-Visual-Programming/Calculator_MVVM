using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Calculator_MVVM.Models;

public partial class CalculatorModel : ObservableObject
{
    private List<double> _numbers = new List<double> { 0 };
    private List<string> _operations = new List<string>();
    private int _currentIndex = 0;
    
    private bool _hasTypedCurrentNumber = false;
    private bool _shouldResetOnNextNumber = false;
    
    [ObservableProperty]
    private string _primaryDisplay = "0";
    
    [ObservableProperty]
    private string _secondaryDisplay = "";

    public void AddDigit(int digit)
    {
        if (_shouldResetOnNextNumber)
            Clear();

        _numbers[_currentIndex] = _numbers[_currentIndex] * 10 + digit;
        _hasTypedCurrentNumber = true;

        NotifyDisplayChanged();
    }

    public void SetOperator(string op)
    {
        _shouldResetOnNextNumber = false;

        if (_currentIndex == 0) 
        {
            if (_operations.Count == 0)
            {
                _operations.Add(op);
                _numbers.Add(0); 
                _currentIndex = 1;
            }
            else
            {
                _operations[0] = op;
                if (_numbers.Count == 1) _numbers.Add(0);
                _currentIndex = 1;
            }
            _hasTypedCurrentNumber = false;
        }
        else if (_currentIndex == 1) 
        {
            if (!_hasTypedCurrentNumber)
            {
                _operations[0] = op; 
            }
            else
            {
                double res = CalculateSingle(_numbers[0], _operations[0], _numbers[1]);
                SecondaryDisplay = res.ToString(); 
                
                _numbers[0] = res;
                _operations[0] = op;
                _numbers[1] = 0;
                _hasTypedCurrentNumber = false;
            }
        }
        NotifyDisplayChanged();
    }

    public void Calculate()
    {
        if (_operations.Count > 0 && _currentIndex == 1)
        {
            double res = CalculateSingle(_numbers[0], _operations[0], _numbers[1]);
            
            
            SecondaryDisplay = res.ToString();
            
            _numbers = new List<double> { res };
            _operations.Clear();
            _currentIndex = 0;
            _hasTypedCurrentNumber = false;
            _shouldResetOnNextNumber = true;
            NotifyDisplayChanged();
        }
    }

    public void Backspace()
    {
        if (_shouldResetOnNextNumber)
        {
            Clear();
            return;
        }

        if (!_hasTypedCurrentNumber && _currentIndex == 1)
        {
            _operations.Clear();
            _numbers.RemoveAt(1);
            _currentIndex = 0;
            _hasTypedCurrentNumber = true;
            
            NotifyDisplayChanged(); 
            return;
        }

        _numbers[_currentIndex] = Math.Truncate(_numbers[_currentIndex] / 10);
        NotifyDisplayChanged();
    }

    public void Clear()
    {
        _numbers = new List<double> { 0 };
        _operations.Clear();
        _currentIndex = 0;
        _hasTypedCurrentNumber = false;
        _shouldResetOnNextNumber = false;
        SecondaryDisplay = ""; 
        
        NotifyDisplayChanged();
    }
    
    private double CalculateSingle(double a, string op, double b)
    {
        double result = a;
        switch (op)
        {
            case "+": result = a + b; break;
            case "-": result = a - b; break;
            case "*": result = a * b; break;
            case "/": result = (b != 0) ? (a / b) : 0; break;
        }
        return result;
    }

    private string GenerateDisplayString()
    {
        string text = _numbers[0].ToString();
        if (_operations.Count > 0)
        {
            text += $" {_operations[0]} ";
            if (_numbers.Count > 1) text += _numbers[1].ToString();
        }
        return text;
    }
    
    private void NotifyDisplayChanged()
    {
        PrimaryDisplay = GenerateDisplayString();
    }
}