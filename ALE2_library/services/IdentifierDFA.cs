using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class IdentifierDFA
    {
        public static bool isDFA(Automaton automaton)
        {
            bool value = true;

            automaton.Alphabet.ForEach(letter =>
            {
                var transitionsWithLetter = automaton.Transitions.FindAll(transition => transition.Letter == letter);
                if (automaton.States.Count != transitionsWithLetter.Count)
                { value = false; return; }
            });

            return value;
        }
    }
}
