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
        private string letter;

        public Transition(string letter, Node start, Node end)
        {
            this.start = start;
            this.end = end;
            this.letter = letter;
        }

        public System.Tuple<string, Node, Node> Path
        {
            get => new Tuple<string, Node, Node>(letter, start, end);
            set
            {
                letter = value.Item1;
                start = value.Item2;
                end = value.Item3;
            }
        }

        public Node Start
        {
            get => start;
        }

        public Node End
        {
            get => end;
            set => end = value;
        }

        public string Letter
        {
            get => letter;
        }

        public override bool Equals(object obj)
        {
            return obj is Transition transition &&
                   EqualityComparer<Node>.Default.Equals(start, transition.start) &&
                   EqualityComparer<Node>.Default.Equals(end, transition.end) &&
                   letter == transition.letter;
        }

        public override string ToString()
        {
            return (start is null ? "*null*" : start.Name) + "," + letter + " --> " + (end is null ? "*null*" : end.Name);
        }
        public string ToStringGrapViz(string extra = "")
        {
            return "\""+ (start is null ? "*null*" : start.Name) + "\" -> \"" + (end is null ? "*null*" : end.Name) + "\" [label=\"" + letter + extra + "\"]";
        }
    }
}