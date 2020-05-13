using Microsoft.VisualStudio.TestTools.UnitTesting;
using ALE2_library.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE2_library.models;
using System.IO;

namespace ALE2_library.services.Tests
{
    [TestClass()]
    public class GVgeneratorTests
    {

        [TestMethod()]
        public void generateGraphVizInput_takesAutomatonAndPath_generatesInputForGraphViz()
        {
            Node testNode1 = new Node("TS1");
            Node testNode2 = new Node("TS2");
            var states = new List<Node>(new Node[] { testNode1, testNode2 });
            var final = new List<Node>(new Node[] { testNode1 });
            var alphabet = new List<string>(new string[] { "t", "e", "s" });
            string stack = null;
            var transitions = new List<Transition>();
            transitions.Add(new Transition("t", testNode2, testNode1));
            transitions.Add(new Transition("e", testNode2, testNode1));
            transitions.Add(new Transition("s", testNode2, testNode2));
            transitions.Add(new Transition("t", testNode1, testNode2));
            transitions.Add(new Transition("e", testNode1, testNode2));
            transitions.Add(new Transition("s", testNode1, testNode2));
            var words = new List<string>();
            words.AddRange(new string[] { "test", "set", "sit" });
            Automaton testInput = new Automaton(alphabet, states, final, transitions, true, false, words);

            GVgenerator.generateGraphVizInput(testInput, "../../materials/GVgeneratorInputTest.txt");

            Assert.AreEqual(
                File.ReadAllText("../../materials/graphvizInput.txt"),
                File.ReadAllText("../../materials/GVgeneratorInputTest.txt"));
        }

        [TestMethod()]
        public void generateGraph_takesInputAndOutputPaths_generatesPictureAtOutputPath()
        {
            Node testNode1 = new Node("TS1");
            Node testNode2 = new Node("TS2");
            var states = new List<Node>(new Node[] { testNode1, testNode2 });
            var final = new List<Node>(new Node[] { testNode1 });
            var alphabet = new List<string>(new string[] { "t", "e", "s" });
            string stack = null;
            var transitions = new List<Transition>();
            transitions.Add(new Transition("t", testNode2, testNode1));
            transitions.Add(new Transition("e", testNode2, testNode1));
            transitions.Add(new Transition("s", testNode2, testNode2));
            transitions.Add(new Transition("t", testNode1, testNode2));
            transitions.Add(new Transition("e", testNode1, testNode2));
            transitions.Add(new Transition("s", testNode1, testNode2));
            var words = new List<string>();
            words.AddRange(new string[] { "test", "set", "sit" });
            Automaton testInput = new Automaton(alphabet, states, final, transitions, true, false, words);

            GVgenerator.generateGraph("D:/projects/ALE2/ALE2_libraryTests/materials/graphvizInput.txt", "D:/projects/ALE2/ALE2_libraryTests/materials/testPicture");

            Assert.IsTrue(File.Exists("../../materials/testPicture.png"));
        }
    }
}