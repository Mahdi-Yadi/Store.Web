using System.Security.Cryptography;
using System.Text;
namespace Application.Tools;
public static class Hashing
{
    public static string EncodePasswordMd5(string password)
    {
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(password);
        encodedBytes = md5.ComputeHash(originalBytes);
        return BitConverter.ToString(encodedBytes);
    }
}