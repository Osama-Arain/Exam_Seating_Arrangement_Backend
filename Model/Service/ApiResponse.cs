
namespace payday_server.Model
{
    public class ApiResponse
    {
        public string? statusCode{get;set;}
        public object? message{get;set;}
        public IEnumerable<string>? error{get;set;}
        public object? data{get;set;}
    }
}