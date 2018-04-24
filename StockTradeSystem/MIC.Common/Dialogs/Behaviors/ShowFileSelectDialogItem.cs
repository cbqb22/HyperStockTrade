using System.Windows;

namespace MIC.Common.Dialogs.Behaviors
{
    /// <summary>
    /// ファイル選択ダイアログ情報を保持します。
    /// </summary>
    public class ShowFileSelectDialogItem : AbstractShowDialogItem
    {
        /// <summary>
        /// 対象ダイアログに表示されるタイトルテキストを設定します。
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ShowFileSelectDialogItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 対象ダイアログに表示されるタイトルテキストを設定します。
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログの初期ディレクトリを設定します。
        /// </summary>
        public static readonly DependencyProperty InitialDirectoryProperty =
            DependencyProperty.Register("InitialDirectory", typeof(string), typeof(ShowFileSelectDialogItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 対象ダイアログの初期ディレクトリを設定します。
        /// </summary>
        public string InitialDirectory
        {
            get { return (string)GetValue(InitialDirectoryProperty); }
            set { SetValue(InitialDirectoryProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログのフィルタ文字列を設定します。
        /// </summary>
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(ShowFileSelectDialogItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 対象ダイアログのフィルタ文字列を設定します。
        /// </summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログの選択されているフィルタを設定します。
        /// </summary>
        public static readonly DependencyProperty FilterIndexProperty =
            DependencyProperty.Register("FilterIndex", typeof(int), typeof(ShowFileSelectDialogItem), new PropertyMetadata(0));

        /// <summary>
        /// 対象ダイアログの選択されているフィルタを設定します。
        /// </summary>
        public int FilterIndex
        {
            get { return (int)GetValue(FilterIndexProperty); }
            set { SetValue(FilterIndexProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログが複数選択可能かを設定します。
        /// </summary>
        public static readonly DependencyProperty MultiSelectProperty =
            DependencyProperty.Register("Multiselect", typeof(bool), typeof(ShowFileSelectDialogItem), new PropertyMetadata(false));

        /// <summary>
        /// 対象ダイアログが複数選択可能かを設定します。
        /// </summary>
        public bool Multiselect
        {
            get { return (bool)GetValue(MultiSelectProperty); }
            set { SetValue(MultiSelectProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログで選択されたファイルのパスを設定します。
        /// </summary>
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(ShowFileSelectDialogItem),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 対象ダイアログで選択されたファイルのパスを設定します。
        /// </summary>
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログで選択されたファイルのパスを設定します。
        /// </summary>
        public static readonly DependencyProperty FileNamesProperty =
            DependencyProperty.Register("FileNames", typeof(string[]), typeof(ShowFileSelectDialogItem),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 対象ダイアログで選択されたファイルのパスを設定します。
        /// </summary>
        public string[] FileNames
        {
            get { return (string[])GetValue(FileNamesProperty); }
            set { SetValue(FileNamesProperty, value); }
        }

        /// <summary>
        /// 開くダイアログか保存ダイアログかを設定します。
        /// </summary>
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(DialogMode), typeof(ShowFileSelectDialogItem), new PropertyMetadata(DialogMode.Open));

        /// <summary>
        /// 開くダイアログか保存ダイアログかを設定します。
        /// </summary>
        public DialogMode Mode
        {
            get { return (DialogMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        /// <summary>
        /// 保存ダイアログの上書きチェック有効かを設定します。
        /// </summary>
        public static readonly DependencyProperty SaveModeOverwriteCheckProperty =
            DependencyProperty.Register("SaveModeOverwriteCheck", typeof(bool), typeof(ShowFileSelectDialogItem), new PropertyMetadata(true));

        /// <summary>
        /// 保存ダイアログの上書きチェック有効かを設定します。
        /// </summary>
        public bool SaveModeOverwriteCheck
        {
            get { return (bool)GetValue(SaveModeOverwriteCheckProperty); }
            set { SetValue(SaveModeOverwriteCheckProperty, value); }
        }

        /// <summary>
        /// ダイアログモード
        /// </summary>
        public enum DialogMode
        {
            /// <summary>
            /// 開く
            /// </summary>
            Open,

            /// <summary>
            /// 保存
            /// </summary>
            Save,

            /// <summary>
            /// フォルダを開く
            /// </summary>
            OpenFolder
        }
    }
}