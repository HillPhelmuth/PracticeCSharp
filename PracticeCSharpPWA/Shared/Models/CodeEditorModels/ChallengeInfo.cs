using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCSharpPWA.Shared.Models.CodeEditorModels
{
    public static class ChallengeInfo
    {
        public const string BracesDescription = "<p>Write a function that takes a string of braces, and determines if the order of the braces is valid. It should return true if the string is valid, and false if it's invalid. All input strings will be nonempty, and will only consist of parentheses, brackets and curly braces: ()[]{}.</p>";
        public const string BracesExamples = "<div>\"(){}[]\" => True<br />\"([{}])\" => True<br />\"(}\" => False<br />\"[(])\" => False<br />\"[({})](]\" => False </div>";
        public const string PrimeDescription = "<p>Define a function that takes an integer argument and returns logical value true or false depending on if the integer is a prime.\nPer Wikipedia, a prime number(or a prime) is a natural number greater than 1 that has no positive divisors other than 1 and itself.</p>";
        public const string PrimeExamples =
            "<div>is_prime(1)  => false<br/>is_prime(2)  => true<br/>is_prime(-1) => false<br/></div>";

        public const string Rot13Description = "<p>ROT13 is a simple letter substitution cipher that replaces a letter with the letter 13 letters after it in the alphabet.ROT13 is an example of the Caesar cipher.<br/>Create a function that takes a string and returns the string ciphered with Rot13. If there are numbers or special characters included in the string, they should be returned as they are. Only letters from the latin/english alphabet should be shifted, like in the original Rot13 \"implementation\".</p>";
        public const string Rot13Examples = "<div>Rot13(Grfg) => \"Test\"</div>";
    }

}
