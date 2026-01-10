using System.Security.Cryptography;
using System.Text;

namespace HDI.Domain.Common.Helpers;
public static class HashHelper
{
    public static string ComputeSha256Hash(string rawData)
    {
        if (string.IsNullOrEmpty(rawData)) 
            return string.Empty;

        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        
        return Convert.ToBase64String(bytes);
    }

    public static bool VerifyHash(string rawData, string hashedData)
    {
        var hashOfInput = ComputeSha256Hash(rawData);
        return string.Equals(hashOfInput, hashedData);
    }
}