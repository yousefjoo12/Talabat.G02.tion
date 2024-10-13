﻿
namespace Talabat.APIS.Erorrs
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statuscode, string? message=null)
        {
            StatusCode=statuscode;
            Message = message ?? GetDefaultMessageForStatusCode(statuscode);
        }

        private string? GetDefaultMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized ",
                404 => "Not found",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
