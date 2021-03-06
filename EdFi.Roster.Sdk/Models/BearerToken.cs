﻿using System;

namespace EdFi.Roster.Sdk.Models
{
    public class BearerTokenInformation
    {
        public string AccessToken { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int ExpiresIn { get; set; }
    }
}
