namespace OrdersApi.Dtos
{
    public class PaginatedDataDto<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public List<T> Data { get; set; }

        public PaginatedDataDto(List<T> data, int pageNumber, int pageSize, int totalCount)
        {

            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            Data = data;
        }
    }
}
