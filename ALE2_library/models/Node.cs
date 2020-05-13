using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALE2_library.models
{
    public class Node
    {
        private string name;
        private List<Transition> transitions;

        public Node(string name, List<Transition> transitions)
        {
            this.name = name;
            this.transitions = transitions;
        }
        public Node(string name)
        {
            this.name = name;
            this.transitions = new List<Transition>();
        }

        public string Name
        {
            get => name;
            set
            {
                this.name = value;
            }
        }

        public List<Transition> Transitions
        {
            get => transitions;
            set
            {
                this.transitions = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Node)
            {
                Node node = (Node)obj;
                return this.name.Equals(node.name);
            }
            else return false;
        }

        public override string ToString()
        {
            return name;
        }
        public string ToStringGraphViz(string shape)
        {
            return "\""+name+"\" [shape="+shape+"]";
        }
    }
}