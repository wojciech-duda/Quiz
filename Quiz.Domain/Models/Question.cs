using Quiz.Domain.Models.Base;

namespace Quiz.Domain.Models;

public class Question : OrderableEntity
{
    private readonly ICollection<Answer> _answers;

    public Question()
    {
        _answers = new List<Answer>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                OrderLetter = "A",
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrderLetter = "B",
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrderLetter = "C",
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrderLetter = "D",
            },
        };
    }

    public IEnumerable<Answer> Answers => _answers;

    public string Text { get; set; }

    public void AddOrUpdateAnswer(Answer answer)
    {
        var existingAnswer = _answers.FirstOrDefault(x => x.Id == answer.Id);
        if (existingAnswer is not null)
        {
            RemoveAnswer(existingAnswer.Id);
        }

        answer.Id = Guid.NewGuid();
        _answers.Add(answer);
    }

    public void RemoveAnswer(Guid answerId)
    {
        var answer = _answers.FirstOrDefault(x => x.Id == answerId);
        if (answer is not null)
        {
            _answers.Remove(answer);
        }
    }
}