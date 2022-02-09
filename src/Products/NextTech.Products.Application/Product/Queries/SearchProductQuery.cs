using NextTech.Core.MediatR.Queries;
using NextTech.Products.Application.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Product.Queries
{
    public class SearchProductQuery : IQuery<SearchProductQueryResponse>
    {
        public string Filter { get; }
        public int? MinimumStockFilter { get; set; }
        public int? MaximumStockFilter { get; set; }
        public int PageNumber { get; }
        public int PageSize { get; }
        private SearchProductQuery(string filter, int? minimumStockFilter, int? maximumStockFilter, int pageNumber, int pageSize)
        {
            Filter = filter;
            MinimumStockFilter = minimumStockFilter;
            MaximumStockFilter = maximumStockFilter;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public static SearchProductQuery Create(string filter, int? minimumStockFilter, int? maximumStockFilter, int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize is <= 0 or > 100) throw new ArgumentOutOfRangeException(nameof(pageSize));

            return new SearchProductQuery(filter, minimumStockFilter, maximumStockFilter, pageNumber, pageSize);
        }
    }
    public class SearchProductQueryResponse: PagedListQueryResponse<ProductDto>
    {
       
    }

    public class PagedListQueryResponse<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
