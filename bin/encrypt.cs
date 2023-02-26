using System;
using System.Text;
using System.IO;
//using WorkingWithFiles.WritingToFile;
using System.Security.Cryptography;
namespace Encrypting{
    class EncryptingProgram{
        static void Main(string[] args){
                // Create a new instance of the Rijndael
                // class.  This generates a new key and initialization
                // vector (IV).
            EncryptStringToBytes(System.IO.File.ReadAllText(args[0]),args[1]);
        }
        static void EncryptStringToBytes(string plainText, string path){
            Console.WriteLine(path);
            byte[] encrypted;
            // Create an Rijndael object with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create()){
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream()){
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)){
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)){
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                        do{
                            if(path[0]=='"'){
                                path = path.Remove(0);
                            }
                            if(path[path.Length - 1]=='"'){
                                path = path.Remove(path.Length - 1);
                            }
                            if(path[path.Length - 1]!='/'){
                                path = path+"/";
                            }
                        }while(path[0]=='"' | path[path.Length - 1]=='"' | path[path.Length - 1]!='/');
                        //Armazenando o conteúdo encripitado
                        WritingBytesToFile(path+"Data",encrypted);
                        //Armazenando iv
                        WritingBytesToFile(path+"IV",rijAlg.IV);
                        //Armazenando as keys
                        WritingBytesToFile(path+"Key",rijAlg.Key);                
                    }
                }
            }                
        }
        static void WritingBytesToFile(string fileByte, byte[] content){
            FileStream file = new FileStream(fileByte, FileMode.OpenOrCreate);
            file.Write(content, 0, content.Length);
        }
    }
}