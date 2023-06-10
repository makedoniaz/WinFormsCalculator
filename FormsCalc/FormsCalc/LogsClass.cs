using LogClass;
using System.Text.Json;

namespace LogsClass;

public class Logs
{
    private List<Log> logs = new List<Log>();
    private readonly string logsPath = "logs.json";

    public void AddLogInFile(Log newLog)
    {
        this.logs.Add(newLog);
        
        string json = JsonSerializer.Serialize(logs);
        File.WriteAllText(logsPath, json);
    }
}
