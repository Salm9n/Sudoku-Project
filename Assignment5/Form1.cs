using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        List<TextBox> sudokuBoxes = new List<TextBox>();
        List<Puzzle> puzzleList = new List<Puzzle>();

        public Form1()
        {
            InitializeComponent();
            HideCursors();
            makeList();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_tick);
            timer.Enabled = true;

            readDirectory();

            fillGameBoard(puzzleList[0].InitialPuzzle);
        }

        private void timer_tick(object sender, EventArgs e)
        {
            time = time.Add(TimeSpan.FromMilliseconds(100));
            label9.Text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
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
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            if (sender as TextBox != null)
            {
                HideCaret(((TextBox)sender).Handle);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void readDirectory()
        {
            string line;
            string[] fields;

            StreamReader file = new System.IO.StreamReader("directory.txt");

            while((line = file.ReadLine()) != null)
            {
                fields = line.Split(new char[] { '/', '.' }); //get the difficulty and name of the puzzle
                puzzleList.Add(new Puzzle(line, fields[0], fields[1])); //Puzzle([path], [difficulty], [name]);
            }

            file.Close();

            getPuzzles(); //go read the files found in the directory
        }

        private void getPuzzles()
        {
            foreach (Puzzle p in puzzleList)
            {
                string[] allLines = File.ReadAllLines(p.Path);

                int counter = 1;

                foreach (string line in allLines)
                {

                    if(line != "" && counter <= 9) //read the initial puzzle
                    {
                        foreach (char c in line)
                        {
                            p.InitialPuzzle.Add(Convert.ToInt32(Convert.ToString(c)));
                        }
                        counter++;
                    }

                    else if(line != "" && counter > 9 && counter <= 18) //read the solution puzzle
                    {
                        foreach (char c in line)
                        {
                            p.SolutionPuzzle.Add(Convert.ToInt32(Convert.ToString(c)));
                        }
                        counter++;
                    }

                    else if(line != "" && counter > 18 && counter <= 27) //read the saved puzzle
                    {
                        foreach (char c in line)
                        {
                            p.SavedPuzzle.Add(Convert.ToInt32(Convert.ToString(c)));
                        }
                        counter++;
                    }
                }
            }

        }

        private void fillGameBoard(List<int> list)
        {
            for(int i = 0; i < sudokuBoxes.Count; i++)
            {
                if (list[i] != 0)
                {
                    sudokuBoxes[i].Text = Convert.ToString(list[i]);
                }
            }
        }

        private void HideCursors()
        {
            textBox1.GotFocus += TextBox_GotFocus;
            textBox2.GotFocus += TextBox_GotFocus;
            textBox3.GotFocus += TextBox_GotFocus;
            textBox4.GotFocus += TextBox_GotFocus;
            textBox5.GotFocus += TextBox_GotFocus;
            textBox6.GotFocus += TextBox_GotFocus;
            textBox7.GotFocus += TextBox_GotFocus;
            textBox8.GotFocus += TextBox_GotFocus;
            textBox9.GotFocus += TextBox_GotFocus;
            textBox10.GotFocus += TextBox_GotFocus;
            textBox11.GotFocus += TextBox_GotFocus;
            textBox12.GotFocus += TextBox_GotFocus;
            textBox13.GotFocus += TextBox_GotFocus;
            textBox14.GotFocus += TextBox_GotFocus;
            textBox15.GotFocus += TextBox_GotFocus;
            textBox16.GotFocus += TextBox_GotFocus;
            textBox17.GotFocus += TextBox_GotFocus;
            textBox18.GotFocus += TextBox_GotFocus;
            textBox19.GotFocus += TextBox_GotFocus;
            textBox20.GotFocus += TextBox_GotFocus;
            textBox21.GotFocus += TextBox_GotFocus;
            textBox22.GotFocus += TextBox_GotFocus;
            textBox23.GotFocus += TextBox_GotFocus;
            textBox24.GotFocus += TextBox_GotFocus;
            textBox25.GotFocus += TextBox_GotFocus;
            textBox26.GotFocus += TextBox_GotFocus;
            textBox27.GotFocus += TextBox_GotFocus;
            textBox28.GotFocus += TextBox_GotFocus;
            textBox29.GotFocus += TextBox_GotFocus;
            textBox30.GotFocus += TextBox_GotFocus;
            textBox31.GotFocus += TextBox_GotFocus;
            textBox32.GotFocus += TextBox_GotFocus;
            textBox33.GotFocus += TextBox_GotFocus;
            textBox34.GotFocus += TextBox_GotFocus;
            textBox35.GotFocus += TextBox_GotFocus;
            textBox36.GotFocus += TextBox_GotFocus;
            textBox37.GotFocus += TextBox_GotFocus;
            textBox38.GotFocus += TextBox_GotFocus;
            textBox39.GotFocus += TextBox_GotFocus;
            textBox40.GotFocus += TextBox_GotFocus;
            textBox41.GotFocus += TextBox_GotFocus;
            textBox42.GotFocus += TextBox_GotFocus;
            textBox43.GotFocus += TextBox_GotFocus;
            textBox44.GotFocus += TextBox_GotFocus;
            textBox45.GotFocus += TextBox_GotFocus;
            textBox46.GotFocus += TextBox_GotFocus;
            textBox47.GotFocus += TextBox_GotFocus;
            textBox48.GotFocus += TextBox_GotFocus;
            textBox49.GotFocus += TextBox_GotFocus;
            textBox50.GotFocus += TextBox_GotFocus;
            textBox51.GotFocus += TextBox_GotFocus;
            textBox52.GotFocus += TextBox_GotFocus;
            textBox53.GotFocus += TextBox_GotFocus;
            textBox54.GotFocus += TextBox_GotFocus;
            textBox55.GotFocus += TextBox_GotFocus;
            textBox56.GotFocus += TextBox_GotFocus;
            textBox57.GotFocus += TextBox_GotFocus;
            textBox58.GotFocus += TextBox_GotFocus;
            textBox59.GotFocus += TextBox_GotFocus;
            textBox60.GotFocus += TextBox_GotFocus;
            textBox61.GotFocus += TextBox_GotFocus;
            textBox62.GotFocus += TextBox_GotFocus;
            textBox63.GotFocus += TextBox_GotFocus;
            textBox64.GotFocus += TextBox_GotFocus;
            textBox65.GotFocus += TextBox_GotFocus;
            textBox66.GotFocus += TextBox_GotFocus;
            textBox67.GotFocus += TextBox_GotFocus;
            textBox68.GotFocus += TextBox_GotFocus;
            textBox69.GotFocus += TextBox_GotFocus;
            textBox70.GotFocus += TextBox_GotFocus;
            textBox71.GotFocus += TextBox_GotFocus;
            textBox72.GotFocus += TextBox_GotFocus;
            textBox73.GotFocus += TextBox_GotFocus;
            textBox74.GotFocus += TextBox_GotFocus;
            textBox75.GotFocus += TextBox_GotFocus;
            textBox76.GotFocus += TextBox_GotFocus;
            textBox77.GotFocus += TextBox_GotFocus;
            textBox78.GotFocus += TextBox_GotFocus;
            textBox79.GotFocus += TextBox_GotFocus;
            textBox80.GotFocus += TextBox_GotFocus;
            textBox81.GotFocus += TextBox_GotFocus;
            
        }

        private void makeList()
        {
            sudokuBoxes.Add(textBox1);
            sudokuBoxes.Add(textBox2);
            sudokuBoxes.Add(textBox3);
            sudokuBoxes.Add(textBox4);
            sudokuBoxes.Add(textBox5);
            sudokuBoxes.Add(textBox6);
            sudokuBoxes.Add(textBox7);
            sudokuBoxes.Add(textBox8);
            sudokuBoxes.Add(textBox9);
            sudokuBoxes.Add(textBox10);
            sudokuBoxes.Add(textBox11);
            sudokuBoxes.Add(textBox12);
            sudokuBoxes.Add(textBox13);
            sudokuBoxes.Add(textBox14);
            sudokuBoxes.Add(textBox15);
            sudokuBoxes.Add(textBox16);
            sudokuBoxes.Add(textBox17);
            sudokuBoxes.Add(textBox18);
            sudokuBoxes.Add(textBox19);
            sudokuBoxes.Add(textBox20);
            sudokuBoxes.Add(textBox21);
            sudokuBoxes.Add(textBox22);
            sudokuBoxes.Add(textBox23);
            sudokuBoxes.Add(textBox24);
            sudokuBoxes.Add(textBox25);
            sudokuBoxes.Add(textBox26);
            sudokuBoxes.Add(textBox27);
            sudokuBoxes.Add(textBox28);
            sudokuBoxes.Add(textBox29);
            sudokuBoxes.Add(textBox30);
            sudokuBoxes.Add(textBox31);
            sudokuBoxes.Add(textBox32);
            sudokuBoxes.Add(textBox33);
            sudokuBoxes.Add(textBox34);
            sudokuBoxes.Add(textBox35);
            sudokuBoxes.Add(textBox36);
            sudokuBoxes.Add(textBox37);
            sudokuBoxes.Add(textBox38);
            sudokuBoxes.Add(textBox39);
            sudokuBoxes.Add(textBox40);
            sudokuBoxes.Add(textBox41);
            sudokuBoxes.Add(textBox42);
            sudokuBoxes.Add(textBox43);
            sudokuBoxes.Add(textBox44);
            sudokuBoxes.Add(textBox45);
            sudokuBoxes.Add(textBox46);
            sudokuBoxes.Add(textBox47);
            sudokuBoxes.Add(textBox48);
            sudokuBoxes.Add(textBox49);
            sudokuBoxes.Add(textBox50);
            sudokuBoxes.Add(textBox51);
            sudokuBoxes.Add(textBox52);
            sudokuBoxes.Add(textBox53);
            sudokuBoxes.Add(textBox54);
            sudokuBoxes.Add(textBox55);
            sudokuBoxes.Add(textBox56);
            sudokuBoxes.Add(textBox57);
            sudokuBoxes.Add(textBox58);
            sudokuBoxes.Add(textBox59);
            sudokuBoxes.Add(textBox60);
            sudokuBoxes.Add(textBox61);
            sudokuBoxes.Add(textBox62);
            sudokuBoxes.Add(textBox63);
            sudokuBoxes.Add(textBox64);
            sudokuBoxes.Add(textBox65);
            sudokuBoxes.Add(textBox66);
            sudokuBoxes.Add(textBox67);
            sudokuBoxes.Add(textBox68);
            sudokuBoxes.Add(textBox69);
            sudokuBoxes.Add(textBox70);
            sudokuBoxes.Add(textBox71);
            sudokuBoxes.Add(textBox72);
            sudokuBoxes.Add(textBox73);
            sudokuBoxes.Add(textBox74);
            sudokuBoxes.Add(textBox75);
            sudokuBoxes.Add(textBox76);
            sudokuBoxes.Add(textBox77);
            sudokuBoxes.Add(textBox78);
            sudokuBoxes.Add(textBox79);
            sudokuBoxes.Add(textBox80);
            sudokuBoxes.Add(textBox81);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
            //label9.Text = i.ToString() + "Seconds elapsed";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            time = TimeSpan.FromMilliseconds(0);
            label9.Text = "0:0";
        }
    }
}
