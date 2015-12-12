using System;

namespace ysi.tools.email.webapi.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string APIKey { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime LastAccessedDateTime { get; set; }
    }
}