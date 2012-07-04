namespace io.iron.ironcache
{
    public class Credentials
    {
        public Credentials(string projectId, string token)
        {
            ProjectId = projectId;
            Token = token;
        }

        public string ProjectId { get; set; }
        public string Token { get; set; }
    }
}