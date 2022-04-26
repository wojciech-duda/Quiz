using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using Quiz.Wpf.Encryption;
using Quiz.Wpf.Factories;
using Quiz.Wpf.Serializers;
using Quiz.Wpf.Services;
using Quiz.Wpf.ViewModels;

namespace Quiz.Wpf;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static ServiceProvider Container;

    protected override async void OnStartup(StartupEventArgs e)
    {
        Container = new ServiceCollection()
            .AddSingleton<IEncrypter, Rod13Encrypter>()
            .AddSingleton<IQuizFileSerializer, QuizFileSerializer>()
            .AddSingleton<IQuizService, QuizService>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<IPageService, MainViewModel>(x => x.GetRequiredService<MainViewModel>())
            .AddSingleton<CreateOpenViewModel>()
            .AddSingleton<QuizOverviewViewModel>()
            .AddTransient<CreateQuestionViewModel>()
            .AddTransient<EditQuestionViewModel>()
            .AddSingleton<IPageViewModelFactory, PageViewModelFactory>()
            .AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
            .BuildServiceProvider();
        base.OnStartup(e);
        
        var mainWindow = new MainWindow();
        var mainWindowViewModel = Container.GetRequiredService<MainViewModel>();
        await mainWindowViewModel.InitializeAsync();
        mainWindow.DataContext = mainWindowViewModel;
        mainWindow.Show();
        Current.MainWindow = mainWindow;
    }
}