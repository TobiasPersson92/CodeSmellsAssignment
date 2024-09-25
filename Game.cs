using CodeSmellsAssignment.Common;
using CodeSmellsAssignment.ConsoleIO;
using CodeSmellsAssignment.Data;
using CodeSmellsAssignment.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsAssignment
{
    public class Game
    {
        private IConsoleInterface _consoleInterface;
        private IGameLogic _gameLogic;
        private IFileIO _fileIO;

        public Game(IConsoleInterface consoleInterface, IGameLogic gameLogic, IFileIO fileIO)
        {
            _consoleInterface = consoleInterface;
            _gameLogic = gameLogic;
            _fileIO = fileIO;
        }

        public void Run()
        {
            HumanPlayer humanPlayer = new HumanPlayer();
            humanPlayer.SetName(_consoleInterface.GetNameFromConsole());

            bool gameIsRunning = true;

            while (gameIsRunning)
            {
                string goalNumber = _gameLogic.CreateFourDigitNumberToGuess();

                _consoleInterface.DisplayMessage("New game:\n");
                _consoleInterface.DisplayMessage("For practice, number is: " + goalNumber + "\n"); //comment out or remove to play a real game!

                string currentGuess;
                string currentBullsAndCows;
                do
                {
                    humanPlayer.UpdateTotalGuessesBy(1);
                    currentGuess = _consoleInterface.GetUserInput();
                    _consoleInterface.DisplayMessage(currentGuess + "\n");
                    currentBullsAndCows = _gameLogic.ControlAmountOfBullsAndCows(goalNumber, currentGuess);
                    _consoleInterface.DisplayMessage(currentBullsAndCows + "\n");
                } while (currentBullsAndCows != "BBBB,");

                _consoleInterface.ShowWinningMessage(humanPlayer.TotalGuesses);

                _fileIO.SaveResultsToFile(humanPlayer.Name, humanPlayer.TotalGuesses);

                List<PlayerData> topPlayers = _fileIO.LoadResultsFromFile();
                _consoleInterface.DisplayPlayerData(topPlayers);

                if (_consoleInterface.AskToContinue() == false)
                {
                    gameIsRunning = false;
                }
            }
        }
    }
}
