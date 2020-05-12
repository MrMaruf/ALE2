using Microsoft.VisualStudio.TestTools.UnitTesting;
using ALE2_library.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE2_library.models;

namespace ALE2_library.services.Tests
{
    [TestClass()]
    public class ParserTests
    {
        static Node testNode1 = new Node("TS1", new List<Transition>());
        static Node testNode2 = new Node("TS2", new List<Transition>());
        static Transition testTransition1 = new Transition('t', testNode1, testNode2);
        static string testInputPath = "../../../ALE2_library/materials/input.txt";
        [TestMethod()]
        public void alphabetLineParser_parsesLine_returnsTransition()
        {
            Transition expectedResponse = testTransition1;
            Assert.Fail();
        }

        [TestMethod()]
        public void fileParserTest_takesPath_returnsAutomaton()
        {
            string testPath = testInputPath;
            
            var file = Parser.fileParser(testPath);

            Assert.IsInstanceOfType(file, typeof(Automaton));
        }
    }
}