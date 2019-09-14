using NUnit.Framework;
using pigeon.utilities.extensions;
using System;
using System.Collections.Generic;

namespace PigeonEngineTest.utilities.extensions {
    [TestFixture]
    public class StringExtensionsTest {
        [TestCase("987654321", 5, "54321")]
        [TestCase("987654321", 0, "")]
        [TestCase("987654321", 20, "987654321")]
        public void Last(string input, int tailLength, string expected) {
            Assert.AreEqual(expected, input.Last(tailLength));
        }

        [TestCase("abcdefghijkl", "abc", "defghijkl")]
        [TestCase("__jeff__", "__", "jeff__")]
        public void After(string input, string after, string expected) {
            Assert.AreEqual(expected, input.After(after));
        }

        [TestCase("abcdefghijkl", "jkl", "abcdefghi")]
        [TestCase("__j__e__f__f__", "__", "__j__e__f__f")]
        public void Chop(string input, string toChop, string expected) {
            Assert.AreEqual(expected, input.Chop(toChop));
        }

        [TestCase("0", 0)]
        [TestCase("127", 127)]
        [TestCase("255", 255)]
        [TestCase("127", 127)]
        public void ToByte(string input, byte expected) {
            Assert.AreEqual(expected, input.ToByte());
        }

        [Test]
        public void ToByteExceptions() {
            Assert.That(() => "-1".ToByte(), Throws.TypeOf<OverflowException>());
            Assert.That(() => "256".ToByte(), Throws.TypeOf<OverflowException>());
            Assert.That(() => "byte".ToByte(), Throws.TypeOf<FormatException>());
        }

        [TestCase("ff", 255)]
        [TestCase("f", 15)]
        [TestCase("0f", 15)]
        public void HexToByte(string input, byte expected) {
            Assert.AreEqual(expected, input.HexToByte());
        }

        [Test]
        public void HexToByteExceptions() {
            Assert.That(() => "gg".HexToByte(), Throws.TypeOf<FormatException>());
            Assert.That(() => "-1".HexToByte(), Throws.TypeOf<ArgumentException>());
            Assert.That(() => "256".HexToByte(), Throws.TypeOf<OverflowException>());
            Assert.That(() => "byte".HexToByte(), Throws.TypeOf<FormatException>());
        }


        [TestCase("0", 0)]
        [TestCase("15", 15)]
        [TestCase("123456789", 123456789)]
        [TestCase("-1", -1)]
        [TestCase("-11111111", -11111111)]
        public void ToInt(string input, int expected) {
            Assert.AreEqual(expected, input.ToInt());
        }

        [Test]
        public void ToIntExceptions() {
            Assert.That(() => "-111111111111111".ToInt(), Throws.TypeOf<OverflowException>());
            Assert.That(() => "111111111111111".ToInt(), Throws.TypeOf<OverflowException>());
            Assert.That(() => "abcdef".ToInt(), Throws.TypeOf<FormatException>());
        }
        
        [TestCase("true", true)]
        [TestCase("false", false)]
        [TestCase("1", true)]
        [TestCase("0", false)]
        [TestCase("on", true)]
        [TestCase("off", false)]
        [TestCase("t", true)]
        [TestCase("f", false)]
        public void ToBool(string input, bool expected) {
            Assert.AreEqual(expected, input.ToBool());
        }

        [TestCase("0", 0.0)]
        [TestCase("0.1", 0.1)]
        [TestCase("0.5", 0.5)]
        [TestCase("0.55555", 0.55555)]
        [TestCase("0.99999", 0.99999)]
        [TestCase("1.0", 1.0)]
        [TestCase("1", 1.0)]
        public void ToUnitInterval(string input, double expected) {
            Assert.AreEqual(expected, input.ToUnitInterval());
        }

        public void ToUnitInterval_Exceptions() {
            Assert.IsNull("-1.0".ToUnitInterval());
            Assert.IsNull("1.5".ToUnitInterval());
            Assert.IsNull("some words".ToUnitInterval());
        }
        // ToVector2
        // ToRectangle
        // ToEnum
        // Tokenize

        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 10, 9)]
        //         {   1    }{   2  }{   3    }{   4   }{   5  }{   6    } {   7  }{   8    } {   9    }
        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 20, 5)]
        //         {       1        }{        2        }{       3         }{       4         }{   5    }
        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 40, 3)]
        //         {              1                    }{                  2                   }{   3  }
        public void SplitWrap(String inputStr, int inputWidth, int expectedLines) {
            List<string> actualLines = new List<string>();
            StringExtensions._wrapString(inputStr, (str) => str.Length * 1, inputWidth, (str) => actualLines.Add(str));
            Assert.AreEqual(expectedLines, actualLines.Count);
        }

        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 5, 17)]
        //         {}{  } {} {  } {} {       } {      } { } { } {    } { } {  } {} {  } {   } {} {     }
        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 100, 1)]
        //         {                                                                                   }
        public void SplitWrap_EdgeCases(String inputStr, int inputWidth, int expectedLines) {
            List<string> actualLines = new List<string>();
            StringExtensions._wrapString(inputStr, (str) => str.Length * 1, inputWidth, (str) => actualLines.Add(str));
            Assert.AreEqual(expectedLines, actualLines.Count);
        }
    }
}
