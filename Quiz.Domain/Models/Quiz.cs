using Quiz.Domain.Helpers;
using Quiz.Domain.Models.Base;

namespace Quiz.Domain.Models;

public class Quiz : Entity
{
    private ICollection<Question> _questions;

    public Quiz()
    {
        _questions = new List<Question>();
    }

    public string Name { get; set; }

    public IEnumerable<Question> Questions => _questions;

    public void AddOrUpdateQuestion(Question question)
    {
        var existingQuestion = _questions.FirstOrDefault(x => x.Id == question.Id);
        if (existingQuestion is not null)
        {
            RemoveQuestion(existingQuestion.Id);
        }

        question.Id = Guid.NewGuid();
        question.OrderNumber = OrderingHelpers.GetMaxOrderNumber(_questions) + 1;
        _questions.Add(question);
        _questions = OrderingHelpers.ReOrderCollection(Questions);
    }

    public void RemoveQuestion(Guid questionId)
    {
        var question = _questions.FirstOrDefault(x => x.Id == questionId);
        if (question is not null)
        {
            _questions.Remove(question);
            OrderingHelpers.ReOrderCollection(Questions);
        }
    }
}