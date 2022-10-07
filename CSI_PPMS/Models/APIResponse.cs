namespace CSI_PPMS.Models
{
    public class APIResponse
    {
        public APIResponse(string messege, int statusCode)
        {
            Messege = messege;
            StatusCode = statusCode;
        }
        public APIResponse()
        {

        }

        public string Messege { get; set; }

        public int StatusCode { get; set; }
    }

    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {

        }
        public ServiceResponse(string messege, int statusCode, T data)
        {
            Messege = messege;
            StatusCode = statusCode;
            Data = data;
        }

        public string Messege { get; set; }

        public int StatusCode { get; set; }

        public T Data { get; set; }
    }


    public class UserDetails
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public int MyProperty { get; set; }
    }
}
