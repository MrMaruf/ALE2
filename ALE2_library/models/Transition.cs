using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALE2_library.models
{
    public class Transition
    {
        private Node start;
        private Node end;
        private char letter;

        public Transition(char letter, Node start, Node end)
        {
            this.start = start;
            this.end = end;
            this.letter = letter;
        }

        public System.Tuple<char, Node, Node> Path
        {
            get => new Tuple<char, Node, Node>(letter, start, end);
            set
            {
                letter = value.Item1;
                start = value.Item2;
                end = value.Item3;
            }
        }

        public Node Start
        {
            get => default;
        }

        public Node End
        {
            get => default;
        }

        public int Letter
        {
            get => default;
        }
    }
}