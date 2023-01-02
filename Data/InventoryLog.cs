using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Data
{
    public class InventoryLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Please choose an item.")]
        public InventoryItem Item { get; set; }
        public int Quantity { get; set; }
        public string ApprovedBy { get; set; }
        public string TakenBy { get; set; }
        public DateTime DateTakenOut { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
