namespace SampleAspNetCoreApplication.Domain.Commands
{
    public class CommandResponse
    {
        public string Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
