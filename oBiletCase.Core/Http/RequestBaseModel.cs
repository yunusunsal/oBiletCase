using Newtonsoft.Json;

namespace oBiletCase.Core.Http
{
    public class RequestBaseModel:IRequestModel
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
