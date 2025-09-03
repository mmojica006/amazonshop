using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Products;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts;

public class PaginationProductsQueryHandler : IRequestHandler<PaginationProductsQuery, PaginationVm<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<ProductVm>> Handle(PaginationProductsQuery request, CancellationToken cancellationToken)
    {
        //Se construye un objeto con todos los filtros y opciones de paginación que vienen en la solicitud.
        var productSpecificationParams = new ProductSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            CategoryId = request.CategoryId,
            PrecioMax = request.PrecioMax,
            PrecioMin = request.PrecioMin,
            Rating = request.Rating,
            Status = request.Status
        };

        var spec = new ProductSpecification(productSpecificationParams);
        //Se consulta el repositorio usando esa especificación.
        var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);

        //Se crea otra especificación, esta vez solo para contar cuántos productos cumplen con los filtros (sin paginación).
        var specCount = new ProductForCountingSpecification(productSpecificationParams);
        
        var totalProducts = await _unitOfWork.Repository<Product>().CountAsync(specCount);

        //Se divide el total de productos entre el tamaño de página.
        var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(request.PageSize));
        //Se redondea hacia arriba para obtener el número total de páginas.
        var totalPages = Convert.ToInt32(rounded);

        var data = _mapper.Map<IReadOnlyList<ProductVm>>(products);
        var productsByPage = products.Count(); //Resultados por pagina aleatorio

        var pagination = new PaginationVm<ProductVm>
        {
            Count = totalProducts,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = productsByPage
        };

        return pagination;
    }
}
