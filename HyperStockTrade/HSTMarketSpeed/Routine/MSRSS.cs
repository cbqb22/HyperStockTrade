using System;
using System.Diagnostics;
using System.Threading;

namespace HSTMarketSpeed.Routine
{
    public static class MSRSS
    {
        public static readonly string WORKING_DIR = @"C:\Program Files (x86)\MarketSpeed\MarketSpeed\";
        public static readonly string FILE_NAME = @"C:\Program Files (x86)\MarketSpeed\MarketSpeed\RSS.exe";


        public static void MarketSpeedRSSStart()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.WorkingDirectory = WORKING_DIR;
            info.FileName = FILE_NAME;
            Process.Start(info);
            Thread.Sleep(5000); // 起動確認のため、必ず５秒
        }

        public static void MarketSpeedRSSShutdown()
        {
            var ps = Process.GetProcessesByName("RSS");


            foreach (System.Diagnostics.Process p in ps)
            {
                p.Kill();
            }

            Thread.Sleep(1000); // 終了確認のため、必ず１秒

        }

        public static bool IsStarting()
        {
            var ps = Process.GetProcessesByName("RSS");
            if (ps.Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
