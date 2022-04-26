using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace Quiz.Wpf.Messages;

public class QuestionEditedMessage : ValueChangedMessage<QuestionEditedMessageArgs>
{
    public QuestionEditedMessage(QuestionEditedMessageArgs value) 
        : base(value)
    {
    }
}