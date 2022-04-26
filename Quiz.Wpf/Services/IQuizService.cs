using DomainQuiz = Quiz.Domain.Models;

namespace Quiz.Wpf.Services
{
    public interface IQuizService
    {
        DomainQuiz.Quiz QuizModel { get; }

        void CreateQuiz();
    }
}