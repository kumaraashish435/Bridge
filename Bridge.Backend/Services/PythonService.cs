using System;
using System.Diagnostics;

namespace Bridge.Backend.Services;

public class PythonService
{
    public string RunExtraction(string filePath)
    {
        var process = new Process();

        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = $"../worker/extract.py \"{filePath}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;

        process.Start();

        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return result;
    }
}
