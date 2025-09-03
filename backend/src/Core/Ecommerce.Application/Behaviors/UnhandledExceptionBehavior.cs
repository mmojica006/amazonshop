using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Behaviors;

/// <summary>
/// Este es un pipeline que va a intersertar los request que llegan
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class UnhandledExceptionBehavior<TRequest, TResponse>
                            : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "Application Request: Sucedio una exception para el request {Name} {@Request}", requestName, request);
            throw new Exception("Application Request con Errores");
        }




    }
}