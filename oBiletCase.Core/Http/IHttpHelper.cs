namespace oBiletCase.Core.Http
{
    public interface IHttpHelper
    {
        Task<DefaultApiResponse> ConnectPost<TRequest>(TRequest request, string clientName, string url, Dictionary<string, string> headers = null, bool camelCaseJson = true)
            where TRequest : IRequestModel, new();

        Task<TResponse> ConnectPost<TRequest, TResponse>(TRequest request, string clientName, string url, Dictionary<string, string> headers = null, bool camelCaseJson = true)
             where TRequest : IRequestModel, new()
            where TResponse : IResponseModel, new();

        Task<TResponse> ConnectPost<TResponse>(string clientName, string url, Dictionary<string, string> headers = null)
                        where TResponse : IResponseModel, new();

        Task<TResponse> ConnectPostString<TResponse>(string request, string clientName, string url, Dictionary<string, string> headers = null)
            where TResponse : IResponseModel, new();

        Task<TResponse> ConnectGet<TResponse>(string clientName, string url, Dictionary<string, string> headers = null)
          where TResponse : IResponseModel, new();

        Task<DefaultApiResponse> ConnectGet(string clientName, string url, Dictionary<string, string> headers = null);


        Task<TResponse> ConnectWithListRequest<TRequest, TResponse>(TRequest request, string clientName, string url, Dictionary<string, string> headers = null)
          where TResponse : IResponseModel, new();

        Task<DefaultApiResponse> ConnectPostStringXML(string request, string clientName, string url, Dictionary<string, string> headers = null);
        Task<TResponse> ConnectPut<TRequest, TResponse>(TRequest request, string clientName, string url, Dictionary<string, string> headers = null, bool camelCaseJson = true)
          where TRequest : IRequestModel, new()
         where TResponse : IResponseModel, new();

        Task<DefaultApiResponse> ConnectPostMultipartFormData(MultipartFormDataContent content, string clientName, string url, Dictionary<string, string> headers = null);

    }
}
