using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ConsumableBoundDto
    {
        public Guid BoundId { get; set; }
        public Guid ConsumableId { get; set; }
        public string ConsumableNumber { get; set; } = string.Empty;
        public string ConsumableTypeName { get; set; } = string.Empty;
        public string? ConsumableTypeModel { get; set;}
        public int Quantity { get; set; }
        public DateTime BoundDate { get; set; }
        public Guid? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? Remarks { get; set; }
        public string Type { get; set; } = null!;
    }
}
