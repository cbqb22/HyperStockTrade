using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MIC.Database.Connection.DataContexts
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbEntityExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static DbPropertyEntry SafeGetProperty(this DbEntityEntry entry, string propertyName)
        {
            return entry.CurrentValues.PropertyNames.Contains(propertyName)
                ? entry.Property(propertyName)
                : null;
        }
    }
}