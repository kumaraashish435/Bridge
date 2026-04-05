using System;
using System.Diagnostics;
using System.IO;

namespace Bridge.Backend.Services;

public class PythonService
{
    public string RunExtraction(string filePath)
    {
        var process = new Process();

        // Python path ( venv)
        var pythonPath = "/Users/kumar/Desktop/Bridge/Bridge/Bridge.Infra/.venv/bin/python";

        // script path (main.py)
        var scriptPath = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), "..", "Bridge.Infra", "src", "main.py")
        );

        process.StartInfo.FileName = pythonPath;
        process.StartInfo.Arguments = $"\"{scriptPath}\" \"{filePath}\"";

        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        // Python errors Handling
        if (string.IsNullOrWhiteSpace(output))
        {
            throw new Exception($"Python failed: {error}");
        }

        if (string.IsNullOrWhiteSpace(output))
        {
            throw new Exception("Python returned empty result");
        }

        return output;
    }
}