using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SAS_SpinDown
{
  internal class DisksManager
  {
    public static Dictionary<string, Disk> DiskList = new Dictionary<string, Disk>();

    public static void LoadDisks()
    {
      //search for list of disk
      string diskMap = LinuxConsole.SendCommand("sg_map");

      foreach(string line in diskMap.Split("\n"))
      {
        if(line.Contains(" "))
        {
          string SgDisk = line.Split(" ")[0].Trim().Replace("/dev/", "");
          string SdDisk = line.Split(" ")[2].Trim().Replace("/dev/","");

          //Console.WriteLine($"Disk Found: {SgDisk} -> {SdDisk}");

          DiskList[SdDisk] = new Disk(SgDisk,SdDisk);
        }
      }

      //search for mount folder

      string listMountJson = LinuxConsole.SendCommand("lsblk -J -o NAME,LABEL,MOUNTPOINT");
      //Console.WriteLine($"Json:{listMountJson}");

      JObject root = JObject.Parse(listMountJson);
      JArray devices = (JArray)root["blockdevices"];

      foreach (JToken device in devices)
      {
        string name = device["name"] != null ? device["name"].ToString() : "";
        //Console.WriteLine($"Name:{name}");
        if (DiskList.ContainsKey(name))
        {
          JToken children = device["children"];

          foreach (JToken child in (JArray)children)
          {
            string childname = child["name"] != null ? child["name"].ToString() : "";
            //Console.WriteLine($"childname:{childname}");
            if (childname == name + "1")
            {
              string mountpoint = child["mountpoint"] != null ? child["mountpoint"].ToString() : "";
              string label = child["label"] != null ? child["label"].ToString() : "";

              if (mountpoint.StartsWith("/mnt/"))
              {
                DiskList[name].MountPoint = mountpoint;
                DiskList[name].Label = label;

                //Console.WriteLine($"Disk Data: {name}:{label} {mountpoint}");
              }
            }
          }
        }
      }

      //remove not target disk
      List<string> tmp = DiskList.Keys.ToList();
      foreach(string key in tmp)
      {
        if (!DiskList[key].isTargetDisk())
          DiskList.Remove(key);
      }
    }

    public static void PrintDiskList()
    {
      foreach (KeyValuePair<string, Disk> Disk in DiskList)
      {
        Disk.Value.LoadDiskInfo();
        Console.WriteLine(Disk.Value.ToString());
      }
    }

    public static void StandbyDiskList()
    {
      foreach (KeyValuePair<string, Disk> Disk in DiskList)
      {
        Disk.Value.SetDiskStandby();
      }
    }

    public static void PowerOnDiskList()
    {
      foreach (KeyValuePair<string, Disk> Disk in DiskList)
      {
        Disk.Value.SetDiskOn();
      }
    }

  }
}
