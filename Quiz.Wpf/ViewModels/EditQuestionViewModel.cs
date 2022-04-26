using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Quiz.Domain.Models;
using Quiz.Wpf.Messages;
using Quiz.Wpf.Services;

namespace Quiz.Wpf.ViewModels;

public class EditQuestionViewModel : InitializablePageViewModelBase<EditQuestionViewModelParameter>
{
    private readonly IPageService _pageService;
    private readonly IQuizService _quizService;
    private readonly IMessenger _messenger;
    private QuestionViewModel _question;

    public EditQuestionViewModel(IPageService pageService, IQuizService quizService, IMessenger messenger)
    {
        _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
        _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
        _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        SaveCommand = new AsyncRelayCommand(HandleSaveCommand);
        CancelCommand = new AsyncRelayCommand(HandleCancelCommand);
    }
    
    private async Task HandleCancelCommand()
    {
        await _pageService.ChangePageAsync<QuizOverviewViewModel>();
    }   

    private async Task HandleSaveCommand()
    {
        _messenger.Send(new QuestionEditedMessage(new QuestionEditedMessageArgs()
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
    
    public override void Initialize(EditQuestionViewModelParameter parameter)
    {
        var question = _quizService.QuizModel.Questions.First(x => x.Id == parameter.QuestionId);
        Question = new QuestionViewModel(question);
    }
}