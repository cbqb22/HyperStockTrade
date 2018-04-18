using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MIC.Common.Commands.Interfaces
{
    /// <summary>
    /// 非同期処理をawaitできるコマンドのインターフェースです。
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// コマンドをawait可能な非同期で実行します。
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task ExecuteAsync(object parameter);
    }
}
