using Quiz.Domain.Models.Base;

namespace Quiz.Domain.Models;

public class Answer : Entity
{
    public bool IsCorrect { get; set; }

    public string OrderLetter { get; set; }

    public string Text { get; set; }
}