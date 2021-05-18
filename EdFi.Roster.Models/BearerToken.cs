using System;

namespace EdFi.Roster.Models
{
    public class BearerTokenInformation
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int ExpiresIn { get; set; }
    }
}
