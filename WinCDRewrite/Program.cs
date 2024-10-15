using System;

namespace WinCDRewrite 
{
    class Creator 
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("WinCD - Install windows from any optical media");
            Console.WriteLine("(C) Planethac 2024");
            Console.WriteLine();
            int mediaType = MediaTypeSelect();
            Console.Clear();
            FileInfo isoFile = GetIsoFile();
        }

        public static int MediaTypeSelect()
        {
            Console.WriteLine("Select Media Type");
            Console.WriteLine("[0] CD     - [700mb]");
            Console.WriteLine("[1] DVD    - [4700mb]");
            Console.WriteLine("[2] DVD-DL - [8400mb]");
            Console.WriteLine("[3] BD     - [24000mb]");
            Console.WriteLine("[9] OTH");
            Console.Write(": ");
            string mediaTypeStr = Console.ReadLine();
            try
            {
                int mediaType = Convert.ToInt32(mediaTypeStr);
                return mediaType;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Please Specify a valid Number (0-3, 9)");
                Console.WriteLine();
                return MediaTypeSelect();
            }
        }

        public static FileInfo GetIsoFile()
        {
            Console.WriteLine("Enter ISO path");
            Console.Write(": ");
            string FileStr = Console.ReadLine();
            try
            {
                FileInfo fileInfo = new FileInfo(FileStr);
                if (fileInfo.Extension == ".iso")
                {
                    return fileInfo;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("File is not an iso");
                    Console.WriteLine();
                    return GetIsoFile();
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Please Enter a valid ISO path (ae: G:\\Files\\Windows.iso)");
                Console.WriteLine();
                return GetIsoFile();
            }
        }
    }
}