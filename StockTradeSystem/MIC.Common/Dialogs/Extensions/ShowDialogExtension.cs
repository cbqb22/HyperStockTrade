using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MIC.Common.Dialogs.Messaging;
using System.Threading.Tasks;

namespace MIC.Common.Dialogs.Extensions
{
    /// <summary>
    /// ViewModelに対してダイアログ表示要求メソッドを付加します。
    /// </summary>
    public static class ShowDialogExtension
    {
        /// <summary>
        /// ダイアログ表示要求メッセージを送信し、結果を待機します。
        /// </summary>
        /// <param name="vm">ViewModel</param>
        /// <param name="token">ダイアログ識別トークン</param>
        /// <returns>ダイアログ結果</returns>
        public static Task<bool?> RequestShowDialog(this ViewModelBase vm, string token)
        {
            var taskCompletionSource = new TaskCompletionSource<bool?>();
            Messenger.Default.Send(new ShowDialogMessage(vm, token, result => taskCompletionSource.TrySetResult(result)));
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// パラメータ付きのダイアログ表示要求メッセージを送信し、結果を待機します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="vm">ViewModel</param>
        /// <param name="token">ダイアログ識別トークン</param>
        /// <param name="parameter">パラメータ</param>
        /// <returns>ダイアログ結果</returns>
        public static Task<bool?> RequestShowDialog<T>(this ViewModelBase vm, string token, T parameter)
        {
            var taskCompletionSource = new TaskCompletionSource<bool?>();
            Messenger.Default.Send(new ShowDialogMessage(vm, token, result => taskCompletionSource.TrySetResult(result))
            {
                Parameter = parameter,
                ParameterType = typeof(T)
            });
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 特定の値を返すダイアログ表示要求メッセージを送信し、結果を待機します。
        /// </summary>
        /// <typeparam name="RT">ダイアログ結果の型</typeparam>
        /// <param name="vm">ViewModel</param>
        /// <param name="token">ダイアログ識別トークン</param>
        /// <returns>ダイアログ結果</returns>
        public static Task<RT> RequestShowDialog<RT>(this ViewModelBase vm, string token)
        {
            var taskCompletionSource = new TaskCompletionSource<RT>();
            var returnValue = new ReturnValueHolder();
            Messenger.Default.Send(new ShowDialogMessage(vm, token, result =>
                taskCompletionSource.TrySetResult((result ?? false) && returnValue.Value is RT ? (RT)returnValue.Value : default(RT)))
            {
                ReturnValue = returnValue
            });
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 特定の値を返すパラメータ付きのダイアログ表示要求メッセージを送信し、結果を待機します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <typeparam name="RT">ダイアログ結果の型</typeparam>
        /// <param name="vm">ViewModel</param>
        /// <param name="token">ダイアログ識別トークン</param>
        /// <param name="parameter">パラメータ</param>
        /// <returns>ダイアログ結果</returns>
        public static Task<RT> RequestShowDialog<T, RT>(this ViewModelBase vm, string token, T parameter)
        {
            var taskCompletionSource = new TaskCompletionSource<RT>();
            var returnValue = new ReturnValueHolder();
            Messenger.Default.Send(new ShowDialogMessage(vm, token, result =>
                taskCompletionSource.TrySetResult((result ?? false) && returnValue.Value is RT ? (RT)returnValue.Value : default(RT)))
            {
                Parameter = parameter,
                ParameterType = typeof(T),
                ReturnValue = returnValue
            });
            return taskCompletionSource.Task;
        }
    }
}
