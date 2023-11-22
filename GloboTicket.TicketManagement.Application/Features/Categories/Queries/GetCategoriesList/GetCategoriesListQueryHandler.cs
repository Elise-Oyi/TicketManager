using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryListVm>>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(IMapper mapper, IAsyncRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<List<CategoryListVm>> Handle(GetCategoriesListQuery request,  CancellationToken cancellationToken)
        {
            var allCategories = (await _categoryRepository.ListAllAsync()).OrderBy(x => x.Name);

            return _mapper.Map<List<CategoryListVm>>(allCategories);
        }

    }
}
