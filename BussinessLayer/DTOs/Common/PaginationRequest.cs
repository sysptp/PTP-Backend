namespace BussinessLayer.DTOs.Common
{
    public class PaginationRequest
    {
        private int _page = 1;
        private int _size = 10;

        public int? Page
        {
            get => HasPagination ? _page : null;
            set => _page = value ?? 1;
        }

        public int? Size
        {
            get => HasPagination ? _size : null;
            set => _size = value ?? 10;
        }

        public bool HasPagination { get; set; }
        public int PageNumber => _page;
        public int PageSize => _size;
        public int Skip => (_page - 1) * _size;

        public PaginationRequest()
        {
        }

        public PaginationRequest(int? page, int? size)
        {
            HasPagination = page.HasValue && size.HasValue;
            if (HasPagination)
            {
                Page = page;
                Size = size;
            }
        }
    }
}