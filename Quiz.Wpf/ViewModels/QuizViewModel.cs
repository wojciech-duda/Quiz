using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Quiz.Wpf.ViewModels;

public class QuizViewModel : ObservableObject
{
    private readonly Domain.Models.Quiz _quiz;
    private readonly IPageService _pageService;
    private string _name;
    private ObservableCollection<QuestionViewModel> _questions;

    public QuizViewModel(Domain.Models.Quiz quiz, IPageService pageService)
    {
        _quiz = quiz;
        _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
        Name = quiz.Name;
        Questions = new ObservableCollection<QuestionViewModel>(quiz.Questions.Select(q => new QuestionViewModel(q)).ToList());
        DeleteQuestionCommand = new RelayCommand<QuestionViewModel>(HandleDeleteQuestionCommand);
        EditQuestionCommand = new AsyncRelayCommand<QuestionViewModel>(HandleEditQuestionCommand);
    }

    public RelayCommand<QuestionViewModel> DeleteQuestionCommand { get; }
    public AsyncRelayCommand<QuestionViewModel> EditQuestionCommand { get; }

    public string Name
    {
        get => _name;
        set
        {
            SetProperty(ref _name, value);
            _quiz.Name = value;
        }
    }

    public ObservableCollection<QuestionViewModel> Questions
    {
        get => _questions;
        set => SetProperty(ref _questions, value);
    }

    private void HandleDeleteQuestionCommand(QuestionViewModel question)
    {
        _quiz.RemoveQuestion(question.InnerObject.Id);
        Questions.Remove(question);
    }

    private async Task HandleEditQuestionCommand(QuestionViewModel question)
    {
        await _pageService.ChangePageAsync<EditQuestionViewModel, EditQuestionViewModelParameter>(new EditQuestionViewModelParameter()
        {
            QuestionId = question.InnerObject.Id
        });
    }
}