namespace DOTNETMACHINETEST.Models
{
    public class UpdateProductViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
