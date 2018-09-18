namespace Oven.Templates.DataAccessLayer.Migrations
{
    internal class CommandLineResult
    {
        public int ExitCode { get; set; } 
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}