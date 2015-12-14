using System;
using System.Collections.Generic;
using System.Text;

namespace A16_Ex02
{
    public class Player
    {
        private string m_Name;
        private int m_Score = 0;

        public Player(string i_Name)
        {
            while (i_Name.Length > 20 || i_Name.Contains(" "))
            {
                Console.WriteLine("Please enter a name wihtout spaces in it and at max size of 20:");
                i_Name = Console.ReadLine();
            }

            m_Name = i_Name;
        }

        public Player() 
        {
            m_Name = "Computer";
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }
    }
}
