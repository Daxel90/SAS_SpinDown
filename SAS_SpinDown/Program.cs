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
          int SleepTimeMin = 10;

          if (args.Length > 1)
          {
            try
            {
              SleepTimeMin = Int32.Parse(args[1]);
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.ToString());
            }
          }

          Console.WriteLine($"Automatic SpinDown after {SleepTimeMin} min of inactivity");

          while (true)
          {
            DisksManager.UpdateDiskStats();
            DisksManager.SpinDownForInactivity(SleepTimeMin);

            //Update every Min
            Thread.Sleep(1000 * 60);
          }

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
