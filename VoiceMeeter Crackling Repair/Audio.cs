using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceMeeter_Crackling_Repair
{
    internal class Audio
    {
        /// <summary>
        /// The name of the process handling the audio.
        /// </summary>
        private const string AUDIO_PROCESS_NAME = "audiodg";

        /// <summary>
        /// Get the list of audio processes.
        /// </summary>
        /// <returns>
        /// The list of audio processes.
        /// </returns>
        private static Process[] GetAudioProcesses()
        {
            return Process.GetProcessesByName(Audio.AUDIO_PROCESS_NAME);
        }

        /// <summary>
        /// Fix crackling noises from audio outputs by boosting each audio process.
        /// </summary>
        /// <returns>
        /// "True" if audio crackling noises have been fixed, "False" otherwise.
        /// </returns>
        public static bool FixAudioCrackling()
        {
            Process[] tasks = Audio.GetAudioProcesses();
            if (tasks.Length > 0)
            {
                IntPtr mask = System.GetLastCoreMask();
                foreach (Process task in tasks)
                    System.BoostProcess(task, mask);
                return true;
            }
            return false;
        }
    }
}
