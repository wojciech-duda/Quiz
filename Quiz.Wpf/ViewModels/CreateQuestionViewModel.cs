using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Quiz.Domain.Models;
using Quiz.Wpf.Messages;

namespace Quiz.Wpf.ViewModels;

public class CreateQuestionViewModel : PageViewModelBase
{
    private readonly IPageService _pageService;
    private readonly IMessenger _messenger;
    private QuestionViewModel _question;

    public CreateQuestionViewModel(IPageService pageService, IMessenger messenger)
    {
        _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
        _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        var question = new Question();
        Question = new QuestionViewModel(question);
        SaveCommand = new AsyncRelayCommand(HandleSaveCommand);
        CancelCommand = new AsyncRelayCommand(HandleCancelCommand);
    }

    private async Task HandleCancelCommand()
    {
        await _pageService.ChangePageAsync<QuizOverviewViewModel>();
    }

    private async Task HandleSaveCommand()
    {
        _messenger.Send(new QuestionAddedMessage(new QuestionAddedMessageArgs()
        {
            Question = Question.InnerObject
        }));
        await _pageService.ChangePageAsync<QuizOverviewViewModel>();
    }

    public QuestionViewModel Question
    {
        get => _question;
        set => SetProperty(ref _question, value);
    }
    
    public AsyncRelayCommand SaveCommand { get; private set; }
    
    public AsyncRelayCommand CancelCommand { get; private set; }
}