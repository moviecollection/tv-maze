using System.Threading.Tasks;
using PublicApiGenerator;
using VerifyXunit;
using Xunit;

namespace MovieCollection.TVMaze.Tests
{
    public class PublicApiTests
    {
        [Fact]
        public Task PublicApiShouldNotChange()
        {
            var publicApi = typeof(TVMazeService).Assembly
                .GeneratePublicApi();

            return Verifier.Verify(publicApi);
        }
    }
}
