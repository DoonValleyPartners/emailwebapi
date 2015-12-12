using System.Collections.Generic;
using System.Web.Http;
using ysi.tools.email.webapi.App_BL;

namespace ysi.tools.email.webapi.Controllers
{
    [AuthenticationFilter]
    public class TemplateController : ApiController
    {
        [HttpPost]
        public void Add()
        { }

        [HttpPost]
        public void Update()
        { }

        [HttpPost]
        public void Delete()
        { }

        [HttpPost]
        public IList<string> List()
        {
            return null;
        }

        [HttpPost]
        public string Parse()
        {
            return null;
        }
    }
}