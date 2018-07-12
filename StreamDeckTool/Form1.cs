using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Touchless.Vision.Camera;
namespace StreamDeck_xSplit_Preview
{
    public partial class Form1 : Form
    {
        List<Job> toProcess = new List<Job>();
        bool enabled = false;
        BackgroundWorker wrk = new BackgroundWorker();
    
        public Form1()
        {
            InitializeComponent();
            
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            wrk.DoWork += Wrk_DoWork;
            StreamDeckWrapper.getInstance().ConnectionChanged += Form1_ConnectionChanged;
            StreamDeckWrapper.getInstance().KeyStateChanged += Form1_KeyStateChanged;


        }
        
        private void Wrk_DoWork(object sender, DoWorkEventArgs e)
        {
            if (System.IO.File.Exists("settingswebcam.txt"))
            {
                try
                {
                    List<WebcamFullScreen> wfs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WebcamFullScreen>>(System.IO.File.ReadAllText("settingswebcam.txt"));
                    foreach (WebcamFullScreen wf in wfs)
                    {

                        wf.HookEvents();
                    }
                    toProcess.AddRange(wfs);
                }
                catch (Exception exc)
                {

                }
            }
            if (System.IO.File.Exists("settingsvumeter.txt"))
            {
                try
                {
                    toProcess.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<VUMeter>>(System.IO.File.ReadAllText("settingsvumeter.txt")));
                }
                catch (Exception exc)
                {

                }
            }
           
         
            while (!this.Disposing)
            {
                btn0.SuspendLayout();
                btn1.SuspendLayout();
                btn2.SuspendLayout();
                btn3.SuspendLayout();
                btn4.SuspendLayout();
                btn5.SuspendLayout();
                btn6.SuspendLayout();
                btn7.SuspendLayout();
                btn8.SuspendLayout();
                btn9.SuspendLayout();
                btn10.SuspendLayout();
                btn11.SuspendLayout();
                btn12.SuspendLayout();
                btn13.SuspendLayout();
                btn14.SuspendLayout();
                try
                {
                    StreamDeckSharp.IStreamDeck deck = StreamDeckWrapper.getInstance().getDeck();
                    foreach (Job j in toProcess)
                    {
                       
                        j.Process(deck);
                        j.Draw(deck);
                        switch(j.ButtonId)
                        {
                            case 0:

                                btn0.Text = "";
                                btn0.CreateGraphics().DrawImage(j.theBitmap,0,0,btn0.Width,btn0.Height);
                                break;
                            case 1:
                                btn1.Text = "";
                                btn1.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 2:
                                btn2.Text = "";
                                btn2.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 3:
                                btn3.Text = "";
                                btn3.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 4:
                                btn4.Text = "";
                                btn4.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 5:
                                btn5.Text = "";
                                btn5.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 6:
                                btn6.Text = "";
                                btn6.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 7:
                                btn7.Text = "";
                                btn7.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 8:
                                btn8.Text = "";
                                btn8.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 9:
                                btn9.Text = "";
                                btn9.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 10:
                                btn10.Text = "";
                                btn10.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 11:
                                btn11.Text = "";
                                btn11.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 12:
                                btn12.Text = "";
                                btn12.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 13:
                                btn13.Text = "";
                                btn13.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                            case 14:
                                btn14.Text = "";
                                btn14.CreateGraphics().DrawImage(j.theBitmap, 0, 0, btn0.Width, btn0.Height);
                                break;
                        }
                    }
                }
                catch(Exception exc)
                {

                }

                btn0.ResumeLayout();
                btn1.ResumeLayout();
                btn2.ResumeLayout();
                btn3.ResumeLayout();
                btn4.ResumeLayout();
                btn5.ResumeLayout();
                btn6.ResumeLayout();
                btn7.ResumeLayout();
                btn8.ResumeLayout();
                btn9.ResumeLayout();
                btn10.ResumeLayout();
                btn11.ResumeLayout();
                btn12.ResumeLayout();
                btn13.ResumeLayout();
                btn14.ResumeLayout();
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
     
            }
            

        }

        private void Form1_KeyStateChanged(object sender, StreamDeckSharp.KeyEventArgs e)
        {
            foreach (Job j in toProcess)
            {
                j.DeckEvent(sender, e);
            }
        }

        private void Form1_ConnectionChanged(object sender, StreamDeckSharp.ConnectionEventArgs e)
        {
            if (chkConnection.InvokeRequired)
            {
                chkConnection.Invoke(new EventHandler<StreamDeckSharp.ConnectionEventArgs>(Form1_ConnectionChanged), sender, e);
            }
            else
            {
                chkConnection.Checked = e.NewConnectionState;
            }
        }

        private void UpdateTitle(object sender, EventArgs e)
        {
            this.Text = sender.ToString();
        }
        private void Deck_KeyStateChanged(object sender, StreamDeckSharp.KeyEventArgs e)
        {
           
           
        }

        private void StartCapture(object sender,EventArgs e )
        {
           
        }
       
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            wrk.RunWorkerAsync();
            foreach (Camera cam in CameraService.AvailableCameras)
            {

                comboBox1.Items.Add(cam.Name);
                 
            
            }
            for(int x=0;x<=14;x++)
            {
                comboBox2.Items.Add(x.ToString());
                comboBox4.Items.Add(x.ToString());
                comboBox6.Items.Add(x.ToString());
                comboBox8.Items.Add(x.ToString());
            }
            comboBox3.Items.AddRange(VUMeter.GetDeviceList().ToArray());

            comboBox5.Items.AddRange(InputMeter.GetDeviceList().ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            WebcamFullScreen wfs = new WebcamFullScreen();
            wfs.ButtonId = int.Parse(comboBox2.SelectedItem.ToString());
            wfs.webcamName = comboBox1.SelectedItem.ToString();
            wfs.HookEvents();
            toProcess.Add(wfs);
            Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VUMeter meter = new VUMeter();
         
            meter.ButtonId = int.Parse(comboBox4.SelectedItem.ToString());
            meter.OutputDeviceName = comboBox3.SelectedItem.ToString() ;
            toProcess.Add(meter);
            Save();
        }
        public void Save()
        {
            List<WebcamFullScreen> wfs = new List<WebcamFullScreen>();
            List<VUMeter> meters = new List<VUMeter>();
            foreach(Job j in toProcess)
            {
                if(j.GetType() == typeof(WebcamFullScreen))
                {
                    wfs.Add((WebcamFullScreen)j);
                }
                if (j.GetType() == typeof(VUMeter))
                {
                    meters.Add((VUMeter)j);
                }
            }
            Newtonsoft.Json.JsonSerializer s = new Newtonsoft.Json.JsonSerializer();
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("settingswebcam.txt"))
            
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                s.Serialize(writer, wfs);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }
            using (System.IO.StreamWriter sw2 = new System.IO.StreamWriter("settingsvumeter.txt"))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw2))
            {
                s.Serialize(writer, meters);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }
        }
        private void RemoveForIndex(int index)
        {
            
            List<Job> toRemove = new List<Job>();
            foreach (Job j in toProcess)
            {
                if (j.ButtonId == index)
                {
                    toRemove.Add(j);
                }
            }
            foreach (Job j in toRemove)
            {
                toProcess.Remove(j);
            }
            switch(index)
            {
                case 0:
                    btn0.Text = "0";
                    break;
                case 1:
                    btn1.Text = "1";
                    break;
                case 2:
                    btn2.Text = "2";
                    break;
                case 3:
                    btn3.Text = "3";
                    break;
                case 4:
                    btn4.Text = "4";
                    break;
                case 5:
                    btn5.Text = "5";
                    break;
                case 6:
                    btn6.Text = "6";
                    break;
                case 7:
                    btn7.Text = "7";
                    break;
                case 8:
                    btn8.Text = "8";
                    break;
                case 9:
                    btn9.Text = "9";
                    break;
                case 10:
                    btn10.Text = "10";
                    break;
                case 11:
                    btn11.Text = "11";
                    break;
                case 12:
                    btn12.Text = "12";
                    break;
                case 13:
                    btn13.Text = "13";
                    break;
                case 14:
                    btn14.Text = "14";
                    break;
            }
            
            Save();
        }
        private void btn0_Click(object sender, EventArgs e)
        {
            RemoveForIndex(0);


        }

        private void btn1_Click(object sender, EventArgs e)
        {
            RemoveForIndex(1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            RemoveForIndex(2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            RemoveForIndex(3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            RemoveForIndex(4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            RemoveForIndex(5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            RemoveForIndex(6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            RemoveForIndex(7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            RemoveForIndex(8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            RemoveForIndex(9);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            RemoveForIndex(10);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            RemoveForIndex(11);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            RemoveForIndex(12);
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            RemoveForIndex(13);
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            RemoveForIndex(14);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputMeter meter = new InputMeter();
         
            meter.ButtonId = int.Parse(comboBox6.SelectedItem.ToString());
            meter.OutputDeviceName = comboBox5.SelectedItem.ToString();
            toProcess.Add(meter);
            Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StopWatch meter = new StopWatch();
        
            meter.ButtonId = int.Parse(comboBox8.SelectedItem.ToString());
        
            toProcess.Add(meter);
            Save();
        }
    }
}
