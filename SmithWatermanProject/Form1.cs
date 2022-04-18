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

namespace SmithWatermanProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.Start();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt|All|*.*";
            openFileDialog.Multiselect = false;

            string filePath = openFileDialog.FileName;


            if (openFileDialog.ShowDialog() == DialogResult.OK) //dialog açıldıktan sonra
            {
                textBox4.Clear();//text kutusunu boşaltma
                filePath = openFileDialog.FileNames[0];

                //dosyayı okuma modunda açıyoruz
                FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);

                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    textBox6.Text = lines[0];//dizinin boyutunu yazdırma
                    textBox4.Text = lines[1];//dizini yazdırma
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt|All|*.*";
            openFileDialog.Multiselect = false;

            string filePath = openFileDialog.FileName;


            if (openFileDialog.ShowDialog() == DialogResult.OK) //dialog açıldıktan sonra
            {
                textBox5.Clear();//text kutusunu boşaltma
                filePath = openFileDialog.FileNames[0];

                //dosyayı okuma modunda açıyoruz
                FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);

                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    textBox7.Text = lines[0];
                    textBox5.Text = lines[1];
                }

            }
        }

        public void degerleriAl()
        {
            int match, mismatch, gap;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            if (textBox1.Text.Length == 0)
            {
                match = 1;
            }
            else
            {
                match = Convert.ToInt32(textBox1.Text);
            }

            if (textBox2.Text.Length == 0)
            {
                mismatch = -1;
            }
            else
            {
                mismatch = Convert.ToInt32(textBox2.Text);
            }

            if (textBox3.Text.Length == 0)
            {
                gap = -2;
            }
            else
            {
                gap = Convert.ToInt32(textBox3.Text);
            }

            textBox1.Text = match.ToString();
            textBox2.Text = mismatch.ToString();
            textBox3.Text = gap.ToString();
        }

        void gridviewDuzenle(string[] dizin1, string[] dizin2)
        {

            DataTable tablo = new DataTable();

            string header1 = " ";
            string header2 = "";
            tablo.Columns.Add(header1);
            tablo.Columns.Add(header1 + header1);

            DataRow row1 = tablo.NewRow();
            tablo.Rows.Add(row1);
            tablo.Rows.Add(header1);

            dataGridView1.DataSource = tablo;

            for (int i = 0; i < dizin1.Length; i++)
            {
                tablo.Columns.Add(header2);
                header2 += header2;
            }

            for (int i = 0; i < dizin2.Length; i++)//aşağı doğru olanlar
            {
                DataRow row = tablo.NewRow();
                row[header1] = dizin2[i];
                tablo.Rows.Add(row);
                dataGridView1.DataSource = tablo;
            }


            for (int i = 1; i < dizin1.Length + 1; i++)
            {
                dataGridView1.Rows[0].Cells[i + 1].Value = dizin1[i - 1];
            }

            dataGridView1.Rows[1].Cells[1].Value = 0;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int boyut1 = Convert.ToInt32(textBox6.Text);
            int boyut2 = Convert.ToInt32(textBox7.Text);

            //textboxlarda yazan dizilimleri diziye atma
            string metin = textBox4.Text;
            string[] dizin1 = new string[boyut1];

            string metin2 = textBox5.Text;
            string[] dizin2 = new string[boyut2];

            for (int i = 0; i < metin.Length; i++)
            {
                dizin1[i] = metin[i].ToString();
            }

            for (int i = 0; i < metin2.Length; i++)
            {
                dizin2[i] = metin2[i].ToString();
            }

            degerleriAl();
            gridviewDuzenle(dizin1, dizin2);
            hizala(dizin1, dizin2);
            //toparla(dizin1, dizin2);
            //timer1.Stop();
        }
        
        public int dizilimKarsilastirma(int i, int j)
        {
            int match = Convert.ToInt32(textBox1.Text);
            int mismatch = Convert.ToInt32(textBox2.Text);

            int sonuc = 0;

            if (String.Compare(dataGridView1.Rows[0].Cells[i].Value.ToString(), dataGridView1.Rows[j].Cells[0].Value.ToString()) == 0)
            {
                sonuc = match;
            }
            else
            {
                sonuc = mismatch;
            }
            return sonuc;
        }
        public int islemlerSonuc(int formul1, int formul2, int formul3)
        {
            int enbuyuk = formul1;
            int sonuc = formul1;//geçici değişken atıyoruz

            if (formul1 > formul2 && formul1 > formul3)
            {
                enbuyuk = formul1;
            }
            else if (formul2 > formul3)
            {
                enbuyuk = formul2;
            }
            else if (formul3 > formul2)
            {
                enbuyuk = formul3;
            }
            sonuc = enbuyuk;
            return sonuc;
        }

        public int dizinBoyutFarkıBul(string[] dizin1, string[] dizin2)
        {
            int fark = 0;

            if (dizin1.Length > dizin2.Length)
            {
                fark = dizin1.Length - dizin2.Length;
            }
            else if (dizin2.Length > dizin1.Length)
            {
                fark = dizin2.Length - dizin1.Length;
            }
            else if (dizin1.Length == dizin2.Length)
            {
                fark = 0;
            }
            return fark;
        }

        void ilkKisimlariDoldur(string[] dizin1, string[] dizin2)
        {
            int gap = Convert.ToInt32(textBox3.Text);
            int t1 = 0, t2 = 0, t3 = 0;
            Random rs = new Random(1);

            for (int j = 1; j < dizin1.Length + 2; j++)//BU SAĞA DORUUUUUUUUU
            {
                int i = 1;
                if (i == 1 && j == 1)
                {

                }
                else if (i - 1 >= 1 && j - 1 >= 1)
                {
                    int karsilastirma = dizilimKarsilastirma(i - 1, j - 1);

                    int parca1 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value);
                    t1 = karsilastirma + parca1;

                    int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                    t2 = gap + parca2;

                    int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                    t3 = gap + parca3;
                }
                else if (i - 1 >= 1 && j >= 1)
                {
                    int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                    t2 = gap + parca2;
                    t1 = rs.Next(-50, t2);
                    t3 = rs.Next(-50, t2);
                }
                else if (i >= 1 && j - 1 >= 1)
                {
                    int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                    t3 = gap + parca3;
                    t1 = rs.Next(-50, t3);
                    t2 = rs.Next(-50, t3);
                }
                int sonucc = islemlerSonuc(t1, t2, t3);
                if (sonucc < 0)
                {
                    sonucc = 0;
                }
                dataGridView1.Rows[i].Cells[j].Value = sonucc;
            }

            for (int i = 2; i < dizin2.Length + 2; i++)//BU AŞŞA DORUUUUUUUU
            {
                int j = 1;
                if (i == 1 && j == 1)
                {

                }
                else if (i - 1 >= 1 && j - 1 >= 1)
                {
                    int karsilastirma = dizilimKarsilastirma(i - 1, j - 1);

                    int parca1 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value);
                    t1 = karsilastirma + parca1;

                    int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                    t2 = gap + parca2;

                    int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                    t3 = gap + parca3;
                }
                else if (i - 1 >= 1 && j >= 1)
                {
                    int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                    t2 = gap + parca2;
                    t1 = rs.Next(-50, t2);
                    t3 = rs.Next(-50, t2);
                }
                else if (i >= 1 && j - 1 >= 1)
                {
                    int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                    t3 = gap + parca3;
                    t1 = rs.Next(-50, t3);
                    t2 = rs.Next(-50, t3);
                }
                int sonucc = islemlerSonuc(t1, t2, t3);
                if (sonucc < 0)
                {
                    sonucc = 0;
                }
                dataGridView1.Rows[i].Cells[j].Value = sonucc;
            }
        }
        void hizala(string[] dizin1, string[] dizin2)
        {
            ilkKisimlariDoldur(dizin1, dizin2);
            int gap = Convert.ToInt32(textBox3.Text);
            int t1 = 0, t2 = 0, t3 = 0;
            Random rs = new Random(1);
            int fark = dizinBoyutFarkıBul(dizin1, dizin2);

            for (int j = 2; j < dizin2.Length + 2; j++)//row //BU DA ORTAAAAAAAAAAA ama YANLIŞ
            {
                for (int i = 2; i < dizin1.Length + 2; i++)//cell
                {
                    int karsilastirma = dizilimKarsilastirma(i, j);

                    int parca1 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i - 1].Value);
                    t1 = karsilastirma + parca1;

                    int parca2 = Convert.ToInt32(dataGridView1.Rows[j].Cells[i-1].Value);
                    t2 = gap + parca2;

                    int parca3 = Convert.ToInt32(dataGridView1.Rows[j-1].Cells[i].Value);
                    t3 = gap + parca3;

                    if (t1<0)
                    {
                        t1 = 0;
                    }
                    if (t2<0)
                    {
                        t2 = 0;
                    }
                    if (t3<0)
                    {
                        t3 = 0;
                    }
                    int sonucc = islemlerSonuc(t1, t2, t3);
                    dataGridView1.Rows[j].Cells[i].Value = sonucc;
                }
            }
        }
    }
}
