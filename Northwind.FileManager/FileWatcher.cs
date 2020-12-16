using System.ServiceProcess;
using System.Threading;

namespace Northwind.FileManager
{
    partial class FileWatcher : ServiceBase
    {
        Logger logger;
        public FileWatcher()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
