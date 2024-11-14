using System;

namespace WinCDRewrite 
{
    class Creator 
    {
        public static void Main(String[] args)
        {
            long chunkSize = 0;
            
            Console.WriteLine("WinCD - Install windows from any optical media");
            Console.WriteLine("(C) Planethac 2024");
            Console.WriteLine();
            long mediaType = MediaTypeSelect();
            Console.Clear();
            FileInfo isoFile = GetIsoFile();
            switch (mediaType)
            {
                case 1:
                    chunkSize = 650;
                    break;
                case 2:
                    chunkSize = 4600;
                    break;
                case 3:
                    chunkSize = 8300;
                    break;
                case 4:
                    chunkSize = 23900;
                    break;
                case 0:
                    chunkSize = GetNonStandardDiskSize();
                    break;
            };
            
            Directory.CreateDirectory("WorkingDir/ImageChunks");

            long byteChunkSize = chunkSize * 1000000;
            long amountOfChunks = isoFile.Length / byteChunkSize;

            if (amountOfChunks == 0) amountOfChunks = 1;
            
            for (long x = 1; x < amountOfChunks+1; x++)
            {
                Console.Clear();
                Console.WriteLine($"Dividing Image ({x}/{isoFile.Length/byteChunkSize})");
                Console.WriteLine("NANmb/NANmb");
                FileStream chunkStream = new FileStream($"WorkingDir/ImageChunks/Chunk{x}.img", FileMode.Append);
                FileStream isoStream = new FileStream(isoFile.FullName, FileMode.Open);
                for (long i = 0; i < chunkSize * 1000000; i++)
                {
                    var readByte = isoStream.ReadByte();
                    if (readByte == -1) break;
                    chunkStream.WriteByte(Convert.ToByte(readByte));
                    if (i % 1000000 == 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"Dividing Image ({x}/{amountOfChunks})");
                        Console.WriteLine($"{i/1000000}mb/{chunkSize}mb");
                    }
                }
                chunkStream.Close();
                isoStream.Close();
                
                Console.Clear();
                Console.WriteLine($"Dividing Image ({x}/{amountOfChunks})");
                Console.WriteLine($"{chunkSize}mb/{chunkSize}mb");
            }

            
        }

        public static long MediaTypeSelect()
        {
            Console.WriteLine("Select Media Type");
            Console.WriteLine("[1] CD     - [700mb]");
            Console.WriteLine("[2] DVD    - [4700mb]");
            Console.WriteLine("[3] DVD-DL - [8400mb]");
            Console.WriteLine("[4] BD     - [24000mb]");
            Console.WriteLine();
            Console.WriteLine("[0] OTH");
            Console.Write(": ");
            string mediaTypeStr = Console.ReadLine();
            try
            {
                long mediaType = Convert.ToInt64(mediaTypeStr);
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

        public static long GetNonStandardDiskSize()
        {
            Console.WriteLine("Please specify disk size (recomended: > 500mb [recommeded: disksize -50mb])");
            Console.Write(": ");
            string csSize = Console.ReadLine();
            try
            {
                long ReturnVal = Convert.ToInt64(csSize);
                return ReturnVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("Input is not a valid number!");
                Console.WriteLine();
                return GetNonStandardDiskSize();
            }
        }
    }
}