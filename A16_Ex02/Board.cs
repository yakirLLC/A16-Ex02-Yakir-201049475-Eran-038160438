using System;
using System.Collections.Generic;
using System.Text;

namespace A16_Ex02
{
    public class Board
    {
        private string[,] m_Board;
        private int m_BoardSize;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public string[,] GetBoard
        {
            get
            {
                return m_Board;
            }
        }

        public static bool IsValidSpot(int i_i, int i_j)
        {
            bool isValid = false;

            if ((i_i % 2 == 0 && i_j % 2 != 0) || (i_i % 2 != 0 && i_j % 2 == 0))
            {
                isValid = true;
            }

            return isValid;
        }

        public bool IsPlayerTerritory(int i_i, int i_PlayerNumber)
        {
            bool playerTerritory = false;

            if (i_i > m_BoardSize / 2 && i_i < m_BoardSize && i_PlayerNumber == 1)
            {
                playerTerritory = true;
            }
            else if (i_i >= 0 && i_i < (m_BoardSize / 2) - 1 && i_PlayerNumber == 2)
            {
                playerTerritory = true;
            }

            return playerTerritory;
        }

        public void IntializeBoard()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (IsValidSpot(i, j) && IsPlayerTerritory(i, 2))
                    {
                        m_Board[i, j] = "O";
                    }
                    else if (IsValidSpot(i, j) && IsPlayerTerritory(i, 1))
                    {
                        m_Board[i, j] = "X";
                    }
                    else
                    {
                        m_Board[i, j] = " ";
                    }
                }
            }
        }

        public Board(int i_BoardSize)
        {
            if (i_BoardSize != 6 && i_BoardSize != 8 && i_BoardSize != 10)
            {
                i_BoardSize = 8;
            }

            m_BoardSize = i_BoardSize;
            m_Board = new string[i_BoardSize, i_BoardSize];
            IntializeBoard();
        }

        public Board()
        {
            m_BoardSize = 8;
            m_Board = new string[m_BoardSize, m_BoardSize];
            IntializeBoard();
        }
    }
}
