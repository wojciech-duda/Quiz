using System.Threading.Tasks;

namespace Quiz.Wpf.Serializers;

public interface IQuizFileSerializer
{
    Task<Domain.Models.Quiz> ReadFromFileAsync(string path);
    Task SaveToFileAsync(Domain.Models.Quiz quiz, string path);
}