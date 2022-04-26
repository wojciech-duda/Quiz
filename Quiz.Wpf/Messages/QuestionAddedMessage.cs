using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace Quiz.Wpf.Messages;

public class QuestionAddedMessage : ValueChangedMessage<QuestionAddedMessageArgs>
{
    public QuestionAddedMessage(QuestionAddedMessageArgs value) 
        : base(value)
    {
    }
}