using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int second = 0;
        private int minute = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private string doLongTask()
        {
            string result = new WebClient().DownloadString("http://msdn.microsoft.com");
            return "Finished!" + result;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Started";            
            Task<string> task = new Task<string>(doLongTask);
            task.Start();

           
            label1.Text = await task;
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (second > 0)
            {
                second--;
            }
            else
            {
                if (minute > -1)
                {                                        
                    if(minute == 0)
                    {
                        //finished counting down!
                        timer1.Enabled = false;
                        buttonStart.Enabled = true;
                        SoundPlayer audio = new SoundPlayer(WindowsFormsApplication1.Properties.Resources.Picked);
                        audio.Play();
                        this.BringToFront();
                        this.TopMost = true;
                    }
                    else
                    {
                        second = 59;
                        minute--;
                    }
                }               
            }
            label1.Text = minute + " : " + second;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            timer1.Enabled = true;
            second = 59;
            minute = 24;
            buttonStart.Enabled = false;           
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            timer1.Enabled = false;
            second = 0;
            minute = 25;
            label1.Text = minute + " : " + second;
            buttonStart.Enabled = true;
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {      
            if (buttonStart.Enabled == false)
            {
                timer1.Enabled = !timer1.Enabled;
            }           
        }
    }
}
