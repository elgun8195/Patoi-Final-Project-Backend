namespace Patoi_Final_Project_Backend.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int? ProduuctId { get; set; }
        public string AppUserId { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
        public Order Order { get; set; }
    }
}
