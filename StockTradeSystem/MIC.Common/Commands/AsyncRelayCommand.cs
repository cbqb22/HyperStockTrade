using GalaSoft.MvvmLight.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MIC.Common.Commands.Interfaces
{
    /// <summary>
    /// 非同期処理をawaitできるコマンドです。
    /// </summary>
    public class AsyncRelayCommand : IAsyncCommand
    {
        private readonly WeakFunc<Task> _execute;
        private readonly WeakFunc<bool> _canExecute;

        /// <summary>
        /// CanExecuteの状態変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// コマンドが実行可能かどうかを返します。
        /// </summary>
        /// <param name="parameter">コマンドパラメータ</param>
        /// <returns>実行可能ならtrue</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null
                || (_canExecute.IsStatic || _canExecute.IsAlive)
                    && _canExecute.Execute();
        }

        /// <summary>
        /// コマンドをawait可能な非同期で実行します。
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter)
                && _execute != null
                && (_execute.IsStatic || _execute.IsAlive))
            {
                await _execute.Execute();
            }
        }

        /// <summary>
        /// コマンドを実行します。
        /// XAMLへのバインドではなく、コードから直接実行する場合はExecuteAsyncを使用してください。
        /// </summary>
        /// <param name="parameter"></param>
        public async void Execute(object parameter)
        {
            if (CanExecute(parameter)
                && _execute != null
                && (_execute.IsStatic || _execute.IsAlive))
            {
                await ExecuteAsync(parameter);
            }
        }

        /// <summary>
        /// CanExecute状態の再計算を行います。
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// 非同期処理をawaitできるコマンドを作成します。
        /// </summary>
        /// <param name="execute">処理内容</param>
        /// <param name="canExecute">実行可能判定</param>
        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = new WeakFunc<Task>(execute);

            if (canExecute != null)
            {
                _canExecute = new WeakFunc<bool>(canExecute);
            }
        }
    }
}
