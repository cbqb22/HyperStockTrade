using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Common.ViewModelBases
{
    /// <summary>
    /// 
    /// </summary>
    public class ProgressViewModelBase : ViewModelBase, IDisposable
    {
        #region Fields

        private Queue<object> _progressQueue = new Queue<object>();

        #endregion

        #region Properties

        private bool _isProgress;
        public bool IsProgress { get { return _isProgress; } set { Set(ref _isProgress, value); } }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public ProgressViewModelBase GetProgress(object progress = null)
        {
            _progressQueue.Enqueue(progress ?? new object());
            IsProgress = true;
            return this;
        }

        #endregion

        #region Implements IDispose 

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _progressQueue.Dequeue();
            IsProgress = _progressQueue.Count != 0;
        }

        #endregion

    }
}
