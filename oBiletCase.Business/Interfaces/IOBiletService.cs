using oBiletCase.Business.Models.RequestDtos;
using oBiletCase.Business.Models.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oBiletCase.Business.Interfaces
{
    //Obilet Business Api için işlemleri içeren Interface
    public interface IOBiletService
    {
        Task<DeviceSession> GetSession();
        Task<IEnumerable<BusLocationDto>> GetBusLocations(string searchText = null);

        Task<IEnumerable<BusJourneyDto>> GetJourneys(GetBusJourneysDto getJourneysDto);
    }
}
