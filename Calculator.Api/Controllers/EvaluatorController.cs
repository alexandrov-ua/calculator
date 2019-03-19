using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Calculator.Api.ViewModels;
using Calculator.Common.Evaluator;
using Calculator.Dal;

namespace Calculator.Api.Controllers
{
    [RoutePrefix("api/evaluator")]
    public class EvaluatorController : ApiController
    {
        private readonly IStringEvaluator _evaluator;
        private readonly ILogStorage<EvaluatorLog> _logStorage;

        public EvaluatorController() : this(StringEvaluatorFactory.Create(), LogSorageFactory.Create())
        {
            
        }

        public EvaluatorController(IStringEvaluator evaluator, ILogStorage<EvaluatorLog> logStorage)
        {
            _evaluator = evaluator;
            _logStorage = logStorage;
        }

        [Route("evaluate")]
        [HttpPost]
        public EvaluatorOutputViewModel Evaluate([FromBody]EvaluatorInputViewModel evaluatorInputView)
        {
            if(evaluatorInputView==null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            return EvaluatorOutputViewModel.FromModel(_evaluator.Evaluate(evaluatorInputView.Expression));
        }

        [Route("log")]
        [HttpGet]
        public EvaluatorLogViewModel[] GetLog()
        {
            return _logStorage.GetAll().Select(t => EvaluatorLogViewModel.FromModel(t.Data)).ToArray();
        }

        [Route("log-file")]
        [HttpGet]
        public HttpResponseMessage GetLogFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Time,Input,Output");
            foreach (var entry in _logStorage.GetAll())
            {
                sb.AppendLine($"{entry.Time},{entry.Data.Input},{entry.Data.Output.Result}");
            }
            
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
            };
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "log.csv"
                };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}