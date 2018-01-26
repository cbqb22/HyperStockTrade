using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Database.Commons.Enums
{
    public enum MuzionzouMarketCode : byte
    {
        /// <summary>
        /// 東証一部
        /// </summary>
        TSE1 = 11,

        /// <summary>
        /// 東証二部
        /// </summary>
        TSE2 = 12,

        /// <summary>
        /// 東証マザーズ（東証マザーズ外国含む）
        /// </summary>
        TSE_Mothers = 13,

        /// <summary>
        /// 東証外国(一部・二部含む)
        /// </summary>
        TSE_Foreign = 14,

        /// <summary>
        /// 名古屋一部
        /// </summary>
        NSE1 = 31,

        /// <summary>
        /// 名古屋二部
        /// </summary>
        NSE1 = 32,

        /// <summary>
        /// 福証（福証Q-Board含む）
        /// </summary>
        FSE = 33,

        /// <summary>
        /// ＪＱ（グロース、スタンダード、スタンダード外国含む）
        /// </summary>
        JQ = 91,

        ///// <summary>
        ///// 札証
        ///// </summary>
        //SSE = 12,

        ///// <summary>
        ///// 札証アンビシャス
        ///// </summary>
        //SSE_Ambitious = 13,


        ///// <summary>
        ///// 福証Q-Board
        ///// </summary>
        //FSE_QBoard = 15,

        ///// <summary>
        ///// 東証マザーズ外国
        ///// </summary>
        //TSE_Mothers_Foreign = 16,

    }


    /// <summary>
    /// MarketCodeの拡張メソッドクラス
    /// </summary>
    public static class MarketCodeExtension
    {
        /// <summary>
        /// 短い市場コードを取得します
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetMarketCode(this MuzionzouMarketCode code)
        {
            switch (code)
            {
                //case MuzionzouMarketCode.TSE:
                case MuzionzouMarketCode.TSE1:
                case MuzionzouMarketCode.TSE_Foreign:
                case MuzionzouMarketCode.TSE2:
                //case MuzionzouMarketCode.TSE2_Foreign:
                //case MuzionzouMarketCode.TSE_TPM:
                case MuzionzouMarketCode.TSE_Mothers:
                //case MuzionzouMarketCode.TSE_Mothers_Foreign:
                    return "T";

                case MuzionzouMarketCode.JQ:
                //case MuzionzouMarketCode.JQ_Growth:
                //case MuzionzouMarketCode.JQ_Standard:
                //case MuzionzouMarketCode.JQ_Standard_Foreign:
                    return "T";

                //case MuzionzouMarketCode.SSE:
                //case MuzionzouMarketCode.SSE_Ambitious:
                //    return "S";

                case MuzionzouMarketCode.FSE:
                //case MuzionzouMarketCode.FSE_QBoard:
                    return "F";
            }

            throw new ArgumentException("無効なMuzionzouMarketCodeが指定されました。MarketCode:" + code);
        }

    }
}