using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.RequestDtos
{
    public class GetBusJourneysDto
    {
        public virtual int OriginId { get; set; }

        public virtual int DestinationId { get; set; }

        public virtual DateTime DepartureDate { get; set; }
    }
}
