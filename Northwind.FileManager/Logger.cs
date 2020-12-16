using Northwind.ConfigurationManager.Parsers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.FileManager
{
    public class Logger
    {
        AppConfig config;
        
        object control = new object();
        
        FileSystemWatcher watcher;
        
        bool enabled = true;
        
        string path;

        public Logger()
        {
            DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
            path = info.Parent.Parent.FullName + "\\appsettings.json";
            config = FileOperations.GetConfiguration(path);
            watcher = new FileSystemWatcher(config.Source);
            watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;
            //Load cfg
            IDeserializer deserializer = new JsonParser();
            //!Load cfg
            watcher.Filter = "*.xml";
            watcher.Created += FileTransfer;
        }
        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                config = FileOperations.GetConfiguration(path);
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        private void FileTransfer(object sender, FileSystemEventArgs e)
        {
            lock (control)
            {
                var dirInfo = new DirectoryInfo(config.Target);
                var filePath = Path.Combine(config.Target, e.Name);
                var fileName = e.Name;
                var dt = DateTime.Now;
                var subPath = $"{dt.ToString("yyyy", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{dt.ToString("MM", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{dt.ToString("dd", DateTimeFormatInfo.InvariantInfo)}";
                var newPath = $"{config.Target}"+ "\\" +
                   $"{dt.ToString("yyyy", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{dt.ToString("MM", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{dt.ToString("dd", DateTimeFormatInfo.InvariantInfo)}\\" +
                   $"{Path.GetFileNameWithoutExtension(fileName)}_" +
                   $"{dt.ToString(@"yyyy_MM_dd_HH_mm_ss", DateTimeFormatInfo.InvariantInfo)}" +
                   $"{Path.GetExtension(fileName)}";

                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                dirInfo.CreateSubdirectory(subPath);
                File.Move(filePath, newPath);
                FileOperations.EncryptFile(newPath, newPath);
                var compressedPath = Path.ChangeExtension(newPath, "gz");
                var newCompressedPath = Path.Combine($"{config.Target}", Path.GetFileName(compressedPath));
                var decompressedPath = Path.ChangeExtension(newCompressedPath, "xml");
                FileOperations.Compress(newPath, compressedPath);
                File.Move(compressedPath, newCompressedPath);
                FileOperations.Decompress(newCompressedPath, decompressedPath);
                FileOperations.DecryptFile(decompressedPath, decompressedPath);
                FileOperations.AddToArchive(decompressedPath);
                File.Delete(newPath);
                File.Delete(newCompressedPath);
                File.Delete(decompressedPath);
            }
        }
    }
}
