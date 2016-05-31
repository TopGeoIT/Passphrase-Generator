using PassphraseGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp
{
    public partial class Form1 : Form
    {
        public PassphraseController passphrasegen;
        public Form1()
        {
            InitializeComponent();
            this.passphrasegen = new PassphraseController("../../../Dictionaries/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int enthropy = Convert.ToInt32(textBox1.Text);
            string[] sentence = this.passphrasegen.generateSentenceFromEntrophy(enthropy);

            string allSentence = string.Join(" ", sentence);
            string binary = passphrasegen.generateBinaryFromSentence(allSentence);

            textBox2.Text = binary;
            textBox3.Text = allSentence;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] sentence = passphrasegen.generateSentenceFromBinary(textBox2.Text);

            string allSentence = string.Join(" ", sentence);

            textBox3.Text = allSentence;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string binary = passphrasegen.generateBinaryFromSentence(textBox3.Text);

            textBox2.Text = binary;
        }
    }
}
