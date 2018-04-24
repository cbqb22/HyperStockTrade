using CommonServiceLocator;
using GalaSoft.MvvmLight.Messaging;
using MIC.Common.Dialogs.Interfaces;
using MIC.Common.Dialogs.Messaging;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace MIC.Common.Dialogs.Behaviors
{
    /// <summary>
    /// ダイアログ表示機能のBehaviorです。
    /// </summary>
    [ContentProperty("ShowDialogItems")]
    public class ShowDialogBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// 表示対象となるダイアログ情報の一覧を保持します。
        /// </summary>
        public ShowDialogItemCollection ShowDialogItems
        {
            get { return (ShowDialogItemCollection)GetValue(ShowDialogItemsProperty); }
            set { SetValue(ShowDialogItemsProperty, value); }
        }

        /// <summary>
        /// 表示対象となるダイアログ情報の一覧を保持します。
        /// </summary>
        public static readonly DependencyProperty ShowDialogItemsProperty =
            DependencyProperty.Register("ShowDialogItems", typeof(ShowDialogItemCollection), typeof(ShowDialogBehavior), new PropertyMetadata(null));

        /// <summary>
        /// ビヘイビア登録時処理
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
        }

        /// <summary>
        /// ビヘイビア解除時処理
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
        }

        /// <summary>
        /// ビヘイビア登録先のloaded時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<ShowDialogMessage>(AssociatedObject, ShowDialog);
        }

        /// <summary>
        /// ビヘイビア登録先のunloaded時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<ShowDialogMessage>(AssociatedObject, ShowDialog);
        }

        /// <summary>
        /// ViewModelからのメッセージを受け取り、該当するダイアログの表示を行います。
        /// </summary>
        /// <param name="message"></param>
        protected void ShowDialog(ShowDialogMessage message)
        {
            if (AssociatedObject.DataContext == message.Sender)
            {
                var dialogItem = ShowDialogItems.FirstOrDefault(item => item.Token == message.Token);
                var messageDialogItem = dialogItem as ShowMessageDialogItem;
                var customDialogItem = dialogItem as ShowCustomDialogItem;
                var fileDialogItem = dialogItem as ShowFileSelectDialogItem;
                var printDialogItem = dialogItem as ShowPrintDialogItem;
                var colorDialogItem = dialogItem as ShowColorDialogItem;

                if (messageDialogItem != null)
                {
                    MessageBoxResult result;
                    var owner = AssociatedObject as Window ?? GetWindow(AssociatedObject);
                    if (owner != null)
                    {
                        result = MessageBox.Show(owner, message.Parameter as string ?? messageDialogItem.Text, messageDialogItem.Caption, messageDialogItem.Button, messageDialogItem.Image,
                            messageDialogItem.DefaultResult);
                    }
                    else
                    {
                        result = MessageBox.Show(message.Parameter as string ?? messageDialogItem.Text, messageDialogItem.Caption, messageDialogItem.Button, messageDialogItem.Image,
                            messageDialogItem.DefaultResult);
                    }

                    switch (result)
                    {
                        case MessageBoxResult.OK:
                        case MessageBoxResult.Yes:
                            message.Callback(true);
                            return;

                        case MessageBoxResult.No:
                            message.Callback(false);
                            return;

                        case MessageBoxResult.Cancel:
                            message.Callback(messageDialogItem.Button == MessageBoxButton.YesNoCancel ? (bool?)null : false);
                            return;

                        case MessageBoxResult.None:
                            message.Callback(null);
                            return;
                    }
                }
                else if (customDialogItem != null && typeof(Window).IsAssignableFrom(customDialogItem.Dialog))
                {
                    if (!customDialogItem.IsModeless)
                    {
                        var closeDialogs = ShowDialogItems.OfType<ShowCustomDialogItem>()
                            .Where(x => x.IsModeless && x.CloseOnModalDialogOpen)
                            .Select(x => x.Dialog)
                            .ToList();
                            //.ToHashSet();
                        foreach (var modeless in Application.Current.Windows.OfType<Window>()
                            .Where(x => closeDialogs.Contains(x.GetType())).ToArray())
                        {
                            modeless.Close();
                        }

                        var dialog = Activator.CreateInstance(customDialogItem.Dialog) as Window;
                        if (customDialogItem.DataContext != null) dialog.DataContext = customDialogItem.DataContext;
                        dialog.ShowInTaskbar = customDialogItem.ShowInTaskbar;
                        if (dialog.DataContext != null)
                        {
                            var service = ServiceLocator.Current.GetInstance<IDialogParameterService>();
                            service.SetParameter(dialog.DataContext, message.Parameter, message.ParameterType);
                            dialog.Owner = AssociatedObject as Window ?? GetWindow(AssociatedObject);
                            message.Callback(dialog.ShowDialog());
                            service.RemoveParameter(dialog.DataContext);
                        }
                        else
                        {
                            dialog.Owner = AssociatedObject as Window ?? GetWindow(AssociatedObject);
                            message.Callback(dialog.ShowDialog());
                        }
                        return;
                    }
                    else
                    {
                        // モードレス
                        if (!Application.Current.Windows.OfType<Window>().Any(x => x.GetType() == customDialogItem.Dialog))
                        {
                            var dialog = Activator.CreateInstance(customDialogItem.Dialog) as Window;
                            if (dialog.DataContext != null)
                            {
                                var service = ServiceLocator.Current.GetInstance<IDialogParameterService>();
                                service.SetParameter(dialog.DataContext, message.Parameter, message.ParameterType);
                                dialog.Closed += (_, __) => service.RemoveParameter(dialog.DataContext);
                                dialog.ShowInTaskbar = customDialogItem.ShowInTaskbar;
                                if (!dialog.ShowInTaskbar)
                                {
                                    dialog.Owner = AssociatedObject as Window ?? GetWindow(AssociatedObject);
                                }
                                dialog.Show();
                                message.Callback(null);
                            }
                            else
                            {
                                dialog.ShowInTaskbar = customDialogItem.ShowInTaskbar;
                                if (!dialog.ShowInTaskbar)
                                {
                                    dialog.Owner = AssociatedObject as Window ?? GetWindow(AssociatedObject);
                                }
                                dialog.Show();
                                message.Callback(null);
                            }
                            return;
                        }
                    }
                }
                else if (fileDialogItem != null)
                {
                    if (fileDialogItem.Mode == ShowFileSelectDialogItem.DialogMode.Open)
                    {
                        var dialog = new OpenFileDialog
                        {
                            Title = fileDialogItem.Title,
                            InitialDirectory = fileDialogItem.InitialDirectory,
                            Filter = fileDialogItem.Filter,
                            FilterIndex = fileDialogItem.FilterIndex,
                            Multiselect = fileDialogItem.Multiselect
                        };

                        var result = dialog.ShowDialog();
                        if (result != true)
                        {
                            fileDialogItem.FileName = string.Empty;
                            fileDialogItem.FileNames = null;
                            message.Callback(null);
                            return;
                        }

                        if (dialog.Multiselect)
                        {
                            fileDialogItem.FileName = string.Empty;
                            fileDialogItem.FileNames = dialog.FileNames;
                            message.ReturnValue.Value = dialog.FileNames;
                        }
                        else
                        {
                            fileDialogItem.FileName = dialog.FileName;
                            fileDialogItem.FileNames = null;
                            message.ReturnValue.Value = dialog.FileName;
                        }
                        message.Callback(true);
                    }
                    else if (fileDialogItem.Mode == ShowFileSelectDialogItem.DialogMode.Save)
                    {
                        var dialog = new SaveFileDialog
                        {
                            Title = fileDialogItem.Title,
                            InitialDirectory = fileDialogItem.InitialDirectory,
                            Filter = fileDialogItem.Filter,
                            FilterIndex = fileDialogItem.FilterIndex,
                            OverwritePrompt = fileDialogItem.SaveModeOverwriteCheck
                        };

                        if (!(dialog.ShowDialog() ?? false))
                        {
                            fileDialogItem.FileName = string.Empty;
                            fileDialogItem.FileNames = null;
                            message.Callback(null);
                            return;
                        }

                        fileDialogItem.FileName = dialog.FileName;
                        fileDialogItem.FileNames = null;
                        message.ReturnValue.Value = dialog.FileName;
                        message.Callback(true);
                    }
                    else if (fileDialogItem.Mode == ShowFileSelectDialogItem.DialogMode.OpenFolder)
                    {
                        var dialog = new CommonOpenFileDialog
                        {
                            Title = fileDialogItem.Title,
                            InitialDirectory = fileDialogItem.InitialDirectory,
                            Multiselect = fileDialogItem.Multiselect,
                            IsFolderPicker = true,
                            AddToMostRecentlyUsedList = false
                        };

                        var mainWindow = Application.Current.MainWindow;
                        try
                        {
                            Application.Current.MainWindow = AssociatedObject as Window ?? GetWindow(AssociatedObject);

                            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
                            {
                                fileDialogItem.FileName = string.Empty;
                                fileDialogItem.FileNames = null;
                                message.Callback(null);
                                return;
                            }
                            if (dialog.Multiselect)
                            {
                                fileDialogItem.FileName = string.Empty;
                                fileDialogItem.FileNames = dialog.FileNames.ToArray();
                                message.ReturnValue.Value = dialog.FileNames;
                            }
                            else
                            {
                                fileDialogItem.FileName = dialog.FileName;
                                fileDialogItem.FileNames = null;
                                message.ReturnValue.Value = dialog.FileName;
                            }
                            message.Callback(true);
                        }
                        finally
                        {
                            Application.Current.MainWindow = mainWindow;
                        }
                    }
                    return;
                }
                else if (printDialogItem != null)
                {
                    // 印刷ダイアログ
                    var dialog = new PrintDialog();

                    // 印刷ダイアログはMainWindowに対してモーダルとなるので、MainWindowを一時的に差し替える
                    var mainWindow = Application.Current.MainWindow;

                    var dialogResult = false;
                    try
                    {
                        Application.Current.MainWindow = AssociatedObject as Window ?? GetWindow(AssociatedObject);
                        dialogResult = dialog.ShowDialog() ?? false;
                    }
                    finally
                    {
                        Application.Current.MainWindow = mainWindow;
                    }

                    if (dialogResult)
                    {
                        message.ReturnValue.Value = dialog;
                        message.Callback(true);
                    }
                    else
                    {
                        message.ReturnValue.Value = null;
                        message.Callback(null);
                    }
                    return;
                }
                else if (colorDialogItem != null)
                {
                    // カラーダイアログ
                    var dialog = new System.Windows.Forms.ColorDialog
                    {
                        FullOpen = colorDialogItem.FullOpen,
                        Color = message.Parameter is Color ? (Color)message.Parameter : Color.White
                    };

                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        message.ReturnValue.Value = dialog.Color;
                        message.Callback(true);
                    }
                    else
                    {
                        message.ReturnValue.Value = null;
                        message.Callback(null);
                    }
                    return;
                }
                message.Callback(null);
            }
            //message.Callback(null);
        }

        /// <summary>
        /// 指定された依存関係オブジェクトが属するウィンドウを取得します。
        /// </summary>
        /// <param name="obj">依存関係オブジェクト</param>
        /// <returns>ウィンドウ</returns>
        protected Window GetWindow(DependencyObject obj)
        {
            var parent = GetParent(obj);

            return parent != null ? parent as Window ?? GetWindow(parent) : null;
        }

        /// <summary>
        /// 指定された依存関係オブジェクトの親オブジェクトを取得します。
        /// </summary>
        /// <param name="obj">子の依存関係オブジェクト</param>
        /// <returns>親の依存関係オブジェクト</returns>
        protected DependencyObject GetParent(DependencyObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            var contentElement = obj as ContentElement;
            if (contentElement != null)
            {
                return ContentOperations.GetParent(contentElement) ?? (obj is FrameworkContentElement ? ((FrameworkContentElement)obj).Parent : null);
            }

            var frameworkElement = obj as FrameworkElement;
            if (frameworkElement != null)
            {
                return frameworkElement.Parent ?? VisualTreeHelper.GetParent(frameworkElement);
            }

            return VisualTreeHelper.GetParent(obj);
        }

        /// <summary>
        /// ダイアログ表示機能のBehaviorです。
        /// </summary>
        public ShowDialogBehavior()
        {
            ShowDialogItems = new ShowDialogItemCollection();
        }
    }
}