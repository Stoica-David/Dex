using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dex.ViewModels
{
    public class GameVM : BaseVM
    {
        private List<Word> randomWords;
        private int currIndex;
        private bool stopDisplaying;

        // PROPERTIES
        private int guessedWordsNumber;
        public int GuessedWordsNumber
        {
            get
            {
                return guessedWordsNumber;
            }

            set
            {
                guessedWordsNumber = value;
                OnPropertyChanged();
            }
        }

        private string descriptionLabel;
        public string DescriptionLabel
        {
            get
            {
                return descriptionLabel;
            }

            set
            {
                descriptionLabel = value;
                OnPropertyChanged();
            }
        }

        private string imageLabel;
        public string ImageLabel
        {
            get
            {
                return imageLabel;
            }

            set
            {
                imageLabel = value;
                OnPropertyChanged();
            }
        }

        private string selectedGuess;
        public string SelectedGuess
        {
            get
            {
                return selectedGuess;
            }

            set
            {
                selectedGuess = value;
                OnPropertyChanged();
            }
        }

        private string guessedLabel;
        public string GuessedLabel
        {
            get
            {
                return "Guessed Words: " + guessedWordsNumber;
            }

            set
            {
                guessedLabel = value;
                OnPropertyChanged();
            }
        }

        private string gameOverLabel;
        public string GameOverLabel
        {
            get
            {
                return gameOverLabel;
            }

            set
            {
                gameOverLabel = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage selectedImage;
        public BitmapImage SelectedImage
        {
            get 
            { 
                return selectedImage; 
            }

            set
            {
                selectedImage = value;
                OnPropertyChanged();
            }
        }

        private string selectedDescription;
        public string SelectedDescription
        {
            get
            {
                return selectedDescription;
            }

            set
            {
                selectedDescription = value;
                OnPropertyChanged();
            }
        }

        // COMMANDS

        private ICommand switchToMenuCommand;
        public ICommand SwitchToMenuCommand
        {
            get
            {
                if (switchToMenuCommand == null)
                {
                    switchToMenuCommand = new RelayCommand(o => true, o => { OnSwitchToMenu(); });
                }

                return switchToMenuCommand;
            }
        }

        private ICommand buttonGuessCommand;
        public ICommand ButtonGuessCommand
        {
            get
            {
                if (buttonGuessCommand == null)
                {
                    buttonGuessCommand = new RelayCommand(o => true, ButtonClickGuess);
                }

                return buttonGuessCommand;
            }
        }

        private ICommand buttonFinishCommand;
        public ICommand ButtonFinishCommand
        {
            get
            {
                if (buttonFinishCommand == null)
                {
                    buttonFinishCommand = new RelayCommand(o => true, ButtonClickFinish);
                }

                return buttonFinishCommand;
            }
        }

        // DELEGATES

        public delegate void SwitchToMenu();
        public SwitchToMenu OnSwitchToMenu { get; set; }


        // METHODS

        public GameVM()
        {
            randomWords = DexManager.Instance.Get5RandomWords();
            currIndex = 0;
            GuessedWordsNumber = 0;
            stopDisplaying = false;

            Random random = new Random();

            int randomNumber = random.Next(0, 101);

            if (randomNumber % 2 == 0)
            {
                selectedDescription = randomWords[currIndex].Description;
                selectedImage = null;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(randomWords[currIndex].Path, UriKind.Relative);
                bitmap.EndInit();

                selectedImage = bitmap;
                selectedDescription = string.Empty;
            }
        }

        public void ButtonClickGuess(object param)
        {
            if(stopDisplaying)
            {
                return;
            }

            if (randomWords[currIndex].Name.ToLower() == SelectedGuess.ToLower())
            {
                MessageBox.Show("Correct! :)");
                GuessedWordsNumber++;
                GuessedLabel = "Guessed Words: " + GuessedWordsNumber;
            }
            else
            {
                MessageBox.Show("Incorrect! :(");
            }

            if (currIndex < 4)
            {
                currIndex = currIndex + 1;
            }
            else
            {
                stopDisplaying = true;
                GameOverLabel = "Game Over!";
            }

            SelectedGuess = string.Empty;
            SelectedDescription = string.Empty;
            SelectedImage = null;

            if (!stopDisplaying)
            {
                Random random = new Random();

                int randomNumber = random.Next(0, 101);

                if (randomNumber % 2 == 0)
                {
                    SelectedDescription = randomWords[currIndex].Description;
                    SelectedImage = null;
                }
                else
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(randomWords[currIndex].Path, UriKind.Relative);
                    bitmap.EndInit();

                    SelectedImage = bitmap;
                    SelectedDescription = string.Empty;
                }
            }
        }

        public void ButtonClickFinish(object param)
        {
            randomWords = DexManager.Instance.Get5RandomWords();
            currIndex = 0;
            GuessedWordsNumber = 0;
            stopDisplaying = false;
            GameOverLabel = string.Empty;
            SelectedDescription = string.Empty;
            SelectedImage = null;
            GuessedLabel = "Guessed Words: " + GuessedWordsNumber;

            Random random = new Random();

            int randomNumber = random.Next(0, 101);

            if (randomNumber % 2 == 0)
            {
                SelectedDescription = randomWords[currIndex].Description;
                SelectedImage = null;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(randomWords[currIndex].Path, UriKind.Relative);
                bitmap.EndInit();

                SelectedImage = bitmap;
                SelectedDescription = string.Empty;
            }
        }
    }
}
