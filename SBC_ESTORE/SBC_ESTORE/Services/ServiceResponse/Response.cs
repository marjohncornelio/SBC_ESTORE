using System.Net;

namespace SBC_ESTORE.Services.ServiceResponse
{
    public class Response
    {
        public record class GeneralResponse(string Message, HttpStatusCode ResponseCode = HttpStatusCode.OK);
        public record class LoginResponse(string Token, string Message, HttpStatusCode ResponseCode = HttpStatusCode.OK);
        public record DataResponse<T>(T Data, string Message, HttpStatusCode ResponseCode = HttpStatusCode.OK);

    }
}
