using Microsoft.VisualStudio.TestTools.UnitTesting;
using ALE2_library.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE2_library.models;
using System.Diagnostics;

namespace ALE2_library.services.Tests
{
    [TestClass()]
    public class RegexReaderTests
    {

        //[DataRow(".(z, | (h, *(.(q, z))))")]
        static string testInputPath1 = "../../materials/input3.txt";
        const string testInputPath2 = "../../materials/input4.txt";
        const string testInputPath3 = "../../materials/input5.txt";
        const string testInputPath4 = "../../materials/input6.txt";
        const string testInputPath5 = "../../materials/input7.txt";
        const string testInputPath6 = "../../materials/input8.txt";
        static int indexer = 0;
        string value1 = "a";
        string value2 = "b";
        static Node q0 = new Node("q0");
        static Node q1 = new Node("q1");
        static Node q2 = new Node("q2");
        List<Transition> list1 = new List<Transition>(new Transition[]{
            new Transition("a", q0, q1),
            new Transition("_", q1, null),
            new Transition("b", q0, q2),
            new Transition("_", q2, null),
            });

        [DataTestMethod]

        [DataRow("|(.(a,b),a)", testInputPath2)]
        [DataRow(".(*(a),b)", testInputPath2)]
        [DataRow("|(*(a),b)", testInputPath2)]
        [DataRow(".(|(a,b),a)", testInputPath2)]
        [DataRow("*(|(a,b))", testInputPath2)]
        [DataRow("*(.(a,b))", testInputPath2)]
        [DataRow("|(a,b)", testInputPath2)]
        [DataRow(".(a,b)", testInputPath3)]
        [DataRow("*(a)", testInputPath4)]
        [DataRow("a", testInputPath5)]
        [DataRow("_", testInputPath6)]
        public void readRegex_passRegularExpression_returnParsedAutomaton(string input, string path)
        {
            //string input = ".(z, | (h, *(.(q, z))))";
            string graphInput = "../../materials/regexpParsedInput"+(indexer)+".txt";
            string graphOutput = "../../materials/regexpParsedOutput" + (indexer++) + ".txt";
            //Automaton expectedResult = Parser.parseFile(path);

            Automaton testResult = RegexReader.readRegex(input);
            GVgenerator.generateGraphVizInput(testResult, graphInput);
            GVgenerator.generateGraph(graphInput, graphOutput);
            //Assert.AreEqual(expectedResult, testResult);
        }
        [DataTestMethod]
        [DataRow("|.aba", 2, 2, 1)]
        [DataRow(".*ab", 4, 1, 1)]
        [DataRow("|*ab", 6, 1, 1)]
        [DataRow(".|aba", 2, 2, 1)]
        [DataRow("*|ab", 6, 1, 1)]
        [DataRow("*.ab", 4, 1, 1)]
        [DataRow("|ab", 2, 1, 1)]
        [DataRow(".ab", 0, 1, 1)]
        [DataRow("*a", 4, 1, 0)]
        [DataRow("a", 0, 1, 0)]
        [DataRow("_", 1, 0, 0)]
        public void readExpression_passRegularExpression_returnFullyParsedTransitions(string expression, int epsilonCount, int aCount, int bCount)
        {
            var result = new List<Transition>();
            RegexReader.readExpression(ref result, ref expression);
            result.ForEach(x => Trace.WriteLine(x));
            int total = epsilonCount + aCount + bCount;
            Assert.AreEqual(total, result.Count);
            Assert.AreEqual(epsilonCount, result.FindAll(x => x.Letter == "_").Count, "Number of '_' transitions");
            Assert.AreEqual(aCount, result.FindAll(x => x.Letter == "a").Count, "Number of 'a' transitions");
            Assert.AreEqual(bCount, result.FindAll(x => x.Letter == "b").Count, "Number of 'b' transitions");
        }
        [TestMethod()]
        public void readLetter_passExpression_returnParsedTransition()
        {
            string input = "a";
            Transition expectedResult = new Transition("a", new Node("q0"), null);
            var list = new List<Transition>();
            RegexReader.readLetter(ref list, input);
            Transition testResult = list[0];

            Assert.AreEqual(expectedResult.Letter, testResult.Letter);
        }

        [TestMethod()]
        public void readLetter_passNothing_returnParsedTransitionWithEpsilonAndLastNode()
        {
            Transition expectedResult = new Transition("_", new Node("q0"), null);

            var list = new List<Transition>();
            RegexReader.readLetter(ref list);
            Transition testResult = list[0];

            Assert.AreEqual(expectedResult.Letter, testResult.Letter);
        }
        [TestMethod()]
        public void readOrExpression_passListAndExpression_returnParsedTransitionsAndLastNode()
        {
            string input = value1 + value2;
            var testResult = new List<Transition>();
            RegexReader.readOr(ref testResult, ref input);
            testResult.ForEach(x => Trace.WriteLine(x));
            Assert.AreEqual(4, testResult.Count, "Number of all transitions");
            Assert.AreEqual(2, testResult.FindAll(x => x.Letter == "_").Count, "Number of '_' transitions");
            Assert.AreEqual(1, testResult.FindAll(x => x.Letter == "a").Count, "Number of 'a' transitions");
            Assert.AreEqual(1, testResult.FindAll(x => x.Letter == "b").Count, "Number of 'b' transitions");

        }
        [TestMethod()]
        public void readConcatExpression_passExpression_returnParsedTransitionsAndLastNode()
        {
            string input = value1 + value2;
            var testResult = new List<Transition>();
            RegexReader.readConcat(ref testResult, ref input);
            testResult.ForEach(x => Trace.WriteLine(x));
            Assert.AreEqual(2, testResult.Count, "Number of all transitions");
            Assert.AreEqual("a", testResult.First().Letter, "'a' transition is the first one");
            Assert.AreEqual(1, testResult.FindAll(x => x.Letter == "b").Count, "Number of 'b' transitions");
        }
        [TestMethod()]
        public void readStarExpression_passExpression_returnParsedTransitionsAndLastNode()
        {
            string input = value1 + value2;
            var testResult = new List<Transition>();
            RegexReader.readStar(ref testResult, ref input);
            testResult.ForEach(x => Trace.WriteLine(x));
            Assert.AreEqual(5, testResult.Count, "Number of all transitions");
            Assert.AreEqual(4, testResult.FindAll(x => x.Letter == "_").Count, "Number of '_' transitions");
            Assert.AreEqual(1, testResult.FindAll(x => x.Letter == "a").Count, "Number of 'a' transitions");
            Assert.AreEqual("b", input, "'a' was taken out of the input");
        }

        [TestCleanup]
        public void testClean()
        {
            RegexReader.indexerReset();
        }
    }
}