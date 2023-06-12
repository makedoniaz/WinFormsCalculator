using LogClass;
using System.Text.Json;

namespace LogsClass;

public class Logs
{
    private List<Log> expressionLogs = new List<Log>();
    private readonly string logsPath = "logs.json";

    public List<Log> ExpressionLogs
    {
        get { return expressionLogs; }
        set { expressionLogs = value; }
    }

    public string LogsPath => this.logsPath;


    public void AddLogInFile(Log newLog)
    {
        this.ExpressionLogs.Add(newLog);

        string json = JsonSerializer.Serialize(expressionLogs);
        File.WriteAllText(logsPath, json);
    }
}
