using System;
using System.Net;
using System.Web.Http;
using Calculator.Api.ViewModels;
using Calculator.Common.Evaluator;

namespace Calculator.Api.Controllers
{
    [RoutePrefix("api/evaluator")]
    public class EvaluatorController : ApiController
    {
        private readonly IStringEvaluator _evaluator;

        public EvaluatorController() : this(StringEvaluatorFactory.Create())
        {
            
        }

        public EvaluatorController(IStringEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        [Route("evaluate")]
        [HttpPost]
        public EvaluatorOutputViewModel Evaluate([FromBody]EvaluatorInputViewModel evaluatorInputView)
        {
            if(evaluatorInputView==null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            return EvaluatorOutputViewModel.FromModel(_evaluator.Evaluate(evaluatorInputView.Expression));
        }
    }
}