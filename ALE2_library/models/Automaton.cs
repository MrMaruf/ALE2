using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.models
{
    public class Automaton
    {
        private List<char> alphabet;
        private List<Node> states;
        private List<Node> final;
        private List<Transition> transitions;
        private bool initialDFA;
        private bool initialFinite;
        private List<string> words;
        private char stack;
        private bool dfa;
        private bool finite;

        public Automaton(
            List<char> alphabet, 
            List<Node> states, 
            List<Node> final, 
            List<Transition> transitions, 
            bool initialDFA, 
            bool initialFinite, 
            List<string> words)
        {
            this.alphabet = alphabet;
            this.states = states;
            this.final = final;
            this.transitions = transitions;
            this.initialDFA = initialDFA;
            this.initialFinite = initialFinite;
            this.words = words;
        }

        public Automaton(
                   List<char> alphabet,
                   List<Node> states,
                   List<Node> final,
                   List<Transition> transitions,
                   bool initialDFA,
                   bool initialFinite,
                   List<string> words, char stack)
        {
            this.alphabet = alphabet;
            this.states = states;
            this.final = final;
            this.transitions = transitions;
            this.initialDFA = initialDFA;
            this.initialFinite = initialFinite;
            this.words = words;
            this.stack = stack;
        }

        public List<char> Alphabet { get => alphabet;}
        public List<Node> States { get => states;}
        public List<Node> Final { get => final;}
        public List<Transition> Transitions { get => transitions;}
        public bool InitialDFA { get => initialDFA;}
        public bool InitialFinite { get => initialFinite;}
        public List<string> Words { get => words; set => words = value; }
        public char Stack { get => stack; set => stack = value; }
        public bool DFA { get => dfa; set => dfa = value; }
        public bool Finite { get => finite; set => finite = value; }
    }
}
