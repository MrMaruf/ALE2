using ALE2_library.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ALE2_library.services;

namespace Ale2_Interface
{
    public partial class AleForm : Form
    {
        private Automaton automaton;
        public AleForm()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;

            automaton = Parser.parseFile(path);
            processAutomaton(automaton);
        }

        private void fillInfo(Automaton inputAutomaton)
        {
            rtbInfo.Clear();
            rtbInfo.Text += inputAutomaton.ToString();
        }

        private void showGraph(Automaton inputAutomaton)
        {
            string path = "./graph";
            GVgenerator.generateGraph(inputAutomaton, path);
            pbGraph.ImageLocation = path + ".png";
        }

        private void processAutomaton(Automaton inputAutomaton)
        {
            showGraph(inputAutomaton);
            fillInfo(inputAutomaton);
            setDFAAndFinite(inputAutomaton);
            checkWords(inputAutomaton);
        }

        private void setDFAAndFinite(Automaton inputAutomaton)
        {
            if (automaton.Stack != null)
                return;
            bool finite = FiniteChecker.isFinite(inputAutomaton);
            bool DFA = IdentifierDFA.isDFA(automaton);
            string text = DFA ? "✔" : "✘";
            tbDFA.Text = text;
            text = finite ? "✔" : "✘";
            tbFinite.Text = text;
            if (finite)
                automaton.Words = FiniteChecker.generateWords(automaton);
        }

        private void checkWords(Automaton inputAutomaton)
        {
            lbWords.Items.Clear();
            inputAutomaton.Words?.ForEach(x =>
            {
                
                bool check;
                if (inputAutomaton.Stack == null)
                    check = WordChecker.checkWord(x, inputAutomaton);
                else
                    check = StackChecker.checkWordPDA(x, inputAutomaton);
                string text = x + (check ? "✔" : "✘");
                lbWords.Items.Add(text);
            });
        }

        private void btnToDFA_Click(object sender, EventArgs e)
        {
            if (automaton == null)
                MessageBox.Show("Stop it... Get help... There is no automaton to transform.");
            else
            {
                automaton = DFAConverter.convertNDFAtoDFA(automaton);
                processAutomaton(automaton);
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string text = tbReg.Text;
            automaton = RegexReader.readRegex(text);
            processAutomaton(automaton);
        }

        private void btnCheckWord_Click(object sender, EventArgs e)
        {
            string word = tbWord.Text;
            bool check = WordChecker.checkWord(word, automaton);

            tbWordCheck.Text = check ? "✔" : "✘";
        }
    }
}
