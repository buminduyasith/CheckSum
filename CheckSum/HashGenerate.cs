using System.Security.Cryptography;
using System.Text;

namespace CheckSum
{
    public class HashGenerate
    {
        public string GetHashcodeForImage(string imagePath)
        {
            string hashCodeHex = string.Empty;

            try
            {
                StringBuilder sbHashCode;
                HashAlgorithm hshAlg;
                byte[] bytes;

                sbHashCode = new StringBuilder();
                hshAlg = SHA256.Create();

                using FileStream fs = File.Open(imagePath, FileMode.Open, FileAccess.Read);
                bytes = FileToByteArray(fs);
                hshAlg.ComputeHash(bytes);

                for (int i = 0; i < hshAlg?.Hash?.Length; i++)
                {
                    sbHashCode.Append(hshAlg.Hash[i].ToString("x2"));
                }

                hashCodeHex = sbHashCode.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return hashCodeHex;
        }

        private byte[] FileToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using MemoryStream ms = new MemoryStream();
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms.ToArray();
        }
    }
}
