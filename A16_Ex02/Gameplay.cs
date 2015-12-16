using System;
using System.Collections.Generic;
using System.Text;

namespace A16_Ex02
{
    public class Gameplay
    {
        private bool m_Player1Turn = true;
        private List<Move> m_MovesArray;

        public bool Player1Turn
        {
            get
            {
                return m_Player1Turn;
            }
        }

        public void SwitchTurn()
        {
            m_Player1Turn = !m_Player1Turn;
        }
        
        public bool IsSourceSquareOccupied(Board i_Board, int i_ISource, int i_JSource)
        {
            bool isSourceSquareOccupied = true;

            if (i_Board.GetBoard[i_ISource, i_JSource].Equals(" "))
            {
                isSourceSquareOccupied = false;
            }

            return isSourceSquareOccupied;
        }

        public bool IsKing(Board i_Board, int i_ISource, int i_JSource)
        {
            bool isKing = false;

            if (i_Board.GetBoard[i_ISource, i_JSource].Equals("K") || i_Board.GetBoard[i_ISource, i_JSource].Equals("U"))
            {
                isKing = true;
            }

            return isKing;
        }

        public bool IsInputStepLegit(Board i_Board, int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            bool isInputStepLegit = false;

            if (i_Board.GetBoard[i_ISource, i_JSource].Equals("X"))
            {
                if (i_ISource - i_IDestination == 1)
                {
                    if (Math.Abs(i_JDestination - i_JSource) == 1)
                    {
                        isInputStepLegit = true;
                    }
                }
            }
            else if (i_Board.GetBoard[i_ISource, i_JSource].Equals("O"))
            {
                if (i_IDestination  - i_ISource == 1)
                {
                    if (Math.Abs(i_JDestination - i_JSource) == 1)
                    {
                        isInputStepLegit = true;
                    }
                }
            }
            else if (IsKing(i_Board, i_ISource, i_JSource))
            {
                if (Math.Abs(i_IDestination - i_ISource) == 1)
                {
                    if (Math.Abs(i_JDestination - i_JSource) == 1)
                    {
                        isInputStepLegit = true;
                    }
                }
            }

            return isInputStepLegit;
        }

        public bool IsMoveDiagonallyFarward(Board i_Board, int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            bool isMoveDiagonallyFarward = false;

            if (Math.Abs(i_IDestination - i_ISource) == 1)
            {
                if (IsInputStepLegit(i_Board, i_ISource, i_JSource, i_IDestination, i_JDestination))
                {
                    isMoveDiagonallyFarward = true;
                }
            }
            else if (Math.Abs(i_IDestination - i_ISource) == 2)
            {
                if (IsInputJumpDiagonalLegit(i_Board, i_ISource, i_JSource, i_IDestination, i_JDestination))
                {
                    isMoveDiagonallyFarward = true;
                }
            }

            return isMoveDiagonallyFarward;
        }

        public bool IsTargetSquerePermitted(Board i_Board, int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            bool isTargetSquerePermitted = true;

            if (!i_Board.GetBoard[i_IDestination, i_JDestination].Equals(" "))
            {
                isTargetSquerePermitted = false;
            }

            return isTargetSquerePermitted;
        }

        public bool IsInputJumpDiagonalLegit(Board i_Board, int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            string sourceTroop, destinationTroop, followingSpot;
            bool isInputJumpDiagonalLegit = false;
            bool isRightDiagonalPossible = IsRightJumpDiagonalPossible(i_Board, i_ISource, i_JSource);
            bool isLeftDiagonalPossible = IsLeftJumpDiagonalPossible(i_Board, i_ISource, i_JSource);

            if (isRightDiagonalPossible)
            {
                if (i_Board.GetBoard[i_ISource, i_JSource].Equals("X"))
                {
                    if (i_IDestination == i_ISource - 2 && i_JDestination == i_JSource + 2)
                    {
                        isInputJumpDiagonalLegit = true;
                    }
                }
                else if (i_Board.GetBoard[i_ISource, i_JSource].Equals("O"))
                {
                    if (i_IDestination == i_ISource + 2 && i_JDestination == i_JSource - 2)
                    {
                        isInputJumpDiagonalLegit = true;
                    }
                }
                else if (IsKing(i_Board, i_ISource, i_JSource))
                {
                    sourceTroop = i_Board.GetBoard[i_ISource, i_JSource];
                    followingSpot = i_Board.GetBoard[i_IDestination, i_JDestination];
                    if (i_ISource - 2 >= 0 && i_JSource + 2 < i_Board.BoardSize)
                    {
                        destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource + 1];
                        if (i_IDestination == i_ISource - 2 && i_JDestination == i_JSource + 2 && CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot))
                        {
                            isInputJumpDiagonalLegit = true;
                        }
                    }
                    else if(i_ISource + 2 < i_Board.BoardSize && i_JSource - 2 >= 0)
                    {
                        destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource - 1];
                        if (i_IDestination == i_ISource + 2 && i_JDestination == i_JSource - 2 && CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot))
                        {
                            isInputJumpDiagonalLegit = true;
                        }
                    }
                }
            }
            else if (isLeftDiagonalPossible)
            {
                if (i_Board.GetBoard[i_ISource, i_JSource].Equals("X"))
                {
                    if (i_IDestination == i_ISource - 2 && i_JDestination == i_JSource - 2)
                    {
                        isInputJumpDiagonalLegit = true;
                    }
                }
                else if (i_Board.GetBoard[i_ISource, i_JSource].Equals("O"))
                {
                    if (i_IDestination == i_ISource + 2 && i_JDestination == i_JSource + 2)
                    {
                        isInputJumpDiagonalLegit = true;
                    }
                }
                else if (IsKing(i_Board, i_ISource, i_JSource))
                {
                    sourceTroop = i_Board.GetBoard[i_ISource, i_JSource];
                    followingSpot = i_Board.GetBoard[i_IDestination, i_JDestination];
                    if (i_ISource - 2 >= 0 && i_JSource - 2 >= 0)
                    {
                        destinationTroop = i_Board.GetBoard[i_ISource - 1, i_JSource - 1];
                        if (i_IDestination == i_ISource - 2 && i_JDestination == i_JSource - 2 && CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot))
                        {
                            isInputJumpDiagonalLegit = true;
                        }
                    }
                    else if(i_ISource + 2 < i_Board.BoardSize && i_JSource + 2 < i_Board.BoardSize)
                    {
                        destinationTroop = i_Board.GetBoard[i_ISource + 1, i_JSource + 1];
                        if (i_IDestination == i_ISource + 2 && i_JDestination == i_JSource + 2 && CheckIfTroopEatIsLegit(sourceTroop, destinationTroop, followingSpot))
                        {
                            isInputJumpDiagonalLegit = true;
                        }
                    }
                }
            }

            return isInputJumpDiagonalLegit;
        }

        public bool IsAnyOtherJumpsAvailable(Board i_Board)
        {
            bool isAnyOtherJumpsAvailable = false;
            string troop, king;

            if (m_Player1Turn)
            {
                troop = "X";
                king = "K";
            }
            else
            {
                troop = "O";
                king = "U";
            }

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    if (i_Board.GetBoard[i, j].Equals(troop) || i_Board.GetBoard[i, j].Equals(king))
                    {
                        if(IsLeftJumpDiagonalPossible(i_Board, i, j) || IsRightJumpDiagonalPossible(i_Board, i, j))
                        {
                            isAnyOtherJumpsAvailable = true;
                            break;
                        }
                    }
                }
            }

            return isAnyOtherJumpsAvailable;
        }

        public bool IsRightStepDiagonalPossible(Board i_Board, int i_ISource, int i_JSource)
        {
            bool isRightStepDiagonalPossible = true;

            if(i_Board.GetBoard[i_ISource, i_JSource].Equals("X"))
            {
                if(i_JSource == i_Board.BoardSize - 1)
                {
                    isRightStepDiagonalPossible = false;
                }
                else
                {
                    if(!i_Board.GetBoard[i_ISource - 1, i_JSource + 1].Equals(" "))
                    {
                        isRightStepDiagonalPossible = false;
                    }
                }
            }
            else if(i_Board.GetBoard[i_ISource, i_JSource].Equals("O"))
            {
                if(i_JSource == 0)
                {
                    isRightStepDiagonalPossible = false;
                }
                else
                {
                    if(!i_Board.GetBoard[i_ISource + 1, i_JSource - 1].Equals(" "))
                    {
                        isRightStepDiagonalPossible = false;
                    }
                }
            }
            else if(IsKing(i_Board, i_ISource, i_JSource))
            {
                if(i_JSource == i_Board.BoardSize - 1 || i_ISource == 0)
                {
                    if(!i_Board.GetBoard[i_ISource + 1, i_JSource - 1].Equals(" "))
                    {
                        isRightStepDiagonalPossible = false;
                    }
                }
                else if(i_JSource == 0 || i_ISource == i_Board.BoardSize - 1)
                {
                    if(!i_Board.GetBoard[i_ISource - 1, i_JSource + 1].Equals(" "))
                    {
                        isRightStepDiagonalPossible = false;
                    }
                }
                else
                {
                    if(!i_Board.GetBoard[i_ISource - 1, i_JSource + 1].Equals(" ") && !i_Board.GetBoard[i_ISource + 1, i_JSource - 1].Equals(" "))
                    {
                        isRightStepDiagonalPossible = false;
                    }
                }
            }

            return isRightStepDiagonalPossible;
        }

        public bool IsLeftStepDiagonalPossible(Board i_Board, int i_ISource, int i_JSource)
        {
            bool isLeftStepDiagonalPossible = true;

            if (i_Board.GetBoard[i_ISource, i_JSource].Equals("X"))
            {
                if (i_JSource == 0)
                {
                    isLeftStepDiagonalPossible = false;
                }
                else
                {
                    if (!i_Board.GetBoard[i_ISource - 1, i_JSource - 1].Equals(" "))
                    {
                        isLeftStepDiagonalPossible = false;
                    }
                }
            }
            else if (i_Board.GetBoard[i_ISource, i_JSource].Equals("O"))
            {
                if (i_JSource == i_Board.BoardSize - 1)
                {
                    isLeftStepDiagonalPossible = false;
                }
                else
                {
                    if (!i_Board.GetBoard[i_ISource + 1, i_JSource + 1].Equals(" "))
                    {
                        isLeftStepDiagonalPossible = false;
                    }
                }
            }
            else if (IsKing(i_Board, i_ISource, i_JSource))
            {
                if ((i_ISource == 0 && i_JSource == i_Board.BoardSize - 1) || (i_ISource == i_Board.BoardSize - 1 && i_JSource == 0))
                {
                    isLeftStepDiagonalPossible = false;
                }

                if (i_JSource == i_Board.BoardSize - 1 || i_ISource == i_Board.BoardSize - 1)
                {
                    if (!i_Board.GetBoard[i_ISource - 1, i_JSource - 1].Equals(" "))
                    {
                        isLeftStepDiagonalPossible = false;
                    }
                }
                else if (i_JSource == 0 || i_ISource == 0)
                {
                    if (!i_Board.GetBoard[i_ISource + 1, i_JSource + 1].Equals(" "))
                    {
                        isLeftStepDiagonalPossible = false;
                    }
                }
                else
                {
                    if (!i_Board.GetBoard[i_ISource - 1, i_JSource - 1].Equals(" ") && !i_Board.GetBoard[i_ISource + 1, i_JSource + 1].Equals(" "))
                    {
                        isLeftStepDiagonalPossible = false;
                    }
                }
            }

            return isLeftStepDiagonalPossible;
        }

        public bool IsAnyOtherStepAvailable(Board i_Board, out string o_WinningTroop)
        {
            bool isAnyOtherStepAvailable = false;
            string troop, king;

            if (m_Player1Turn)
            {
                troop = "X";
                king = "K";
            }
            else
            {
                troop = "O";
                king = "U";
            }

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    if (i_Board.GetBoard[i, j].Equals(troop) || i_Board.GetBoard[i, j].Equals(king))
                    {
                        if (IsLeftStepDiagonalPossible(i_Board, i, j) || IsRightStepDiagonalPossible(i_Board, i, j))
                        {
                            isAnyOtherStepAvailable = true;
                            break;
                        }
                    }
                }
            }

            if (isAnyOtherStepAvailable)
            {
                o_WinningTroop = " ";
            }
            else
            {
                if (m_Player1Turn)
                {
                    o_WinningTroop = "O";
                }
                else
                {
                    o_WinningTroop = "X";
                }
            }

            return isAnyOtherStepAvailable;
        }

        public bool HaveJumpedIfNeeded(Board i_Board, int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            bool haveJumpedIfNeeded = true;
            bool isRightDiagonalPossible = IsRightJumpDiagonalPossible(i_Board, i_ISource, i_JSource);
            bool isLeftDiagonalPossible = IsLeftJumpDiagonalPossible(i_Board, i_ISource, i_JSource);

            if (!IsInputJumpDiagonalLegit(i_Board, i_ISource, i_JSource, i_IDestination, i_JDestination))
            {
                if (IsAnyOtherJumpsAvailable(i_Board))
                {
                    haveJumpedIfNeeded = false;
                }
            }

            return haveJumpedIfNeeded;
        }

        public bool IsMoveOpponentTroop(Board i_Board, int i_ISource, int i_JSource)
        {
            bool isMoveOpponentTroop = false;

            if (m_Player1Turn)
            {
                if (i_Board.GetBoard[i_ISource, i_JSource].Equals("O") || i_Board.GetBoard[i_ISource, i_JSource].Equals("U"))
                {
                    isMoveOpponentTroop = true;
                }
            }
            else if (!m_Player1Turn)
            {
                if (i_Board.GetBoard[i_ISource, i_JSource].Equals("X") || i_Board.GetBoard[i_ISource, i_JSource].Equals("K"))
                {
                    isMoveOpponentTroop = true;
                }
            }

            return isMoveOpponentTroop;
        }

        public bool IsQuit(int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            bool quit = false;

            if (i_ISource == -1 && i_JSource == -1 && i_IDestination == -1 && i_JDestination == -1)
            {
                quit = true;
            }

            return quit;
        }

        public bool IsValidMove(Board i_Board, int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            bool isValidMove = true;
            bool isSourceSquareOccupied;
            bool haveJumpedIfNeeded;
            bool isMoveDiagonallyFarward;
            bool isTargetSquerePermitted;
            bool isMoveOpponentTroop;
            bool isQuit = IsQuit(i_ISource, i_JSource, i_IDestination, i_JDestination);

            if (isQuit)
            {
                isValidMove = true;
            }
            else
            {
                isSourceSquareOccupied = IsSourceSquareOccupied(i_Board, i_ISource, i_JSource);
                haveJumpedIfNeeded = HaveJumpedIfNeeded(i_Board, i_ISource, i_JSource, i_IDestination, i_JDestination);
                isMoveDiagonallyFarward = IsMoveDiagonallyFarward(i_Board, i_ISource, i_JSource, i_IDestination, i_JDestination);
                isTargetSquerePermitted = IsTargetSquerePermitted(i_Board, i_ISource, i_JSource, i_IDestination, i_JDestination);
                isMoveOpponentTroop = IsMoveOpponentTroop(i_Board, i_ISource, i_JSource);
                if (!isSourceSquareOccupied || !isMoveDiagonallyFarward || !haveJumpedIfNeeded || !isTargetSquerePermitted || isMoveOpponentTroop)
                {
                    isValidMove = false;
                }
            }

            return isValidMove;
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
            if (i_Board.GetBoard[i_ISource, i_JSource].Equals("X") && i_IDestination == 0)
            {
                i_Board.GetBoard[i_IDestination, i_JDestination] = "K";
            }
            else if (i_Board.GetBoard[i_ISource, i_JSource].Equals("O") && i_IDestination == i_Board.BoardSize - 1)
            {
                i_Board.GetBoard[i_IDestination, i_JDestination] = "U";
            }
            else
            {
                i_Board.GetBoard[i_IDestination, i_JDestination] = i_Board.GetBoard[i_ISource, i_JSource];
            }

            i_Board.GetBoard[i_ISource, i_JSource] = " ";

            if (Math.Abs(i_ISource - i_IDestination) == 2)
            {
                EatTroop(ref i_Board, i_ISource, i_JSource, i_IDestination, i_JDestination);
            }
        }

        public bool IsDraw(Board i_Board)
        {
            string troop;
            bool isAnyOtherStepAvailable1, isAnyOtherStepAvailable2;

            isAnyOtherStepAvailable1 = IsAnyOtherStepAvailable(i_Board, out troop);
            SwitchTurn();
            isAnyOtherStepAvailable2 = IsAnyOtherStepAvailable(i_Board, out troop);
            SwitchTurn();

            return !isAnyOtherStepAvailable1 && !isAnyOtherStepAvailable2 && !IsAnyOtherJumpsAvailable(i_Board);
        }

        public int CountTroop(Board i_Board, string i_Troop)
        {
            int count = 0;
            string kingToCount;

            if (i_Troop.Equals("X"))
            {
                kingToCount = "K";
            }
            else
            {
                kingToCount = "U";
            }

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    if (i_Troop == i_Board.GetBoard[i, j] || kingToCount == i_Board.GetBoard[i, j])
                    {
                        if (IsKing(i_Board, i, j))
                        {
                            count = count + 4;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public bool IsGameOver(Board i_Board, bool i_Quit, out string o_WinningTroop)
        {
            bool gameOver = true;
            
            if(i_Quit)
            {
                if (m_Player1Turn)
                {
                    o_WinningTroop = "O";
                }
                else
                {
                    o_WinningTroop = "X";
                }
            }
            else if (CountTroop(i_Board, "X") == 0 && CountTroop(i_Board, "K") == 0)
            {
                o_WinningTroop = "O";
            }
            else if (CountTroop(i_Board, "O") == 0 && CountTroop(i_Board, "U") == 0)
            {
                o_WinningTroop = "X";
            }
            else if (IsAnyOtherStepAvailable(i_Board, out o_WinningTroop))
            {
                gameOver = false;
            }

            return gameOver;
        }

        public bool CheckIfTroopEatIsLegit(string i_SourceTroop, string i_DestinationTroop, string i_FollowingSpot)
        {
            bool isTroopEatLegit = false;

            if (i_SourceTroop.Equals("X") || i_SourceTroop.Equals("K"))
            {
                isTroopEatLegit = i_FollowingSpot.Equals(" ") && (i_DestinationTroop.Equals("O") || i_DestinationTroop.Equals("U"));
            }
            else if (i_SourceTroop.Equals("O") || i_SourceTroop.Equals("U"))
            {
                isTroopEatLegit = i_FollowingSpot.Equals(" ") && (i_DestinationTroop.Equals("X") || i_DestinationTroop.Equals("K"));
            }

            return isTroopEatLegit;
        }
        
        public bool IsRightJumpDiagonalPossible(Board i_Board, int i_ISource, int i_JSource)
        {
            string sourceTroop = i_Board.GetBoard[i_ISource, i_JSource];
            string destinationTroop, followingSpot;
            bool result = true;

            if (sourceTroop.Equals("X"))
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
            else if(sourceTroop.Equals("O"))
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
            else if (sourceTroop.Equals("K") || sourceTroop.Equals("U"))
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

        public bool IsLeftJumpDiagonalPossible(Board i_Board, int i_ISource, int i_JSource)
        {
            string sourceTroop = i_Board.GetBoard[i_ISource, i_JSource];
            string destinationTroop, followingSpot;
            bool result = true;

            if (sourceTroop.Equals("X"))
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
            else if (sourceTroop.Equals("O"))
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
            else if (sourceTroop.Equals("K") || sourceTroop.Equals("U"))
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

        public bool AnyAdditionalMove(Board i_Board, int i_ISource, int i_JSource, int i_Gap)
        {
            string troop = i_Board.GetBoard[i_ISource, i_JSource];

            return (i_Gap == 2) && (IsRightJumpDiagonalPossible(i_Board, i_ISource, i_JSource) || IsLeftJumpDiagonalPossible(i_Board, i_ISource, i_JSource));
        }

        public string GetLosingTroop(string i_WinningTroop)
        {
            string losingTroop = "X";

            if (i_WinningTroop.Equals("X"))
            {
                losingTroop = "O";
            }

            return losingTroop;
        }

        public int CalculateScore(Board i_Board, string i_WinningTroop, bool i_Quit)
        {
            int score = 0, winningTroopCounter, losingTroopCounter, deltaScore;

            winningTroopCounter = CountTroop(i_Board, i_WinningTroop);
            losingTroopCounter = CountTroop(i_Board, GetLosingTroop(i_WinningTroop));
            deltaScore = winningTroopCounter - losingTroopCounter;
            if (deltaScore == 0 || i_Quit)
            {
                score = winningTroopCounter;
            }
            else
            {
                score = deltaScore;
            }

            return score;
        }

        public void CreatePossibleMovesPoolForComputer(Board i_Board)
        {
            Move move;

            m_MovesArray = new List<Move>();
            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    if (i_Board.GetBoard[i, j].Equals("O") || i_Board.GetBoard[i, j].Equals("U"))
                    {
                        if (i + 2 < i_Board.BoardSize && j - 2 >= 0)
                        {
                            if(IsValidMove(i_Board, i, j, i + 2, j - 2))
                            {
                                move = new Move(i, j, i + 2, j - 2);
                                m_MovesArray.Add(move);
                            }
                        }

                        if (i + 2 < i_Board.BoardSize && j + 2 < i_Board.BoardSize)
                        {
                            if (IsValidMove(i_Board, i, j, i + 2, j + 2))
                            {
                                move = new Move(i, j, i + 2, j + 2);
                                m_MovesArray.Add(move);
                            }
                        }

                        if (i + 1 < i_Board.BoardSize && j + 1 < i_Board.BoardSize)
                        {
                            if (IsValidMove(i_Board, i, j, i + 1, j + 1))
                            {
                                move = new Move(i, j, i + 1, j + 1);
                                m_MovesArray.Add(move);
                            }
                        }

                        if (i + 1 < i_Board.BoardSize && j - 1 >= 0)
                        {
                            if (IsValidMove(i_Board, i, j, i + 1, j - 1))
                            {
                                move = new Move(i, j, i + 1, j - 1);
                                m_MovesArray.Add(move);
                            }
                        }

                        if (i_Board.GetBoard[i, j].Equals("U"))
                        {
                            if (i - 2 >= 0 && j - 2 >= 0)
                            {
                                if (IsValidMove(i_Board, i, j, i - 2, j - 2))
                                {
                                    move = new Move(i, j, i - 2, j - 2);
                                    m_MovesArray.Add(move);
                                }
                            }

                            if (i - 2 >= 0 && j + 2 < i_Board.BoardSize)
                            {
                                if (IsValidMove(i_Board, i, j, i - 2, j + 2))
                                {
                                    move = new Move(i, j, i - 2, j + 2);
                                    m_MovesArray.Add(move);
                                }
                            }

                            if (i - 1 >= 0 && j - 1 >= 0)
                            {
                                if (IsValidMove(i_Board, i, j, i - 1, j - 1))
                                {
                                    move = new Move(i, j, i - 1, j - 1);
                                    m_MovesArray.Add(move);
                                }
                            }

                            if (i - 1 >= 0 && j + 1 < i_Board.BoardSize)
                            {
                                if (IsValidMove(i_Board, i, j, i - 1, j + 1))
                                {
                                    move = new Move(i, j, i - 1, j + 1);
                                    m_MovesArray.Add(move);
                                }
                            }
                        }
                    }
                }
            }
        }

        public Move GetComputerMove(Board i_Board)
        {
            int i;
            Random random = new Random();

            CreatePossibleMovesPoolForComputer(i_Board);
            i = random.Next(0, m_MovesArray.Count);

            return m_MovesArray[i];
        }

        public bool IsComputerHasRemainingMoves(Board i_Board)
        {
            string troop;

            return IsAnyOtherJumpsAvailable(i_Board) || IsAnyOtherStepAvailable(i_Board, out troop);
        }
    }
}
