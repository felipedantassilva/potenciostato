using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace potenciostato
{
    public partial class Form1 : Form
    {
        Arduino arduino = null;
        
        public Form1()
        {
            InitializeComponent();
            button4.Enabled = false;
            button5.Enabled = false;
            textBox1.Text = Convert.ToString(0);
            textBox2.Text = Convert.ToString(0);
            textBox3.Text = Convert.ToString(0);
        }

        private void serial_combo(ComboBox combo)
        {
            combo.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                combo.Items.Add(port);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ler();
            draw();
        }
        private void ler()
        {
            
            System.IO.StreamReader arq = new System.IO.StreamReader(@"C:\Users\Felipe\Desktop\Potenciostato\Potenciostato - VS\potenciostato\dadost.txt");

            while (!arq.EndOfStream)
            {
                ltb.Items.Add(arq.ReadLine());
            }
            
            /*
            for (int i = 0; i < 50; i++)
            {
                ltb.Items.Add(Convert.ToString(i) + " " + Convert.ToString(i*-1));
            }
            */
        }

        private void draw()
        {
            Graphics gra = pcb.CreateGraphics();
            Point pt = new Point();
            Point ptAnt = new Point(0, 100);

            gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            for (int ContItens = 0; ContItens <= ltb.Items.Count - 1; ContItens++)
            {
                string str = ltb.Items[ContItens].ToString();

                for (int ContChar = 0; ContChar < str.Length; ContChar++)
                {
                    if (ltb.Items[ContItens].ToString().Substring(ContChar, 1) == " ")
                    {
                        pt.X = Convert.ToInt32(str.Remove(ContChar, str.Length - ContChar));
                        pt.Y = 100 - Convert.ToInt32(str.Remove(0, ContChar));
                        ContChar = str.Length;
                    }
                }

                gra.DrawLine(new Pen(Color.Red, 2), ptAnt.X * 5, ptAnt.Y * 5, pt.X * 5, pt.Y * 5);

                gra.FillEllipse(new SolidBrush(Color.Black), pt.X * 5 - 3, pt.Y * 5 - 3, 6, 6);

                gra.DrawString("X " + pt.X + " Y " + (100 - pt.Y), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.RoyalBlue), pt.X * 5, pt.Y * 5);

                ptAnt = pt;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            serial_combo(this.combo);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.combo.Items.Count > 0)
            {
                if (this.combo.SelectedIndex > -1)//Se alguma porta selecionada
                {
                    arduino = new Arduino(this.combo.Text, 115200);//Inicializa 0 arduino a 115200bps
                    arduino.Init_COM();
                }
            }
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal v0 = 0;
            decimal v1 = 0;
            int vel = 0;

            decimal v0d = 0;
            decimal v1d = 0;

            v0 = Convert.ToDecimal(textBox1.Text);
            v1 = Convert.ToDecimal(textBox2.Text);
            vel = Convert.ToInt32(textBox3.Text);

            if (v0 < -2)
            {
                v0 = -2;
            }
            if (v0 > 2)
            {
                v0 = 2;
            }

            if (v1 < -2)
            {
                v1 = -2;
            }
            if (v1 > 2)
            {
                v1 = 2;
            }
            if (vel < 2)
            {
                vel = 2;
            }
            if (vel > 1000)
            {
                vel = 1000;
            }

            textBox1.Text = Convert.ToString(v0);
            textBox2.Text = Convert.ToString(v1);
            textBox3.Text = Convert.ToString(vel);

            v0d = Decimal.Divide((v0+2), Convert.ToDecimal(0.0196078431));
            v1d = Decimal.Divide((v1+2), Convert.ToDecimal(0.0196078431));
            

            label3.Text = Convert.ToString(Convert.ToByte(Convert.ToInt16(v0d)));
            label4.Text = Convert.ToString(Convert.ToByte(Convert.ToInt16(v1d)));
            label6.Text = Convert.ToString(Convert.ToInt32(vel));


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap pic = new Bitmap(pcb.Image);

            /*
            //Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    //codigo para o stream
                    //myStream.Close();
                }
            }
            */
        }
    }
}
