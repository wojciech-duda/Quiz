namespace Quiz.Wpf.Encryption;

public interface IEncrypter
{
    string Decrypt(string text);
    string Encrypt(string text);
}