using System.Collections.Generic;

namespace ysi.tools.email.webapi.Models
{
    public class Version
    {
        public string id { get; set; }
        public string template_id { get; set; }
        public int active { get; set; }
        public string name { get; set; }
        public string subject { get; set; }
        public string updated_at { get; set; }
    }

    public class Template
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Version> versions { get; set; }
    }

    public class TemplateContainer
    {
        public List<Template> templates { get; set; }
    }
}