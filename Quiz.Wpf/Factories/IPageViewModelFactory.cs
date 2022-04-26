using Quiz.Wpf.ViewModels;

namespace Quiz.Wpf.Factories;

public interface IPageViewModelFactory
{
    CreateOpenViewModel CreateCreateOpenViewModel();
    QuizOverviewViewModel CreateQuizOverviewViewModel();
    CreateQuestionViewModel CreateCreateQuestionViewModel();
    EditQuestionViewModel CreateEditQuestionViewModel();
}