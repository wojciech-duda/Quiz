using DomainQuiz = Quiz.Domain.Models;

namespace Quiz.Wpf.Services
{
    public class QuizService : IQuizService
    {
        public DomainQuiz.Quiz QuizModel { get; private set; }

        public void CreateQuiz()
        {
            QuizModel = new DomainQuiz.Quiz();
        }                
    }
}
