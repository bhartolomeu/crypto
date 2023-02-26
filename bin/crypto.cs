using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace cryptoController{
    class EncryptingMain{
        static void Main(string[] args){
            //Console.WriteLine(string.Join(" ",args));

            if(args.Length==3){
                //caso o primeiro parâmetro seja "-e
                if(args[0]=="-e"){
                    string[] argEncrypt = new string[2];
                    //Se o arquivo do primeiro argumento existir
                    if(File.Exists(args[1])){
                        argEncrypt[0] = '"'+args[1]+'"';// nome e diretório do arquivo para codificar
                    //    Se não existir...
                    }else{
                        //alerta de arquivo não encontrado
                        Console.WriteLine("File not found: "+args[1]);
                    }
                    //Se o arquivo do primeiro argumento existir
                    if(Directory.Exists(args[2])){
                        argEncrypt[1] = '"'+args[2]+'"';// diretório para guardar os arquivos gerados
                    //    Se não existir...
                    }else{
                        //alerta de diretório não encontrado
                        Console.WriteLine("Path not found: "+args[2]);
                    }
                    if(File.Exists(args[1]) && Directory.Exists(args[2])){
                        //pedido para executar o processo de codificação
                        executar("encrypt",argEncrypt);
                    //Se o arquivo ou o Diretório não existirem
                    }else{
                        //orientação para verificação
                        Console.WriteLine("Please, verify if there is something wrong with your file or yor path and try again.");
                    }
                }
            }
            //caso tenham 4 argumentos
            if(args.Length==4){
                //caso o primeiro parâmetro seja "-d"
                if(args[0]=="-d"){
                    string[] argDecrypt = new string[3];//parâmetros para decodificar
                    //caso o arquivo codficado exista
                    if(File.Exists(args[1])){
                        argDecrypt[0] = '"'+args[1]+'"';// data encrypted
                    //   Se não existir...
                    }else{
                        Console.WriteLine("Encrypted file not found: "+args[1]);
                    }
                    //caso o initialization vector exista
                    if(File.Exists(args[2])){
                        argDecrypt[1] = '"'+args[2]+'"';// iv file
                    //   Se não existir...
                    }else{
                        Console.WriteLine("IV file not found: "+args[2]);
                    }
                    //caso o arquivo de chaves exista
                    if(File.Exists(args[3])){
                        argDecrypt[2] = '"'+args[3]+'"';// key file
                    //   Se não existir...
                    }else{
                        Console.WriteLine("Key file not found: "+args[3]);
                    }
                    //Se todos os arquivos existirem
                    if(File.Exists(args[1]) && File.Exists(args[2]) && File.Exists(args[3])){
                        //pedido para executar o processo de decodificação
                        executar("decrypt",argDecrypt);
                    //   Se um deles não existir...
                    }else{
                        //orientação para verificação
                        Console.WriteLine("Please, verify if there is something wrong with your file or yor path and try again.");
                    }
                }
            }
            //Escopo para caso o usuário digite apenas dois argumentos
            if(args.Length==1 | args.Length==2){
                //caso o primeiro agumento seja "man" e enha um total de dois argumentos(man + alguma coisa)
                if(args[0]=="man" && args.Length==2){
                    //caso o usuário tenha preferido utilizar a versão em inglês
                    if(args[1]=="en"){
                        //procedimento para imprimir manual em inglês na tela
                        help("en");
                    //caso o usuário tenha preferido utilizar a versão em português
                    }else if(args[1]=="pt"){
                        //procedimento para imprimir manual em português na tela
                        help("pt");
                    //Caso tenha digitado man e osegundo argumento esteja incorreto, será impresso um alerta e o manual em inglês
                    }else{
                        //alerta de erro
                        Console.WriteLine("There is something wrong. Please, check the manual.\n\n");
                        //procedimento para imprimir manual em inglês na tela
                        help("en");
                    }
                //caso o usuário tenha digitado apenas man, será mostrado o manual em inglês
                }else if(args[0]=="man" && args.Length==1){
                    //procedimento para imprimir manual em inglês na tela
                    help("en");
                //Caso tenha digitado um valor qualquer como parâmetro
                }else{
                    //alerta de erro
                    Console.WriteLine("There is something wrong, please check the manual.");
                    //help("en");
                }
            }
            //Caso o usuário apenas execute o programa sem passar parâmetro algum
            if(args.Length==0){
                //alerta de erro
                Console.WriteLine("Not enough parameter. Please, check the manual.\n\n");
                //procedimento para imprimir manual em inglês na tela
                help("en");
            }
            //caso o usuário tenha passado algum parâmetro(1 ou n parâmetros)
            if(args.Length!=0){
                //caso o primeiro parâmetro não seja nenhuma das três opções esperadas
                if(args[0]!="man" && args[0]!="-d" && args[0]!="-e"){
                    //alerta de erro
                    Console.WriteLine("Command did not work. Please, check the manual.\n\n");
                    //procedimento para imprimir manual em inglês na tela
                    help("en");
                }

            }
        }
        static Dictionary<string,string[]> getManualContent(){

            var manual = new Dictionary <string, string[]> ();
            string[] index1 = new string[4]; 
            string[] index2 = new string[4]; 

            index1[0]="Manual for crypto command";
            index1[1]="argument for encrypting a message.\nYou must pass the file name and path to put the encrypted data, IV file and Key file. \ncrypto -e [file to encrypt] [path to put the Encrypted file, the IV file and the Key file]";
            index1[2]="argument for decrypting a message.\nYou must pass the encrypted file path, the IV - initialization vector - file path and the Key file path\n crypto -d [Encrypted file] [IV file] [Key file]";
            index1[3]="show the manual. \nYou must pass the language code right after man parammeter.\n crypto man [language code].";
            manual.Add("en",index1);

            index2[0] = "Manual para o comando crypto";
            index2[1] = "argumento para codificar uma mensagem. \nVocê deve passar  o nome do arquivo para ser codificado e o diretório paa colocar o arquivo codificado, com IV e as chaves. \n crypto -e [arquivo para codificar][diretório para colocar o arquivo encripitado, o arquivo IV - initialization vector - e arquivo com as chaves]";
            index2[2] = "argumento para decodificar uma mensagem. \nVocê deve passar o endereço do arquivo codificado, do IV - initialization vector - e o arquivo com as chaves. \n crypto -d [arquivo codificado] [arquivo IV ] [arquivo com as chaves]";
            index2[3] = "mostra o manual. \nVocê deve passar o código da língua logo após o parâmetro man. \n crypto man [código da língua]";
            manual.Add("pt",index2);

            return manual;
        }
        static void help(string lg){
            //para obter o manual em formato de dictionary, chama-se a função getManualContent
            Dictionary <string,string[]> manual = getManualContent();
            //Exibindo o manual
            Console.WriteLine("--------"+manual[lg][0]+"--------\n");;
            Console.WriteLine("-e : "+manual[lg][1]);
            Console.WriteLine("----------------------------\n");
            Console.WriteLine("-d : "+manual[lg][2] );
            Console.WriteLine("----------------------------\n");
            Console.WriteLine("man: "+manual[lg][3]);
            Console.WriteLine("----------------------------");
        }
        static void executar(string filename, string[] args){
            //Descomente esta linha para testes
            //Console.WriteLine(filename+" "+string.Join(" ",args));
            
            //informando o nome e diretório do arquivo executável
            ProcessStartInfo processo = new ProcessStartInfo(@""+filename);
            //passando os argumentos para serem usados
            processo.Arguments = string.Join(" ",args);
            //pedindo para não criar outra janela de prompt
            processo.CreateNoWindow = true;
            //iniciando o processo            
            Process.Start(processo);
        }
    }
}