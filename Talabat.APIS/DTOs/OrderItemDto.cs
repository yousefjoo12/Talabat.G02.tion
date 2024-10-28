namespace Talabat.APIS.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }//id bta3 item gwa order
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}