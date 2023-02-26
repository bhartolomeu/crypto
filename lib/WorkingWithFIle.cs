using System.IO;
using System.Text;

namespace WorkingWithFiles{
    class WriteFile{      
        static void Bytes(string fileName, byte[] content){
            FileStream fileBytes = new FileStream(fileName, FileMode.OpenOrCreate);
            fileBytes.Write(content, 0, content.Length);
        }
        static void StringToBytes(string fileName, string content){
            FileStream fileString = new FileStream(fileName,FileMode.OpenOrCreate);
            byte[] contentInByte = new UTF8Encoding(true).GetBytes(content);
            fileString.Write(contentInByte, 0, contentInByte.Length);
        }
    }
    class ReadFile{
        static byte[] Bytes(string file){
            return File.ReadAllBytes(file);
        }
        static string BytesToString(string file){
            return File.ReadAllBytes(file).ToString();
        }
    }
}
