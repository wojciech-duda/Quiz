using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Quiz.Wpf.Messages;
using Quiz.Wpf.Serializers;
using Quiz.Wpf.Services;

namespace Quiz.Wpf.ViewModels;

public class QuizOverviewViewModel : PageViewModelBase
{
    private readonly IPageService _pageService;
    private readonly IMessenger _messenger;
    private readonly IQuizFileSerializer _quizFileSerializer;
    private readonly IQuizService _quizService;

    private QuizViewModel _quizVM;

    public QuizOverviewViewModel(IQuizService quizService, IPageService pageService, IMessenger messenger, IQuizFileSerializer quizFileSerializer)
    {
        _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
        _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
        _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        _quizFileSerializer = quizFileSerializer ?? throw new ArgumentNullException(nameof(quizFileSerializer));
        _messenger.Register<QuestionAddedMessage>(this, HandleQuestionAdded);
        _messenger.Register<QuestionEditedMessage>(this, HandleQuestionEditedMessage);
        QuizVM = new QuizViewModel(_quizService.QuizModel, pageService);
        AddQuestionCommand = new AsyncRelayCommand(HandleAddQuestionCommand);
        SaveToFileCommand = new AsyncRelayCommand(HandleSaveToFileCommand);
    }

    private async Task HandleSaveToFileCommand()
    {
        var filePath = await _quizFileSerializer.SaveToFileAsync(_quizService.QuizModel, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        MessageBox.Show($"Pomyślnie zapisano do pliku {filePath}");
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
    
    public AsyncRelayCommand SaveToFileCommand { get; }

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