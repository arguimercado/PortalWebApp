using System.Security.Cryptography;
using System.Text;

namespace Auth.Core.Supports;

public static class SimpleEncode
{
    public static string EncodePassword(string password)
    {
        //Declarations
        byte[] originalBytes;
        byte[] encodedBytes;
        MD5 md5;

        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
        md5 = new MD5CryptoServiceProvider();
        originalBytes = Encoding.Default.GetBytes(password);
        encodedBytes = md5.ComputeHash(originalBytes);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < encodedBytes.Length; i++)
        {
            //change it into 2 hexadecimal digits  
            //for each byte  
            builder.Append(encodedBytes[i].ToString("x2"));
        }

        //Convert encoded bytes back to a ‘readable’ string
        return builder.ToString();
    }
}
