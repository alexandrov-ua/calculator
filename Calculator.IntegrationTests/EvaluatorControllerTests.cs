using System.Threading.Tasks;
using Calculator.Api.ViewModels;
using Calculator.IntegrationTests.Fixtures;
using Calculator.IntegrationTests.Tools;
using FluentAssertions;
using Xunit;

namespace Calculator.IntegrationTests
{
    public class EvaluatorControllerTests : IClassFixture<ControllerFixture>
    {
        private readonly ControllerFixture _fixture;

        public EvaluatorControllerTests(ControllerFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task Evaluator_Evaluate_ShouldReturnResult()
        {
            var response = await _fixture.Client.PostJson("api/evaluator/evaluate",
                    new EvaluatorInputViewModel()
                    {
                        Expression = "2+3*4"
                    })
                .ThrowIfNotSuccess()
                .ToModel<EvaluatorOutputViewModel>();

            response.Result.Should().Be(14);
        }
    }
}