using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL
{
    public  class PagedResponse : ApiResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public PagedResponse(int statusCode, string message, object result, int pageNumber, int pageSize, int totalPages, int totalCount)
            : base(statusCode, message, result)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;    
            TotalPages = totalPages;
            TotalCount = totalCount;
        }
    }
}
