namespace Trustesse.Ivoluntia.Payment.Gateway.Response
{
    public class ResponseType<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public ResponseType()
        {
        }

        public ResponseType(int statusCode, bool success, string msg, T data)
        {
            Data = data;
            Succeeded = success;
            StatusCode = statusCode;
            Message = msg;
        }
       
        /// <summary>
        /// Sets the data to the appropriate response
        /// at run time
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static ResponseType<T> Fail(string errorMessage)
        {
            return new ResponseType<T> { Succeeded = false, Message = errorMessage};
        }
        public static ResponseType<T> Success(string successMessage, T data, int statusCode = 200)
        {
            return new ResponseType<T> { Succeeded = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        
    }
}

