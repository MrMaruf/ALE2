using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class FiniteChecker
    {
        public static bool isFinite(Automaton automaton)
        {
            List<Transition> transitions = new List<Transition>();
            bool finite = FiniteChecker.isFinite(ref transitions, automaton.States[0].Name, automaton);
            return finite;
        }
        public static bool isFinite(ref List<Transition> visitedStates, string startState, Automaton automaton)
        {
            bool result = true;
            bool temp_goes_to_final = true;
            List<Transition> possibleTransitions = automaton.Transitions.Where(x => x.Start.Name == startState).ToList();
            foreach (Transition t in possibleTransitions)
            {
                List<Transition> passedTransitions = new List<Transition>();
                if (t.Start == t.End)
                    result = WordChecker.doesNotGoFinale(ref passedTransitions, t.End.Name, ref temp_goes_to_final, automaton);
                else if (visitedStates.Contains(t))
                {
                    result = WordChecker.doesNotGoFinale(ref passedTransitions, t.End.Name, ref temp_goes_to_final, automaton);
                    visitedStates.Add(t);
                }
                else
                {
                    visitedStates.Add(t);
                    result = isFinite(ref visitedStates, t.End.Name, automaton);
                }
                if (!result)
                    return result;
            }
            return result;
        }

        public static List<string> generateWords(Automaton automaton)
        {
            List<string> words = new List<string>();

            return generateWords(ref words, automaton.States[0].Name, automaton);
        }
        public static List<string> generateWords(ref List<string> words, string state, Automaton automaton,string word = "")
        {
            List<Transition> transitions = automaton.Transitions.Where(x => x.Start.Name == state && x.End.Name != "sink").ToList();
            foreach (Transition t in transitions)
            {
                word += t.Letter;
                if (automaton.Final.Contains(t.End))
                {
                    words.Add(word);
                    word = word.Remove(word.Length - 1);
                }
                else
                {
                    generateWords(ref words, t.End.Name,  automaton, word);
                    word = word.Remove(word.Length - 1);
                }
            }
            return words;
        }
    }
}
