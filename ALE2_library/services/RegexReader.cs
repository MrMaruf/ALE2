using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class RegexReader
    {
        static string operators = "*|.";
        static int indexer = 1;
        static Node latestNode;
        public static Automaton readRegex(string expression)
        {

            indexerReset();
            Automaton automaton;
            List<string> alphabet = expression.Split().ToList().FindAll(x => !"(,)|*._".Contains(x));
            List<Node> states = new List<Node>();
            List<Node> final = new List<Node>();
            List<Transition> transitions = new List<Transition>();
            Regex pattern = new Regex("[,()]");
            expression = pattern.Replace(expression, "");
            readExpression(ref transitions, ref expression);
            transitions.ForEach(x=> {  //add all the states
                if (!states.Contains(x.Start))
                    states.Add(x.Start);
                if (!states.Contains(x.End))
                    states.Add(x.End);
            }); 
            final.Add(latestNode); // get final state
            states.ForEach(x => // adds respective transitions to each state
            {
                x.Transitions.AddRange(transitions.FindAll(t => t.Start.Equals(x) || t.End.Equals(x)));
            });
            automaton = new Automaton(alphabet, states, final, transitions);
            return automaton;

        }
        public static Node readExpression(
            ref List<Transition> transitions,
            ref string expression
            )
        {
            if (expression.Length == 1)
                readLetter(ref transitions, expression);
            else
            {
                char sign = expression[0];
                expression = expression.Remove(0, 1);
                switch (sign)
                {
                    case '*':
                        readStar(ref transitions, ref expression, latestNode);
                        break;
                    case '.':
                        readConcat(ref transitions, ref expression, latestNode);
                        break;
                    case '|':
                        readOr(ref transitions, ref expression, latestNode);
                        break;
                    default:
                        break;

                }

            }
            return latestNode;
        }
        public static Node readLetter(
            ref List<Transition> transitions,
            string expression = "_",
            Node lastNode = null,
            Node closingNode = null)
        {
            if (lastNode is null)
                if (latestNode is null)
                    lastNode = latestNode = new Node("q0");
                else
                    lastNode = latestNode;
            latestNode = new Node("q" + indexer++);
            transitions.Add(new Transition(expression, lastNode, latestNode));
            if (!(closingNode is null))
            {
                transitions.Add(new Transition("_", latestNode, closingNode));
            }
            return latestNode;
        }
        public static Node readOr(ref List<Transition> transitions, ref string expression, Node lastNode = null)
        {
            if (lastNode is null)
                if (latestNode is null)
                    lastNode = latestNode = new Node("q0");
                else
                    lastNode = latestNode;
            Node closingNode = new Node("q" + indexer++);
            for (int i = 0; i < 2; i++)
            {
                if (operators.Contains(expression[0]))
                {
                    Node toClose = readExpression(ref transitions, ref expression);
                    transitions.Add(new Transition("_", toClose, closingNode));
                }
                else
                {
                    string letter = expression[0].ToString();
                    readLetter(ref transitions, letter, lastNode, closingNode);
                    expression = expression.Remove(0, 1);
                }
            }
            latestNode = closingNode;
            return latestNode;
        }
        public static Node readConcat(ref List<Transition> transitions, ref string expression, Node lastNode = null)
        {
            if (lastNode is null)
                if (latestNode is null)
                    lastNode = latestNode = new Node("q0");
                else
                    lastNode = latestNode;
            for (int i = 0; i < 2; i++)
            {

                if (operators.Contains(expression[0]))
                    lastNode = readExpression(ref transitions, ref expression);
                else
                {
                    string letter = expression[0].ToString();
                    lastNode = readLetter(ref transitions, letter, lastNode);
                    expression = expression.Remove(0, 1);
                }
            }

            return latestNode;
        }
        public static Node readStar(ref List<Transition> transitions, ref string expression, Node lastNode = null)
        {
            if (lastNode is null)
                if (latestNode is null)
                    lastNode = latestNode = new Node("q0");
                else
                    lastNode = latestNode;
            Node closingNode = readLetter(ref transitions, "_", lastNode);
            lastNode = readLetter(ref transitions, "_", lastNode);
            Node nodeToLoop;
            if (operators.Contains(expression[0]))
            {
                nodeToLoop = readExpression(ref transitions, ref expression);
                transitions.Add(new Transition("_", nodeToLoop, closingNode));
            }
            else
            {
                string letter = expression[0].ToString();
                nodeToLoop = readLetter(ref transitions, letter, lastNode, closingNode);
                expression = expression.Remove(0, 1);
            }
            transitions.Add(new Transition("_", nodeToLoop, lastNode));
            latestNode = closingNode;
            return latestNode;
        }

        public static void indexerReset()
        {
            indexer = 1;
            latestNode = null;
        }
    }
}
