using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Quiz.Domain.Models;

namespace Quiz.Wpf.ViewModels;

public class QuestionViewModel : ObservableObject
{
    private ObservableCollection<AnswerViewModel> _answers;

    private int _orderNumber;

    private string _text;

    public QuestionViewModel(Question question)
    {
        InnerObject = question;
        Text = question.Text;
        Answers = new ObservableCollection<AnswerViewModel>(question.Answers.Select(a => new AnswerViewModel(a)).ToList());
    }

    public ObservableCollection<AnswerViewModel> Answers
    {
        get => _answers;
        set => SetProperty(ref _answers, value);
    }

    public Question InnerObject { get; }

    public int OrderNumber
    {
        get => _orderNumber;
        set
        {
            SetProperty(ref _orderNumber, value);
            InnerObject.OrderNumber = value;
        }
    }

    public string Text
    {
        get => _text;
        set
        {
            SetProperty(ref _text, value);
            InnerObject.Text = value;
        }
    }

    public string TextWithOrderNumber => $"{OrderNumber}. {Text}";
}