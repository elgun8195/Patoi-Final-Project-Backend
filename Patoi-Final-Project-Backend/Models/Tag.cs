using System;
using System.Collections.Generic;

namespace Patoi_Final_Project_Backend.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Tag Parent { get; set; }
        public Nullable<DateTime> CreatedTime { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> LastUpdatedAt { get; set; }
    }
}
