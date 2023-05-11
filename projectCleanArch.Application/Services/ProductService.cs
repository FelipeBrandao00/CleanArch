using AutoMapper;
using MediatR;
using projectCleanArch.Application.DTOs;
using projectCleanArch.Application.Interfaces;
using projectCleanArch.Application.Products.Commands;
using projectCleanArch.Application.Products.Queries;

namespace projectCleanArch.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task Create(ProductDTO productDTO)
        {
            var ProductCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(ProductCreateCommand);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception("Entity could not be loaded.");

            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productQuery = new GetProductsQuery();

            if (productQuery == null)
                throw new Exception("Entity could not be loaded.");

            var result = await _mediator.Send(productQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task Remove(int? id)
        {
            var ProductRemoveCommand = new ProductRemoveCommand(id.Value);

            if (ProductRemoveCommand == null)
                throw new Exception("Entity could not be loaded.");

            await _mediator.Send(ProductRemoveCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var ProductUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(ProductUpdateCommand);
        }
    }
}
