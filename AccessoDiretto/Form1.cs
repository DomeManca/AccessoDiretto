using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessoDiretto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("info");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string a = textBox1.Text;
            int na = a.Length;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = (Ricerca("veneto_verona.csv", textBox1.Text));
        }
        static string Ricerca(string filename, string loc)
        {
            string a = "";
            var file = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);// accesso al file binario 
            BinaryReader reader = new BinaryReader(file);
            BinaryWriter writer = new BinaryWriter(file);
            int righetot = Convert.ToInt32(file.Length);//byte totali
            int lunghezzariga = 528;
            righetot /= 528; // per ottenere il numero di righe 

            string result = "";

            int lung = Convert.ToInt32(file.Length);
            int i = 0, j = righetot - 1, m, pos = -1;

            do // RICERCA DICOTOMICA
            {
                m = (i + j) / 2;
                file.Seek(m * lunghezzariga, SeekOrigin.Begin);
                a = Encoding.ASCII.GetString(reader.ReadBytes(lunghezzariga));
                result = FromString(a, 0);

                if (myCompare(result, loc) == 0)
                {
                    pos = m;
                }
                else if (myCompare(result, loc) == -1)
                {
                    i = m + 1;
                }
                else
                    j = m - 1;


            } while (i <= j && pos == -1);

            string fine;
            if (pos != -1)
            { 
                fine = FromString(a, 7);
            }
            else
            {
                MessageBox.Show("Non esiste questa località");
                fine = "|";
            }
            
            file.Close();
            return fine;

        }
        static int myCompare(string stringa1, string stringa2)
        {
            if (stringa1 == stringa2)//0=sono uguali 1=stringa viene prima -1=stringa viene dopo
                return 0;

            char[] char1 = stringa1.ToCharArray();
            char[] char2 = stringa2.ToCharArray();
            int l = char1.Length;
            if (char2.Length < l)//in l c'è la lunghezza più piccola
                l = char2.Length;

            for (int i = 0; i < l; i++)
            {
                if (char1[i] < char2[i])
                    return -1;
                if (char1[i] > char2[i])
                    return 1;
            }

            return 0;//visto che qui non mi interessa lunghezza allora mi basta che la prima parte si uguale
        }
        public static string FromString(string Stringa, int pos, string sep = ";")//funzione che da una stringa separa i campi e ritorna una stringa
        {
            string[] pulito = Stringa.Split(';');
            return pulito[pos];
        }
    }
}
