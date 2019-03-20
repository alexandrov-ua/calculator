using System;
using System.Threading.Tasks;
using Calculator.Api;
using Calculator.Api.Models;
using Calculator.IntegrationTests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Calculator.IntegrationTests
{
    public class EvaluatorControllerTests
    {
        private readonly WebApplicationFactory<Startup> _factory = new WebApplicationFactory<Startup>();

        [Fact]
        public async Task Evaluator_Evaluate_ShouldReturnResult()
        {
            var result = await _factory.CreateClient().PostJson("api/evaluator/evaluate", new EvaluatorInputViewModel()
            {
                Expression = "2+3*4"
            })
                .ThrowIfNotSuccess()
                .ToModel<EvaluatorOutputViewModel>();

            result.Result.Should().Be(14);
        }
        
        [Fact]
        public async Task Evaluator_Log_Smoke()
        {
            var result = await _factory.CreateClient().GetAsync("api/evaluator/log")
                .ThrowIfNotSuccess();
        }
        
        [Fact]
        public async Task Evaluator_LogFile_Smoke()
        {
            var result = await _factory.CreateClient().GetAsync("api/evaluator/log-file")
                .ThrowIfNotSuccess();
        }
    }
}