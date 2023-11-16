namespace Talabat_Project_API.Errors
{
	public class ValidationErrorResponse:ApiErrorResponse
	{
       public string[] errors { get; set; }
        public ValidationErrorResponse(int code , string? message=null , string[]? errors=null):base(code,message)
        {
			this.errors = errors;

		}
    }
}
