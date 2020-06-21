using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ALE2_library.services
{
    public class DFAConverter
    {
        public static Automaton convertNDFAtoDFA(Automaton automaton)
        {
            List<string> finals = new List<string>();
            automaton.Final.ForEach(x => finals.Add(x.Name));
            finals.ForEach(x=>Trace.WriteLine(x));
            DataTable tableInitial = new DataTable();
            var columns = makeColumnsInitial(automaton);
            Trace.WriteLine("Columns1");
            columns.ForEach(x => Trace.WriteLine(x));
            Trace.WriteLine("Table1");
            foreach (DataRow row in tableInitial.Rows)
            {
                Trace.WriteLine("");
                foreach (DataColumn col in tableInitial.Columns)
                {
                    Trace.Write(row[col]);
                }
            }

            createColumns(ref tableInitial, columns);
            fillInRows(ref tableInitial, automaton);

            DataTable tableSecond = new DataTable();
            columns = makeColumnsSecond(automaton);
            Trace.WriteLine("Columns2");
            columns.ForEach(x => Trace.WriteLine(x));
            createColumns(ref tableSecond, columns);
            fillInRows(tableInitial, ref tableSecond);
            Trace.WriteLine("Table2");
            foreach (DataRow row in tableSecond.Rows)
            {
                foreach (DataColumn col in tableSecond.Columns)
                {
                    Trace.WriteLine(row[col]);
                }
            }
            return tableToAutomaton(tableSecond, finals, automaton.Alphabet);
        }
        public static void createColumns(ref DataTable table, List<string> columns)
        {
            DataColumn column;
            for (int i = 0; i < columns.Count; i++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = columns[i];
                table.Columns.Add(column);
            }
        }
        /// <summary>
        /// Makes columns from automaton for the initial table
        /// </summary>
        /// <param name="a">Automaton</param>
        /// <returns></returns>
        public static List<string> makeColumnsInitial(Automaton a)
        {
            List<string> columns = new List<string>();
            columns.Add("node");
            columns.AddRange(a.Alphabet);
            columns.Add("ε*");
            return columns;
        }
        /// <summary>
        /// Make columns from automaton for the secondary table
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static List<string> makeColumnsSecond(Automaton a)
        {
            List<string> columns = new List<string>();
            columns.Add("node");
            a.Alphabet.ForEach(x => columns.Add(x + "ε*"));
            return columns;
        }
        /// <summary>
        /// Fills in initial table based on the Automaton
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="automaton">Automaton</param>
        public static void fillInRows(ref DataTable table, Automaton automaton)
        {
            for (int index = 0; index < automaton.States.Count; index++)
            {
                DataRow row = table.NewRow();
                foreach (DataColumn col in table.Columns)
                {
                    string stateName = automaton.States[index].Name;
                    if (col.ColumnName == "node")
                    {
                        row[col] = stateName;
                        continue;
                    }
                    List<Transition> transitions;
                    string columnValue = "";
                    if (col.ColumnName == "ε*")
                    {
                        columnValue += stateName;
                        transitions = automaton.Transitions.FindAll(x => x.Start.Name == stateName && x.Letter == "_");
                    }
                    else
                        transitions = automaton.Transitions.FindAll(x => x.Start.Name == stateName && x.Letter == col.ColumnName);

                    transitions.ForEach(x => columnValue += x.End);
                    row[col] = columnValue;
                }
                table.Rows.Add(row);
            }
        }
        /// <summary>
        ///  Fills in second Table based on the initial Table
        /// </summary>
        /// <param name="inputTable">DataTable</param>
        /// <param name="outputTable">DataTable</param>
        public static void fillInRows(DataTable inputTable, ref DataTable outputTable)
        {
            List<string> rows = new List<string>();
            rows.Add(inputTable.Rows[0]["ε*"].ToString());
            for (int index = 0; index < rows.Count; index++)
            {
                DataRow row = outputTable.NewRow();
                string state = "";
                foreach (DataColumn col in outputTable.Columns)
                {
                    if (col.ColumnName == "node")
                    {
                        row[col] = rows[index];
                        state = row["node"].ToString();
                        continue;
                    }
                    string letter = col.ColumnName[0].ToString();
                    string columnValue = calculateColumn(inputTable, state, letter);
                    if (!rows.Contains(columnValue) && columnValue != "")
                        rows.Add(columnValue);
                    row[col] = columnValue;
                }
                outputTable.Rows.Add(row);
            }
        }
        public static string calculateColumn(DataTable inputTable, string state, string letter)
        {
            string columnValue = "";
            string seekingState = "";
            for (int i = 0; i < state.Length; i++)
            {
                string s = state[i].ToString();
                foreach (DataRow row in inputTable.Rows)
                {
                    if (row["node"].ToString() == s && !seekingState.Contains(row["ε*"].ToString()))
                    {
                        seekingState += row[letter];
                    }
                }
            }
            for (int i = 0; i < seekingState.Length; i++)
            {
                string s = seekingState[i].ToString();
                foreach (DataRow row in inputTable.Rows)
                {
                    if (row["node"].ToString() == s && !columnValue.Contains(row["ε*"].ToString()))
                    {
                        columnValue += row["ε*"];
                    }
                }
            }
            return columnValue;
        }
        public static Automaton tableToAutomaton(DataTable inputTable, List<string> initialFinals, List<string> alphabet)
        {
            List<Node> states = new List<Node>();
            List<Node> final = new List<Node>();
            string stack = null;
            List<Transition> transitions = new List<Transition>();
            List<string> words = new List<string>();
            Node sink = new Node("sink");
            var sinkTransitions = new List<Transition>();
            alphabet.ForEach(x =>
            {
                sinkTransitions.Add(new Transition(x, sink, sink));
            });
            sink.Transitions = sinkTransitions;
            foreach (DataRow row in inputTable.Rows)
            {
                Node startState = null;
                foreach (DataColumn col in inputTable.Columns)
                {
                    if (col.ColumnName == "node")
                    {
                        if (initialFinals.Any(x => row[col].ToString().Contains(x)) && final.FindAll(x => x.Name == row[col].ToString()).Count < 1)
                        {
                            final.Add(new Node(row[col].ToString()));
                        }
                        if (!states.Any(x => x.Name == row[col].ToString()))
                        {
                            startState = new Node(row[col].ToString());
                            states.Add(startState);
                        }
                        else
                        {
                            startState = states.Find(x => x.Name == row[col].ToString());
                        }
                        continue;
                    }
                    Node endState;
                    if (row[col].ToString() == "")
                    {
                        endState = sink;
                    }
                    else
                    {
                        if (states.FindAll(x => x.Name == row[col].ToString()).Count < 1)
                        {
                            endState = new Node(row[col].ToString());
                            states.Add(endState);
                        }
                        else
                        {
                            endState = states.Find(x => x.Name == row[col].ToString());
                        }
                    }
                    transitions.Add(new Transition(col.ColumnName[0].ToString(), startState, endState));
                }
            }
            if (transitions.Any(x => x.End.Equals(sink)))
            {
                states.Add(sink);
                transitions.AddRange(sinkTransitions);
            }
            Automaton automaton = new Automaton(alphabet, states, final, transitions, true, false, words);
            return automaton;
        }
    }
}
