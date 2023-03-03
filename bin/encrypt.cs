using System;
using System.Text;
using System.IO;
using WorkingWithFiles;
using System.Security.Cryptography;
namespace Encrypting{
    class EncryptingProgram{
        static void Main(string[] args){
            // Create a new instance of the Rijndael class. This generates a new key and 
            //initialization vector (IV).
            //[DllImport("../lib/WorkingWithFiles.dll")]
            EncryptStringToBytes(WorkingWithFiles.ReadFile.BytesToString(args[0]),args[1]);
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
                        //[DllImport("../lib/WorkingWithFiles.dll")]
                        //Armazenando o conteúdo encripitado
                        WorkingWithFiles.WriteFile.Bytes(path+"Data",encrypted);
                        //Armazenando iv
                        WorkingWithFiles.WriteFile.Bytes(path+"IV",rijAlg.IV);
                        //Armazenando as keys
                        WorkingWithFiles.WriteFile.Bytes(path+"Key",rijAlg.Key);                
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