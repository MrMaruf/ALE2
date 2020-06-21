using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class StackChecker
    {
        public static bool checkWordPDA(string word, Automaton automaton)
        {
            List<Transition> transitions = new List<Transition>();
            return checkWordPDA(word, automaton, automaton.States[0].Name, "", ref transitions);
        }
        public static bool checkWordPDA(string word, Automaton a, string currentState, string stack, ref List<Transition> passedTransitions)
        {
            if(a.Stack == null)
                throw new ArgumentException("Automaton do NOT have a stack! It's NOT a PDA!");
            bool temp_goes_to_final = true;
            if (word == "_" || word == "ε" || word == "")
            {
                return !StackChecker.doesNotGoFinalePDA(ref passedTransitions, currentState, stack, ref temp_goes_to_final, a);
            }
            string partToBeChecked;
            List<Transition> possibleTransitions = new List<Transition>();
            if (stack == "")
            {
                possibleTransitions = a.Transitions.Where(x =>
                                    (x.Letter == word[0].ToString() || x.Letter == "_")
                                    && x.Start.Name == currentState
                                    && x.TakeFromStack == "_"
                                 ).ToList();
            }
            else
            {
                possibleTransitions = a.Transitions.Where(x => (
                                    x.Letter == word[0].ToString() || x.Letter == "_")
                                    && x.Start.Name == currentState
                                    && (x.TakeFromStack == "_" || x.TakeFromStack == stack[stack.Length - 1].ToString())
                                    ).ToList();
            }
            possibleTransitions = possibleTransitions.OrderBy(x => x.Letter).ToList();
            possibleTransitions.ForEach(x=>Trace.WriteLine(x.ToString()));
            bool result = false;
            if (possibleTransitions.Count() > 0)
            {
                if (word.Count() > 0)
                {
                    partToBeChecked = word.Remove(0, 1);
                    Trace.WriteLine("Word: " + word +" | Checking: " + partToBeChecked);
                }
                else
                    return false;
                foreach (Transition t in possibleTransitions)
                {
                    if (a.Final.Contains(t.End) && word.Count() == 0 && stack == "")
                        return true;
                    else if (word.Count() == 0)
                    {
                        result = !StackChecker.doesNotGoFinalePDA(ref passedTransitions, t.End.Name, stack, ref temp_goes_to_final, a);
                    }
                    else
                    {
                        if (t.Letter != "_")
                        {
                            if (t.TakeFromStack != "_")
                                stack = stack.Remove(stack.Length - 1);
                            if (t.PutOnStack != "_")
                                stack = stack + t.PutOnStack;
                            passedTransitions.Add(t);
                            result = checkWordPDA(partToBeChecked, a, t.End.Name, stack, ref passedTransitions);
                            passedTransitions.Remove(t);
                        }
                        else
                        {
                            if (t.TakeFromStack != "_")
                                stack = stack.Remove(stack.Length - 1);
                            if (t.PutOnStack != "_")
                                stack = stack + t.PutOnStack;
                            passedTransitions.Add(t);
                            result = checkWordPDA(word, a, t.End.Name, stack, ref passedTransitions);
                            passedTransitions.Remove(t);
                        }
                        if (result) break;
                    }
                }
            }
            Trace.WriteLine(result);
            return result;
        }
        public static bool doesNotGoFinalePDA(ref List<Transition> passedTransitions, string state, string stack, ref bool result, Automaton automaton)
        {
            if (automaton.Final.Any(x => x.Name == state) && stack == "")
                return false;
            List<Transition> possibletransitions = new List<Transition>();
            if (stack == "")
                possibletransitions = automaton.Transitions.Where(x => x.Start.Name == state && x.Letter == "_" && x.TakeFromStack == "_").ToList();
            else
            {
                possibletransitions = automaton.Transitions.Where(x =>
                                        x.Start.Name == state
                                        && x.Letter == "_"
                                        && (x.TakeFromStack == "_" || x.TakeFromStack == stack[stack.Length - 1].ToString())
                                    ).ToList();
            }
            foreach (Transition t in possibletransitions)
            {
                if (t.TakeFromStack != "_")
                    stack = stack.Remove(stack.Length - 1);
                if (t.PutOnStack != "_")
                    stack = stack + t.PutOnStack;
                passedTransitions.Add(t);
                if (automaton.Final.Contains(t.End) && stack == "")
                    result = false;
                else
                {
                    doesNotGoFinalePDA(ref passedTransitions, t.End.Name, stack, ref result, automaton);
                    passedTransitions.Remove(t);
                }
            }
            return result;
        }
    }
}
