using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SampleAPI.Core;
using SampleAPI.Tech.Http;

namespace SampleAPI.Provider
{
    /// <summary>
    /// Wraps communication via Http to another service and maps the result to an object from the core.
    /// A robust implementation will handle robustness-patterns one way or another (i.e. Poly library)
    /// and streamline possible exceptions so that the calling Core will not be "surprised" by unexpected behaviour.
    /// </summary>
    public class AnotherDatasourceHttpClient : HttpClientBase, IAnotherDatasource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnotherDatasourceHttpClient"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="client">The injected HttpClient.</param>
        public AnotherDatasourceHttpClient(ILogger<AnotherDatasourceHttpClient> logger, HttpClient client)
         : base(client, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<SomeData> GetSomeData()
        {
            // Call the endpoint and map to SomeData, which is a class from the Core

            var result = await GetAsync<string>("myendpoint");

            return new SomeData() { MyProperty = result };

        }
    }
}
