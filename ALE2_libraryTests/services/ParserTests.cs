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
    public class ParserTests
    {
        static Node testNode1 = new Node("TS1");
        static Node testNode2 = new Node("TS2");
        static Transition testTransition1 = new Transition("t", testNode1, testNode2);
        static string testInputPath1 = "../../materials/input.txt";
        static string testInputPath2 = "../../materials/input2.txt";
        
        [TestMethod()]
        public void parseFile_takesPath_returnsAutomatonObject()
        {
            string testInput = testInputPath1;

            var file = Parser.parseFile(testInput);

            Assert.IsInstanceOfType(file, typeof(Automaton));
        }
        [TestMethod()]
        public void parseTransitionLine_takesStringLineAndNodeList_returnFilledInTransition()
        {
            Transition expectedResponse = testTransition1;
            string testInput = "TS1,t --> TS2";
            List<Node> testStates = new List<Node>(new Node[] { testNode1, testNode2 });

            Transition testResult = Parser.parseTransitionLine(testInput, testStates);

            Assert.AreEqual(expectedResponse, testResult,
                "Test node: " + testResult.Start.Name + "," + testResult.Letter + " --> " + testResult.End.Name);
        }

        [TestMethod()]
        public void parseAlphabetLine_takesStringLine_returnsListOfStringsContainingAlphabet()
        {
            List<string> expectedResult = new List<string>(new string[] { "t", "e", "s" });
            string testInput = "alphabet: tes";
            
            var testResult = Parser.parseAlphabetLine(testInput);

            Assert.AreEqual(expectedResult.Count, testResult.Count);
            testResult.ForEach(x => Assert.IsTrue(expectedResult.Contains(x)));
        }

        [TestMethod()]
        public void parseStatesLine_takesStringLine_returnsListOfNodesContainingStates()
        {
            List<Node> expectedResult = new List<Node>(new Node[] { testNode1, testNode2 });
            string testInput = "states: TS1, TS2";

            var testResult = Parser.parseStatesLine(testInput);

            Assert.AreEqual(expectedResult.Count, testResult.Count);
            testResult.ForEach(x => Assert.IsTrue(expectedResult.Contains(x)));
        }

        [TestMethod()]
        public void parseFinalLine_takesStringLine_returnsListOfNodesContainingStates()
        {
            List<Node> expectedResult = new List<Node>(new Node[] { testNode2 });
            string testInput = "final: TS2";

            var testResult = Parser.parseStatesLine(testInput);

            Assert.AreEqual(expectedResult.Count, testResult.Count);
            testResult.ForEach(x => Assert.IsTrue(expectedResult.Contains(x)));
        }

        [TestMethod()]
        public void parseBoolLine_takesStringLine_returnsBoolean()
        {
            bool expectedResult = true;
            string testInput = "finite: y";

            var testResult = Parser.parseBoolLine(testInput);

            Assert.AreEqual(expectedResult, testResult);
        }

        [TestMethod()]
        public void parseWordsLine_takesStringLine_returnsTupleContainingWordAndInitialStatus()
        {
            var expectedResult = new Tuple<string, bool, bool>("test", true, false);
            string testInput = "test, y";

            var testResult = Parser.parseWordsLine(testInput);

            Assert.AreEqual(expectedResult, testResult);
        }

        [TestMethod()]
        public void parseFile_takesPath_returnsParsedAutomaton()
        {
            var states = new List<Node>(new Node[] { testNode1, testNode2 });
            var final = new List<Node>(new Node[] { testNode1 });
            var alphabet = new List<string>(new string[] { "t", "e", "s" });
            string stack = null;
            var transitions = new List<Transition>();
            transitions.Add(new Transition("t", testNode2, testNode1));
            transitions.Add(new Transition("e", testNode2, testNode1));
            transitions.Add(new Transition("s", testNode1, testNode2));
            transitions.Add(new Transition("t", testNode1, testNode2));
            transitions.Add(new Transition("e", testNode1, testNode2));
            var words = new List<string>();
            words.AddRange(new string[]{ "test", "set", "sit"});
            Automaton expectedResult = new Automaton(alphabet, states, final, transitions, true, false, words);
            string testInput = testInputPath2;
            
            var testResult = Parser.parseFile(testInput);

            Assert.AreEqual(expectedResult, testResult);
        }
    }
}