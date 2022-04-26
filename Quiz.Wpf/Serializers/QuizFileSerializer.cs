using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Quiz.Domain.Models;
using Quiz.Wpf.Encryption;

namespace Quiz.Wpf.Serializers;

public class QuizFileSerializer : IQuizFileSerializer
{
    private const string QuestionIdToken = "id ";
    private const string QuestionToken = "pytanie ";

    private static readonly List<string> AnswerTokens = new()
    {
        "A ",
        "B ",
        "C ",
        "D "
    };

    private readonly IEncrypter _encrypter;

    public QuizFileSerializer(IEncrypter encrypter)
    {
        _encrypter = encrypter ?? throw new ArgumentNullException(nameof(encrypter));
    }

    public async Task SaveToFileAsync(Domain.Models.Quiz quiz, string path)
    {
        var filePath = Path.Combine(path, $"{quiz.Name}.txt");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        await using var file = new StreamWriter(filePath);
        foreach (var question in quiz.Questions)
        {
            await file.WriteLineAsync(_encrypter.Encrypt($"{QuestionIdToken} {question.OrderNumber}"));
            await file.WriteLineAsync(_encrypter.Encrypt($"{QuestionToken} {question.Text}"));
            foreach (var answer in question.Answers)
            {
                var isCorrectAnswer = answer.IsCorrect ? "1" : "0";
                await file.WriteLineAsync(_encrypter.Encrypt($"{answer.OrderLetter} {answer.Text} {isCorrectAnswer}"));
            }

            await file.WriteLineAsync();
        }
    }

    public async Task<Domain.Models.Quiz> ReadFromFileAsync(string path)
    {
        var quiz = new Domain.Models.Quiz();
        Question question = null;
        using var reader = new StreamReader(path);
        while (await reader.ReadLineAsync() is { } encryptedLine)
        {
            var line = _encrypter.Decrypt(encryptedLine);
            if (line.StartsWith(QuestionIdToken))
            {
                question = new Question();
                quiz.AddOrUpdateQuestion(question);
            }
            else if (line.StartsWith(QuestionToken))
            {
                if (question is null)
                {
                    throw new InvalidOperationException("The definition of the question is invalid.");
                }

                question.Text = line.Replace(QuestionToken, string.Empty).Trim();
            }
            else if (AnswerTokens.Contains(line[..2]))
            {
                if (question is null)
                {
                    throw new InvalidOperationException("The definition of the question is invalid.");
                }

                var answer = new Answer
                {
                    OrderLetter = line[..1],
                    Text = line.Remove(0, 2).Remove(line.Length - 2),
                    IsCorrect = line[^1].ToString() == "1"
                };

                question.AddOrUpdateAnswer(answer);
            }
        }

        return quiz;
    }
}