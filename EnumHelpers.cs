using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Hangman
{
    internal static class EnumHelpers
    {

        public static Difficulties GetDifficulty(string difficulty)
        {
            switch (difficulty)
            {
                case "Hard":
                    return Difficulties.Hard;
                case "Normal":
                default:
                    return Difficulties.Normal;
            }
        }
    }
}
