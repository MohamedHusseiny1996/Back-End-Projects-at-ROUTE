using Microsoft.AspNetCore.Http;
using System.Net;

namespace Talabat_Project_API.Errors
{
	public class ExceptionResponse:ApiErrorResponse
	{
        public string? Details { get; set; }
        public ExceptionResponse(int code , string? meesage=null , string? details=null):base(code, meesage) 
        {
            Details = details;
        }
    }
}
