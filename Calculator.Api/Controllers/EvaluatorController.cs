using System.Web.Http;

namespace Calculator.Api.Controllers
{
    [RoutePrefix("api/evaluator")]
    public class EvaluatorController : ApiController
    {
        [Route("evaluate")]
        [HttpPost]
        public object Evaluate([FromBody]InputModel input)
        {
            return new
            {
                IsSuccessful = true,
                Result = 12.0,
                Diagnostics = new int[0],
            };
        }

        public class InputModel
        {
            public string Expression { get; set; }
        }
    }
}