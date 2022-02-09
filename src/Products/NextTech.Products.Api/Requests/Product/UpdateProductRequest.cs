namespace NextTech.Products.Api.Requests.Product
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
