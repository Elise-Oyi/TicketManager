using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Exceptions;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Unit>
    {

        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

   

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);

            if(eventToUpdate == null)
            {
                throw new NotFoundException(nameof(Event), request.EventId);
            }

//            var validator = new UpdateEventCommandValidator();

            _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event) );

            return Unit.Value;
        }
    }
}
