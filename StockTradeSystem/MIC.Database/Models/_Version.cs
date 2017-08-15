using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MIC.Database.Commons;

namespace MIC.Database.Models
{
    /// <summary>
    /// 名　　称 ： 着信データ
    /// カテゴリ ： データ（利用者向け）
    /// システム ： 受付処理、ベリファイヤー
    /// </summary>
    public class _Version
    {
        private const string IndexName = "_VersionIndex";

        /// <summary>
        /// ID
        /// </summary>
        public int _VersionId { get; set; }

        /// <summary>
        /// シーケンス番号
        /// </summary>
        [Required,
         Index(IndexName, IsUnique = true),
         MaxLength(ColumnMaxLength.SqlSequenceLength)]
        public string Sequence { get; set; }

        /// <summary>
        /// ファイル名
        /// </summary>
        [Required(AllowEmptyStrings = true),
         MaxLength(ColumnMaxLength.SqlFileNameLength)]
        public string FileName { get; set; }

        /// <summary>
        /// バージョン名
        /// </summary>
        [Required(AllowEmptyStrings = true),
         MaxLength(ColumnMaxLength.VersionLength)]
        public string Version { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public _Version()
        {
            FileName = string.Empty;
        }
    }
}
