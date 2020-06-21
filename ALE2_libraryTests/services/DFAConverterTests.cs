using Microsoft.VisualStudio.TestTools.UnitTesting;
using ALE2_library.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ALE2_library.models;
using System.Diagnostics;

namespace ALE2_library.services.Tests
{
    [TestClass()]
    public class DFAConverterTests
    {
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        DataColumn column;
        DataRow row;
        Automaton automaton1 = Parser.parseFile("../../materials/NDFAinput.txt");
        Automaton automaton2 = Parser.parseFile("../../materials/NDFAinputConverted.txt");
        private Automaton automaton3 = RegexReader.readRegex(".a|bc");
        string[] columns1 = { "node", "a", "b", "c", "ε*" };
        string[] columns2 = { "node", "aε*", "bε*", "cε*" };
        string[][] rows1 = {
                new string[] {"1", "2", "", "4", "1"},
                new string[] {"2",  "", "3", "", "21" },
                new string[] {"3", "2", "", "", "3"},
                new string[] {"4", "", "", "3", "43"},
            };
        string[][] rows2 = {
                new string[] {"1", "21", "", "43"},
                new string[] {"21",  "21", "3", "43" },
                new string[] {"43", "21", "", "3"},
                new string[] {"3", "21", "", ""},
            };
        [TestInitialize]
        public void TestInitialize()
        {
            string input = "../../materials/NDFAgv.txt";
            string output = "../../materials/NDFAgv";
            GVgenerator.generateGraphVizInput(automaton1, input);
            GVgenerator.generateGraph(input, output);
            // table1 initialization
            for (int i = 0; i < 5; i++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = columns1[i];
                table1.Columns.Add(column);
            }
            for (int i = 0; i < 4; i++)
            {
                row = table1.NewRow();
                row[columns1[0]] = rows1[i][0];
                row[columns1[1]] = rows1[i][1];
                row[columns1[2]] = rows1[i][2];
                row[columns1[3]] = rows1[i][3];
                row[columns1[4]] = rows1[i][4];
                table1.Rows.Add(row);
            }
            //table2 initialization
            for (int i = 0; i < 4; i++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = columns2[i];
                table2.Columns.Add(column);
            }
            for (int i = 0; i < 4; i++)
            {
                row = table2.NewRow();
                row[columns2[0]] = rows2[i][0];
                row[columns2[1]] = rows2[i][1];
                row[columns2[2]] = rows2[i][2];
                row[columns2[3]] = rows2[i][3];
                table2.Rows.Add(row);
            }
        }
        [TestMethod()]
        public void createColumnsTest_passAutomatonAndRefTable()
        {
            DataTable expected = table1;
            DataTable inputTable = new DataTable();
            Automaton inputAutomaton = automaton1;
            var columns = DFAConverter.makeColumnsInitial(inputAutomaton);
            DFAConverter.createColumns(ref inputTable, columns);
            Trace.WriteLine(inputTable.Columns.Count);
            foreach (DataColumn col in inputTable.Columns)
            {
                Trace.WriteLine(col);
                Trace.WriteLine(expected.Columns.Contains(col.ColumnName));
                Assert.IsTrue(expected.Columns.Contains(col.ColumnName));
            }
        }

        [TestMethod()]
        public void makeColumnsInitialTest_passAutomaton_returnsListOfColumns()
        {
            Automaton inputAutomaton = automaton1;
            List<string> expectedColumns = new List<string>(columns1);
            var testColumns = DFAConverter.makeColumnsInitial(inputAutomaton);
            testColumns.ForEach(x => Assert.IsTrue(expectedColumns.Contains(x)));
        }
        [TestMethod()]
        public void makeColumnsSecondTest_passAutomaton_returnsListOfColumns()
        {
            Automaton inputAutomaton = automaton1;
            List<string> expectedColumns = new List<string>(columns2);
            var testColumns = DFAConverter.makeColumnsSecond(inputAutomaton);
            testColumns.ForEach(x => Assert.IsTrue(expectedColumns.Contains(x)));
        }
        [TestMethod()]
        public void fillInRowsTest_passAutomatonAndRefTable()
        {
            DataTable expectedTable = table1;
            DataTable inputTable = new DataTable();
            Automaton inputAutomaton = automaton1;
            List<string> inputColumns = new List<string>(columns1);
            DFAConverter.createColumns(ref inputTable, inputColumns);
            DFAConverter.fillInRows(ref inputTable, inputAutomaton);
            Trace.WriteLine("rows: " + inputTable.Rows.Count + " -- columns: " + inputTable.Columns.Count);
            Trace.WriteLine("rows: " + expectedTable.Rows.Count + " -- columns: " + expectedTable.Columns.Count);
            foreach (DataRow row in inputTable.Rows)
            {
                foreach (DataColumn col in inputTable.Columns)
                {
                    int index = inputTable.Rows.IndexOf(row);
                    Trace.Write(row[col].ToString() == "" ? "- " : row[col] + " ");
                    Assert.IsTrue(expectedTable.Rows[index][col.ColumnName].ToString() == row[col].ToString());
                }
                Trace.WriteLine("");
            }
        }
        [DataTestMethod]
        [DataRow("1", "a", "21")]
        [DataRow("21", "c", "43")]
        [DataRow("43", "b", "")]
        public void calculateColumnTest_PassInputTableAndStateAndLetter_ReturnsColumnsValue(string inputState, string inputLetter, string expectedColumnValue)
        {
            DataTable inputTable = table1;
            string testColumnValue = DFAConverter.calculateColumn(inputTable, inputState, inputLetter);
            Assert.AreEqual(expectedColumnValue, testColumnValue);
        }

        [TestMethod()]
        public void fillInRowsTest_passInputTableAndRefOutputTable()
        {
            DataTable expectedTable = table2;
            DataTable inputTable = table1;
            DataTable outputTable = new DataTable();
            Automaton inputAutomaton = automaton1;
            DFAConverter.createColumns(ref outputTable, DFAConverter.makeColumnsSecond(inputAutomaton));
            DFAConverter.fillInRows(inputTable, ref outputTable);

            Trace.WriteLine("rows: " + outputTable.Rows.Count + " -- columns: " + outputTable.Columns.Count);
            Trace.WriteLine("rows: " + expectedTable.Rows.Count + " -- columns: " + expectedTable.Columns.Count);
            foreach (DataRow row in outputTable.Rows)
            {
                foreach (DataColumn col in outputTable.Columns)
                {
                    int index = outputTable.Rows.IndexOf(row);
                    Trace.Write(row[col].ToString() == "" ? "- " : row[col] + " ");
                    //Trace.Write("!!" + expectedTable.Rows[index][col.ColumnName].ToString() == "" ? "- " : expectedTable.Rows[index][col.ColumnName] + " ");
                    Assert.IsTrue(expectedTable.Rows[index][col.ColumnName].ToString() == row[col].ToString());
                }
                Trace.WriteLine("");
            }
        }

        [TestMethod()]
        public void tableToAutomatonTest()
        {
            Automaton expectedAutomaton = automaton2;
            List<string> inputFinals = new List<string>();
            automaton1.Final.ForEach(x => inputFinals.Add(x.Name));
            DataTable inputTable = table2;
            Automaton testAutomaton = DFAConverter.tableToAutomaton(table2, inputFinals, expectedAutomaton.Alphabet);
            Assert.AreEqual(expectedAutomaton, testAutomaton);
        }

        [TestMethod()]
        public void convertNDFAtoDFATest()
        {
            Automaton expectedAutomaton = automaton2;
            Automaton inputAutomaton = automaton1;
            Automaton testAutomaton = DFAConverter.convertNDFAtoDFA(inputAutomaton);
            Assert.AreEqual(expectedAutomaton, testAutomaton);
            testAutomaton = DFAConverter.convertNDFAtoDFA(automaton3);
        }
    }
}