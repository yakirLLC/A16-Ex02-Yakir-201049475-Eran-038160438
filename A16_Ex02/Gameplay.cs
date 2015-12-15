using System;
using System.Collections.Generic;
using System.Text;

namespace A16_Ex02
{
    public class Gameplay
    {
        private bool m_Player1Turn = true;

        public bool Player1Turn
        {
            get
            {
                return m_Player1Turn;
            }

            set
            {
                m_Player1Turn = !m_Player1Turn;
            }
        }

        // TODO
        public static bool IsValidMove(Board  i_Board, int i_ISource, int i_IDestination, int i_JSource, int i_JDestination)
        {
            return true;
        }

        public void EatTroop(ref Board i_Board, int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            if (i_ISource - i_IDestination == 2)
            {
                if (i_JDestination < i_JSource)
                {
                    i_Board.GetBoard[i_ISource - 1, i_JDestination + 1] = " ";
                }
                else
                {
                    i_Board.GetBoard[i_ISource - 1, i_JDestination - 1] = " ";
                }
            }
            else if (i_IDestination - i_ISource == 2)
            {
                if (i_JDestination < i_JSource)
                {
                    i_Board.GetBoard[i_ISource + 1, i_JDestination + 1] = " ";
                }
                else
                {
                    i_Board.GetBoard[i_ISource + 1, i_JDestination - 1] = " ";
                }
            }
        }

        public void Move(ref Board i_Board, int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            if (i_Board.GetBoard[i_ISource, i_JSource] == "X" && i_IDestination == 0)
            {
                i_Board.GetBoard[i_IDestination, i_JDestination] = "K";
            }
            else if (i_Board.GetBoard[i_ISource, i_JSource] == "O" && i_IDestination == i_Board.BoardSize - 1)
            {
                i_Board.GetBoard[i_IDestination, i_JDestination] = "U";
            }
            else
            {
                i_Board.GetBoard[i_IDestination, i_JDestination] = i_Board.GetBoard[i_ISource, i_JSource];
                i_Board.GetBoard[i_ISource, i_JSource] = " ";
            }

            if (Math.Abs(i_ISource - i_IDestination) == 2)
            {
                EatTroop(ref i_Board, i_ISource, i_JSource, i_IDestination, i_JDestination);
            }
        }

        // TODO
        public bool IsDraw()
        {
            return true;
        }

        public int CountTroop(Board i_Board, string i_Troop)
        {
            int count = 0;

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    if (i_Troop == i_Board.GetBoard[i, j])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public bool IsGameOver(Board i_Board, out string o_WinningTroop)
        {
            bool gameOver = true;

            if (CountTroop(i_Board, "X") == 0 && CountTroop(i_Board, "O") != 0)
            {
                o_WinningTroop = "O";
            }
            else if (CountTroop(i_Board, "X") != 0 && CountTroop(i_Board, "O") == 0)
            {
                o_WinningTroop = "X";
            }
            else
            {
                o_WinningTroop = " ";
                gameOver = false;
            }

            return gameOver;
        }

        public bool CheckIfTroopEatIsLegit(string i_SourceTroop, string i_DestinationTroop , string i_FollowingSpot)
        {
            return !(i_FollowingSpot != " " || i_DestinationTroop == i_SourceTroop || i_DestinationTroop == " ");
        }
        
        public bool IsRightDiagonalPossible(Board i_Board, int i_ISource, int i_JSource)
        {
            string sourceTroop = i_Board.GetBoard[i_ISource, i_JSource];
            string destinationTroop, followingSpot;
            bool result = true;

            if (sourceTroop == "X")
            {
                if (i_JSource >= i_Board.BoardSize - 2 || i_ISource <= 1)
                {
                    result = false;
                }
                else
                {
                    destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource + 1];
                    followingSpot = i_Board.GetBoard[i_ISource - 2, i_JSource + 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
            }
            else if(sourceTroop == "O")
            {
                if (i_JSource <= 1 || i_ISource >= i_Board.BoardSize - 2)
                {
                    result = false;
                }
                else
                {
                    destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource - 1];
                    followingSpot = i_Board.GetBoard[i_ISource + 2, i_JSource - 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
            }
            else if (sourceTroop == "K" || sourceTroop == "U")
            {
                if ((i_JSource >= i_Board.BoardSize - 2 && i_ISource >= i_Board.BoardSize - 2) || (i_JSource <= 1 && i_ISource <= 1))
                {
                    result = false;
                }
                else if (i_JSource >= i_Board.BoardSize - 2)
                {
                    destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource - 1];
                    followingSpot = i_Board.GetBoard[i_ISource + 2, i_JSource - 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
                else if (i_ISource <= 1)
                {
                    destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource - 1];
                    followingSpot = i_Board.GetBoard[i_ISource + 2, i_JSource - 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
                else if (i_JSource <= 1 || i_ISource >= i_Board.BoardSize - 2)
                {
                    destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource + 1];
                    followingSpot = i_Board.GetBoard[i_ISource - 2, i_JSource + 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
                else
                {
                    destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource - 1];
                    followingSpot = i_Board.GetBoard[i_ISource - 2, i_JSource - 2];
                    if (!CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot))
                    {
                        destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource + 1];
                        followingSpot = i_Board.GetBoard[i_ISource + 2, i_JSource + 2];
                        result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                    }
                }
            }

            return result;
        }

        public bool IsLeftDiagonalPossible(Board i_Board, int i_ISource, int i_JSource)
        {
            string sourceTroop = i_Board.GetBoard[i_ISource, i_JSource];
            string destinationTroop, followingSpot;
            bool result = true;

            if (sourceTroop == "X")
            {
                if (i_JSource <= 1 || i_ISource <= 1)
                {
                    result = false;
                }
                else
                {
                    destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource - 1];
                    followingSpot = i_Board.GetBoard[i_ISource - 2, i_JSource - 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
            }
            else if (sourceTroop == "O")
            {
                if (i_JSource >= i_Board.BoardSize - 2 || i_ISource >= i_Board.BoardSize - 2)
                {
                    result = false;
                }
                else
                {
                    destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource + 1];
                    followingSpot = i_Board.GetBoard[i_ISource + 2, i_JSource + 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
            }
            else if (sourceTroop == "K" || sourceTroop == "U")
            {
                if ((i_JSource <= 1 && i_ISource >= i_Board.BoardSize - 2) || (i_JSource >= i_Board.BoardSize - 2 && i_ISource <= 1))
                {
                    result = false;
                }
                else if (i_JSource >= i_Board.BoardSize - 2)
                {
                    destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource - 1];
                    followingSpot = i_Board.GetBoard[i_ISource - 2, i_JSource - 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
                else if (i_ISource <= 1 || i_JSource <= 1)
                {
                    destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource + 1];
                    followingSpot = i_Board.GetBoard[i_ISource + 2, i_JSource + 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
                else if (i_ISource >= i_Board.BoardSize - 2)
                {
                    destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource - 1];
                    followingSpot = i_Board.GetBoard[i_ISource - 2, i_JSource - 2];
                    result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                }
                else
                {
                    destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource - 1];
                    followingSpot = i_Board.GetBoard[i_ISource - 2, i_JSource - 2];
                    if (!CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot))
                    {
                        destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource + 1];
                        followingSpot = i_Board.GetBoard[i_ISource + 2, i_JSource + 2];
                        result = CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot);
                    }
                }
            }

            return result;
        }

        public bool AnyAdditionalMove(Board i_Board, int i_ISource, int i_JSource)
        {
            string troop = i_Board.GetBoard[i_ISource, i_JSource];

            return IsRightDiagonalPossible(i_Board, i_ISource, i_JSource) || IsLeftDiagonalPossible(i_Board, i_ISource, i_JSource);
        }

        // TODO
        public bool AnyFutureMove(string i_CurrentLocation, string i_Troop)
        {
            return true;
        }

        // TODO
        public bool IsTroopInBoundry(Board i_Board, int i_ISource, int i_JSource)
        {
            return true;
        }
    }
}
