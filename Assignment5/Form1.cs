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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        Timer t = new Timer();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private TimeSpan time = new TimeSpan();
        List<TextBox> sudokuBoxes = new List<TextBox>();
        List<Puzzle> puzzleList = new List<Puzzle>();
        List<Puzzle> easyPuzzleList = new List<Puzzle>();
        List<Puzzle> mediumPuzzleList = new List<Puzzle>();
        List<Puzzle> hardPuzzleList = new List<Puzzle>();
        List<int> userSolution = new List<int>();
        Puzzle currentPuzzle;
        static Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;    //center it 
            HideCursors();
            makeList();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_tick);
            timer.Enabled = true;

            readDirectory();
        }
        //getter for currentPuzzle (for form2)
        public Puzzle CurrentPuzzle => currentPuzzle;

        //property for time (for form2)
        public TimeSpan Time
        {
            get => time;
            set { time = value; }        //getters needed for form 2
        }


        /***************************************************
        * 
        *   timer_tick()
        * 
        *   Purpose: Keeps track of time and updates Timer output
        * 
        **************************************************/
        private void timer_tick(object sender, EventArgs e)
        {
            label9.Text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);

            if (currentPuzzle != null)
            {
                time = time.Add(TimeSpan.FromSeconds(0.1));
            }
        }


        /***************************************************
        * 
        *   pauseButton_Click()
        * 
        *   Purpose: The eventhandler for the button that
        *       pauses/unpauses the Timer
        * 
        **************************************************/
        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Enabled = false;
                pauseButton.Text = "Resume";
                hideBox.Visible = true;
            }

            else
            {
                timer.Enabled = true;
                pauseButton.Text = "Pause";
                hideBox.Visible = false;
            }

        }


        //necessary to get rid of blinking text cursor in textboxes
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        /***************************************************
        * 
        *   TextBox_GotFocus()
        * 
        *   Purpose: Removes the text cursor from a component
        * 
        **************************************************/
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            if (sender as TextBox != null)
            {
                HideCaret(((TextBox)sender).Handle);

                if (((TextBox)sender).ReadOnly == false)
                {
                    ((TextBox)sender).BackColor = Color.PaleTurquoise; //sets the color of the selected textbox
                }
            }
        }


        /***************************************************
        * 
        *   TextBox_Leave()
        * 
        *   Purpose: The event handler for the TextBoxes
        *       on the board. They focus is shifted away
        *       from a TextBox (the user is not in that
        *       TextBox), the color goes back to it's original color
        * 
        **************************************************/
        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (sender as TextBox != null)
            {
                if (((TextBox)sender).ReadOnly == false)
                {
                    ((TextBox)sender).BackColor = Color.White; //sets the color of the selected textbox
                }
            }
        }

        /***************************************************
        * 
        *   readDirectory()
        *   
        *   Purpose: Reads the directory file and creates
        *       a puzzle for each puzzle file found
        * 
        **************************************************/
        private void readDirectory()
        {
            string line;
            string[] fields;

            StreamReader file = new System.IO.StreamReader("directory.txt");

            while ((line = file.ReadLine()) != null)
            {
                fields = line.Split(new char[] { '/', '.' }); //get the difficulty and name of the puzzle
                puzzleList.Add(new Puzzle(line, fields[0], fields[1])); //Puzzle([path], [difficulty], [name]);
            }

            file.Close();

            getPuzzles(); //go read the files found in the directory
        }

        /***************************************************
        * 
        *   fillGameBoard()
        *   
        *   Purpose: Reads every line of every puzzle file
        *       and fills the already existing Puzzle object
        *       with the data from the file.
        * 
        **************************************************/
        private void getPuzzles()
        {
            easyPuzzleList.Clear();
            mediumPuzzleList.Clear();
            hardPuzzleList.Clear();
            foreach (Puzzle p in puzzleList)
            {
                string[] allLines = File.ReadAllLines(p.Path);

                int counter = 1; //keeps track of what line we're at in the puzzle file

                foreach (string line in allLines)
                {

                    if (line != "" && counter <= 9) //read the initial puzzle
                    {
                        foreach (char c in line)
                        {
                            p.InitialPuzzle.Add(Convert.ToInt32(Convert.ToString(c)));
                        }
                        counter++;
                    }

                    else if (line != "" && counter > 9 && counter <= 18) //read the solution puzzle
                    {
                        foreach (char c in line)
                        {
                            p.SolutionPuzzle.Add(Convert.ToInt32(Convert.ToString(c)));
                        }
                        counter++;
                    }

                    else if (line != "" && counter > 18 && counter <= 27) //read the saved puzzle
                    {
                        foreach (char c in line)
                        {
                            p.SavedPuzzle.Add(Convert.ToInt32(Convert.ToString(c)));
                        }
                        counter++;
                    }

                    else if (line != "" && counter == 28) //the recorded saved puzzle time
                    {
                        string[] newTime = line.Split('.');
                        TimeSpan ts = TimeSpan.Parse(newTime[0]);
                        p.SetPuzzleTime(ts);
                        counter++;
                    }

                    else if (line != "" && counter == 29) // the recorded best time
                    {
                        string[] newTime = line.Split('.');
                        TimeSpan ts = TimeSpan.Parse(newTime[0]);
                        p.SetBestTime(ts);
                        counter++;
                    }
                }

                //filling the difficulty lists
                if (p.Difficulty == "easy")
                {
                    easyPuzzleList.Add(p);
                }
                if (p.Difficulty == "medium")
                {
                    mediumPuzzleList.Add(p);
                }
                if (p.Difficulty == "hard")
                {
                    hardPuzzleList.Add(p);
                }
            }
        }


        /***************************************************
        * 
        *   fillGameBoard()
        * 
        *   Arguments:
        *           1. list - An list of ints that will be placed
        *               in the associated numbered textbox
        *               
        *           2. startup - A bool that will be set to true
        *               if it is placing just the initial puzzle.
        *               Otherwise, it should be false so that
        *               the user can change the numbers he has
        *               previously placed.
        * 
        *   Purpose: puts the contents of a puzzle onto
        *       the UI puzzle board
        * 
        **************************************************/
        private void fillGameBoard(List<int> list, bool startup)
        {
            for (int i = 0; i < sudokuBoxes.Count; i++)
            {
                if (list[i] != 0)
                {
                    sudokuBoxes[i].Text = Convert.ToString(list[i]);
                    if (startup == true)
                    {
                        sudokuBoxes[i].ReadOnly = true; //I just put this number here from initial. it's correct and it's going to be readonly
                    }
                }
            }
        }


        /***************************************************
        * 
        *   selectPuzzle_Click()
        *   
        *   Purpose: Event handler for the three buttons that let
        *       the user select a difficulty. A random puzzle
        *       will be loaded into the board every time
        *       one of these difficulty buttons is pressed.
        * 
        **************************************************/
        private void selectPuzzle_Click(object sender, EventArgs e)
        {
            ((Button)sender).Select(); //sometimes if you have your mouse in a box and then change a puzzle, that box might be readonly,
                                       //but it changes to a white background instead of gray. Not sure where this is happening (probably gotfocus)

            getPuzzles();
            foreach (TextBox t in sudokuBoxes)
            {
                //clearing the board
                t.Text = "";
                t.ReadOnly = false;
            }

            if (sender as Button != null)
            {
                if (((Button)sender).Text == "Easy")
                {
                    
                    int r = rnd.Next(easyPuzzleList.Count());
                    if (easyPuzzleList[r].SavedPuzzle.Count == 0)
                    {
                        fillGameBoard(easyPuzzleList[r].InitialPuzzle, true);
                        time = TimeSpan.Zero;
                    }
                    else
                    {
                        fillGameBoard(easyPuzzleList[r].SavedPuzzle, false);
                        time = easyPuzzleList[r].SavedPuzzleTime;
                    }
                    currentPuzzle = easyPuzzleList[r];
                }
                else if (((Button)sender).Text == "Medium")
                {
                    int r = rnd.Next(mediumPuzzleList.Count());
                    if (mediumPuzzleList[r].SavedPuzzle.Count == 0)
                    {
                        fillGameBoard(mediumPuzzleList[r].InitialPuzzle, true);
                        time = TimeSpan.Zero;
                    }
                    else
                    {
                        fillGameBoard(mediumPuzzleList[r].SavedPuzzle, false);
                        time = mediumPuzzleList[r].SavedPuzzleTime;
                    }
                    currentPuzzle = mediumPuzzleList[r];
                }
                else if (((Button)sender).Text == "Hard")
                {
                    int r = rnd.Next(hardPuzzleList.Count());
                    if (hardPuzzleList[r].SavedPuzzle.Count == 0)
                    {
                        fillGameBoard(hardPuzzleList[r].InitialPuzzle, true);
                        time = System.TimeSpan.Zero;
                    }
                    else
                    {
                        fillGameBoard(hardPuzzleList[r].SavedPuzzle, false);
                        time = hardPuzzleList[r].SavedPuzzleTime;
                    }
                    currentPuzzle = hardPuzzleList[r];
                }
            }
            for (int i = 0; i < sudokuBoxes.Count; i++) //try to make sure the correct answers are not changeable
            {
                if (sudokuBoxes[i].Text == currentPuzzle.InitialPuzzle[i].ToString())
                {
                    sudokuBoxes[i].ReadOnly = true;
                }
            }
        }


        /***************************************************
        * 
        *   Reset_Click()
        *   
        *   Purpose: The event handler for the reset button.
        *       This will reset the timer to 0, and load
        *       up the initial puzzle. It will erase the user's progress,
        *       although the progress really gets reset if the user
        *       saves after pressing reset
        * 
        **************************************************/
        private void Reset_Click(object sender, EventArgs e)
        {
            time = TimeSpan.Zero;
            currentPuzzle.SavedPuzzle = currentPuzzle.InitialPuzzle;
            getPuzzles();
            foreach (TextBox t in sudokuBoxes)
            {
                //clearing the board
                t.Text = "";
            }
            fillGameBoard(currentPuzzle.InitialPuzzle, true);
        }


        /***************************************************
        * 
        *   SaveProgress_Click()
        * 
        *   Purpose: The event handler for the Save Progress
        *       button. This will try to take what is on the
        *       board and save it to the same txt file it
        *       was loaded from. It checks whether or not
        *       there is an already existing saved puzzle. If
        *       there isn't, it just appends the data to the file.
        *       If there is a saved puzzle, it will remove the saved
        *       puzzle, the time, and the best recorded time. THEN
        *       it will append all of the data that is stored in
        *       the currentPuzzle Puzzle object.
        * 
        **************************************************/
        private void SaveProgress_Click(object sender, EventArgs e)
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < sudokuBoxes.Count; i++)
            {
                if (sudokuBoxes[i].Text == "")
                {
                    temp.Add(0);
                }
                else
                {
                    temp.Add(Convert.ToInt32(sudokuBoxes[i].Text));
                }
            }


            if (currentPuzzle.SavedPuzzle.Count == 0)
            {
                File.AppendAllText(currentPuzzle.Path, Environment.NewLine);

                for (int i = 0; i < temp.Count; i++)
                {
                    File.AppendAllText(currentPuzzle.Path, Convert.ToString(temp[i]));
                    if ((i + 1) % 9 == 0 && i != 0)
                    {
                        File.AppendAllText(currentPuzzle.Path, Environment.NewLine);
                    }
                }
            }

            else
            {
                currentPuzzle.SavedPuzzle.Clear();
                string[] lines = File.ReadAllLines(currentPuzzle.Path);
                File.WriteAllLines(currentPuzzle.Path, lines.Take(lines.Length - 12).ToArray());

                for (int i = 0; i < temp.Count; i++)
                {
                    File.AppendAllText(currentPuzzle.Path, Convert.ToString(temp[i]));
                    if ((i + 1) % 9 == 0 && i != 0) //tried to put new lines between puzzles/times. Sometimes it works?
                    {
                        File.AppendAllText(currentPuzzle.Path, Environment.NewLine);
                    }
                }
            }
            File.AppendAllText(currentPuzzle.Path, Environment.NewLine + Convert.ToString(time)); //append timer

            File.AppendAllText(currentPuzzle.Path, Environment.NewLine + Convert.ToString(currentPuzzle.BestTime)); //append best time
            getPuzzles();
        }

        /***************************************************
        * 
        *   TextBox_Leave()
        * 
        *   Purpose: Adds the eventHandler to get
        *       rid of the blinking text cursor in
        *       the TextBoxes
        * 
        **************************************************/
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


        /***************************************************
        * 
        *   makeList() 
        * 
        *   Purpose: adds all the TextBoxes from the game board
        *       to one list, so they can be accessed more easily
        * 
        **************************************************/
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


        /***************************************************
        * 
        *   solutionButton_Click() 
        * 
        *   Purpose: The event handler for the Check Solution
        *       button. It will check if the game board matches
        *       the puzzle solution from the file. If it is correct,
        *       a dialog box will pop up telling the user they won.
        *       It will also check if the time it took to solve the puzzle
        *       beat the previous best time.
        * 
        **************************************************/
        private void solutionButton_Click(object sender, EventArgs e)
        {
            bool isCorrect = true;

            for(int i = 0; i < sudokuBoxes.Count; i++)
            {
                if (currentPuzzle != null)
                {
                    if (sudokuBoxes[i].Text != currentPuzzle.SolutionPuzzle[i].ToString())
                    {
                        isCorrect = false;
                    }
                }
            }

            if (isCorrect == true)
            {
                timer.Enabled = false;
                Form f2 = new Form2(this);
                f2.Show();

                if (time < currentPuzzle.BestTime || currentPuzzle.BestTime == TimeSpan.Zero || currentPuzzle.BestTime == null)
                {
                    currentPuzzle.SetBestTime(time);
                    string[] lines = File.ReadAllLines(currentPuzzle.Path);
                    File.WriteAllLines(currentPuzzle.Path, lines.Take(lines.Length - 1));
                    File.AppendAllText(currentPuzzle.Path, currentPuzzle.BestTime.ToString());
                }
                getPuzzles();
            }
            else
            {
                label12.Visible = true;
                t.Interval = 3000;
                t.Tick += TimerHideLabel_Tick;
                t.Start();
            }
        }


        /***************************************************
        * 
        *   TimerHideLabel_Tick()
        *   
        *   Purpose: 
        * 
        **************************************************/
        private void TimerHideLabel_Tick(object sender, EventArgs e)
        {
            label12.Visible = false;
            t.Stop();
        }


        /***************************************************
        * 
        *   button5_Click()
        * 
        *   Purpose: The event handler for Tips button.
        *       This is not fully implemented yet.
        * 
        **************************************************/
        private void button5_Click(object sender, EventArgs e)
        {
            int index, solution;
            foreach (int number in currentPuzzle.SavedPuzzle)
            {
                if (number == 0)
                {
                    index = currentPuzzle.SavedPuzzle[number];
                    //Console.WriteLine(index);
                    solution = currentPuzzle.SolutionPuzzle[index];         //an idea that kiinda worked? it worked once then never worked again rofl
                    //Console.WriteLine(solution);
                    currentPuzzle.SavedPuzzle[index] = solution;
                    break;
                }
            }
        }

        /***************************************************
        * 
        *   textBox_KeyPress() 
        * 
        *   Purpose: The event handler added to each TextBox
        *       on the game board. It doesn't allow the
        *       user to enter non-numerical characters
        * 
        **************************************************/
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var validKeys = new[] { Keys.Back, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9 };
            e.Handled = !validKeys.Contains((Keys)e.KeyChar);
        }
    }
}
