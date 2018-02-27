﻿using System;
using System.Linq;
using FuzzySharp.Algorithms;
using FuzzySharp.Ratios;

namespace FuzzySharp
{
    public static class Fuzzy
    {
        /// <summary>
        /// Calculates a Levenshtein simple ratio between the strings.
        /// This indicates a measure of similarity
        /// </summary>
        /// <param name="s1">Input String</param>
        /// <param name="s2">Input String</param>
        /// <param name="options">Optional options to handle the input strings</param>
        /// <returns>The Simple Ratio</returns>
        public static int Ratio(string s1, string s2, params StringOptions[] options)
        {
            s1 = Prepare(s1, options);
            s2 = Prepare(s2, options);

            return new SimpleRatio().Score(s1, s2);
        }

        /// <summary>
        /// Inconsistent substrings lead to problems in matching. This ratio
        /// uses a heuristic called "best partial" for when two strings 
        /// are of noticeably different lengths.
        /// </summary>
        /// <param name="s1">Input String</param>
        /// <param name="s2">Input String</param>
        /// <param name="options">Optional options to handle the input strings</param>
        /// <returns>The Partial Ratio</returns>
        public static int PartialRatio(string s1, string s2, params StringOptions[] options)
        {
            s1 = Prepare(s1, options);
            s2 = Prepare(s2, options);

            return new PartialRatio().Score(s1, s2);
        }

        /// <summary>
        /// Find all alphanumeric tokens in the string, sort those tokens,
        /// and then take ratio of resulting joined strings.
        /// </summary>
        /// <param name="s1">Input String</param>
        /// <param name="s2">Input String</param>
        /// <param name="options">Optional options to handle the input strings</param>
        /// <returns>The full ratio of the strings</returns>
        public static int TokenSortRatio(string s1, string s2, params StringOptions[] options)
        {
            s1 = Prepare(s1, options);
            s2 = Prepare(s2, options);
            return new TokenSort().Score(s1, s2, new SimpleRatio());
        }
        
        /// <summary>
        /// Find all alphanumeric tokens in the string, sort those tokens,
        /// and then take ratio of resulting joined strings.
        /// </summary>
        /// <param name="s1">Input String</param>
        /// <param name="s2">Input String</param>
        /// <param name="options">Optional options to handle the input strings</param>
        /// <returns>The Partial ratio of the strings</returns>
        public static int TokenSortPartialRatio(string s1, string s2, params StringOptions[] options)
        {
            s1 = Prepare(s1, options);
            s2 = Prepare(s2, options);
            return new TokenSort().Score(s1, s2, new PartialRatio());
        }

        /// <summary>
        /// Splits the strings into tokens and computes intersections and remainders between the tokens of the two strings.
        /// A comparison string is then built up and is compared using the simple ratio algorithm.
        /// Useful for strings where words appear redundantly.
        /// </summary>
        /// <param name="s1">Input String</param>
        /// <param name="s2">Input String</param>
        /// <param name="options">Optional options to handle the input strings</param>
        /// <returns>The Ratio of similarity</returns>
        public static int TokenSetRatio(string s1, string s2, params StringOptions[] options)
        {
            s1 = Prepare(s1, options);
            s2 = Prepare(s2, options);
            return new TokenSet().Score(s1, s2, new SimpleRatio());
        }
        
        /// <summary>
        /// Splits the strings into tokens and computes intersections and remainders between the tokens of the two strings.
        /// A comparison string is then built up and is compared using the simple ratio algorithm.
        /// Useful for strings where words appear redundantly.
        /// </summary>
        /// <param name="s1">Input String</param>
        /// <param name="s2">Input String</param>
        /// <param name="options">Optional options to handle the input strings</param>
        /// <returns>The Partial Ratio of similarity</returns>
        public static int TokenSetPartialRatio(string s1, string s2, params StringOptions[] options)
        {
            s1 = Prepare(s1, options);
            s2 = Prepare(s2, options);
            return new TokenSet().Score(s1, s2, new PartialRatio());
        }

        /// <summary>
        /// Calculates a weighted ratio between the different algorithms for best results.
        /// </summary>
        /// <param name="s1">Input String</param>
        /// <param name="s2">Input String</param>
        /// <param name="options">Optional options to handle the input strings</param>
        /// <returns>The Ratio of similarity</returns>
        public static int WeightedRatio(string s1, string s2, params StringOptions[] options)
        {
            s1 = Prepare(s1, options);
            s2 = Prepare(s2, options);
            return new WeightedRatio().Score(s1, s2);
        }

        private static string Prepare(string value, params StringOptions[] options)
        {
            if (string.IsNullOrEmpty(value)) 
                throw new ArgumentNullException();

            if (options.All(x => x != StringOptions.CaseSensitive))
                value = value.ToLower();

            if (options.All(x => x != StringOptions.PreserveWhitespace))
                value = value.Trim();
                
            return value;
        }
    }
}