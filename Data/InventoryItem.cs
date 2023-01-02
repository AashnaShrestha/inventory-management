using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Data
{
    public class InventoryItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime LastTakenOut { get; set; }
    }
}
