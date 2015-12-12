using System;

namespace ysi.tools.email.webapi.Models
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateDescription { get; set; }
        public string TemplateContent { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}