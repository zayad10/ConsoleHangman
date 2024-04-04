namespace Console_Hangman
{
    public class Program
    {
        //private static Difficulties difficulty = Difficulties.Normal;
        public static void Main(string[] args)
        {
            Console.WriteLine("Choose a word!");
            string word = Console.ReadLine();
            //difficulty = EnumHelpers.GetDifficulty(Console.ReadLine() ?? "Normal");

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