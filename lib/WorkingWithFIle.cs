using System.IO;
using System.Text;

namespace WorkingWithFiles{
    class publicWriteFile{      
        public static void Bytes(string fileName, byte[] content){
            FileStream fileBytes = new FileStream(fileName, FileMode.OpenOrCreate);
            fileBytes.Write(content, 0, content.Length);
        }
        public static void StringToBytes(string fileName, string content){
            FileStream fileString = new FileStream(fileName,FileMode.OpenOrCreate);
            byte[] contentInByte = new UTF8Encoding(true).GetBytes(content);
            fileString.Write(contentInByte, 0, contentInByte.Length);
        }
    }
    public class ReadFile{
        publicstatic byte[] Bytes(string file){
            return File.ReadAllBytes(file);
        }
        publicstatic string BytesToString(string file){
            return File.ReadAllBytes(file).ToString();
        }
    }
}
