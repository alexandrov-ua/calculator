using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Calculator.Api.Models;
using Calculator.Common.Evaluator;
using Calculator.Dal;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Api.Controllers
{
    [Route("api/evaluator")]
    public class EvaluatorController : Controller
    {
        private readonly IStringEvaluator _evaluator;
        private readonly ILogStorage<EvaluatorLog> _logStorage;

        public EvaluatorController(IStringEvaluator evaluator, ILogStorage<EvaluatorLog> logStorage)
        {
            _evaluator = evaluator;
            _logStorage = logStorage;
        }
        
        [Route("evaluate")]
        [HttpPost]
        public EvaluatorOutputViewModel Evaluate([FromBody]EvaluatorInputViewModel evaluatorInputView)
        {
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
        public IActionResult GetLogFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Time,Input,Output");
            foreach (var entry in _logStorage.GetAll())
            {
                sb.AppendLine($"{entry.Time},{entry.Data.Input},{entry.Data.Output.Result}");
            }
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
            return File(stream, "application/octet-stream", "log.csv");
        }
    }
}