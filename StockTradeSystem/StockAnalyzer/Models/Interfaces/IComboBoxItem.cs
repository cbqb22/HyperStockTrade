using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Models.Interfaces
{
    public interface IComboBoxItem<T>
    {
        T Id { get; set; }
        string Name { get; set; }
    }
}
