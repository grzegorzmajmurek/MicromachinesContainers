namespace UserService.Dtos
{
    public class OrderPublishedDto
    {
        public string ProductName { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Event { get; set; }
    }
}
