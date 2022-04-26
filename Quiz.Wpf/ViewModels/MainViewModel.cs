using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Quiz.Wpf.Factories;

namespace Quiz.Wpf.ViewModels;

public class MainViewModel : ObservableObject, IPageService
{
    private readonly IPageViewModelFactory _pageViewModelFactory;

    private PageViewModelBase _selectedPageViewModel;

    public MainViewModel(IPageViewModelFactory pageViewModelFactory)
    {
        _pageViewModelFactory = pageViewModelFactory ?? throw new ArgumentNullException(nameof(pageViewModelFactory));
    }

    public PageViewModelBase SelectedPageViewModel
    {
        get => _selectedPageViewModel;
        set => SetProperty(ref _selectedPageViewModel, value);
    }

    public async Task ChangePageAsync<TViewModel>() where TViewModel : PageViewModelBase
    {
        if (typeof(TViewModel) == typeof(CreateOpenViewModel))
        {
            SelectedPageViewModel = _pageViewModelFactory.CreateCreateOpenViewModel();
        }
        else if (typeof(TViewModel) == typeof(QuizOverviewViewModel))
        {
            SelectedPageViewModel = _pageViewModelFactory.CreateQuizOverviewViewModel();
        }
        else if (typeof(TViewModel) == typeof(CreateQuestionViewModel))
        {
            SelectedPageViewModel = _pageViewModelFactory.CreateCreateQuestionViewModel();
        }
        

        await Task.CompletedTask;
    }

    public async Task ChangePageAsync<TViewModel, TParameter>(TParameter parameter) where TViewModel : InitializablePageViewModelBase<TParameter> where TParameter : class
    {
        if (typeof(TViewModel) == typeof(EditQuestionViewModel))
        {
            var vm = _pageViewModelFactory.CreateEditQuestionViewModel();
            vm.Initialize((EditQuestionViewModelParameter)(object)parameter);
            SelectedPageViewModel = vm;
        }
        
        await Task.CompletedTask;
    }
    
    public async Task InitializeAsync()
    {
        var vm = _pageViewModelFactory.CreateCreateOpenViewModel();
        SelectedPageViewModel = vm;

        await Task.CompletedTask;
    }
}