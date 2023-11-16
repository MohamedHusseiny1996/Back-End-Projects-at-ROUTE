namespace Talabat_Project_API.Errors
{
	public class ApiErrorResponse
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }

        public ApiErrorResponse(int code,string? message=null)
        {
            StatusCode = code;
            if(message != null)
            {
				Message = message;
			}
            else
            {
                Message = GetMessage(code);
            }

        }

        private string GetMessage(int code)
        {
            return code switch
            {
                
                400=> "you made bad request",
                401=> "you aren't authorized",
                404=> "Resources NotFound",
                500=> "There is server error",
                _=> "not match status code"
            };
        }
    }
}
