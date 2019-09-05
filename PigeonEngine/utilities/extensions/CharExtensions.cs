namespace pigeon.utilities.extensions {
    public static class CharExtensions {
        public static char ToLower(this char character) {
            return char.ToLower(character);
        }

        public static bool IsLower(this char character) {
            return char.IsLower(character);
        }

        public static int ParseIntDigit(this char character) {
            return (int) char.GetNumericValue(character);
        }

        public static int Ascii(this char character) {
            return character;
        }
    }
}
