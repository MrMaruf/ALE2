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
            get => default;
            set
            {
                this.transitions = value;
            }
        }
    }
}