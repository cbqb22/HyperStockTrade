using System.Windows;

namespace MIC.Common.Dialogs.Behaviors
{
    /// <summary>
    /// 色ダイアログ情報を保持します。
    /// </summary>
    public class ShowColorDialogItem : AbstractShowDialogItem
    {
        /// <summary>
        /// カスタムカラー作成用コントロールの表示有無を設定します。
        /// </summary>
        public static readonly DependencyProperty FullOpenProperty =
            DependencyProperty.Register("FullOpen", typeof(bool), typeof(ShowColorDialogItem), new PropertyMetadata(false));

        /// <summary>
        /// カスタムカラー作成用コントロールの表示有無を設定します。
        /// </summary>
        public bool FullOpen
        {
            get { return (bool)GetValue(FullOpenProperty); }
            set { SetValue(FullOpenProperty, value); }
        }
    }
}