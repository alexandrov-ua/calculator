using System.Web.Http;

namespace Calculator.Api.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        public int[] GetValues()
        {
            return new[] {1, 2, 3};
        }
    }
}