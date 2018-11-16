using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
/*****************************************
 * 
 *  Programmers: Salman Mohammed, Ryne Heron
 * 
 *       Course: CSCI 473
 * 
 *   Assignment: 5
 *         Date: November 15, 2018
 * 
 *****************************************/

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment5
{
    public partial class Form2 : Form
    {
        protected Form1 CallingForm;
        private TimeSpan ts;
        string[] newTime;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 form)    //takes form 1 in the constructor 
        {
            InitializeComponent();

            this.CallingForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;    //center it 
            ts = this.CallingForm.Time;
            String time1 = Convert.ToString(ts);
            newTime = time1.Split('.');
            label4.Text = newTime[0];
            if(this.CallingForm.CurrentPuzzle.BestTime != null)
            {
                label5.Text = this.CallingForm.CurrentPuzzle.BestTime.ToString();
            }
            else
            {
                label5.Text = "None";
            }
        }
    }
}
