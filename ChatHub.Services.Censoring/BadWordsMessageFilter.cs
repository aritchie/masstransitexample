//http://james.newtonking.com/archive/2009/07/03/simple-net-profanity-filter
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace ChatHub.Services
{
    public class BadWordsMessageCensor : IMessageCensor
    {
        public IList<string> CensoredWords { get; set; } = new List<string>
        {
            "shit",
            "dick",
            "fuck"
        };


        public string Scrub(string message)
        {
            // TODO: cache the regex creation
            foreach (var word in this.CensoredWords)
            {
                var regularExpression = ToRegexPattern(word);

                message = Regex.Replace(
                    message,
                    regularExpression,
                    StarCensoredMatch,
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant
                );
            }

            return message;
        }


        static string StarCensoredMatch(Match m)
        {
            var word = m.Captures[0].Value;
            return new string('*', word.Length);
        }


        static string ToRegexPattern(string wildcardSearch)
        {
            var regexPattern = Regex
                .Escape(wildcardSearch)
                .Replace(@"\*", ".*?")
                .Replace(@"\?", ".");

            if (regexPattern.StartsWith(".*?"))
            {
                regexPattern = regexPattern.Substring(3);
                regexPattern = @"(^\b)*?" + regexPattern;
            }

            regexPattern = @"\b" + regexPattern + @"\b";

            return regexPattern;
        }
    }
}
