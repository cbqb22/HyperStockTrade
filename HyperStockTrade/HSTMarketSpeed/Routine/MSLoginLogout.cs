using System;
using System.Diagnostics;
using System.Threading;

namespace HSTMarketSpeed.Routine
{
    public class MSLoginLogout
    {
        public static readonly string WORKING_DIR = @"C:/Program Files (x86)/MarketSpeed/MLauncher";
        public static readonly string FILE_NAME = @"C:/Program Files (x86)/MarketSpeed/MLauncher/MLauncher.exe";
        public static readonly string ARGS = "MarketSpeed";

        public static void LoginWork(string user, string password)
        {
            if (IsStarting())
            {
                return;
            }


            ProcessStartInfo info = new ProcessStartInfo();
            info.WorkingDirectory = WORKING_DIR;
            info.FileName = FILE_NAME;
            info.Arguments = ARGS;
            Process.Start(info);

            // 本体が起動するのを待つ
            IntPtr hWndMarketSpeed = MSLoginHelper.WaitFindingTopWindow("Market Speed Ver13.2");

            // ログインボタンが配置されているエリアのハンドルを取得する
            IntPtr hWndLoginArea = MSLoginHelper.WaitFindingChildWindow(hWndMarketSpeed, "Custom", "ToolMenu");

            // Market Speedのログインダイアログを探す 
            IntPtr hWndLoginDialog = MSLoginHelper.WaitFindingTopWindow("Market Speed - ﾛｸﾞｲﾝ",
                delegate()
                {
                    // ログインボタンを押下する
                    MSLoginHelper.RECT pos;
                    MSLoginHelper.GetWindowRect(hWndLoginArea, out pos);
                    int x = pos.right - pos.left - 20;
                    int y = 20;
                    MSLoginHelper.PushButton(hWndLoginArea, x, y);
                    Thread.Sleep(100);
                });

            // ユーザーID入力欄に入力する   
            MSLoginHelper.SendText(MSLoginHelper.FindNextWindow(hWndLoginDialog, "ﾛｸﾞｲﾝID"), user);

            // パスワード入力欄に入力する
            MSLoginHelper.SendText(MSLoginHelper.FindNextWindow(hWndLoginDialog, "ﾊﾟｽﾜｰﾄﾞ"), password);

            // "OK"ボタンを押下する
            MSLoginHelper.PushButton(MSLoginHelper.WaitFindingChildWindow(hWndLoginDialog, "Button", "OK"), 0, 0);

            Thread.Sleep(5000); // 起動確認のため、必ず５秒

        }

        public static void Exit()
        {
            var ps = Process.GetProcessesByName("MarketSpeed");


            foreach (Process p in ps)
            {
                p.Kill();
            }
            Thread.Sleep(1000); // 終了確認のため、必ず１秒

        }

        public static bool IsStarting()
        {
            var ps = Process.GetProcessesByName("MarketSpeed");
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
