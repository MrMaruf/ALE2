using Microsoft.VisualStudio.TestTools.UnitTesting;
using ALE2_library.services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE2_library.models;

namespace ALE2_library.services.Tests
{
    [TestClass()]
    public class StackCheckerTests
    {
        static Automaton automaton1 = Parser.parseFile("../../materials/pda_input.txt");
        private static Automaton automaton2 = RegexReader.readRegex(".(.(a,b),|(c,b))");
        static List<Automaton> automatons =
            new List<Automaton>(new Automaton[] { automaton1, automaton2 });
        [DataTestMethod]
        [DataRow("abc", false)]
        [DataRow("ca", false)]
        [DataRow("a", false)]
        [DataRow("", true)]
        [DataRow("_", true)]
        public void checkWordPDATest_passStateAndStackAndWordAndAutomaton_returnsBool
            (string inputWord, bool expectedResult)
        {
            Automaton inputAutomaton = automatons[0];
            List<Transition> inputTransitions = new List<Transition>();

            Trace.WriteLine(inputWord);
            //inputAutomaton.Transitions.ForEach(x => Trace.WriteLine(x.ToString()));
            Trace.WriteLine("Check!");
            bool testResult =
                StackChecker.checkWordPDA(inputWord, inputAutomaton, inputAutomaton.States[0].Name,
                    "", ref inputTransitions);
            Assert.AreEqual(expectedResult, testResult);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Automaton do NOT have a stack! It's NOT a PDA!")]
        public void checkWordPDATest_passWrongAutomaton_throwsException()
        {
            Automaton inputAutomaton = automatons[1];
            List<Transition> inputTransitions = new List<Transition>();
            bool testResult =
                StackChecker.checkWordPDA("a", inputAutomaton, inputAutomaton.States[0].Name,
                    "",ref inputTransitions );
        }
        [TestMethod()]
        public void doesNotGoFinalePDATest_passRefPassedTransitionsAndStateAndStackAndRefResultAndAutomaton_returnsBool()
        {
            Automaton inputAutomaton = automatons[0];
            List<Transition> inputTransitions = new List<Transition>();
            bool testResult = true;
            testResult = 
                StackChecker.doesNotGoFinalePDA(ref inputTransitions, "1", "", ref testResult, inputAutomaton);
            Assert.IsFalse(testResult);
        }
    }
}