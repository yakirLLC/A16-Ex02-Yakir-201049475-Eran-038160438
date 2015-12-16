using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace A16_Ex02
{
    public class View
    {
        private Board m_Board;
        private Player m_Player1, m_Player2;
        private Gameplay m_Gameplay = new Gameplay();

        private enum eCapLetters
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J
        }

        private enum eLowerLetters
        {
            a,
            b,
            c,
            d,
            e,
            f,
            g,
            h,
            i,
            j
        }

        public void PrintBoard()
        {
            int i, j;
            StringBuilder lineSeperator = new StringBuilder(" =========================");

            Console.Write("   ");
            for (i = 0; i < m_Board.BoardSize; i++)
            {
                Console.Write("{0}   ", (eCapLetters)i);
            }

            Console.WriteLine(string.Empty);

            if (m_Board.BoardSize == 8)
            {
                lineSeperator.Append("========");
            }
            else if (m_Board.BoardSize == 10)
            {
                lineSeperator.Append("================");
            }

            Console.WriteLine(lineSeperator);
            for (i = 0; i < m_Board.BoardSize; i++)
            {
                Console.Write("{0}|", (eLowerLetters)i);
                for (j = 0; j < m_Board.BoardSize; j++)
                {
                    Console.Write(" {0} |", m_Board.GetBoard[i, j]);
                }
                
                Console.WriteLine("\n{0}", lineSeperator);
            }
        }

        public void GetPlayerDetails(out Player o_Player)
        {
            string playerName;

            Console.WriteLine("Please enter your name(max length is 20 and contains no spaces):");
            playerName = Console.ReadLine();
            o_Player = new Player(playerName);
        }

        public void CheckOpponent(out Player o_Player)
        {
            string answer;

            do
            {
                Console.WriteLine("Enter 'PvP' to play vs another player and 'PvC' to play vs the computer:");
                answer = Console.ReadLine();
            }
            while (answer != "PvP" && answer != "PvC");

            if (answer == "PvP")
            {
                GetPlayerDetails(out o_Player);
            }
            else
            {
                o_Player = new Player();
            }
        }

        public int GetBoardSize()
        {
            int boardSize;
            bool parseResult;

            do
            {
                Console.WriteLine("Enter board size(6/8/10):");
                parseResult = int.TryParse(Console.ReadLine(), out boardSize);
            }
            while (!parseResult || (boardSize != 6 && boardSize != 8 && boardSize != 10));

            return boardSize;
        }

        public void GetIndexers(string i_PreviousLocation, string i_NextLocation, out int o_ISource, out int o_JSource, out int o_IDestination, out int o_JDestination)
        {
            eCapLetters sourceCapital, destinationCapital;
            eLowerLetters sourceLower, destinationLower;

            sourceCapital = (eCapLetters)Enum.Parse(typeof(eCapLetters), i_PreviousLocation.Substring(0, 1));
            sourceLower = (eLowerLetters)Enum.Parse(typeof(eLowerLetters), i_PreviousLocation.Substring(1, 1));
            destinationCapital = (eCapLetters)Enum.Parse(typeof(eCapLetters), i_NextLocation.Substring(0, 1));
            destinationLower = (eLowerLetters)Enum.Parse(typeof(eLowerLetters), i_NextLocation.Substring(1, 1));
            o_ISource = (int)sourceLower;
            o_JSource = (int)sourceCapital;
            o_IDestination = (int)destinationLower;
            o_JDestination = (int)destinationCapital;
        }

        public void GetValidInput(Board i_Board, out int o_ISource, out int o_JSource, out int o_IDestination, out int o_JDestination, ref bool io_Quit, out string o_InputMove)
        {
            string prevMove, nextMove, regexString;
            Regex regex;

            o_InputMove = Console.ReadLine();
            regexString = string.Format(@"[A-{0}][a-{1}]>[A-{0}][a-{1}]|^Q$", (eCapLetters)i_Board.BoardSize - 1, (eLowerLetters)i_Board.BoardSize - 1);
            regex = new Regex(regexString);

            while (!regex.IsMatch(o_InputMove))
            {
                Console.WriteLine("Illegal move, please enter a valid move(for example: Ab>Bc) or Q to forfeit:");
                PrintCurrentTurn();
                o_InputMove = Console.ReadLine();
            }

            if (o_InputMove == "Q")
            {
                io_Quit = true;
                o_ISource = o_JSource = o_IDestination = o_JDestination = -1;
            }
            else
            {
                io_Quit = false;
                prevMove = o_InputMove.Substring(0, 2);
                nextMove = o_InputMove.Substring(3, 2);
                GetIndexers(prevMove, nextMove, out o_ISource, out o_JSource, out o_IDestination, out o_JDestination);
            }

            while (!m_Gameplay.IsValidMove(i_Board, o_ISource, o_JSource, o_IDestination, o_JDestination) && !io_Quit)
            {
                Console.WriteLine("Illegal move, please enter a valid move(for example: Ab>Bc) or Q to forfeit:");
                PrintCurrentTurn();
                o_InputMove = Console.ReadLine();
                while (!regex.IsMatch(o_InputMove))
                {
                    Console.WriteLine("Illegal move, please enter a valid move(for example: Ab>Bc) or Q to forfeit:");
                    PrintCurrentTurn();
                    o_InputMove = Console.ReadLine();
                }

                if (o_InputMove == "Q")
                {
                    io_Quit = true;
                    o_ISource = o_JSource = o_IDestination = o_JDestination = -1;
                }
                else
                {
                    io_Quit = false;
                    prevMove = o_InputMove.Substring(0, 2);
                    nextMove = o_InputMove.Substring(3, 2);
                    GetIndexers(prevMove, nextMove, out o_ISource, out o_JSource, out o_IDestination, out o_JDestination);
                }
            }
        }

        public void PrintCurrentBoardStatus()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard();
        }

        public void PrintPlayerTurn(string i_PreviousMove)
        {
            if (m_Gameplay.Player1Turn)
            {
                Console.WriteLine("{0}'s move was (O): {1}", m_Player2.Name, i_PreviousMove);
                PrintCurrentTurn();
            }
            else
            {
                Console.WriteLine("{0}'s move was (X): {1}", m_Player1.Name, i_PreviousMove);
                PrintCurrentTurn();
            }
        }

        public void PrintCurrentTurn()
        {
            if (m_Gameplay.Player1Turn)
            {
                Console.Write("{0}'s turn (X): ", m_Player1.Name);
            }
            else
            {
                Console.Write("{0}'s turn (O): ", m_Player2.Name);
            }
        }

        public bool NextGameDialog()
        {
            string answer;

            do
            {
                Console.WriteLine("{0}'s score is: {1}", m_Player1.Name, m_Player1.Score);
                Console.WriteLine("{0}'s score is: {1}", m_Player2.Name, m_Player2.Score);
                Console.WriteLine("Would you like to play again? (yes or no)");
                answer = Console.ReadLine();
            }
            while (!answer.Equals("yes") && !answer.Equals("no"));

            return answer.Equals("yes");
        }

        public string TranslateComputerMoveToString(int i_ISource, int i_JSource, int i_IDestination, int i_JDestination)
        {
            StringBuilder computerMove = new StringBuilder();

            computerMove.Append((eCapLetters)i_JSource);
            computerMove.Append((eLowerLetters)i_ISource);
            computerMove.Append(">");
            computerMove.Append((eCapLetters)i_JDestination);
            computerMove.Append((eLowerLetters)i_IDestination);

            return computerMove.ToString();
        }

        public void RunCheckers()
        {
            int iSource, jSource, iDestination, jDestination;
            string winningTroop, inputMove;
            bool quit = false, nextGame = true;
            
            GetPlayerDetails(out m_Player1);
            m_Board = new Board(GetBoardSize());
            CheckOpponent(out m_Player2);
            while (nextGame)
            {
                PrintCurrentBoardStatus();
                Console.Write("{0}'s turn (X): ", m_Player1.Name);
                while (!m_Gameplay.IsGameOver(m_Board, quit, out winningTroop) && !m_Gameplay.IsDraw(m_Board) && !quit)
                {
                    if (m_Player2.IsComputer && !m_Gameplay.Player1Turn)
                    {
                        Move move = m_Gameplay.GetComputerMove(m_Board);
                        iSource = move.IPreviousMove;
                        jSource = move.JPreviousMove;
                        iDestination = move.INextMove;
                        jDestination = move.JNextMove;
                        inputMove = TranslateComputerMoveToString(iSource, jSource, iDestination, jDestination);
                    }
                    else
                    {
                        GetValidInput(m_Board, out iSource, out jSource, out iDestination, out jDestination, ref quit, out inputMove);
                    }

                    if (!quit)
                    {
                        m_Gameplay.Move(ref m_Board, iSource, jSource, iDestination, jDestination);
                        PrintCurrentBoardStatus();
                        while (m_Gameplay.AnyAdditionalMove(m_Board, iDestination, jDestination, Math.Abs(iDestination - iSource)))
                        {
                            PrintPlayerTurn(inputMove);
                            if (m_Player2.IsComputer && !m_Gameplay.Player1Turn)
                            {
                                Move move = m_Gameplay.GetComputerMove(m_Board);
                                iSource = move.IPreviousMove;
                                jSource = move.JPreviousMove;
                                iDestination = move.INextMove;
                                jDestination = move.JNextMove;
                                inputMove = TranslateComputerMoveToString(iSource, jSource, iDestination, jDestination);
                            }
                            else
                            {
                                GetValidInput(m_Board, out iSource, out jSource, out iDestination, out jDestination, ref quit, out inputMove);
                            }

                            m_Gameplay.Move(ref m_Board, iSource, jSource, iDestination, jDestination);
                            PrintCurrentBoardStatus();
                        }

                        m_Gameplay.SwitchTurn();
                        PrintPlayerTurn(inputMove);
                    }
                }

                Ex02.ConsoleUtils.Screen.Clear();
                if (winningTroop.Equals("X"))
                {
                    m_Player1.Score = m_Gameplay.CalculateScore(m_Board, winningTroop, quit);
                    Console.WriteLine("Congratulation!!! {0} has won!", m_Player1.Name);
                }
                else if (winningTroop.Equals("O"))
                {
                    m_Player2.Score = m_Gameplay.CalculateScore(m_Board, winningTroop, quit);
                    Console.WriteLine("Congratulation!!! {0} has won!", m_Player2.Name);
                }

                nextGame = NextGameDialog();
                quit = false;
                if (!m_Gameplay.Player1Turn)
                {
                    m_Gameplay.SwitchTurn();
                }

                m_Board = new Board(m_Board.BoardSize);
            }
        }
    }
}
