using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StreamDeckSharp;

namespace StreamDeck_xSplit_Preview
{
    public class InputMeter : Job
    {
        private Boolean isRecording = false;
        public static List<string> GetDeviceList()
        {
            NAudio.CoreAudioApi.MMDeviceEnumerator enu = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            var devices = enu.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Capture, NAudio.CoreAudioApi.DeviceState.Active);
            List<string> ret = new List<string>();
            foreach (var d in devices)
            {
                try
                {
                    ret.Add(d.FriendlyName);

                }
                catch (Exception exc)
                {

                }

            }
            ret.Sort();
            return ret;
        }
        [Newtonsoft.Json.JsonIgnore]
        public Bitmap baseBitMap;
        public string OutputDeviceName;
        private void StartRecording()
        {
            string[] devices = NAudio.Wave.AsioOut.GetDriverNames();
            Console.Write("");
        }
        private void StopRecording()
        {

        }
        NAudio.CoreAudioApi.MMDevice dev;
        protected override void ProcessEvent(object sender, KeyEventArgs e)
        {
            if(e.IsDown)
            {
                if(!isRecording)
                {
                    isRecording = true;
                    StartRecording();
                }
                else
                {
                    isRecording = false;
                    StopRecording();
                }
            }
        }
        public override void Process(StreamDeckSharp.IStreamDeck deck)
        {
            if (baseBitMap == null)
            {
                baseBitMap = (Bitmap)Bitmap.FromFile("Audio.bmp");
            }
            Bitmap tempBitmap = new Bitmap(baseBitMap.Width, baseBitMap.Height);

            if (dev == null)
            {
                NAudio.CoreAudioApi.MMDeviceEnumerator enu = new NAudio.CoreAudioApi.MMDeviceEnumerator();
                var devices = enu.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Capture, NAudio.CoreAudioApi.DeviceState.All);
                foreach (var d in devices)
                {
                    try
                    {
                        if (d.FriendlyName == OutputDeviceName)
                        {
                            dev = d;
                            break;
                        }
                    }
                    catch (Exception exc)
                    {

                    }
                }
            }
            if (dev != null)
            {
                try
                {
                    //"XSplit  Stream  Audio  Renderer")
                    {

                        if (base.enabled)

                        {
                            int pct = (int)(Math.Round(dev.AudioMeterInformation.MasterPeakValue * 100));
                            Console.Write("");

                            System.Drawing.Brush GreenPen = new SolidBrush(Color.Green);
                            System.Drawing.Brush YellowPen = new SolidBrush(Color.Yellow);
                            System.Drawing.Brush RedPen = new SolidBrush(Color.Red);
                            using (var graph = Graphics.FromImage(tempBitmap))
                            {
                                graph.DrawImage(baseBitMap, 0, 0);

                                int properWith = 72 * pct / 100;
                                if (pct > 95)
                                {
                                    graph.FillRectangle(RedPen, 0, 0, properWith, 72 - 0);
                                }
                                else
                                {
                                    if (pct > 75)
                                    {
                                        graph.FillRectangle(YellowPen, 0, 0, properWith, 72 - 0);
                                    }
                                    else
                                    {
                                        graph.FillRectangle(GreenPen, 0, 0, properWith, 72 - 0);
                                    }
                                }

                            }
                            theBitmap = tempBitmap;




                        }



                    }
                }
                catch (Exception exc)
                {

                }

            }
        }
    }
}
