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
        private string takeFromStack;
        private string putOnStack;
        public Transition(string letter, Node start, Node end, string takeFromStack = null, string putOnStack = null)
        {
            this.start = start;
            this.end = end;
            this.letter = letter;
            this.takeFromStack = takeFromStack;
            this.putOnStack = putOnStack;
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
        public string PutOnStack { get => putOnStack; set => putOnStack = value; }
        public string TakeFromStack { get => takeFromStack; set => takeFromStack = value; }

        public override bool Equals(object obj)
        {
            return obj is Transition transition &&
                   EqualityComparer<Node>.Default.Equals(start, transition.start) &&
                   EqualityComparer<Node>.Default.Equals(end, transition.end) &&
                   letter == transition.letter;
        }

        public override string ToString()
        {
            if (takeFromStack == null)
                return (start is null ? "*null*" : start.Name) + "," + letter + " --> " + (end is null ? "*null*" : end.Name);
            else
            {
                return (start is null ? "*null*" : start.Name) + "," + letter + "["+takeFromStack+","+putOnStack+"] --> " + (end is null ? "*null*" : end.Name);
            }
        }
        public string ToStringGrapViz(string extra = "")
        {
            if(takeFromStack == null)
                return "\""+ (start is null ? "*null*" : start.Name) + "\" -> \"" + (end is null ? "*null*" : end.Name) + "\" [label=\"" + epsilonTransorm(letter) + extra + "\"]";
            else
                return "\"" + (start is null ? "*null*" : start.Name) + "\" -> \"" + (end is null ? "*null*" : end.Name) + "\" [label=\"" + epsilonTransorm(letter) + "["+ epsilonTransorm(takeFromStack) + ","+ epsilonTransorm(putOnStack) + "]"+ extra + "\"]";
        }

        private string epsilonTransorm(string input)
        {
            return input == "_" ? "ε" : input;
        }
    }
}