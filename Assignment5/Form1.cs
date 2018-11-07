using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment5
{
    public partial class Form1 : Form
    {
        Timer gameTimer = new Timer();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private TimeSpan time = new TimeSpan();
        public Form1()
        {
            InitializeComponent();
            HideCursors();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_tick);
            timer.Enabled = true;
           
        }

        private void timer_tick(object sender, EventArgs e)
        {
            time = time.Add(TimeSpan.FromMilliseconds(100));
            label9.Text = string.Format("{0}:{1}", time.Minutes, time.Seconds);
        }

        private void textBox1x1_TextChanged(object sender, EventArgs e)
        {

        }

        //necessary to get rid of blinking text cursor in textboxes
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        /***************************************************
        * 
        *   Color_GotFocus()
        * 
        *   Purpose: Removes the text cursor from a component
        * 
        **************************************************/
        private void Color_GotFocus(object sender, EventArgs e)
        {
            if (sender as TextBox != null)
            {
                HideCaret(((TextBox)sender).Handle);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void HideCursors()
        {
            textBox1.GotFocus += Color_GotFocus;
            textBox2.GotFocus += Color_GotFocus;
            textBox3.GotFocus += Color_GotFocus;
            textBox4.GotFocus += Color_GotFocus;
            textBox5.GotFocus += Color_GotFocus;
            textBox6.GotFocus += Color_GotFocus;
            textBox7.GotFocus += Color_GotFocus;
            textBox8.GotFocus += Color_GotFocus;
            textBox9.GotFocus += Color_GotFocus;
            textBox10.GotFocus += Color_GotFocus;
            textBox11.GotFocus += Color_GotFocus;
            textBox12.GotFocus += Color_GotFocus;
            textBox13.GotFocus += Color_GotFocus;
            textBox14.GotFocus += Color_GotFocus;
            textBox15.GotFocus += Color_GotFocus;
            textBox16.GotFocus += Color_GotFocus;
            textBox17.GotFocus += Color_GotFocus;
            textBox18.GotFocus += Color_GotFocus;
            textBox19.GotFocus += Color_GotFocus;
            textBox20.GotFocus += Color_GotFocus;
            textBox21.GotFocus += Color_GotFocus;
            textBox22.GotFocus += Color_GotFocus;
            textBox23.GotFocus += Color_GotFocus;
            textBox24.GotFocus += Color_GotFocus;
            textBox25.GotFocus += Color_GotFocus;
            textBox26.GotFocus += Color_GotFocus;
            textBox27.GotFocus += Color_GotFocus;
            textBox28.GotFocus += Color_GotFocus;
            textBox29.GotFocus += Color_GotFocus;
            textBox30.GotFocus += Color_GotFocus;
            textBox31.GotFocus += Color_GotFocus;
            textBox32.GotFocus += Color_GotFocus;
            textBox33.GotFocus += Color_GotFocus;
            textBox34.GotFocus += Color_GotFocus;
            textBox35.GotFocus += Color_GotFocus;
            textBox36.GotFocus += Color_GotFocus;
            textBox37.GotFocus += Color_GotFocus;
            textBox38.GotFocus += Color_GotFocus;
            textBox39.GotFocus += Color_GotFocus;
            textBox40.GotFocus += Color_GotFocus;
            textBox41.GotFocus += Color_GotFocus;
            textBox42.GotFocus += Color_GotFocus;
            textBox43.GotFocus += Color_GotFocus;
            textBox44.GotFocus += Color_GotFocus;
            textBox45.GotFocus += Color_GotFocus;
            textBox46.GotFocus += Color_GotFocus;
            textBox47.GotFocus += Color_GotFocus;
            textBox48.GotFocus += Color_GotFocus;
            textBox49.GotFocus += Color_GotFocus;
            textBox50.GotFocus += Color_GotFocus;
            textBox51.GotFocus += Color_GotFocus;
            textBox52.GotFocus += Color_GotFocus;
            textBox53.GotFocus += Color_GotFocus;
            textBox54.GotFocus += Color_GotFocus;
            textBox55.GotFocus += Color_GotFocus;
            textBox56.GotFocus += Color_GotFocus;
            textBox57.GotFocus += Color_GotFocus;
            textBox58.GotFocus += Color_GotFocus;
            textBox59.GotFocus += Color_GotFocus;
            textBox60.GotFocus += Color_GotFocus;
            textBox61.GotFocus += Color_GotFocus;
            textBox62.GotFocus += Color_GotFocus;
            textBox63.GotFocus += Color_GotFocus;
            textBox64.GotFocus += Color_GotFocus;
            textBox65.GotFocus += Color_GotFocus;
            textBox66.GotFocus += Color_GotFocus;
            textBox67.GotFocus += Color_GotFocus;
            textBox68.GotFocus += Color_GotFocus;
            textBox69.GotFocus += Color_GotFocus;
            textBox70.GotFocus += Color_GotFocus;
            textBox71.GotFocus += Color_GotFocus;
            textBox72.GotFocus += Color_GotFocus;
            textBox73.GotFocus += Color_GotFocus;
            textBox74.GotFocus += Color_GotFocus;
            textBox75.GotFocus += Color_GotFocus;
            textBox76.GotFocus += Color_GotFocus;
            textBox77.GotFocus += Color_GotFocus;
            textBox78.GotFocus += Color_GotFocus;
            textBox79.GotFocus += Color_GotFocus;
            textBox80.GotFocus += Color_GotFocus;
            textBox81.GotFocus += Color_GotFocus;
            
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            //label9.Text = i.ToString() + "Seconds elapsed";
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Enabled = false;
                pauseButton.Text = "Resume";
            }

            else
            {
                timer.Enabled = true;
                pauseButton.Text = "Pause";
            }

        }
    }
}
