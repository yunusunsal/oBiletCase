namespace oBiletCase.Core.Http
{
    public interface IResponseModel
    {
        bool IsRequestSuccess { get; set; }
        string JsonErrors { get; set; }
        int StatusCode { get; set; }
    }
}
