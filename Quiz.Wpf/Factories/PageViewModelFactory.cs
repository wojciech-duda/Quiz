using System;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Wpf.ViewModels;

namespace Quiz.Wpf.Factories;

public class PageViewModelFactory : IPageViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PageViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public CreateOpenViewModel CreateCreateOpenViewModel() => _serviceProvider.GetRequiredService<CreateOpenViewModel>();
    
    public QuizOverviewViewModel CreateQuizOverviewViewModel() => _serviceProvider.GetRequiredService<QuizOverviewViewModel>();
    
    public CreateQuestionViewModel CreateCreateQuestionViewModel() => _serviceProvider.GetRequiredService<CreateQuestionViewModel>();
    public EditQuestionViewModel CreateEditQuestionViewModel() => _serviceProvider.GetRequiredService<EditQuestionViewModel>();
}