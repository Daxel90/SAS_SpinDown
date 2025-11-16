namespace SAS_SpinDown
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("SAS_SpinDown Usage:");
      Console.WriteLine("'SAS_SpinDown' to see disks status");
      Console.WriteLine("'SAS_SpinDown OFF' standby all disks");
      Console.WriteLine("'SAS_SpinDown ON' poweron all disks");

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
      }

      DisksManager.PrintDiskList();

    }
  }
}
