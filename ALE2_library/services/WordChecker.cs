using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class WordChecker
    {
        public static bool checkWord(string word, Automaton automaton)
        {
            return checkWord(word, automaton, automaton.States[0].Name);
        }
        public static bool checkWord(string word, Automaton automaton, string currentState)
        {
            List<Transition> possibleTransitions = 
                automaton.Transitions.Where(x => (x.Letter == word[0].ToString() || x.Letter == "_") && x.Start.Name == currentState).ToList();
            bool temp_goes_to_final = true;
            string partToBeChecked;
            bool result = false;
            if (possibleTransitions.Count() > 0)
            {
                if (word.Count() > 0)
                    partToBeChecked = word.Remove(0, 1);
                else
                    return false;
                foreach (Transition t in possibleTransitions)
                {
                    if (automaton.Final.Contains(t.End) && partToBeChecked.Count() == 0)
                        return true;
                    else if (partToBeChecked.Count() == 0)
                    {
                        List<Transition> passedTransitions = new List<Transition>();
                        result = doesNotGoFinale(ref passedTransitions, t.End.Name, ref temp_goes_to_final, automaton);
                    }
                    else
                    {
                        if (t.Letter != "_")
                            result = checkWord(partToBeChecked, automaton, t.End.Name);
                        else
                            result = checkWord(word, automaton, t.End.Name);
                        if (result) break;
                    }
                }
            }
            return result;
        }
        public static bool doesNotGoFinale(ref List<Transition> passedTransitions, string state, ref bool result, Automaton automaton)
        {
            if (automaton.Final.Any(x => x.Name == state))
                return false;
            List<Transition> possibletransitions = new List<Transition>();
            possibletransitions = automaton.Transitions.Where(x => x.Start.Name == state && x.Letter == "_").ToList();
            foreach (Transition t in possibletransitions)
            {
                Trace.WriteLine(t.ToString());
                passedTransitions.Add(t);
                if (automaton.Final.Contains(t.End))
                    result = false;
                else
                {
                    doesNotGoFinale(ref passedTransitions, t.End.Name, ref result, automaton);
                    passedTransitions.Remove(t);
                }
            }
            return result;
        }
    }
}
