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
        public void Last(string input, int tailLength, string result) {
            Assert.AreEqual(result, input.Last(tailLength));
        }

        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 5, 17)]
        //         {}{  } {} {  } {} {       } {      } { } { } {    } { } {  } {} {  } {   } {} {     }
        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 10, 9)]
        //         {   1    }{   2  }{   3    }{   4   }{   5  }{   6    } {   7  }{   8    } {   9    }
        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 20, 5)]
        //         {       1        }{        2        }{       3         }{       4         }{   5    }
        [TestCase("I want to test my SplitWrap function but I'm really not sure if it's going to work...", 40, 3)]
        //         {              1                    }{                  2                   }{   3  }
        public void SplitWrap(String inputStr, int inputWidth, int expectedLines) {
            

            List<string> actualLines = new List<string>();
            StringExtensions._wrapString(inputStr, (str) => str.Length * 1, inputWidth, (str) => actualLines.Add(str));
            
            foreach(var line in actualLines) {
                Console.Write(line);
            }

            Assert.AreEqual(expectedLines, actualLines.Count);
        }
    }
}
