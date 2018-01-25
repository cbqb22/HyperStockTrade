using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Database.Commons.Enums
{
    public enum MarketCode : byte
    {
        /// <summary>
        /// 東証
        /// </summary>
        TSE = 1,

        /// <summary>
        /// 東証一部
        /// </summary>
        TSE1 = 2,

        /// <summary>
        /// 東証一部外国
        /// </summary>
        TSE1_Foreign = 3,

        /// <summary>
        /// 東証二部
        /// </summary>
        TSE2 = 4,

        /// <summary>
        /// 東証二部外国
        /// </summary>
        TSE2_Foreign = 5,

        /// <summary>
        /// 東証TPM
        /// </summary>
        TSE_TPM = 6,

        /// <summary>
        /// 東証マザーズ
        /// </summary>
        TSE_Mothers = 7,

        /// <summary>
        /// ＪＱ
        /// </summary>
        JQ = 8,

        /// <summary>
        /// ＪＱグロース
        /// </summary>
        JQ_Growth = 9,

        /// <summary>
        /// ＪＱスタンダード
        /// </summary>
        JQ_Standard = 10,

        /// <summary>
        /// ＪＱスタンダード外国
        /// </summary>
        JQ_Standard_Foreign = 11,

        /// <summary>
        /// 札証
        /// </summary>
        SSE = 12,

        /// <summary>
        /// 札証アンビシャス
        /// </summary>
        SSE_Ambitious = 13,

        /// <summary>
        /// 福証
        /// </summary>
        FSE = 14,

        /// <summary>
        /// 福証Q-Board
        /// </summary>
        FSE_QBoard = 15,

        /// <summary>
        /// 東証マザーズ外国
        /// </summary>
        TSE_Mothers_Foreign = 16,

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
        public static string GetMarketCode(this MarketCode code)
        {
            switch (code)
            {
                case MarketCode.TSE:
                case MarketCode.TSE1:
                case MarketCode.TSE1_Foreign:
                case MarketCode.TSE2:
                case MarketCode.TSE2_Foreign:
                case MarketCode.TSE_TPM:
                case MarketCode.TSE_Mothers:
                case MarketCode.TSE_Mothers_Foreign:
                    return "T";

                case MarketCode.JQ:
                case MarketCode.JQ_Growth:
                case MarketCode.JQ_Standard:
                case MarketCode.JQ_Standard_Foreign:
                    return "T";

                case MarketCode.SSE:
                case MarketCode.SSE_Ambitious:
                    return "S";

                case MarketCode.FSE:
                case MarketCode.FSE_QBoard:
                    return "F";
            }

            throw new ArgumentException("無効なMarketCodeが指定されました。MarketCode:" + code);
        }

        /// <summary>
        /// 市場名称を返します。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetMarketName(this MarketCode code)
        {
            switch (code)
            {
                //TSE
                case MarketCode.TSE:
                    return "東証";
                case MarketCode.TSE1:
                    return "東証1部";
                case MarketCode.TSE1_Foreign:
                    return "東証1部外国";
                case MarketCode.TSE2:
                    return "東証2部";
                case MarketCode.TSE2_Foreign:
                    return "東証2部外国";
                case MarketCode.TSE_TPM:
                    return "東証TPM";
                case MarketCode.TSE_Mothers:
                    return "東証マザーズ";
                case MarketCode.TSE_Mothers_Foreign:
                    return "東証マザーズ外国";

                    

                //JQ
                case MarketCode.JQ:
                    return "JQ";
                case MarketCode.JQ_Growth:
                    return "JQグロース";
                case MarketCode.JQ_Standard:
                    return "JQスタンダード";
                case MarketCode.JQ_Standard_Foreign:
                    return "JQスタンダード外国";

                //SSE
                case MarketCode.SSE:
                    return "札証";
                case MarketCode.SSE_Ambitious:
                    return "札証アンビシャス";

                //FSE
                case MarketCode.FSE:
                    return "福証";
                case MarketCode.FSE_QBoard:
                    return "福証Q-Board";
            }

            throw new ArgumentException("無効なMarketCodeが指定されました。MarketCode:" + code);
        }



        /// <summary>
        /// 市場名称からMarketCodeを返します。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static MarketCode GetMarketCodeByMarketName(string marketName)
        {
            if (marketName == MarketCode.TSE.GetMarketName())
                return MarketCode.TSE;
            else if (marketName == MarketCode.TSE1.GetMarketName())
                return MarketCode.TSE1;
            else if (marketName == MarketCode.TSE1_Foreign.GetMarketName())
                return MarketCode.TSE1_Foreign;
            else if (marketName == MarketCode.TSE2.GetMarketName())
                return MarketCode.TSE2;
            else if (marketName == MarketCode.TSE2_Foreign.GetMarketName())
                return MarketCode.TSE2_Foreign;
            else if (marketName == MarketCode.TSE_TPM.GetMarketName())
                return MarketCode.TSE_TPM;
            else if (marketName == MarketCode.TSE_Mothers.GetMarketName())
                return MarketCode.TSE_Mothers;
            else if (marketName == MarketCode.TSE_Mothers_Foreign.GetMarketName())
                return MarketCode.TSE_Mothers_Foreign;

            else if (marketName == MarketCode.JQ.GetMarketName())
                return MarketCode.JQ;
            else if (marketName == MarketCode.JQ_Growth.GetMarketName())
                return MarketCode.JQ_Growth;
            else if (marketName == MarketCode.JQ_Standard.GetMarketName())
                return MarketCode.JQ_Standard;
            else if (marketName == MarketCode.JQ_Standard_Foreign.GetMarketName())
                return MarketCode.JQ_Standard_Foreign;

            else if (marketName == MarketCode.SSE.GetMarketName())
                return MarketCode.SSE;
            else if (marketName == MarketCode.SSE_Ambitious.GetMarketName())
                return MarketCode.SSE_Ambitious;

            else if (marketName == MarketCode.FSE.GetMarketName())
                return MarketCode.FSE;
            else if (marketName == MarketCode.FSE_QBoard.GetMarketName())
                return MarketCode.FSE_QBoard;

            throw new ArgumentException("無効なMarketNameが指定されました。MarketName:" + marketName);
        }
    }
}