using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VoiceMeeter_Crackling_Repair
{
    internal class System
    {
        /// <summary>
        /// The registry path to the startup folder.
        /// </summary>
        private const string AUTORUN_REGISTRY_PATH = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// Create a bitmask with respect to the number of cores by validating only the last bit.
        /// </summary>
        /// <returns>
        /// The bitmask created for this CPU.
        /// </returns>
        public static IntPtr GetLastCoreMask()
        {
            return new IntPtr(1 << (Environment.ProcessorCount - 1));
        }

        /// <summary>
        /// Check if the program has administrator rights.
        /// </summary>
        /// <returns>
        /// "True" if the program runs in administrator mode, "False" otherwise.
        /// </returns>
        public static bool IsAdminMode()
        {
            bool isAdmin;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                isAdmin = ((new WindowsPrincipal(identity))
                    .IsInRole(WindowsBuiltInRole.Administrator));
            return isAdmin;
        }

        /// <summary>
        /// Check if on Vista or higher.
        /// (XP cannot run a program in administrator mode.)
        /// </summary>
        /// <returns>
        /// "True" if the program runs on a version equivalent to or higher than Vista, "False" otherwise.
        /// </returns>
        public static bool IsVistaOrHigher()
        {
            return Environment.OSVersion.Version.Major >= 6;
        }

        /// <summary>
        /// Enable the program to launch when the computer starts up.
        /// </summary>
        public static void EnableAutoStart()
        {
            Assembly program = Assembly.GetExecutingAssembly();
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(System.AUTORUN_REGISTRY_PATH, true))
                key.SetValue(program.GetName().Name, program.Location);
        }

        /// <summary>
        /// Boost the performance of a process by increasing its priority, boosting it when it is focused, and making it work on the choosen core.
        /// </summary>
        /// <param name="task">The process to boost.</param>
        /// <param name="cores">The bit mask of the cores to use.</param>
        public static void BoostProcess(Process task, IntPtr cores)
        {
            task.ProcessorAffinity = cores;
            task.PriorityClass = ProcessPriorityClass.High;
            task.PriorityBoostEnabled = true;
        }

        /// <summary>
        /// Restart the program in administrator mode.
        /// (Using a manifest prevents the program from launching when the computer starts.)
        /// </summary>
        public static void RestartInAdminMode()
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = Assembly.GetExecutingAssembly().Location,
                    Verb = "runas",
                });
                Environment.Exit(0);
            }
            catch (Win32Exception) { }
        }
    }
}
