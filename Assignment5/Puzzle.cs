using System;
using System.Collections.Generic;
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

namespace Assignment5
{
    public class Puzzle
    {
        private List<int> initialPuzzle;
        private List<int> savedPuzzle;
        private List<int> solutionPuzzle;
        private string path;
        private string difficulty;
        private string name;
        private TimeSpan savedPuzzleTime;
        private TimeSpan bestTime;


        public Puzzle()
        {
            this.path = "";
            this.difficulty = "";
            this.name = "";
            initialPuzzle = new List<int>();
            savedPuzzle = new List<int>();
            solutionPuzzle = new List<int>();
        }

        public Puzzle(string path, string difficulty, string name)
        {
            this.path = path;
            this.difficulty = difficulty;
            this.name = name;
            initialPuzzle = new List<int>();
            savedPuzzle = new List<int>();
            solutionPuzzle = new List<int>();
            bestTime = new TimeSpan();
        }

       public void SetPuzzleTime (TimeSpan ts)
        {
            savedPuzzleTime = ts;
        }

        public void SetBestTime (TimeSpan ts)
        {
            bestTime = ts;
        }

        public List<int> InitialPuzzle 
        {
            get
            {
                return initialPuzzle;
            }
            set
            {
                value = initialPuzzle;
            }
        }

        public List<int> SavedPuzzle
        {
            get
            {
                return savedPuzzle;
            }
            set
            {
                value = savedPuzzle;
            }
        }

        public List<int> SolutionPuzzle
        {
            get
            {
                return solutionPuzzle;
            }
            set
            {
                value = solutionPuzzle;
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                value = path;
            }
        }

        public string Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                value = difficulty;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                value = name;
            }
        }

        public TimeSpan SavedPuzzleTime
        {
            get
            {
                return savedPuzzleTime;
            }
            set
            {
                value = savedPuzzleTime;
            }
        }

        public TimeSpan BestTime
        {
            get
            {
                return bestTime;
            }
            set
            {
                value = bestTime;
            }
        }
    }
}
