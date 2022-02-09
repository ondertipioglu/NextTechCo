using System.ComponentModel.DataAnnotations;

namespace NextTech.Products.Api.Requests.Product
{   
    public class SearchProductRequest
    {
        public string Filter { get; set; }
        public int? MinimumStock { get; set; }
        public int? MaximumStock { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}
