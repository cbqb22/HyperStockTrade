using MIC.Common.Archives.Lzhs.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Common.Archives.Lzhs
{
    public static class UnlhaManager
    {
        #region Public Method

        /// <summary>
        /// Lzhファイルを解凍します。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="outputPath"></param>
        public static void UnLzh(string filePath, string outputPath)
        {
            fnExtract(filePath, outputPath);
        }

        #endregion

        private const string DllPath = @"./UNLHA32.DLL";

        /// <summary>
        /// ロックオブジェクト
        /// </summary>
        private static readonly object m_oLockObject = new object();

        /// <summary>
        /// [DLL Import] Unlhaのコマンドラインメソッド
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドル(=0)</param>
        /// <param name="szCmdLine">コマンドライン</param>
        /// <param name="szOutput">実行結果文字列</param>
        /// <param name="dwSize">実行結果文字列格納サイズ</param>
        /// <returns>
        /// 0:正常、0以外:異常終了
        /// </returns>
        [DllImport(DllPath, CharSet = CharSet.Ansi)]
        private static extern int Unlha(IntPtr hwnd, string szCmdLine, StringBuilder szOutput, int dwSize);



        /// <summary>
        /// LZH圧縮[複数ファイル]
        /// </summary>
        /// <param name="aryFilePath">圧縮対象ファイル一覧(フルパス指定)</param>
        /// <param name="sLzhFilePath">LZHファイル名</param>
        private static void fnCompressFiles(List<string> aryFilePath, string sLzhFilePath)
        {
            lock (m_oLockObject)
            {
                StringBuilder sbCmdLine = new StringBuilder(1024);   // コマンドライン文字列
                StringBuilder sbOutput = new StringBuilder(1024);    // UNLHA32.dll出力文字
                                                                     //---------------------------------------------------------------------------------
                                                                     // コマンドライン文字列の作成
                                                                     //---------------------------------------------------------------------------------
                                                                     // a:書庫にファイルを追加
                                                                     // -jso1:ファイル排他制御
                sbCmdLine.AppendFormat("a -jso1 \"{0}\"", sLzhFilePath);
                // 圧縮対象ファイルをコマンドライン化
                foreach (string sFilePath in aryFilePath)
                {
                    sbCmdLine.AppendFormat(" \"{0}\"", sFilePath);
                }
                string sCmdLine = sbCmdLine.ToString();
                //---------------------------------------------------------------------------------
                // 圧縮実行
                //---------------------------------------------------------------------------------
                int iUnlhaRtn = Unlha((IntPtr)0, sCmdLine, sbOutput, sbOutput.Capacity);
                //---------------------------------------------------------------------------------
                // 成功判定
                //---------------------------------------------------------------------------------
                fnCheckLzhProc(iUnlhaRtn, sbOutput);
            }
        }
        /// <summary>
        /// LZH圧縮[フォルダ指定]
        /// </summary>
        /// <param name="sFolderPath">圧縮対象フォルダ(フルパス指定)</param>
        /// <param name="sLzhFilePath">LZHファイル名</param>
        private static void fnCompressFolder(string sFolderPath, string sLzhFilePath)
        {
            lock (m_oLockObject)
            {
                StringBuilder sbOutput = new StringBuilder(1024);   // UNLHA32.dll出力文字
                                                                    //---------------------------------------------------------------------------------
                                                                    // コマンドライン文字列の作成
                                                                    //---------------------------------------------------------------------------------
                                                                    // a:書庫にファイルを追加]
                                                                    // -d1:ディレクトリ (配下) の格納
                                                                    // -jso1:ファイル排他制御
                string sCmdLine = string.Format(
                    "a -d1 -jso1 \"{0}\" \"{1}\"\\ \"{2}\\*\"",
                    sLzhFilePath, Directory.GetParent(sFolderPath).FullName, sFolderPath);
                //---------------------------------------------------------------------------------
                // 圧縮実行
                //---------------------------------------------------------------------------------
                int iUnlhaRtn = Unlha((IntPtr)0, sCmdLine, sbOutput, sbOutput.Capacity);
                //---------------------------------------------------------------------------------
                // 成功判定
                //---------------------------------------------------------------------------------
                fnCheckLzhProc(iUnlhaRtn, sbOutput);
            }
        }
        /// <summary>
        /// LZH解凍
        /// </summary>
        /// <param name="sLzhFilePath">LZHファイル名</param>
        /// <param name="sDustFolder">出力先フォルダ</param>
        private static void fnExtract(string sLzhFilePath, string sDustFolder)
        {
            lock (m_oLockObject)
            {
                StringBuilder sbOutput = new StringBuilder(1024);   // UNLHA32.dll出力文字
                                                                    //---------------------------------------------------------------------------------
                                                                    // コマンドライン文字列の作成
                                                                    //---------------------------------------------------------------------------------
                                                                    // -x:解凍
                                                                    // -r2:ディレクトリ指定再帰モード
                                                                    // -jf0:指定した基準ディレクトリからの相対パスで展開
                                                                    // -jso1:ファイル排他制御
                string sCmdLine = string.Format(
                    "x -r2 -jf0 -jso1 \"{0}\" \"{1}\\\"", sLzhFilePath, sDustFolder);
                //---------------------------------------------------------------------------------
                // 解凍実行
                //---------------------------------------------------------------------------------
                int iUnlhaRtn = Unlha((IntPtr)0, sCmdLine, sbOutput, sbOutput.Capacity);
                //---------------------------------------------------------------------------------
                // 成功判定
                //---------------------------------------------------------------------------------
                fnCheckLzhProc(iUnlhaRtn, sbOutput);
            }
        }
        /// <summary>
        /// Unlhaメソッド成功判定
        /// </summary>
        /// <param name="iUnlhaRtn">Unlhaメソッドの戻り値</param>
        /// <param name="sbLzhOutputString">Unlhaメソッドの第3引数</param>
        private static void fnCheckLzhProc(int iUnlhaRtn, StringBuilder sbLzhOutputString)
        {
            //-------------------------------------------------------------------------------------
            // メソッドの戻り値=0なら正常終了
            //-------------------------------------------------------------------------------------
            if (iUnlhaRtn == 0)
                return;
            //-------------------------------------------------------------------------------------
            // 例外をスロー
            //-------------------------------------------------------------------------------------
            string sMsg = string.Format(
                "LZH圧縮/解凍処理に失敗:\nエラーコード={0}:\n{1}", iUnlhaRtn, sbLzhOutputString);
            throw new ApplicationException(sMsg);
        }
    }
}
