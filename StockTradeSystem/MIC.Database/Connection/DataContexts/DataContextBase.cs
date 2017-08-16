using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Database.Connection.DataContexts
{
    /// <summary>
    ///
    /// </summary>
    public class DataContextBase : DbContext
    {
        private const int DefaultCommandTimeout = 180;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public DataContextBase()
        {
            SetDefaultCommandTimeout();
        }

        /// <summary>
        /// 接続文字列から接続
        /// </summary>
        /// <param name="connectionString"></param>
        public DataContextBase(string connectionString)
            : base(connectionString)
        {
            SetDefaultCommandTimeout();
        }

        /// <summary>
        /// コネクションを渡すコンストラクタ
        /// </summary>
        /// <param name="connection"></param>
        public DataContextBase(DbConnection connection)
            : base(connection, false)
        {
            SetDefaultCommandTimeout();
        }

        /// <summary>
        /// 追加、更新
        ///
        /// 作成日時や更新日時を自動的に設定します。
        /// また、既存のバリデーションエラーの情報が少ないので例外発生時はエラー内容を詰めて返します。
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            // 作成日時、更新日時を設定する
            var now = DateTime.Now;
            foreach (var e in ChangeTracker.Entries())
            {
                if (e.State == EntityState.Added)
                {
                    var createTime = e.SafeGetProperty("CreateTime");
                    if (createTime != null)
                        createTime.CurrentValue = now;
                }

                if (e.State != EntityState.Added && e.State != EntityState.Modified)
                    continue;

                var updateTime = e.SafeGetProperty("UpdateTime");
                if (updateTime != null)
                    updateTime.CurrentValue = now;
            }

            // 基底の SaveChanges を呼び出す（バリデーションエラーの情報が少ないので例外発生時はエラー内容を詰めて返す）
            try { return base.SaveChanges(); }
            catch (DbEntityValidationException ex)
            {
                // 全部のバリデーションエラーを詰めて返す
                var sb = new StringBuilder();
                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    sb.AppendLine(string.Format("{0} {1}", entityValidationError.Entry.State, entityValidationError.Entry.Entity.GetType().Name));
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        sb.AppendLine(string.Format(" {0}:{1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
                var exceptionMessage = String.Concat(ex.Message, " The validation errors are: ", sb.ToString());
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        /// <summary>
        /// 追加、更新
        ///
        /// 作成日時や更新日時を自動的に設定します。
        /// また、既存のバリデーションエラーの情報が少ないので例外発生時はエラー内容を詰めて返します。
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync()
        {
            // 作成日時、更新日時を設定する
            var now = DateTime.Now;
            foreach (var e in ChangeTracker.Entries())
            {
                if (e.State == EntityState.Added)
                {
                    var createTime = e.SafeGetProperty("CreateTime");
                    if (createTime != null)
                        createTime.CurrentValue = now;
                }

                if (e.State != EntityState.Added && e.State != EntityState.Modified)
                    continue;

                var updateTime = e.SafeGetProperty("UpdateTime");
                if (updateTime != null)
                    updateTime.CurrentValue = now;
            }

            // 基底の SaveChanges を呼び出す（バリデーションエラーの情報が少ないので例外発生時はエラー内容を詰めて返す）
            try { return base.SaveChangesAsync(); }
            catch (DbEntityValidationException ex)
            {
                // 全部のバリデーションエラーを詰めて返す
                var sb = new StringBuilder();
                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    sb.AppendLine(string.Format("{0} {1}", entityValidationError.Entry.State, entityValidationError.Entry.Entity.GetType().Name));
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        sb.AppendLine(string.Format(" {0}:{1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
                var exceptionMessage = String.Concat(ex.Message, " The validation errors are: ", sb.ToString());
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        /// <summary>
        /// トランザクション分離レベルを指定して開始
        /// </summary>
        /// <param name="isolationLevel"></param>
        public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            IsolationLevel settingLevel = isolationLevel;
            return Database.BeginTransaction(settingLevel);
        }

        private void SetDefaultCommandTimeout()
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = DefaultCommandTimeout;
        }
    }
}