using System;
using System.Collections.Generic;
using System.Text;

namespace A16_Ex02
{
    public class Player
    {
        private string m_Name;
        private int m_Score;
        private bool m_IsComputer;

        public Player(string i_Name)
        {
            while (i_Name.Length > 20 || i_Name.Contains(" "))
            {
                Console.WriteLine("Please enter a name wihtout spaces in it and at max size of 20:");
                i_Name = Console.ReadLine();
            }

            m_Name = i_Name;
            m_Score = 0;
            m_IsComputer = false;
        }

        public Player() 
        {
            m_Name = "Computer";
            m_Score = 0;
            m_IsComputer = true;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score += value;
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
        }
    }
}
