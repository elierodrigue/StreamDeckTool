using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StreamDeckSharp;

namespace StreamDeck_xSplit_Preview
{
    public class StopWatch : Job
    {
        [Newtonsoft.Json.JsonIgnore]
        public string state = "Stopped";
        [Newtonsoft.Json.JsonIgnore]
        System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
        [Newtonsoft.Json.JsonIgnore]
        public Bitmap baseBitMap;
        [Newtonsoft.Json.JsonIgnore]
        public Bitmap runningBitMap;

        protected override void ProcessEvent(object sender, KeyEventArgs e)
        {
            if (e.IsDown)
            {
                if(state == "Stopped")
                {
                    
                    start();
                }
                else if(state == "Running")
                {
                    pause();
                }
                else if (state == "Paused")
                {
                    stop();
                }
            }
        }
        public void stop()
        {
            state = "Stopped";
            st.Reset();

        }
        public void pause()
        {
            state = "Paused";
            st.Stop();
        }
        public void start()
        {
            state = "Running";
            st.Start();
        }
        public override void Process(StreamDeckSharp.IStreamDeck deck)
        {
            if (baseBitMap == null)
            {
                baseBitMap = (Bitmap)Bitmap.FromFile("StopWatch.bmp");
            }
            if(runningBitMap == null)
            {
                runningBitMap = (Bitmap)Bitmap.FromFile("StopWatch-Running.bmp");
            }
            Bitmap tempBitmap = new Bitmap(baseBitMap.Width, baseBitMap.Height);
            var graph = Graphics.FromImage(tempBitmap);

            if (state == "Stopped")
            {

                graph.DrawImage(baseBitMap, 0, 0);
            }
            else
            {
                graph.DrawImage(runningBitMap, 0, 0);
                if(state == "Running")
                {
                    System.Drawing.Pen redPen = new Pen(Color.Red, 1);
                    graph.DrawEllipse(redPen, 7, 13, 57, 57);
                }
                graph.DrawString(st.Elapsed.ToString().Substring(0,8), new System.Drawing.Font("Arial", 8), new System.Drawing.SolidBrush(Color.Black), 15, 32);
            }

            theBitmap = tempBitmap;
        
          
        }
    }
}
