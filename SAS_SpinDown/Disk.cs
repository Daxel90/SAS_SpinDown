using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SAS_SpinDown
{
  internal class Disk
  {
    //Config
    public string SgName = "";
    public string SdName = "";

    public string MountPoint = "";
    public string Label = "";
    public bool IsTargetDisk = false;

    //Status
    int Temperature = 0;
    bool IsInStandby = false;

    public Disk(string pSgName, string pSdName)
    {
      SgName = pSgName;
      SdName = pSdName;
    }

    public void LoadDiskInfo()
    {
      IsInStandby = DiskIsInStanby();

      if(!IsInStandby)
      {
        string Result = LinuxConsole.SendCommand("smartctl --all /dev/" + SgName);

        foreach (string line in Result.Split("\n"))
        {
          if (line.Contains("Current Drive Temperature:"))
          {
            Temperature = Int32.Parse(line.Replace("Current Drive Temperature:", "").Replace("C", "").Trim());
          }
        }
      }


    }

    public void SetDiskStandby()
    {
      Console.Write(SgName + "...");
      LinuxConsole.SendCommand("sg_start -vvv  --pc=3 /dev/" + SgName);
      Console.WriteLine(" Stopped");
    }

    public void SetDiskOn()
    {
      Console.Write(SgName + "...");
      LinuxConsole.SendCommand("sg_start --start /dev/" + SgName);
      Console.WriteLine(" Started");
    }

    private bool DiskIsInStanby()
    {
      string Result = LinuxConsole.SendCommand("sdparm --command=sense /dev/" + SgName);

      if (Result.Contains("Additional sense: Standby"))
        return true;
      else
        return false;
    }

    public override string ToString()
    {
      return string.Format("{0,-6} {1,-6} {2,-6} {3,-12} {4,-12} {5,-4}C", SgName, SdName, Label, MountPoint, IsInStandby ? "STANDBY" : "ON", IsInStandby ? "-" : Temperature.ToString());
    }

  }
}
