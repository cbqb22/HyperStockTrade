using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace MIC.Common.Dialogs.Messaging
{
    /// <summary>
    /// ダイアログ表示要求メッセージです。
    /// </summary>
    public class ShowDialogMessage : MessageBase
    {
        /// <summary>
        /// ダイアログ識別トークン
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// パラメータ
        /// </summary>
        public object Parameter { get; set; }

        /// <summary>
        /// パラメータの型
        /// </summary>
        public Type ParameterType { get; set; }

        /// <summary>
        /// 返り値
        /// </summary>
        public ReturnValueHolder ReturnValue { get; set; }

        /// <summary>
        /// 完了時コールバック
        /// </summary>
        public Action<bool?> Callback { get; private set; }

        /// <summary>
        /// ダイアログ表示要求メッセージを生成します。
        /// </summary>
        /// <param name="sender">メッセージ送信元ViewModel</param>
        /// <param name="token">ダイアログ識別トークン</param>
        /// <param name="callback">完了時コールバック</param>
        public ShowDialogMessage(ViewModelBase sender, string token, Action<bool?> callback)
            : base(sender)
        {
            Token = token;
            Callback = callback;
        }
    }
}
