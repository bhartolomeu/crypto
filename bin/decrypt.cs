using System;
//using System.IO;
using WorkingWithFile;
using System.Text;
using System.Security.Cryptography;

namespace RijndaelManaged_Example{
    class RijndaelExample{
        public static void Main(string[] args){
            //Console.WriteLine("decripitar dentro "+string.Join(" ",args));
            byte[] encrypted = ReadFile.Bytes(args[0]);
            byte[] IV = ReadFile.Bytes(args[1]);
            byte[] Key = ReadFile.Bytes(args[2]);
            string text = DecryptStringFromBytes(encrypted, Key,IV);
            //Console.WriteLine(text);
            WriteFile.StringToBytes(Path.GetFileNameWithoutExtension(args[0])+"Decrypted.txt",text);
        }
        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV){
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold the decrypted text.
            string plaintext = null;

            // Create an Rijndael object with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText)){
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)){
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt)){
                            // Read the decrypted bytes from the decrypting stream and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
/*        static void WritingTextToFile(string filename, string content){
            FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
            byte[] contentInBytes = new UTF8Encoding(true).GetBytes(content);
            file.Write(contentInBytes, 0, contentInBytes.Length);
        }
*/
    }
}
