using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace MIC.Common.ExtensionMethods
{
    /// <summary>
    /// stringに対する拡張メソッド郡
    /// </summary>
    public static class StringExtension
    {

        /// <summary>
        /// この文字列の左側から指定した文字数分、文字を取り出します。
        /// 文字数が足りない場合は、文字列をそのまま返します。
        /// </summary>
        /// <param name="text">元となる文字列</param>
        /// <param name="count">抽出する文字数</param>
        /// <returns>抽出された文字列</returns>
        public static string Left(this string text, int count) { return SubstringIfPossible(text, 0, count); }

        /// <summary>
        /// この文字列の左側から指定した文字数分、文字を取り出します。
        /// 文字数が足りない場合は、文字列をそのまま返します。
        /// </summary>
        /// <param name="text">元となる文字列</param>
        /// <param name="startIndex">開始位置</param>
        /// <param name="count">抽出する文字数</param>
        /// <returns>抽出された文字列</returns>
        public static string Left(this string text, int startIndex, int count) { return SubstringIfPossible(text, startIndex, count); }

        /// <summary>
        /// この文字列の右側から指定した文字数分、文字を取り出します。
        /// 文字数が足りない場合は、文字列をそのまま返します。
        /// </summary>
        /// <param name="text">元となる文字列</param>
        /// <param name="count">抽出する文字数</param>
        /// <returns>抽出された文字列</returns>
        public static string Right(this string text, int count) { return SubstringIfPossible(text, text.Length - count, count); }

        /// <summary>
        /// この文字列の右側から指定した文字数分、文字を取り出します。
        /// 文字数が足りない場合は、文字列をそのまま返します。
        /// </summary>
        /// <param name="text">元となる文字列</param>
        /// <param name="startIndex">開始位置</param>
        /// <param name="count">抽出する文字数</param>
        /// <returns>抽出された文字列</returns>
        public static string Right(this string text, int startIndex, int count) { return SubstringIfPossible(text, startIndex, count); }

        /// <summary>
        /// この文字列の指定した範囲の文字を取り出します。
        /// 文字数が範囲外になる場合は、含まれる文字だけを返し、例外は発生させません。
        /// </summary>
        /// <param name="text">元となる文字列</param>
        /// <param name="startIndex">開始位置</param>
        /// <param name="count">抽出する文字数</param>
        /// <returns>抽出された文字列</returns>
        public static string SubstringIfPossible(this string text, int startIndex, int count)
        {
            if (text == null) return null;
            if (text.Length <= startIndex) return string.Empty;

            var remainCharCount = text.Length - startIndex;

            return text.Substring(startIndex, (remainCharCount <= count ? remainCharCount : count));
        }

        /// <summary>
        /// 文字列をGuidに変換します。変換できない場合Guid.Emptyが返されます。
        /// </summary>
        /// <param name="text">元となる文字列</param>
        /// <returns>変換結果</returns>        
        public static Guid ToGuid(this string text)
        {
            Guid resultGuid;
            Guid.TryParse(text, out resultGuid);
            return resultGuid;
        }

        /// <summary>
        /// XMLドキュメントをクラスに変換します。
        /// </summary>
        /// <param name="xmlDocumentText">XML文字列</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T ParseXml<T>(this string xmlDocumentText) where T : class, new()
        {
            // 空ならクラスを生成して返す
            if (string.IsNullOrEmpty(xmlDocumentText)) return new T();

            using (var reader = new StringReader(xmlDocumentText))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return xmlSerializer.Deserialize(reader) as T;
            }
        }

        /// <summary>
        /// XMLファイルをクラスに変換します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">ファイルパス</param>
        /// <returns></returns>
        public static T DeserializeXmlFile<T>(this string filePath) where T : class, new()
        {
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var xmlReader = XmlReader.Create(fileStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    return xmlSerializer.Deserialize(xmlReader) as T;
                }
            }
            return null;
        }

        /// <summary>
        /// クラスをXMLファイルに変換します。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filePath">ファイルパス</param>
        public static void SerializeXmlFile<T>(this T obj, string filePath) where T : class
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());
            using (var streamWriter = new StreamWriter(filePath))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                xmlSerializer.Serialize(streamWriter, obj, ns);
            }
        }

        /// <summary>
        /// 指定した区切り文字で結合した文字列を返します
        /// </summary>
        /// <param name="values">文字列の配列</param>
        /// <param name="separator">区切り文字</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }

        public static bool Contains(this string text, string str2, StringComparison comp)
        {
            var options = comp == StringComparison.CurrentCulture ? RegexOptions.None : RegexOptions.IgnoreCase;

            // OR検索
            //return OrSearch(text, str2, options)
            // AND検索
            return AndSearch(text, str2, options);
        }

        /// <summary>
        /// スペース区切りでAnd検索
        /// </summary>
        /// <param name="text"></param>
        /// <param name="str2"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static bool AndSearch(string text, string str2, RegexOptions options)
        {
            var spl = string.Join(string.Empty, str2.SplitSpaces().Select(s => "(?=.*" + Regex.Escape(s.Trim()) + ")")) + ".*";
            return Regex.IsMatch(text, spl, options);
        }

        private static IEnumerable<string> SplitSpaces(this string str)
        {
            return str.Split(new[] { " ", "　" }, StringSplitOptions.RemoveEmptyEntries);
        }

        ///// <summary>
        ///// スペース区切りでOR検索
        ///// </summary>
        ///// <param name="text"></param>
        ///// <param name="str2"></param>
        ///// <param name="options"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //private static bool OrSearch(string text, string str2, RegexOptions options)
        //{
        //    var spl = "(" + String.Join("|", str2.SplitSpaces().Select(s => Regex.Escape(s.Trim()))) + ")";
        //    return Regex.IsMatch(text, spl, options);
        //}

        /// <summary>
        /// 半角の0～9ならTrue
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsHalfNum(this string text)
        {
            return text.ToCharArray().All(c => '0' <= c && c <= '9');
        }

        /// <summary>
        /// 半角のa～z、A～ZならTrue
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsHalfAlpha(this string text)
        {
            return text.ToCharArray().All(c => ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'));
        }

        /// <summary>
        /// 半角の0～9、a～z、A～ZならTrue
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsHalfAlNum(this string text)
        {
            return text.ToCharArray().All(c => ('0' <= c && c <= '9') || ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'));
        }

        /// <summary><![CDATA[
        /// 半角ならTrue
        /// '\\u0020-\\u007E' -> [ !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~]
        /// '\\uFF66-\\uFF9F' -> [ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ]
        /// ]]>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsHalfOnly(this string text)
        {
            // 半角の判定を正規表現で行います。半角カタカナは「ｦ」～半濁点を半角とみなします
            return new Regex("^[\u0020-\u007E\uFF66-\uFF9F]+$").IsMatch(text);
        }

        /// <summary>
        /// 全角ならTrue（つまり、半角でなければTrue）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsFullOnly(this string text)
        {
            return !Regex.IsMatch(text, "[\u0020-\u007E\uFF66-\uFF9F]+");
        }

        /// <summary>
        /// int変換 null許容
        /// 変換に失敗するとnullになります。
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int? ToNullableInt(this string number)
        {
            int i;
            return int.TryParse(number, out i) ? i : (int?)null;
        }

        /// <summary>
        /// DateTime変換 null許容
        /// 変換に失敗するとnullになります。
        /// </summary>
        /// <param name="dateString">日付の文字列</param>
        /// <returns>DateTime</returns>
        public static DateTime? ToNullableDateTime(this string dateString)
        {
            DateTime dateTime;
            return DateTime.TryParse(dateString, out dateTime) ? dateTime : (DateTime?)null;
        }

        /// <summary>
        /// nullでもstring.Emptyでもない最初のstringを取得します。
        /// なかった場合はstring.Emptyを返します。
        /// </summary>
        /// <param name="strings">stringの配列</param>
        /// <returns>最初のnullでもEmptyでもない値</returns>
        public static string TryFirstNonEmpty(this string[] strings)
        {
            return strings.FirstOrDefault(s => !string.IsNullOrEmpty(s)) ?? string.Empty;
        }

        /// <summary>
        /// 8 ビット符号なし整数配列の値を、Base64 の数字でエンコードされた等価の文字列形式に変換します。
        /// </summary>
        /// <param name="inArray">8 ビット符号なし整数の配列。</param>
        /// <returns>inArray の内容の Base64 形式での文字列形式。</returns>
        public static string ToBase64String(this byte[] inArray)
        {
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// 指定した文字列を変換します。これにより、バイナリ データは Base64 の数字として等価の 8 ビット符号なし整数配列にエンコードされます。
        /// </summary>
        /// <param name="s">変換する文字列。</param>
        /// <returns>s と等価な 8 ビット符号なし整数の配列。</returns>
        public static byte[] FromBase64String(this string s)
        {
            return Convert.FromBase64String(s);
        }

        /// <summary>
        /// バイトシーケンスに変換します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string s, Encoding encoding)
        {
            return encoding.GetBytes(s);
        }

        /// <summary>
        /// バイト配列を文字列にデコードします
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
    }
}
