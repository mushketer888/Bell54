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
        List<bellItem> bellItems =new List<bellItem>();

        struct bellItem
        {
            public String time;
            public String mp3file;
        };



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Time", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("MP3", 600, HorizontalAlignment.Center);
          
            //listView1.Columns.Add("Count", 300, HorizontalAlignment.Center);

            listView1.View = View.Details;
        }


        string lastTime = "";
        private bool runStatus=true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToLongTimeString();

            foreach (ListViewItem lit in listView1.Items)
            {
                if (lit.Text == timeLabel.Text && lastTime!=lit.Text)
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
            runStatus = false;
            label3.Text = "STOP";
            label3.ForeColor = Color.Red;
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            runStatus = true;
            label3.Text = "RUN";
            label3.ForeColor = Color.Green;
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void addItem(object sender, EventArgs e)
        {
            //ADD
            bellItem ZI = new bellItem();
            ZI.time = dateTimePicker1.Value.ToString("HH:mm:ss");
            ZI.mp3file = textBox1.Text;

            

            string[] arr = new string[3];
            arr[0] = ZI.time;
            arr[1] = ZI.mp3file;
            ListViewItem lv = new ListViewItem(arr);

         
            listView1.Items.Add(lv);
            bellItems.Add(ZI);

            //MessageBox.Show(ZI.laiks);


        }

        private void chooseFile(object sender, EventArgs e)
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
            AudioFileReader audioFileReader = new AudioFileReader(bellItems.ElementAt<bellItem>(0).mp3file);

            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        private void saveList(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.Items.ToString());
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName);
                foreach (bellItem zvs in bellItems) { 
                    file.WriteLine(zvs.time+";"+ zvs.mp3file);
                }
                file.Close();
            }
        }

        private void LoadList(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bellItems.Clear();
                string[] lines = System.IO.File.ReadAllLines(openFileDialog2.FileName);

                foreach (string line in lines)
                {
                    string[] it = line.Split('|');

                    ListViewItem lv = new ListViewItem(it);

                    bellItem ZI = new bellItem();
                    ZI.time = it[0];
                    ZI.mp3file = it[1];

                    listView1.Items.Add(lv);
                    bellItems.Add(ZI);
                }
                Console.Out.WriteLine();
            }
        }

        private void deleteItem(object sender, EventArgs e)
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Selected)
                {
                    bellItems.RemoveAt(i);
                    listView1.Items[i].Remove();
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
