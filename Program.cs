namespace Console_Hangman
{
    public class Program
    {
        //private static Difficulties difficulty = Difficulties.Normal;
        public static void Main(string[] args)
        {
            Console.WriteLine("Choose a word!");

            var word = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && word.Length > 0)
                {
                    Console.Write("\b \b");
                    word = word[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    word += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Game game = new Game(word);
            game.DrawStart();
            while (game.GameIsOver() != true)
            {
                Console.WriteLine(game.AskForLetter());
                char guessedLetter = Console.ReadKey().KeyChar;
                Console.WriteLine(game.CheckLetter(guessedLetter));
            }
        }
    }
}