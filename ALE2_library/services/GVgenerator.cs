using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class GVgenerator
    {
        public static void generateGraphVizInput(Automaton automaton, string path)
        {
            List<string> lines = new List<string>();
            lines.Add("digraph myAutomaton {");
            lines.Add("rankdir=LR;");
            lines.Add("\"\" [shape=none]");
            automaton.States.ForEach(x =>
            {
                if (automaton.Final.Contains(x))
                    lines.Add(x.ToStringGraphViz("doublecircle"));
                else lines.Add(x.ToStringGraphViz("circle"));
            });
            lines.Add("");
            lines.Add("\"\" -> \"" + automaton.States[0].Name+"\"");
            automaton.Transitions.ForEach(x => lines.Add(x.ToStringGrapViz()));
            lines.Add("}");
            System.IO.File.WriteAllLines(@path, lines);
        }

        public static void generateGraph(string inputPath, string outputPath)
        {
            Process dot = new Process();
            dot.StartInfo.FileName = "dot.exe";
            dot.StartInfo.Arguments = "-Tpng -o"+outputPath+".png "+inputPath;
            dot.Start();
            dot.WaitForExit();
        }
    }
}
