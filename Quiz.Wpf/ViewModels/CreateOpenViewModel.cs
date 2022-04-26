using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using Quiz.Wpf.Services;

namespace Quiz.Wpf.ViewModels;

public class CreateOpenViewModel : PageViewModelBase
{
    private readonly IPageService _pageService;
    private readonly IQuizService _quizService;

    private string _quizName;

    public CreateOpenViewModel(IPageService pageService, IQuizService quizService)
    {
        _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
        _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
        CreateCommand = new AsyncRelayCommand(HandleCreateCommand);
    }

    public AsyncRelayCommand CreateCommand { get; }

    public string QuizName
    {
        get => _quizName;
        set => SetProperty(ref _quizName, value);
    }

    private async Task HandleCreateCommand()
    {
        // TODO: validate
        _quizService.CreateQuiz();
        _quizService.QuizModel.Name = QuizName;
        await _pageService.ChangePageAsync<QuizOverviewViewModel>();
    }
}