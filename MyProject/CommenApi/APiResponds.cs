using Microsoft.Azure.Amqp.Framing;

namespace MyProject.CommenApi
{
    public class APiResponds<T>
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public  T Data { get; set; }
        public APiResponds(string status, string message, T data)
        {
            StatusCode = status;
            Message = message;
            Data = data;
        }
    }
}
