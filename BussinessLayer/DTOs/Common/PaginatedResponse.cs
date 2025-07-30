using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Common
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public static PaginatedResponse<T> Create(IEnumerable<T> data, int totalCount, PaginationRequest pagination)
        {
            if (!pagination.HasPagination)
            {
                return new PaginatedResponse<T>
                {
                    Data = data,
                    TotalCount = totalCount,
                    Page = 1,
                    Size = totalCount,
                    TotalPages = 1,
                    HasNextPage = false,
                    HasPreviousPage = false
                };
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / pagination.PageSize);

            return new PaginatedResponse<T>
            {
                Data = data,
                TotalCount = totalCount,
                Page = pagination.PageNumber,
                Size = pagination.PageSize,
                TotalPages = totalPages,
                HasNextPage = pagination.PageNumber < totalPages,
                HasPreviousPage = pagination.PageNumber > 1
            };
        }
    }
}