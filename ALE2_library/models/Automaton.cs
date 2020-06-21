using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.models
{
    public class Automaton
    {
        private List<string> alphabet;
        private List<Node> states;
        private List<Node> final;
        private List<Transition> transitions;
        private bool initialDFA;
        private bool initialFinite;
        private List<string> words;
        private List<Tuple<string, bool, bool>> wordsShowcase;
        private List<string> stack;
        private bool dfa;
        private bool finite;

        public Automaton(
                   List<string> alphabet,
                   List<Node> states,
                   List<Node> final,
                   List<Transition> transitions,
                   bool initialDFA = false,
                   bool initialFinite = false,
                   List<string> words = null, List<string> stack = null)
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

        public List<string> Alphabet { get => alphabet;}
        public List<Node> States { get => states;}
        public List<Node> Final { get => final;}
        public List<Transition> Transitions { get => transitions;}
        public bool InitialDFA { get => initialDFA;}
        public bool InitialFinite { get => initialFinite;}
        public List<string> Words { get => words; set => words = value; }
        public List<string> Stack { get => stack; set => stack = value; }
        public bool DFA { get => dfa; set => dfa = value; }
        public bool Finite { get => finite; set => finite = value; }
        public List<Tuple<string, bool, bool>> WordsShowcase { get => wordsShowcase; set => wordsShowcase = value; }

        public override bool Equals(object obj)
        {
            return obj is Automaton automaton &&
                   alphabet.All(x=>automaton.alphabet.Contains(x)) &&
                   states.All(x => automaton.states.Contains(x)) &&
                   final.All(x => automaton.final.Contains(x)) &&
                   transitions.All(x => automaton.transitions.Contains(x)) &&
                   stack == automaton.stack;
        }

        public override string ToString()
        {
            string message = "";
            message += "alphabet: ";
            alphabet.ForEach(x => message += x);
            message += "\nstack: "+ stack +"\nstates: ";
            states.ForEach(x => message += x.Name + ",");
            message = message.Remove(message.Length - 1, 1);
            message += "\nfinal: ";
            final.ForEach(x => message += x.Name + ",");
            message = message.Remove(message.Length - 1, 1);
            message += "\n\ntransitions: \n";
            transitions.ForEach(x => message += x.ToString()+"\n");
            message += "end.\n\ndfa: " + (initialDFA ? "y" : "n") + "\nfinite: " + (initialFinite ? "y" : "n") + "\n\nwords: \n";
            if(words!=null)
            words.ForEach(x => message += x + "\n");
            message += "end";
            return message;
        }
    }
}
