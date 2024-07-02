using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Models.ResponseDtos
{
    public class BusJourneyDto
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public decimal? Price { get; set; } = 0;
    }
}
