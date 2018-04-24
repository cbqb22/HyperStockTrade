using System.Windows;

namespace MIC.Common.Dialogs.Behaviors
{
    /// <summary>
    /// ダイアログ情報の抽象クラスです。
    /// </summary>
    public abstract class AbstractShowDialogItem : DependencyObject
    {
        /// <summary>
        /// 対象ダイアログを識別するトークンです。呼び出し元で渡すトークンと一致するダイアログが表示されます。
        /// </summary>
        public string Token
        {
            get { return (string)GetValue(TokenProperty); }
            set { SetValue(TokenProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログを識別するトークンです。呼び出し元で渡すトークンと一致するダイアログが表示されます。
        /// </summary>
        public static readonly DependencyProperty TokenProperty =
            DependencyProperty.Register("Token", typeof(string), typeof(ShowMessageDialogItem), new PropertyMetadata(string.Empty));
    }
}
