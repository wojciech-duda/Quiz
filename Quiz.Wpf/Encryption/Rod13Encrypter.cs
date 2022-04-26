using System.Text;
using System.Text.RegularExpressions;

namespace Quiz.Wpf.Encryption;

public class Rod13Encrypter : IEncrypter
{
    public string Encrypt(string text)
    {
        return EncryptOrDecrypt(text);
    }

    public string Decrypt(string text)
    {
        return EncryptOrDecrypt(text);
    }

    private static string EncryptOrDecrypt(string input)
    {
        var result = new StringBuilder();
        var regex = new Regex("[A-Za-z]");
        foreach (var c in input)
        {
            if (regex.IsMatch(c.ToString()))
            {
                var code = ((c & 223) - 52) % 26 + (c & 32) + 65;
                result.Append((char) code);
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
}