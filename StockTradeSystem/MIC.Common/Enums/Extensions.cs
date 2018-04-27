using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Common.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum FileExt
    {
        Zip,
        Lzh,
    }



    /// <summary>
    /// 
    /// </summary>
    public static class FileExtExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetExtension(this FileExt ext)
        {
            switch (ext)
            {
                case FileExt.Zip:
                    return ".zip";

                case FileExt.Lzh:
                    return ".lzh";
            }

            throw new ArgumentException("不明なFileExtが指定されました。");
        }
    }
}
