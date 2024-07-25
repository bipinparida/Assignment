namespace CloudVOffice.Core.Infrastructure.Http
{
    public interface IHttpWebClients
    {


        string PostRequest(string URI, string parameterValues, bool isAnonymous = false);

        Task<string> GetRequest(string URI, bool isAnonymous = false);

    }
}
