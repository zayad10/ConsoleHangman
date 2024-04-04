using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Hangman
{
    internal class Game
    {
        private string _word { get; set; }
        private char[] _wordAsLetters { get; set; }

        private char[] _correctlyGuessedLetters;
        private int _currentStage { get; set; } = 0;
        private bool _gameOver { get; set; } = false;

        public bool _won { get; private set; } = false;

        private List<char> _alreadyGuessedLetters { get; set; }

        private readonly string[] _hangMan = new string[] {
                                                            @"  +---+
                                                            |   |
                                                                |
                                                                |
                                                                |
                                                                |
                                                        =========",
                                                            @"  +---+
                                                            |   |
                                                            O   |
                                                                |
                                                                |
                                                                |
                                                        =========",
                                                            @"  +---+
                                                            |   |
                                                            O   |
                                                            |   |
                                                                |
                                                                |
                                                        =========",
                                                            @"  +---+
                                                            |   |
                                                            O   |
                                                           /|   |
                                                                |
                                                                |
                                                        =========",
                                                            @"  +---+
                                                            |   |
                                                            O   |
                                                           /|\  |
                                                                |
                                                                |
                                                        =========",
                                                            @"  +---+
                                                            |   |
                                                            O   |
                                                           /|\  |
                                                           /    |
                                                                |
                                                        =========",
                                                            @"  +---+
                                                            |   |
                                                            O   |
                                                           /|\  |
                                                           / \  |
                                                                |
                                                        ========="
                                                        };

        public Game(string word)
        {
            this._word = word;
            this._wordAsLetters = word.ToLower().ToCharArray();
            this._correctlyGuessedLetters = Enumerable.Repeat('_', word.Length).ToArray();
            this._alreadyGuessedLetters = new List<char>();
        }

        public string AskForLetter()
        {
            return "\nPlease enter a letter you think is contained in the word and press enter.";
        }

        public string CheckLetter(char letter)
        {
            letter = char.ToLower(letter);
            if (this._alreadyGuessedLetters.Contains(letter))
            {
                return $"\nYouve already tried to guess the letter {letter} previously";
            }
            else
            {
                List<int> checkLetterResult = FindCorrectLetterIndex(letter);
                if (checkLetterResult.Any())
                {
                    if ((_correctlyGuessedLetters.Where(c => !c.Equals('_')).Count()) == _word.Length)
                    {
                        DeclareWon();
                        return String.Empty;
                    }
                    else
                    {
                        return CorrectLetter(checkLetterResult);
                    }
                }
                else
                {
                    return this.IncorrectLetter();
                }
            }
        }

        private string CorrectLetter(List<int> correctLetterIndexes)
        {
            Console.WriteLine($"\n\nThat is a correctly guessed letter! You've found the letter at position(s) {String.Join(", ", correctLetterIndexes.Select(i => i + 1))}!\n" +
                $"Still have {(_wordAsLetters.Length - _correctlyGuessedLetters.Where(c => !c.Equals('_')).Count())} correct letters still to guess!" +
                $"\nThe word you have guessed so far is {string.Join("",_correctlyGuessedLetters)}");
            return this._hangMan[_currentStage].ToString();
        }

        private string IncorrectLetter()
        {
            this._currentStage++;
            if (GameIsOver())
            {
                DeclareLost();
                return String.Empty;
            }
            else
            {
                Console.WriteLine($"\nUnfortunately that is an incorrect letter and you are now on level {(_currentStage)}.\n" +
                $"There are only {(6 - _currentStage)} stages left before game is over.");
            }
            
            return this._hangMan[_currentStage].ToString();
        }

        private List<int> FindCorrectLetterIndex(char letter)
        {
            this._alreadyGuessedLetters.Add(letter);
            List<int> correctLetterIndexes = [];
            for (int i = 0; i < _wordAsLetters.Length; i++)
            {
                if (_wordAsLetters[i] == letter)
                {
                    correctLetterIndexes.Add(i);
                    this._correctlyGuessedLetters[i] = letter;
                }
            }

            return correctLetterIndexes;
        }

        public bool GameIsOver()
        {
            if (_currentStage == 6)
            {
                this._gameOver = true;
            }

            else if (this._correctlyGuessedLetters.Where(c => !c.Equals('_')).Count() == this._wordAsLetters.Length)
            {
                this._won = true;
                this._gameOver = true;
            }
            
            return this._gameOver;
        }

        public void DrawStart()
        {
            Console.WriteLine(this._hangMan[0]);
        }

        public bool GameWon()
        {
            return this._won;
        }

        public void DeclareLost()
        {
            Console.WriteLine($"\n\nUnfortunately you've lost the game! The word was {this._word}. Sadly, the hangman is now complete. {_hangMan[this._currentStage]}");
        }

        public void DeclareWon()
        {
            Console.WriteLine($"\n\nCongratulations you've won the game! and successfully guessed the word {this._word} !.");
        }
    }
}
