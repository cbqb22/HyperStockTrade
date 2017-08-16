using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Database.Commons.Enums
{
    public enum MarketCodes : byte
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
        FSE_QBoard = 15
    }
}
