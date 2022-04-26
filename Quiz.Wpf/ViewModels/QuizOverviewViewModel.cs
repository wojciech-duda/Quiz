using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Quiz.Wpf.Messages;
using Quiz.Wpf.Services;

namespace Quiz.Wpf.ViewModels;

public class QuizOverviewViewModel : PageViewModelBase
{
    private readonly IPageService _pageService;
    private readonly IMessenger _messenger;
    private readonly IQuizService _quizService;

    private QuizViewModel _quizVM;

    public QuizOverviewViewModel(IQuizService quizService, IPageService pageService, IMessenger messenger)
    {
        _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
        _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
        _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        _messenger.Register<QuestionAddedMessage>(this, HandleQuestionAdded);
        _messenger.Register<QuestionEditedMessage>(this, HandleQuestionEditedMessage);
        QuizVM = new QuizViewModel(_quizService.QuizModel, pageService);
        AddQuestionCommand = new AsyncRelayCommand(HandleAddQuestionCommand);
    }

    private void HandleQuestionEditedMessage(object recipient, QuestionEditedMessage message)
    {
        QuizVM = new QuizViewModel(_quizService.QuizModel, _pageService);
        _quizService.QuizModel.AddOrUpdateQuestion(message.Value.Question);
    }

    private void HandleQuestionAdded(object recipient, QuestionAddedMessage message)
    {
        QuizVM.Questions.Add(new QuestionViewModel(message.Value.Question));
        _quizService.QuizModel.AddOrUpdateQuestion(message.Value.Question);
    }

    public AsyncRelayCommand AddQuestionCommand { get; }

    public QuizViewModel QuizVM
    {
        get => _quizVM;
        set => SetProperty(ref _quizVM, value);
    }

    private async Task HandleAddQuestionCommand()
    {
        await _pageService.ChangePageAsync<CreateQuestionViewModel>();
    }
}