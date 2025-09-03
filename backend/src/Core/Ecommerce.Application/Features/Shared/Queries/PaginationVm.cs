namespace Ecommerce.Application.Features.Shared.Queries;

public class PaginationVm<T> where T : class
{
    public int Count { get; set; }

    public int PageIndex { get; set; }

    /// <summary>
    /// tamaño de página
    /// </summary> 
    public int PageSize { get; set; }

    public IReadOnlyList<T>? Data { get; set; }

    public int PageCount { get; set; }

    public int ResultByPage { get; set; }
}