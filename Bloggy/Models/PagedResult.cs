namespace Bloggy.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPosts { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalPosts / PageSize);
    }
}
