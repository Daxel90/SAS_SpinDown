using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS_SpinDown
{
  internal class LinuxConsole
  {
    public static string SendCommand(string pCommand)
    {
      string Result = "";

      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = "/bin/bash";
      startInfo.Arguments = $"-c \"{pCommand}\""; // Sostituisci con il tuo comando
      startInfo.RedirectStandardOutput = true;
      startInfo.RedirectStandardError = true;
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;

      Process process = new Process();
      process.StartInfo = startInfo;

      process.Start();

      string output = process.StandardOutput.ReadToEnd();
      string error = process.StandardError.ReadToEnd();

      process.WaitForExit();
      Result = output;

      if (!string.IsNullOrEmpty(error))
      {
        Result += error;
      }

      return Result;
    }


  }
}
