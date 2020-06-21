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
    public class FiniteCheckerTests
    {

        static Automaton automaton1 = Parser.parseFile("../../materials/input.txt");
        static Automaton automaton2 = Parser.parseFile("../../materials/NDFAinput.txt");
        private static Automaton automaton3 = RegexReader.readRegex(".(.(a,b),|(c,b))");
        static List<Automaton> automatons = 
            new List<Automaton>(new Automaton[] { automaton1, automaton2, automaton3 });
        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, false)]
        [DataRow(2, true)]
        public void isFiniteTest_passVisitedStatesAndStartStateAndAutomaton_returnsBool(int automatonIndex, bool expectedResult)
        {
            Automaton inputAutomaton = automatons[automatonIndex];
            List<Transition> inputTransitions = new List<Transition>();
            bool testResult = 
                FiniteChecker.isFinite(ref inputTransitions, inputAutomaton.States[0].Name, inputAutomaton);
            Assert.AreEqual(expectedResult, testResult);
        }
    }
}