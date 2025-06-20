using System.Diagnostics.Metrics;

namespace SHM.ProfileService.API;

public class MetricsService
{
    private readonly Meter _meter;
    private readonly Counter<int> _requestCounter;
    private readonly Histogram<double> _requestDuration;

    public MetricsService(Meter meter)
    {
        _meter = meter;
        
        // Counter - monotonically increasing value
        _requestCounter = _meter.CreateCounter<int>(
            "custom_requests_total",
            "requests",
            "Total number of custom requests"
        );
    }

    public void IncrementRequestCount(string method)
    {
        _requestCounter.Add(1, new KeyValuePair<string, object?>("method", method));
    }
}