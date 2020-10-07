using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using NAudio;
using NAudio.Wave;

namespace Bell54
{
    public partial class Form1 : Form
    {
        List<zvansItem> zvanuSaraksts =new List<zvansItem>();

        struct zvansItem
        {
            public String laiks;
            public String mp3;
            public int count;
        };

        bool status = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Laiks", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("MP3", 500, HorizontalAlignment.Center);
            listView1.Columns.Add("Count", 300, HorizontalAlignment.Center);

            listView1.View = View.Details;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        string lastTime = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();

            foreach (ListViewItem lit in listView1.Items)
            {
                if (lit.Text == label2.Text && lastTime!=lit.Text)
                {
                    lastTime = lit.Text;
                    Console.Out.WriteLine(lit.SubItems[1]);
                    //play!
                    IWavePlayer waveOutDevice = new WaveOut();
                    AudioFileReader audioFileReader = new AudioFileReader(lit.SubItems[1].Text);

                    waveOutDevice.Init(audioFileReader);
                    waveOutDevice.Play();
                }

                
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            status = false;
            label3.Text = "STOP";
            label3.ForeColor = Color.Red;
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            status = true;
            label3.Text = "RUN";
            label3.ForeColor = Color.Green;
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ADD
            zvansItem ZI = new zvansItem();
            ZI.laiks = dateTimePicker1.Value.ToString("HH:mm:ss");
            ZI.mp3 = textBox1.Text;
            ZI.count = int.Parse( textBox2.Text);

            

            string[] arr = new string[3];
            arr[0] = ZI.laiks;
            arr[1] = ZI.mp3;
            arr[2] = ZI.count.ToString();
            ListViewItem lv = new ListViewItem(arr);

         
            listView1.Items.Add(lv);
            zvanuSaraksts.Add(ZI);

            //MessageBox.Show(ZI.laiks);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                //MessageBox.Show(openFileDialog1.FileName);
               
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
          
            IWavePlayer waveOutDevice = new WaveOut();
            AudioFileReader audioFileReader = new AudioFileReader(zvanuSaraksts.ElementAt<zvansItem>(0).mp3);

            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.Items.ToString());
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName);
                foreach (zvansItem zvs in zvanuSaraksts) { 
                    file.WriteLine(zvs.laiks+"|"+ zvs.mp3+"|"+ zvs.count);
                }
                file.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                zvanuSaraksts.Clear();
                string[] lines = System.IO.File.ReadAllLines(openFileDialog2.FileName);

                foreach (string line in lines)
                {
                    string[] it = line.Split('|');

                    ListViewItem lv = new ListViewItem(it);

                    zvansItem ZI = new zvansItem();
                    ZI.laiks = it[0];
                    ZI.mp3 = it[1];
                    ZI.count = int.Parse(it[2]);

                    listView1.Items.Add(lv);
                    zvanuSaraksts.Add(ZI);
                }
                Console.Out.WriteLine();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Selected)
                {
                    zvanuSaraksts.RemoveAt(i);
                    listView1.Items[i].Remove();
                }
            }
        }
    }
}
