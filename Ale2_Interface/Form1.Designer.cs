namespace Ale2_Interface
{
    partial class AleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnReg = new System.Windows.Forms.Button();
            this.tbReg = new System.Windows.Forms.TextBox();
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbWord = new System.Windows.Forms.TextBox();
            this.btnCheckWord = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lbRE = new System.Windows.Forms.Label();
            this.lblWordChecker = new System.Windows.Forms.Label();
            this.lblDefWords = new System.Windows.Forms.Label();
            this.lblAutomataInfo = new System.Windows.Forms.Label();
            this.btnToDFA = new System.Windows.Forms.Button();
            this.lbWords = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFinite = new System.Windows.Forms.TextBox();
            this.tbDFA = new System.Windows.Forms.TextBox();
            this.tbWordCheck = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbInfo
            // 
            this.rtbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbInfo.Location = new System.Drawing.Point(1484, 109);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.Size = new System.Drawing.Size(307, 478);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnImport.Location = new System.Drawing.Point(1179, 344);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(160, 62);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import file to Automata";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnReg
            // 
            this.btnReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReg.Location = new System.Drawing.Point(872, 24);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(181, 62);
            this.btnReg.TabIndex = 2;
            this.btnReg.Text = "Read Regular Expression";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // tbReg
            // 
            this.tbReg.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbReg.Location = new System.Drawing.Point(78, 36);
            this.tbReg.Name = "tbReg";
            this.tbReg.Size = new System.Drawing.Size(788, 33);
            this.tbReg.TabIndex = 1;
            // 
            // pbGraph
            // 
            this.pbGraph.Location = new System.Drawing.Point(78, 124);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(788, 425);
            this.pbGraph.TabIndex = 3;
            this.pbGraph.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tbWord
            // 
            this.tbWord.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbWord.Location = new System.Drawing.Point(872, 144);
            this.tbWord.Name = "tbWord";
            this.tbWord.Size = new System.Drawing.Size(536, 33);
            this.tbWord.TabIndex = 7;
            // 
            // btnCheckWord
            // 
            this.btnCheckWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCheckWord.Location = new System.Drawing.Point(872, 183);
            this.btnCheckWord.Name = "btnCheckWord";
            this.btnCheckWord.Size = new System.Drawing.Size(536, 39);
            this.btnCheckWord.TabIndex = 8;
            this.btnCheckWord.Text = "Check word";
            this.btnCheckWord.UseVisualStyleBackColor = true;
            this.btnCheckWord.Click += new System.EventHandler(this.btnCheckWord_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbRE
            // 
            this.lbRE.AutoSize = true;
            this.lbRE.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbRE.Location = new System.Drawing.Point(78, 4);
            this.lbRE.Name = "lbRE";
            this.lbRE.Size = new System.Drawing.Size(224, 29);
            this.lbRE.TabIndex = 9;
            this.lbRE.Text = "Regular Expression";
            // 
            // lblWordChecker
            // 
            this.lblWordChecker.AutoSize = true;
            this.lblWordChecker.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblWordChecker.Location = new System.Drawing.Point(1125, 112);
            this.lblWordChecker.Name = "lblWordChecker";
            this.lblWordChecker.Size = new System.Drawing.Size(71, 29);
            this.lblWordChecker.TabIndex = 10;
            this.lblWordChecker.Text = "Word";
            // 
            // lblDefWords
            // 
            this.lblDefWords.AutoSize = true;
            this.lblDefWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDefWords.Location = new System.Drawing.Point(931, 275);
            this.lblDefWords.Name = "lblDefWords";
            this.lblDefWords.Size = new System.Drawing.Size(208, 29);
            this.lblDefWords.TabIndex = 12;
            this.lblDefWords.Text = "Predefined Words";
            // 
            // lblAutomataInfo
            // 
            this.lblAutomataInfo.AutoSize = true;
            this.lblAutomataInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAutomataInfo.Location = new System.Drawing.Point(1479, 77);
            this.lblAutomataInfo.Name = "lblAutomataInfo";
            this.lblAutomataInfo.Size = new System.Drawing.Size(158, 29);
            this.lblAutomataInfo.TabIndex = 13;
            this.lblAutomataInfo.Text = "Automata Info";
            // 
            // btnToDFA
            // 
            this.btnToDFA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnToDFA.Location = new System.Drawing.Point(1179, 412);
            this.btnToDFA.Name = "btnToDFA";
            this.btnToDFA.Size = new System.Drawing.Size(160, 62);
            this.btnToDFA.TabIndex = 14;
            this.btnToDFA.Text = "Transform To DFA";
            this.btnToDFA.UseVisualStyleBackColor = true;
            this.btnToDFA.Click += new System.EventHandler(this.btnToDFA_Click);
            // 
            // lbWords
            // 
            this.lbWords.FormattingEnabled = true;
            this.lbWords.Location = new System.Drawing.Point(936, 307);
            this.lbWords.Name = "lbWords";
            this.lbWords.Size = new System.Drawing.Size(237, 290);
            this.lbWords.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(1179, 477);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 29);
            this.label1.TabIndex = 16;
            this.label1.Text = "DFA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(1179, 542);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 29);
            this.label2.TabIndex = 17;
            this.label2.Text = "Finite";
            // 
            // tbFinite
            // 
            this.tbFinite.Font = new System.Drawing.Font("Verdana", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbFinite.Location = new System.Drawing.Point(1258, 542);
            this.tbFinite.Name = "tbFinite";
            this.tbFinite.Size = new System.Drawing.Size(62, 59);
            this.tbFinite.TabIndex = 18;
            this.tbFinite.Text = " ? ";
            // 
            // tbDFA
            // 
            this.tbDFA.Font = new System.Drawing.Font("Verdana", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbDFA.Location = new System.Drawing.Point(1258, 477);
            this.tbDFA.Name = "tbDFA";
            this.tbDFA.Size = new System.Drawing.Size(62, 59);
            this.tbDFA.TabIndex = 19;
            this.tbDFA.Text = " ? ";
            // 
            // tbWordCheck
            // 
            this.tbWordCheck.Font = new System.Drawing.Font("Verdana", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbWordCheck.Location = new System.Drawing.Point(1414, 154);
            this.tbWordCheck.Name = "tbWordCheck";
            this.tbWordCheck.Size = new System.Drawing.Size(62, 59);
            this.tbWordCheck.TabIndex = 20;
            this.tbWordCheck.Text = " ? ";
            // 
            // AleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1803, 635);
            this.Controls.Add(this.tbWordCheck);
            this.Controls.Add(this.tbDFA);
            this.Controls.Add(this.tbFinite);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbWords);
            this.Controls.Add(this.btnToDFA);
            this.Controls.Add(this.lblAutomataInfo);
            this.Controls.Add(this.lblDefWords);
            this.Controls.Add(this.lblWordChecker);
            this.Controls.Add(this.lbRE);
            this.Controls.Add(this.tbWord);
            this.Controls.Add(this.btnCheckWord);
            this.Controls.Add(this.pbGraph);
            this.Controls.Add(this.tbReg);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.rtbInfo);
            this.Name = "AleForm";
            this.Text = "Ale2 Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.TextBox tbReg;
        private System.Windows.Forms.PictureBox pbGraph;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox tbWord;
        private System.Windows.Forms.Button btnCheckWord;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lbRE;
        private System.Windows.Forms.Label lblWordChecker;
        private System.Windows.Forms.Label lblDefWords;
        private System.Windows.Forms.Label lblAutomataInfo;
        private System.Windows.Forms.Button btnToDFA;
        private System.Windows.Forms.ListBox lbWords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFinite;
        private System.Windows.Forms.TextBox tbDFA;
        private System.Windows.Forms.TextBox tbWordCheck;
    }
}

