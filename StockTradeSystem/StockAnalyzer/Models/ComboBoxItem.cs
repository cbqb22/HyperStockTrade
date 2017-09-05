using StockAnalyzer.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Models
{
    public class ComboBoxItem<T> : IComboBoxItem<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }
}
