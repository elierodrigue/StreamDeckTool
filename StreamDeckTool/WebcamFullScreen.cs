using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamDeckSharp;
using Touchless.Vision.Camera;
using System.Diagnostics;
namespace StreamDeck_xSplit_Preview
{
    public class WebcamFullScreen:Job
    {
        public string webcamName;
        private Byte[] imgData = new byte[0];
        private Byte[] imgSmall = new byte[0];
        bool captureStarted = false;
        CameraFrameSource cfs;
        Stopwatch stopWatch = new Stopwatch();
        long frameDelay = 100;
        public void HookEvents()
        {
            enabled = false;
            cfs = null;
            foreach (Camera cam in CameraService.AvailableCameras)
            {
                if (cam.Name == webcamName)
                {
                    Console.Write("");
                    cfs = new CameraFrameSource(cam);
                    break;
                }
            }
            
            if (cfs != null)
            {
              /*  cfs.Camera.CaptureWidth = 1920;
                cfs.Camera.CaptureHeight = 1080;
                cfs.Camera.Fps = 30;*/
                cfs.NewFrame += Cfs_NewFrame;

                try
                {
                    cfs.StartFrameCapture();
                    stopWatch.Start();
                    captureStarted = true;
                }
                catch(Exception exc)
                {
                    
                }
            }
        }

        private void Cfs_NewFrame(Touchless.Vision.Contracts.IFrameSource arg1, Touchless.Vision.Contracts.Frame arg2, double arg3)
        {
            if (stopWatch.ElapsedMilliseconds > frameDelay)
            {
                stopWatch.Stop();
                stopWatch.Reset();
                
                theBitmap = StreamDeckSharp.Extensions.StreamDeckFullScreenDrawingExtension.ResizeToKeyStreamDeckImage(arg2.Image);

                if (enabled)
                {
                    imgData = StreamDeckSharp.Extensions.StreamDeckFullScreenDrawingExtension.GetFullScreenBitmap(arg2.Image);
                  
                }
                stopWatch.Start();
            }
        }

        public WebcamFullScreen()
        {
            enabled = false;
        }
        public override void Process(IStreamDeck deck)
        {
            if (!captureStarted)
            {
                try
                {
                    cfs.StartFrameCapture();
                    stopWatch.Start();
                    captureStarted = true;
                }
                catch (Exception exc)
                {

                }
            }
            if (imgData.Length != 0)
            {

                // StreamDeckSharp.Extensions.StreamDeckFullScreenDrawingExtension.DrawFullScreenBitmap(deck, imgData);
            }
            if (enabled)
            {
                
                StreamDeckSharp.Extensions.StreamDeckFullScreenDrawingExtension.DrawFullScreenBitmap(StreamDeckWrapper.getInstance().getDeck(), imgData);
            }
        }
        protected override void ProcessEvent(object sender, StreamDeckSharp.KeyEventArgs arg)
        {
            if(arg.IsDown)
            {
                enabled = true;
            }
            else
            {
                enabled = false;
            }
        }
    }
}
