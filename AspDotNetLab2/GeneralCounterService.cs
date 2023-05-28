using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace AspDotNetLab2
{
    public class GeneralCounterService
    {

        private readonly ConcurrentDictionary<string, int> _counters;

        public GeneralCounterService()
        {
            _counters = new ConcurrentDictionary<string, int>();
        }

        public void IncrementCounter(string url)
        {
            _counters.AddOrUpdate(url, 1, (_, count) => count + 1);
        }

        public IReadOnlyDictionary<string, int> GetCounters()
        {
            return _counters;
        }
    }
}
