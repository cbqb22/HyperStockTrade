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
        // バージョンテーブル
        /// <summary>
        ///
        /// </summary>
        public DbSet<_Version> _Version { get; set; }

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