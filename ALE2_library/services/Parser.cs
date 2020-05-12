using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class Parser
    {
        public static Transition alphabetLineParser(string line)
        {
            return null;
        }
        public static Automaton fileParser(string path)
        {
            List<char> alphabet = new List<char>();
            List<Node> states = new List<Node>();
            List<Node> final = new List<Node>();
            List<Transition> transitions = new List<Transition>();
            bool initialDFA = false;
            bool initialFinite = false;
            List<string> words = new List<string>();
            
            List<string> lines = File.ReadAllLines(path).ToList();
            for (int i = 0; i < lines.Count; i++)
            {

            }
            Automaton automaton = new Automaton(alphabet, states, final, transitions, initialDFA, initialFinite, words);
            return automaton;
        }
    }
}
