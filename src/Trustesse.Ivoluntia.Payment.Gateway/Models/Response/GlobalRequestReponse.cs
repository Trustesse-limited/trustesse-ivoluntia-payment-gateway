namespace Trustesse.Ivoluntia.Payment.Gateway.Models.Response
{
    public class GlobalRequestReponse<T>
    {
        public int ResponseCode { get; set; }
        public bool isSuccessfull { get; set; }
        public string Message { get; set; }
        public List<ErrorItem> Errors { get; set; }
        public T Data { get; set; }
        public GlobalRequestReponse()
        {
            Errors = new List<ErrorItem>();
        }
    }
    public class ErrorItem
    {
        public string Key { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
