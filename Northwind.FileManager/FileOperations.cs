using Northwind.ConfigurationManager.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Northwind.FileManager
{
    public static class FileOperations
    {
        public static void EncryptFile(string inputFile, string outputFile)
        {
            string password = @"myKey123";
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);
            byte[] arr = File.ReadAllBytes(inputFile);

            string cryptFile = outputFile;
            using (FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create))
            {
                RijndaelManaged RMCrypto = new RijndaelManaged();

                using (CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write))
                {
                    foreach (byte bt in arr)
                    {
                        cs.WriteByte(bt);
                    }
                }
            }
        }

        public static void DecryptFile(string inputFile, string outputFile)
        {
            {
                string password = @"myKey123";

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                var bt = new List<byte>();

                using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                {
                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    using (CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, key),
                        CryptoStreamMode.Read))
                    {
                        int data;
                        while ((data = cs.ReadByte()) != -1)
                        {
                            bt.Add((byte)data);
                        }
                    }
                }
                using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                {
                    foreach (var b in bt)
                    {
                        fsOut.WriteByte(b);
                    }
                }
            }
        }

        public static void Compress(string sourceFile, string compressedFile)
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        public static void Decompress(string compressedFile, string targetFile)
        {
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(targetFile))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }

        public static void AddToArchive(string filePath)
        {
            var archivePath = "C:\\TargetDirectory\\archive.zip";

            using (ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Update))
            {
                zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
            }
        }

        public static AppConfig GetConfiguration(string src)
        {
            using (var file = File.Open(src, FileMode.Open))
            {
                StreamReader reader = new StreamReader(file);
                string xml = reader.ReadToEnd();
                reader.Close();
                IDeserializer deserializer = new JsonParser();
                AppConfig configuration = deserializer.Deserialize<AppConfig>(xml);
                return configuration;
            }
        }

        public static void SetConfiguration(string src, AppConfig configuration)
        {
            using (var file = File.Open(src, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(file);
                ISerializer serrializer = new JsonParser();
                string xml = serrializer.Serialize(configuration);
                writer.Write(xml);
                writer.Close();
            }
        }
    }
}