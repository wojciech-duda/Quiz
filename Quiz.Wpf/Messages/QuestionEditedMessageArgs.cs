using Quiz.Domain.Models;

namespace Quiz.Wpf.Messages;

public class QuestionEditedMessageArgs
{
    public Question Question { get; set; }
}