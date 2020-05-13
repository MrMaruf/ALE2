using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class Parser
    {
        public static Automaton parseFile(string path)
        {
            List<string> alphabet = new List<string>();
            List<Node> states = new List<Node>();
            List<Node> final = new List<Node>();
            List<Transition> transitions = new List<Transition>();
            bool initialDFA = false;
            bool initialFinite = false;
            List<string> words = new List<string>();

            List<string> lines = File.ReadAllLines(path).ToList();
            for (int i = 0; i < lines.Count; i++)
            {

                string line = lines[i];
                
                if (line.Contains("#")) // ignoring comments
                    continue;

                if (line.Contains("alphabet"))
                    alphabet = parseAlphabetLine(line);
                else if (line.Contains("states"))
                    states = parseStatesLine(line);
                else if (line.Contains("final"))
                    final = parseStatesLine(line);
                else if (line.Contains("transitions"))
                {
                    while (true)
                    {
                        line = lines[++i];
                        if (line.Contains("end"))
                            break;
                        transitions.Add(parseTransitionLine(line, states));
                    }
                }
                else if (line.Contains("dfa"))
                    initialDFA = parseBoolLine(line);
                else if (line.Contains("finite"))
                    initialFinite = parseBoolLine(line);
                else if (line.Contains("words"))
                {
                    while (true)
                    {
                        line = lines[++i];
                        if (line.Contains("end"))
                            break;
                        words.Add(parseWordsLine(line).Item1);
                    }
                }
            }
            Automaton automaton = new Automaton(alphabet, states, final, transitions, initialDFA, initialFinite, words);
            return automaton;
        }

        public static Transition parseTransitionLine(string line, List<Node> states)
        {
            Transition newTransition;

            // removes whitespaces
            string letter = line.Split(',')[1].Split('-')[0].Trim();
            Node start = states.Find(x => x.Name == line.Split(',')[0]);
            Node end = states.Find(x => x.Name == line.Split('>')[1].Trim());
            newTransition = new Transition(letter, start, end);
            start.Transitions.Add(newTransition);
            end.Transitions.Add(newTransition);
            return newTransition;
        }
        
        public static List<string> parseAlphabetLine(string line)
        {
            List<string> letters;

            var concatenatedLetters = line.Split(':')[1].Trim();
            letters = concatenatedLetters.Select(c => c.ToString()).ToList();
            
            return letters;
        }

        public static List<Node> parseStatesLine(string line)
        {
            List<Node> states = new List<Node>();

            var concatenatedStatesNames = line.Split(':')[1].Trim();
            concatenatedStatesNames.Split(',').ToList().ForEach(x =>
            {
                Node newNode = new Node(x.Trim());
                if (!states.Contains(newNode))
                    states.Add(newNode);
            });

            return states;

        }
        public static bool parseBoolLine(string line)
        {
            bool boolValue;

            var stringWithBool = line.Split(':')[1].Trim();
            boolValue = stringWithBool == "y" ? true : false;
            
            return boolValue;

        }
        public static Tuple<string, bool, bool> parseWordsLine(string line)
        {
            Tuple<string, bool, bool> wordValues;

            var splitLine = line.Split(',');
            string word = splitLine[0].Trim();
            bool boolValue = splitLine[1].Trim() == "y" ? true : false;
            wordValues = new Tuple<string, bool, bool>(word, boolValue, false);
            
            return wordValues;
        }
    }
}
