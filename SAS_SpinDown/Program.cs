namespace SAS_SpinDown
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Config.LoadConfig();

      DisksManager.LoadDisks();

      if (args.Length > 0)
      {
        if (args[0] == "OFF")
        {
          DisksManager.StandbyDiskList();
        }
        else if (args[0] == "ON")
        {
          DisksManager.PowerOnDiskList();
        }
        else if(args[0] == "AUTO")
        {

        }
        else if(args[0] == "STATUS")
        {
          DisksManager.PrintDiskList();
        }
        else
        {
          Console.WriteLine("SAS_SpinDown 1.00 Usage:");
          Console.WriteLine("'SAS_SpinDown STATUS' to see disks status");
          Console.WriteLine("'SAS_SpinDown OFF' Standby all disks");
          Console.WriteLine("'SAS_SpinDown ON' PowerOn all disks");
          Console.WriteLine("'SAS_SpinDown AUTO N' Start automatic SpinDown after N min");
        }
      }
      else
      {
        Console.WriteLine("SAS_SpinDown 1.00 Usage:");
        Console.WriteLine("'SAS_SpinDown STATUS' to see disks status");
        Console.WriteLine("'SAS_SpinDown OFF' Standby all disks");
        Console.WriteLine("'SAS_SpinDown ON' PowerOn all disks");
        Console.WriteLine("'SAS_SpinDown AUTO N' Start automatic SpinDown after N min");
      }
    }
  }
}
