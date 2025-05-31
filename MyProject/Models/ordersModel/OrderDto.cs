namespace MyProject.Models.ordersModel
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
   
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
}
