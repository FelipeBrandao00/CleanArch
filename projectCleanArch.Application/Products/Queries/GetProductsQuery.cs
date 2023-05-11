using MediatR;
using projectCleanArch.Domain.Entities;

namespace projectCleanArch.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
