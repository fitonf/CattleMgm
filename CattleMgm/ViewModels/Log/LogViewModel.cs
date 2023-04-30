namespace CattleMgm.ViewModels.Log
{
    public class LogViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string HttpMethod { get; set; }

        public string Url { get; set; }

    }

    public class LogDetailsViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string HttpMethod { get; set; }

        public string Url { get; set; }

        public string Exception { get; set; }

        public string Date { get; set; }

    }
}