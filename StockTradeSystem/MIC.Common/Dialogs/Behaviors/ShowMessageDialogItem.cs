using System.Windows;

namespace MIC.Common.Dialogs.Behaviors
{
    /// <summary>
    /// 表示対象のメッセージダイアログ情報を保持します。
    /// </summary>
    public class ShowMessageDialogItem : AbstractShowDialogItem
    {
        /// <summary>
        /// 対象ダイアログに表示されるボタンを指定します。
        /// </summary>
        public MessageBoxButton Button
        {
            get { return (MessageBoxButton)GetValue(ButtonProperty); }
            set { SetValue(ButtonProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログに表示されるボタンを指定します。
        /// </summary>
        public static readonly DependencyProperty ButtonProperty =
            DependencyProperty.Register("Button", typeof(MessageBoxButton), typeof(ShowMessageDialogItem), new PropertyMetadata(MessageBoxButton.OK));

        /// <summary>
        /// 対象ダイアログに表示されるアイコンを指定します。
        /// </summary>
        public MessageBoxImage Image
        {
            get { return (MessageBoxImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログに表示されるアイコンを指定します。
        /// </summary>
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(MessageBoxImage), typeof(ShowMessageDialogItem), new PropertyMetadata(MessageBoxImage.None));

        /// <summary>
        /// 対象ダイアログに表示されるメッセージテキストを設定します。
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログに表示されるメッセージテキストを設定します。
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ShowMessageDialogItem), new PropertyMetadata(string.Empty));
        
        /// <summary>
        /// 対象ダイアログのデフォルト結果を設定します。
        /// </summary>
        public MessageBoxResult DefaultResult
        {
            get { return (MessageBoxResult)GetValue(DefaultResultProperty); }
            set { SetValue(DefaultResultProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログのデフォルト結果を設定します。
        /// </summary>
        public static readonly DependencyProperty DefaultResultProperty =
            DependencyProperty.Register("DefaultResult", typeof(MessageBoxResult), typeof(ShowMessageDialogItem), new PropertyMetadata(MessageBoxResult.None));

        /// <summary>
        /// 対象ダイアログのタイトルキャプションを設定します。
        /// </summary>
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログのタイトルキャプションを設定します。
        /// </summary>
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(ShowMessageDialogItem), new PropertyMetadata(string.Empty));

    }
}
