using System;
using System.IO;

namespace BashApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "/home/test/test/";
            string fileUrl = "/home/wwwroot/publish/BashApp.deps.json";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                Console.WriteLine("开始拷贝");

                File.Copy(fileUrl, filePath, true);

                Console.WriteLine("拷贝完成");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        
    }
}
