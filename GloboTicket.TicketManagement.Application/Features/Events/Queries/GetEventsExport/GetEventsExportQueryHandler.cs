using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;

        public GetEventsExportQueryHandler(IAsyncRepository<Event> eventRepository,
             IMapper mapper, ICsvExporter csvExporter)
        {
            _csvExporter = csvExporter;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventExportFileVm> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var allEvents = _mapper.Map<List<EventExportDto>>((await _eventRepository.ListAllAsync()).OrderBy( x => x.Date));

            var fileData = _csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDto = new EventExportFileVm()
            {
                ContentType = "text/csv",
                Data = fileData,
                EventExportFileName = $"{Guid.NewGuid()}.csv"
            };

            return eventExportFileDto;
        }


    }

    
   
}
