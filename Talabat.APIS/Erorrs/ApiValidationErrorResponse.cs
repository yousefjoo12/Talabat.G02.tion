namespace Talabat.APIS.Erorrs
{
    public class ApiValidationErrorResponse :ApiResponse
    {
        //InvalidModelStateResponseFactory
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
