using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
    {

        private readonly IAsyncRepository<Event> _eventRepository; 
        private readonly IMapper _mapper;

        public DeleteEventCommandHandler(IAsyncRepository<Event> eventRepository, IMapper mapper)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId);

            await _eventRepository.DeleteAsync(eventToDelete);

            return Unit.Value;
        }

    }
}
