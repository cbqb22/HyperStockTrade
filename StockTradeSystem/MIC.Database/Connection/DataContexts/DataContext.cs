using System;
using System.Data.Common;
using System.Data.Entity;
using MIC.Database.Models;
using MIC.Database.Models.Constants;

namespace MIC.Database.Connection.DataContexts
{
    /// <summary>
    ///
    /// </summary>
    public class DataContext : DataContextBase
    {
        /// <summary>
        /// バージョンテーブル
        /// </summary>
        public DbSet<_Version> _Version { get; set; }

        /// <summary>
        /// 日足データ
        /// </summary>
        public DbSet<DailyPrice> DailyPrice { get; set; }

        /// <summary>
        /// 株式銘柄
        /// </summary>
        public DbSet<StockCompany> StockCompany { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionString"></param>
        public DataContext(string connectionString) : base(connectionString) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connection"></param>
        public DataContext(DbConnection connection) : base(connection) { }

        /// <summary>
        ///
        /// </summary>
        public DataContext() : base() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType(ColumnTypeName.DateTime2));
        }
    }
}