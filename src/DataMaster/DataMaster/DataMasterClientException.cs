using System.Net;

namespace DataMaster;

public class DataMasterClientException : Exception
{
    public HttpStatusCode StatusCode { get; private set; }

    public DataMasterClientException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = httpStatusCode;
    }
}
