using Quiz.Domain.Models;

namespace Quiz.Wpf.Messages;

public class QuestionAddedMessageArgs : MessageArgs
{
    public Question Question { get; set; }
}