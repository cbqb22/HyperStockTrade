using System;
using System.Windows;

namespace MIC.Common.Dialogs.Behaviors
{
    /// <summary>
    /// 表示対象のカスタムダイアログ情報を保持します。
    /// </summary>
    public class ShowCustomDialogItem : AbstractShowDialogItem
    {
        /// <summary>
        /// 対象ダイアログの型を保持します。ダイアログはデフォルトのコンストラクタを使用してインスタンス生成されます。
        /// </summary>
        public Type Dialog
        {
            get { return (Type)GetValue(DialogProperty); }
            set { SetValue(DialogProperty, value); }
        }

        /// <summary>
        /// 対象ダイアログの型を保持します。ダイアログはデフォルトのコンストラクタを使用してインスタンス生成されます。
        /// </summary>
        public static readonly DependencyProperty DialogProperty =
            DependencyProperty.Register("Dialog", typeof(Type), typeof(ShowCustomDialogItem), new PropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        public object DataContext
        {
            get { return (object)GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.Register("DataContext", typeof(object), typeof(ShowCustomDialogItem), new PropertyMetadata(null));

        /// <summary>
        /// ウィンドウモード。trueのときダイアログはモードレスで開きます
        /// </summary>
        public bool IsModeless
        {
            get { return (bool)GetValue(IsModelessProperty); }
            set { SetValue(IsModelessProperty, value); }
        }

        /// <summary>
        /// ウィンドウモード。trueのときダイアログはモードレスで開きます
        /// </summary>
        public static readonly DependencyProperty IsModelessProperty =
            DependencyProperty.Register("IsModeless", typeof(bool), typeof(ShowCustomDialogItem), new PropertyMetadata(false));

        /// <summary>
        /// タスクバーに表示するかを設定します
        /// </summary>
        public bool ShowInTaskbar
        {
            get { return (bool)GetValue(ShowInTaskbarProperty); }
            set { SetValue(ShowInTaskbarProperty, value); }
        }

        /// <summary>
        /// タスクバーに表示するかを設定します
        /// </summary>
        public static readonly DependencyProperty ShowInTaskbarProperty =
            DependencyProperty.Register("ShowInTaskbar", typeof(bool), typeof(ShowCustomDialogItem), new PropertyMetadata(false));

        /// <summary>
        /// 他にモーダルのダイアログが開かれたときに閉じるかどうか。モードレスダイアログの場合のみ設定可能。
        /// </summary>
        public bool CloseOnModalDialogOpen
        {
            get { return (bool)GetValue(CloseOnModalDialogOpenProperty); }
            set { SetValue(CloseOnModalDialogOpenProperty, value); }
        }

        /// <summary>
        /// 他にモーダルのダイアログが開かれたときに閉じるかどうか。モードレスダイアログの場合のみ設定可能。
        /// </summary>
        public static readonly DependencyProperty CloseOnModalDialogOpenProperty =
            DependencyProperty.Register("CloseOnModalDialogOpen", typeof(bool), typeof(ShowColorDialogItem), new PropertyMetadata(false));


    }
}
