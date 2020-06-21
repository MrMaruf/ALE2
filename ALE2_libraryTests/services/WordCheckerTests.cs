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
    public class WordCheckerTests
    {
        
        static Automaton automaton1 = Parser.parseFile("../../materials/input.txt");
        static Automaton automaton2 = Parser.parseFile("../../materials/NDFAinput.txt");
        static List<Automaton> automatons = new List<Automaton>(new Automaton[] {automaton1, automaton2});

        [DataTestMethod]
        [DataRow(0, "cabc")]
        [DataRow(1, "cab")]
        public void checkWordTest_passWordAndAutomatonAndCurrentState_ReturnsBool(int automatonIndex, string inputWord)
        {
            Automaton inputAutomaton = automatons[automatonIndex];
            Assert.IsTrue(WordChecker.checkWord(inputWord, inputAutomaton, inputAutomaton.States[0].Name));
        }

        [TestMethod()]
        public void doesNotGoFinaleTest_passPassedTransitionsAndStateAndRefResultAndAutomaton_returnsBool()
        {
            Automaton inputAutomaton = automaton2;
            Trace.WriteLine(inputAutomaton.Transitions.Count);
            Trace.WriteLine(inputAutomaton.Transitions[1].ToString());
            List<Transition> inputPassedTransitions = new List<Transition>(new Transition[]{inputAutomaton.Transitions[1]});
            bool result = true; 
            result = WordChecker.doesNotGoFinale(ref inputPassedTransitions, "4", ref result, inputAutomaton);
            Assert.IsFalse(result);
        }
    }
}