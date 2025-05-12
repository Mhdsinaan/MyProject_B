namespace MyProject.CommenApi
{
    public class APiResponds
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public APiResponds(string status, string message, object data)
        {
            StatusCode = status;
            Message = message;
            Data = data;
        }
    }
}
