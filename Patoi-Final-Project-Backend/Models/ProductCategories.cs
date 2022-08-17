namespace Patoi_Final_Project_Backend.Models
{
    public class ProductCategories
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
        public Product Product { get; set; }
    }
}
