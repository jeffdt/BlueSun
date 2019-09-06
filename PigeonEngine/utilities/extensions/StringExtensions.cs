using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PigeonEngine.utilities.extensions;

namespace pigeon.utilities.extensions {
    public static class StringExtensions {
        private static readonly Regex quoteSplitter = new Regex(@"[\""].+?[\""]|[^ ]+");
        private static readonly char[] COMMA_SEPARATOR = { ',' };
        private static readonly char[] SPACE_SEPARATOR = { ' ' };

        public static string Last(this string source, int tail_length) {
            return tail_length >= source.Length ? source : source.Substring(source.Length - tail_length);
        }

        public static string After(this string source, string openingString) {
            return source.Substring(openingString.Length, source.Length - openingString.Length);
        }

        public static string[] SplitArgs(this string str) {
            return str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitArgsWithQuotes(string args) {
            return quoteSplitter.Matches(args).Cast<Match>().Select(m => m.Value).ToArray();
        }

        public static byte ToByte(this string str) {
            return Convert.ToByte(str);
        }

        public static int ToInt(this string str) {
            return Convert.ToInt32(str);
        }

        public static float ToFloat(this string str) {
            return Convert.ToSingle(str);
        }

        public static double ToDouble(this string str) {
            return Convert.ToDouble(str);
        }

        public static bool ToBool(this string str) {
            return Convert.ToBoolean(str);
        }

        public static double? ToUnitInterval(this string str) {
            double result;
            bool parsed = double.TryParse(str, out result);
            return (parsed && result >= 0f && result <= 1f) ? result : (double?) null;
        }

        public static Vector2 ToVector2(this string str) {
            string[] values = str.Split(COMMA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != 2) {
                throw new FormatException(string.Format("cannot parse '{0}' into a vector because it does not contain an X and Y component in the format 'x,y'.", str));
            }

            return new Vector2(Convert.ToSingle(values[0]), Convert.ToSingle(values[1]));
        }

        public static Rectangle ToRectangle(this string str) {
            string[] values = str.Split(COMMA_SEPARATOR, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != 4) {
                throw new FormatException(string.Format("cannot parse '{0}' into a rectangle because it does not contain X, Y, width and height components in the format 'x,y,width,height'.", str));
            }

            return new Rectangle(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), Convert.ToInt32(values[3]));
        }

        public static T ToEnum<T>(this string str) {
            return (T) Enum.Parse(typeof(T), str, true);
        }

        public static string[] Tokenize(this string str) {
            return str.Split(SPACE_SEPARATOR, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string FormatWrap(this string text, SpriteFont font, int width, int maxLines = 0) {
            StringBuilder finalString = new StringBuilder();

            string[] words = text.Split(' ');
            string line = string.Empty;
            int lineCount = 1;

            foreach (string word in words) {
                if (font.MeasureWidth(line + word) > width) {
                    if (maxLines != 0 && lineCount >= maxLines) {
                        throw new ArgumentException(string.Format("cannot format string into {0} lines. original string: {1}", maxLines, text));
                    }

                    finalString.Append(line + '\n');
                    line = string.Empty;
                    lineCount++;
                }

                line = line + word + ' ';
            }

            return finalString.Append(line).ToString();
        }

        public static List<string> SplitWrap(this string text, SpriteFont font, int width) {
            if (font.MeasureWidth(text) <= width) {
                return new List<string> { text };
            }

            List<string> lines = new List<string>();

            string remainingText = text;
            while (font.MeasureWidth(remainingText) > width) {    // still more lines to parse
                //parse into two lines
                int charIndex = 0;
                while (font.MeasureWidth(remainingText.Substring(0, charIndex)) <= width) {
                    charIndex++;
                }
                lines.Add(remainingText.Substring(0, charIndex));
                remainingText = remainingText.Substring(charIndex);
            }

            if (remainingText.Length > 0) {
                lines.Add(remainingText);
            }

            return lines;
        }
    }
}
