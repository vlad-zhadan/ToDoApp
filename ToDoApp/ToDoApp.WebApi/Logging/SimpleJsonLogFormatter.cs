using System.Globalization;
using System.Text.Json;
using Serilog.Events;
using Serilog.Formatting;

namespace ToDoApp.WebApi.Logging;

public sealed class SimpleJsonLogFormatter : ITextFormatter
{
    public void Format(LogEvent logEvent, TextWriter output)
    {
        var payload = new Dictionary<string, object?>
        {
            ["timestamp"] = logEvent.Timestamp.UtcDateTime.ToString("O", CultureInfo.InvariantCulture),
            ["level"] = logEvent.Level.ToString().ToUpperInvariant(),
            ["message"] = logEvent.RenderMessage()
        };

        if (logEvent.Exception is not null)
        {
            payload["exception"] = logEvent.Exception.ToString();
        }

        output.WriteLine(JsonSerializer.Serialize(payload));
    }
}
