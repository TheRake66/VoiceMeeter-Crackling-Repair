using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;

namespace VoiceMeeter_Crackling_Repair
{
    internal class Program
    {
        /// <summary>
        /// Main function of the program.
        /// </summary>
        /// <param name="args">The command arguments sent to the program.</param>
        static void Main(string[] args)
        {
            try
            {
                if (System.IsAdminMode())
                {
                    System.EnableAutoStart();
                    Console.WriteLine("Launching the program at computer startup has been enabled.");

                    Console.WriteLine(Audio.FixAudioCrackling() ?
                        "Audio crackling correction is complete." :
                        "Fixing audio crackles failed.");
                }
                else if (System.IsVistaOrHigher())
                {
                    Console.WriteLine("Restarting the program in administrator mode...");
                    System.RestartInAdminMode();
                    Console.WriteLine("Authorization denied by user.");
                }
                else
                    Console.WriteLine("The program must be run in administrator mode.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occurred!{Environment.NewLine}{ex}");
            }
            finally
            {
                for (int i = 5; i > 0; i--)
                {
                    Console.WriteLine($"The program is finished, it will close in {i} seconds...");
                    Thread.Sleep(1000);
                }
                Environment.Exit(0);
            }
        }
    }
}
