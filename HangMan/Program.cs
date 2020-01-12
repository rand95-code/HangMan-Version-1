using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangMan
{
    class HangManGame
    {   
        // START OF MAIN LOOP
        // =================================================
        static void Main()
        {
            Console.Title = ("HangMan");

            List<string> lettersGuessed = new List<string>();  //generate a list for guessed (input) letters
            List<string> mySecrets = new List<string>();       //generate a list for secret words

            //add secret words to the list ( mySecrets )
            mySecrets.Add("superwoman");    
            mySecrets.Add("nice");
            mySecrets.Add("powerful");
            mySecrets.Add("vip");

            int live = 5;  //initiate number of lives 

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to Hangman Game");

           
            Random r = new Random();  //create a random instance for random secret word selection
            int counter = 0;          //initiate a counter to indicate the index of secret word that is being used

            //start the random iteration through the secret words list (mySecrets)
            foreach (int i in Enumerable.Range(0, mySecrets.Count).OrderBy(x => r.Next()))
            {
                string secretword = mySecrets[i];
                Console.WriteLine("Playing secret word #{0}", counter);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Guess for a {0} letter Word ", secretword.Length);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("You have {0} live", live);

                counter++; //after each iteration, increase the counter by 1 

                while (live > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string input = Console.ReadLine();

                    //check if the input letter (or word) already exists in the lettersGuessed list
                    //if the input letter or word already exists, raise a message
                    if (lettersGuessed.Contains(input))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You Entered letter [{0}] already", input);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Try a different letter");
                        GetAlphabet(input);
                        continue;
                    }
                    //if the input doesn't score a conflict, add it to the list
                    else
                    {
                        lettersGuessed.Add(input);
                    }

                    //check if a full word is matching the secret word
                    if (IsWord(secretword, lettersGuessed))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(secretword);
                        Console.WriteLine("Congratulations");
                        lettersGuessed.Clear(); // after a successful guess for a full secret word, 
                                                // empty the list and start over for a new secret word.
                        break;
                    }
                    //check if a single letter is existing in the secret word
                    else if (secretword.Contains(input))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("wow nice entry");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        string letters = Isletter(secretword, lettersGuessed);
                        Console.Write(letters);

                    }

                    else   // when a wrong letter is entered
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Oops, letter not in my word");
                        live -= 1;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("You have {0} live", live);
                    }

                    Console.WriteLine();
                }

                if (live == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game over");

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("My secret Word is [ {0} ]", secretword);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Press any key to exit.");

                    break;

                }
                if (counter == mySecrets.Count())  //if successful guessing for all secret words has been made
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("CONGRATULATIONS!!! YOU WON THE GAME!! WOHOOOOO");
                    Console.WriteLine("Press any key to exit.");
                    break;
                }

            }

            Console.ReadKey(); //keep the console screen running until a ( break; ) statement is called.
        }
        // =================================================
        // END OF MAIN LOOP
        // =================================================



        // =================================================
        // START OF FUNCTIONS
        // =================================================

        // checking for word 
        static bool IsWord(string secreword, List<string> lettersGuessed)
        {

            bool match = false;

            if (secreword == lettersGuessed[0])
            {
                match = true;
            }
            else
            {

                // loop through secretword
                for (int i = 0; i < secreword.Length; i++)
                {
                    // initialize c with the index of secretword[i]
                    string c = Convert.ToString(secreword[i]);
                    // check if c is in list of letters Guess
                    if (lettersGuessed.Contains(c))
                    {
                        match = true;
                    }
                    /*if c is not in the letters guessed then we dont have the
                     * we dont have the full word*/
                    else
                    {
                        // change the value of word to false and return false
                        return match = false;
                    }
                }

            }
            return match;
        }

        // check for single letter 
        static string Isletter(string secretword, List<string> lettersGuessed)
        {
            // set guessedword as empty string
            string correctletters = "";
            // loop through secret word
            for (int i = 0; i < secretword.Length; i++)
            {
                /* initialize c with the value of index i
                 * mean when i = 0
                 * c = secretword[0] is the first index of secretword
                 * c = secretword[1] is the second index of secretword and so on */
                string c = Convert.ToString(secretword[i]);

                // if c is in list of lettersGuessed 
                if (lettersGuessed.Contains(c))
                {
                    // add c to correct letters
                    correctletters += c;
                }
                else
                {
                    // else print (__) to show that the letter is not in the secretword
                    correctletters += "_ ";
                }

            }
            // after looping return all the correct letters found
            return correctletters;

        }

        // The alphabet to use
        static void GetAlphabet(string letters)
        {
            List<string> alphabet = new List<string>();

            for (int i = 1; i <= 26; i++)
            {
                char alpha = Convert.ToChar(i + 96);
                alphabet.Add(Convert.ToString(alpha));
            }

            // for regulating the number of alphabet left
            int num = 49;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Letters Left are:");

            for (int i = 0; i < num; i++)
            {
                if (letters.Contains(letters))
                {
                    alphabet.Remove(letters);
                    num -= 1;
                }
                Console.Write("[" + alphabet[i] + "] ");
            }

            Console.WriteLine();

        }
        // =================================================
        // END OF FUNCTIONS
        // =================================================
    }
}