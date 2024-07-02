using Newtonsoft.Json;

namespace oBiletCase.Core.Http
{
    public class ResponseBaseModel: IResponseModel
    {
        public bool IsRequestSuccess { get ; set ; }
        public string JsonErrors { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
