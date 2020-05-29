using SampleAPI.Core;
using System.Threading.Tasks;

namespace SampleAPI.Provider
{
    public interface IAnotherDatasource
    {
        /// <summary>
        /// Returns SomeData from the dummy service.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<SomeData> GetSomeData();
    }
}
