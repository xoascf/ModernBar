using System;

namespace ModernBar
{
    internal sealed class Program
    {
        private const string MutexName = "ModernBar";
        private const int MutexAttempts = 10;
        private const int MutexWaitMs = 1000;

        private static System.Threading.Mutex _ModernBarMutex;

        /// <summary>
        /// The main entry point for the application
        /// </summary>
        [STAThread]
        public static int Main(string[] args)
        {
            if (!SingleInstanceCheck())
            {
                return 1;
            }

            App app = new App();
            app.InitializeComponent();

            return app.Run();
        }

        private static bool GetMutex()
        {
            _ModernBarMutex = new System.Threading.Mutex(true, MutexName, out bool ok);

            return ok;
        }

        private static bool SingleInstanceCheck()
        {
            for (int i = 0; i < MutexAttempts; i++)
            {
                if (!GetMutex())
                {
                    // Dispose the mutex, otherwise it will never create new
                    _ModernBarMutex.Dispose();
                    System.Threading.Thread.Sleep(MutexWaitMs);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
