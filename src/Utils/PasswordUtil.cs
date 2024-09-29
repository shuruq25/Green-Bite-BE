using System.Security.Cryptography;
using System.Text;

public class PasswordUtils
{
    public static string HashPassword(
        string originaPassword,
        out string HashedPassword,
        out byte[] salt
    )
    {
        var hasher = new HMACSHA256();
        salt = hasher.Key;
        HashedPassword = BitConverter.ToString(
            hasher.ComputeHash(Encoding.UTF8.GetBytes(originaPassword))
        );
        return HashedPassword;
    }

    public static bool VerifyPassword(string originaPassword, string HashedPassword, byte[] salt)
    {
        var hasher = new HMACSHA256(salt);
        return BitConverter.ToString(hasher.ComputeHash(Encoding.UTF8.GetBytes(originaPassword)))
            == HashedPassword;
    }
}
