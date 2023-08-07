﻿namespace Ada.Kanban.Service.Options
{
    public class JwtOptions
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Key { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
