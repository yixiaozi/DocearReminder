using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Media.Audio
{
    public class Audio
    {

        public static int GetCurrentSpeakerVolume()
        {
            int volume = 0;
            var enumerator = new MMDeviceEnumerator();

            //获取音频输出设备
            IEnumerable<MMDevice> speakDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToArray();
            if (speakDevices.Count() > 0)
            {
                MMDevice mMDevice = speakDevices.ToList()[0];
                volume = Convert.ToInt16(mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            }
            return volume;
        }

        public static  void GetCurrentSpeakerVolume(int volume)
        {
            var enumerator = new MMDeviceEnumerator();
            IEnumerable<MMDevice> speakDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToArray();
            if (speakDevices.Count() > 0)
            {

                MMDevice mMDevice = speakDevices.ToList()[0];
                mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volume / 100.0f;
            }
        }

        public static bool ISTheMute()
        {
            var enumerator = new MMDeviceEnumerator();
            IEnumerable<MMDevice> speakDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToArray();
            MMDevice mMDevice = speakDevices.ToList()[0];
            return mMDevice.AudioEndpointVolume.Mute;
        }
        public static void SpeakText(string world)
        {
            try
            {
                if (!ISTheMute())
                {
                    using (SpeechSynthesizer speech = new SpeechSynthesizer())
                    {
                        speech.Rate = 0;  //语速
                        speech.Volume = 100;  //音量
                        speech.Speak(world);
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}
