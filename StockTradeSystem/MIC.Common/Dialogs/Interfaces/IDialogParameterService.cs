using System;

namespace MIC.Common.Dialogs.Interfaces
{
    /// <summary>
    /// ダイアログ表示要求にてパラメータをやりとりするサービスのインターフェースです。
    /// </summary>
    public interface IDialogParameterService
    {
        /// <summary>
        /// ダイアログ表示要求で渡されたパラメータを取得します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="receiver">表示要求されたダイアログのViewModel</param>
        /// <returns>パラメータ</returns>
        T GetParameter<T>(object receiver);

        /// <summary>
        /// ダイアログ表示要求で渡されたパラメータを設定します。
        /// </summary>
        /// <param name="receiver">表示要求されたダイアログのViewModel</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="parameterType">パラメータの型</param>
        void SetParameter(object receiver, object parameter, Type parameterType);

        /// <summary>
        /// ダイアログ表示要求で渡されたパラメータを削除します。
        /// </summary>
        /// <param name="receiver">表示要求されたダイアログのViewModel</param>
        void RemoveParameter(object receiver);
    }
}
