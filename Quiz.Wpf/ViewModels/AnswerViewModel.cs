using Microsoft.Toolkit.Mvvm.ComponentModel;
using Quiz.Domain.Models;

namespace Quiz.Wpf.ViewModels;

public class AnswerViewModel : ObservableObject
{
    private readonly Answer _answer;
    private bool _isCorrect;

    private string _orderLetter;

    private string _text;

    public AnswerViewModel(Answer answer)
    {
        _answer = answer;
        Text = answer.Text;
        IsCorrect = answer.IsCorrect;
        OrderLetter = answer.OrderLetter;
    }

    public bool IsCorrect
    {
        get => _isCorrect;
        set
        {
            SetProperty(ref _isCorrect, value);
            _answer.IsCorrect = value;
        }
    }

    public string OrderLetter
    {
        get => _orderLetter;
        set
        {
            SetProperty(ref _orderLetter, value);
            _answer.OrderLetter = value;
        }
    }

    public string Text
    {
        get => _text;
        set
        {
            SetProperty(ref _text, value);
            _answer.Text = value;
        }
    }
}