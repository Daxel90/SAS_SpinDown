using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS_SpinDown
{
  internal static class Config
  {
    static string ConfigPath = "config.cfg";
    private static Dictionary<string,string> ConfigFields = new Dictionary<string,string>();

    /// <summary>
    /// Load Config Dictionary from file
    /// </summary>
    public static void LoadConfig()
    {
      if(File.Exists(ConfigPath))
      {
        string[] lines = File.ReadAllLines(ConfigPath);

        foreach (string line in lines)
        {
          string CleanLine = line.Trim();

          if (!CleanLine.StartsWith("#") && CleanLine.Contains("="))
          {
            string[] field = CleanLine.Split('=', 2);

            ConfigFields[field[0].Trim()] = field[1].Trim();
          }
        }
      }
    }

    /// <summary>
    /// Save Config Dictionary to file
    /// </summary>
    public static void SaveConfig()
    {
      List<string> outputLines = new List<string>();
      HashSet<string> keysWritten = new HashSet<string>();

      if (File.Exists(ConfigPath))
      {
        string[] lines = File.ReadAllLines(ConfigPath);

        foreach (string line in lines)
        {
          string trimmed = line.Trim();

          if (trimmed.StartsWith("#") || !trimmed.Contains("="))
          {
            outputLines.Add(line);
            continue;
          }

          string[] parts = trimmed.Split('=', 2);
          string key = parts[0].Trim();

          if (ConfigFields.ContainsKey(key))
          {
            string value = ConfigFields[key];
            outputLines.Add(key + " = " + value);
            keysWritten.Add(key);
          }
          else
          {
            outputLines.Add(line);
          }
        }
      }

      foreach (KeyValuePair<string, string> kvp in ConfigFields)
      {
        if (!keysWritten.Contains(kvp.Key))
        {
          outputLines.Add(kvp.Key + " = " + kvp.Value);
        }
      }

      File.WriteAllLines(ConfigPath, outputLines);
    }

    /// <summary>
    /// Return stored config value or default one
    /// </summary>
    /// <param name="pConfigKey"></param>
    /// <param name="pDefault"></param>
    /// <returns></returns>
    public static string GetConfig(string pConfigKey, string pDefault = "")
    {
      string Result = pDefault;

      if(ConfigFields.ContainsKey(pConfigKey))
      {
        Result = ConfigFields[pConfigKey];
      }
      else
      {
        ConfigFields[pConfigKey] = pDefault;
        SaveConfig();
      }

      return Result;
    }

    /// <summary>
    /// Set Config value and save config
    /// </summary>
    /// <param name="pConfigKey"></param>
    /// <param name="pValue"></param>
    public static void SetConfig(string pConfigKey, string pValue)
    {
      ConfigFields[pConfigKey] = pValue;
      SaveConfig();
    }
  }
}
