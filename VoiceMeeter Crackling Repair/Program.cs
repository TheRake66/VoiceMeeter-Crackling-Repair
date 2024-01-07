using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
                IntPtr mask = new IntPtr(1 << Environment.ProcessorCount - 1);
                foreach (Process audio in Process.GetProcessesByName("audiodg"))
                {
                    audio.ProcessorAffinity = mask;
                    audio.PriorityBoostEnabled = true;
                    audio.PriorityClass = ProcessPriorityClass.High;
                }
                Console.WriteLine("Fixed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!" + Environment.NewLine + ex.ToString());
            }
            Console.ReadKey();
        }
    }
}
