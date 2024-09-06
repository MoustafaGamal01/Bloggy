namespace Bloggy.Models
{
    public class PagedResultConverter<TSource, TDestination>
    : ITypeConverter<PagedResult<TSource>, PagedResult<TDestination>>
    {
        public PagedResult<TDestination> Convert(PagedResult<TSource> source,
            PagedResult<TDestination> destination,
            ResolutionContext context)
        {
            var mappedItems = context.Mapper.Map<List<TDestination>>(source.Items);

            return new PagedResult<TDestination>
            {
                Items = mappedItems,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                TotalPosts = source.TotalPosts
            };
        }
    }
}
