using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VoiceMeeter_Crackling_Repair
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // To launch the program when the computer starts.
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    Assembly app = Assembly.GetExecutingAssembly();
                    key.SetValue(app.GetName().Name, app.Location);
                }
                Console.WriteLine("Launching the program when the computer starts.");

                // Get the list of audio processes.
                string name = "audiodg";
                Process[] processes = Process.GetProcessesByName(name);
                if (processes.Length > 0)
                {
                    // Create a bitmask with the number of cores.
                    IntPtr mask = new IntPtr(1 << (Environment.ProcessorCount - 1));

                    // Define the priority and affinity of each process.
                    foreach (Process audio in processes)
                    {
                        audio.ProcessorAffinity = mask;
                        audio.PriorityBoostEnabled = true;
                        audio.PriorityClass = ProcessPriorityClass.High;
                    }

                    // The program has finished its work.
                    Console.WriteLine("Fixed audio crackles.");
                }
                else
                {
                    // If there is no audio process.
                    Console.WriteLine($"No \"{name}\" processes found.");
                }
            }
            catch (Exception ex)
            {
                // In case of a program error.
                Console.WriteLine($"An error has occurred!{Environment.NewLine}{ex}");
            }
            finally
            {
                // Pause before closing the program.
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
