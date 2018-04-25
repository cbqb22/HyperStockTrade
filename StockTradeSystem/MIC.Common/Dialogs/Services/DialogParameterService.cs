using MIC.Common.Dialogs.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MIC.Common.Dialogs.Services
{
    /// <summary>
    /// ダイアログ表示要求にてパラメータをやりとりするサービスです。
    /// </summary>
    public class DialogParameterService : IDialogParameterService
    {
        private ConcurrentDictionary<object, KeyValuePair<Type, object>> _parameters = new ConcurrentDictionary<object, KeyValuePair<Type, object>>();

        /// <summary>
        /// ダイアログ表示要求で渡されたパラメータを取得します。
        /// </summary>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <param name="receiver">表示要求されたダイアログのViewModel</param>
        /// <returns>パラメータ</returns>
        public T GetParameter<T>(object receiver)
        {
            KeyValuePair<Type, object> pair;
            if (_parameters.TryGetValue(receiver, out pair))
            {
                if (typeof(T).IsAssignableFrom(pair.Key))
                {
                    return (T)pair.Value;
                }
            }
            return default(T);

        }

        /// <summary>
        /// ダイアログ表示要求で渡されたパラメータを設定します。
        /// </summary>
        /// <param name="receiver">表示要求されたダイアログのViewModel</param>
        /// <param name="parameter">パラメータ</param>
        /// <param name="parameterType">パラメータの型</param>
        public void SetParameter(object receiver, object parameter, Type parameterType)
        {
            _parameters[receiver] = new KeyValuePair<Type, object>(parameterType, parameter);
        }

        /// <summary>
        /// ダイアログ表示要求で渡されたパラメータを削除します。
        /// </summary>
        /// <param name="receiver">表示要求されたダイアログのViewModel</param>
        public void RemoveParameter(object receiver)
        {
            KeyValuePair<Type, object> result;
            _parameters.TryRemove(receiver, out result);
        }
    }
}
