namespace MIC.Database.Commons
{
    /// <summary>
    /// 列サイズ定義
    /// </summary>
    public class ColumnMaxLength
    {
        /// <summary>ファイル名の長さ</summary>
        public const int FilePathLength = 260;

        /// <summary>コンピューター名</summary>
        public const int ComputerNameLength = 63;

        /// <summary>フィールドの長さ</summary>
        public const int FieldLength = 256;

        /// <summary>フィールド名の長さ</summary>
        public const int FieldNameLength = 50;

        /// <summary>フィールド表示名の長さ</summary>
        public const int FieldDisplayNameLength = 50;

        /// <summary>SqlSequenceの長さ</summary>
        public const int SqlSequenceLength = 30;

        /// <summary>SqlFileNameLengthの長さ</summary>
        public const int SqlFileNameLength = 30;

        /// <summary>VersionLengthの長さ</summary>
        public const int VersionLength = 30;

        /// <summary>StockCodeLengthの長さ</summary>
        public const int StockCodeLength = 10;
    }
}