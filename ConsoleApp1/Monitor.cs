using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

  internal class monitor
    {
        public monitor() {
            PerformanceCounter _cpuCounter = new PerformanceCounter();
            PerformanceCounter _memoryCounter = new PerformanceCounter();
            PerformanceCounter _diskReadCounter = new PerformanceCounter();
            DriveInfo dDrive = new DriveInfo("F");
            //bool _compactFormat = false;

            if (dDrive.IsReady)
            {
                // Calculate the percentage free space
                double freeSpacePerc =
                    (dDrive.AvailableFreeSpace / (float)dDrive.TotalSize) * 100;

                // Ouput drive information
                Console.WriteLine("Drive: {0} ({1}, {2})",
                    dDrive.Name, dDrive.DriveFormat, dDrive.DriveType);

                Console.WriteLine("\tFree space:\t{0}",
                    dDrive.AvailableFreeSpace);
                Console.WriteLine("\tTotal space:\t{0}",
                    dDrive.TotalSize);

                Console.WriteLine("\n\tPercentage free space: {0:0.00}%.",
                    freeSpacePerc);
            }

            while (true)
            {
                Console.WriteLine("CPU Usage: " + monitor.GetCounterValue(_cpuCounter, "Processor", "% Processor Time", "_Total"));
                Console.WriteLine("Memory Usage: " + monitor.GetCounterValue(_memoryCounter, "Memory", "% Committed Bytes In Use", null));
                Console.WriteLine("Disk Usage: " + monitor.GetCounterValue(_diskReadCounter, "PhysicalDisk", "Disk Read Bytes/sec", "_Total"));
                Thread.Sleep(1000);
            }
        }
        public static double GetCounterValue(PerformanceCounter pc, string categoryName,
        string counterName, string instanceName)
        {
            pc.CategoryName = categoryName;
            pc.CounterName = counterName;
            pc.InstanceName = instanceName;
            return pc.NextValue();
        }
    }

