using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace QLearning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Hucre[,] odulMatrisi = new Hucre[50, 50];
        Hucre[,] qMatrisi = new Hucre[50, 50];
        Hucre[,] array = new Hucre[5, 10];
        int isimdi = 0, jsimdi = 0;
        int isuan = 0,secilen;
        int bitisi=4 , bitisj=9;
        Random random = new Random(Guid.NewGuid().GetHashCode());
        int giti = 0, gitj = 0; int yazdir;
        int gitiGelecek = 0, gitjGelecek = 0;
        float learning=1;
        public void Baslat(Hucre[,] array)
        {
            int l = 1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    array[i, j] = new Hucre();
                    array[i, j].liste = new List<Hucre>();
                    array[i, j].l = l;
                    array[i, j].i = i;
                    array[i, j].j = j;
                    l++;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i - 1 != -1)
                    {
                        array[i, j].liste.Add(array[i - 1, j]);
                    }
                    if (i + 1 != 5)
                    {
                        array[i, j].liste.Add(array[i + 1, j]);
                    }
                    if (j - 1 != -1)
                    {
                        array[i, j].liste.Add(array[i, j - 1]);
                    }
                    if (j + 1 != 10)
                    {
                        array[i, j].liste.Add(array[i, j + 1]);
                    }
                }
            }

            for (int k = 0; k < 50; k++)
            {
                for (int m = 0; m < 50; m++)
                {
                    odulMatrisi[k, m] = new Hucre();
                    if (k < 10)
                    {
                        odulMatrisi[k,m].gideni = 0;
                        odulMatrisi[k, m].gidenj =k;
                    }
                    else if (k < 20)
                    {
                        odulMatrisi[k, m].gideni = 1;
                        odulMatrisi[k, m].gidenj = k%10;
                    }
                    else if (k < 30)
                    {
                        odulMatrisi[k, m].gideni = 2;
                        odulMatrisi[k, m].gidenj = k % 10;
                    }
                    else if (k < 40)
                    {
                        odulMatrisi[k, m].gideni = 3;
                        odulMatrisi[k, m].gidenj = k % 10;
                    }
                    else if (k < 50)
                    {
                        odulMatrisi[k, m].gideni = 4;
                        odulMatrisi[k, m].gidenj = k % 10;
                    }
                    if (m < 10)
                    {
                        odulMatrisi[k, m].gidileni = 0;
                        odulMatrisi[k, m].gidilenj = m;
                    }
                    else if (m < 20)
                    {
                        odulMatrisi[k, m].gidileni = 1;
                        odulMatrisi[k, m].gidilenj = m % 10;
                    }
                    else if (m < 30)
                    {
                        odulMatrisi[k, m].gidileni = 2;
                        odulMatrisi[k, m].gidilenj = m % 10;
                    }
                    else if (m < 40)
                    {
                        odulMatrisi[k, m].gidileni = 3;
                        odulMatrisi[k, m].gidilenj = m % 10;
                    }
                    else if (m < 50)
                    {
                        odulMatrisi[k, m].gidileni = 4;
                        odulMatrisi[k, m].gidilenj = m % 10;
                    }
                }
            }

            array[isimdi, jsimdi].k = 1;
            array[bitisi, bitisj].k = -1;
            for (int k = 0; k < 50; k++)
            {
                for (int m = 0; m < 50; m++)
                {
                    if (k==m)
                    {
                        odulMatrisi[k, m].odul = -1;
                        if (odulMatrisi[k, m].gidileni == 4 && odulMatrisi[k, m].gidilenj == 9)
                        {
                            odulMatrisi[k, m].odul = 100;
                        }
                    }
                    else
                    {
                        if( (Math.Abs(odulMatrisi[k,m].gidileni- odulMatrisi[k, m].gideni)==1 && Math.Abs(odulMatrisi[k, m].gidilenj - odulMatrisi[k, m].gidenj) == 0) || (Math.Abs(odulMatrisi[k, m].gidileni - odulMatrisi[k, m].gideni) == 0 && Math.Abs(odulMatrisi[k, m].gidilenj - odulMatrisi[k, m].gidenj) == 1))
                        {
                            if (odulMatrisi[k, m].gidileni - 1 != -1)
                            {
                                odulMatrisi[k, m].odul = 0;
                                if (odulMatrisi[k, m].gidileni == 4 && odulMatrisi[k, m].gidilenj == 9)
                                {
                                    odulMatrisi[k, m].odul = 100;
                                }
                            }
                            if (odulMatrisi[k, m].gidileni + 1 != 5)
                            {
                                odulMatrisi[k, m].odul = 0;
                                if (odulMatrisi[k, m].gidileni == 4 && odulMatrisi[k, m].gidilenj == 9)
                                {
                                    odulMatrisi[k, m].odul = 100;
                                }
                            }
                            if (odulMatrisi[k, m].gidilenj - 1 != -1)
                            {
                                odulMatrisi[k, m].odul = 0;
                                if (odulMatrisi[k, m].gidileni == 4 && odulMatrisi[k, m].gidilenj == 9)
                                {
                                    odulMatrisi[k, m].odul = 100;
                                }
                            }
                            if (odulMatrisi[k, m].gidilenj + 1 != 10)
                            {
                                odulMatrisi[k, m].odul = 0;
                                if (odulMatrisi[k, m].gidileni == 4 && odulMatrisi[k, m].gidilenj == 9)
                                {
                                    odulMatrisi[k, m].odul = 100;
                                }
                            }
                        }
                        else
                        {
                            odulMatrisi[k, m].odul = -1;
                        }
                        
                    }
                }
            }


            for (int k = 0; k < 50; k++)
            {
                for (int m = 0; m < 50; m++)
                {
                    qMatrisi[k, m] = new Hucre();
                    if (k < 10)
                    {
                        qMatrisi[k, m].gideni = 0;
                        qMatrisi[k, m].gidenj = k;
                    }
                    else if (k < 20)
                    {
                        qMatrisi[k, m].gideni = 1;
                        qMatrisi[k, m].gidenj = k % 10;
                    }
                    else if (k < 30)
                    {
                        qMatrisi[k, m].gideni = 2;
                        qMatrisi[k, m].gidenj = k % 10;
                    }
                    else if (k < 40)
                    {
                        qMatrisi[k, m].gideni = 3;
                        qMatrisi[k, m].gidenj = k % 10;
                    }
                    else if (k < 50)
                    {
                        qMatrisi[k, m].gideni = 4;
                        qMatrisi[k, m].gidenj = k % 10;
                    }
                    if (m < 10)
                    {
                        qMatrisi[k, m].gidileni = 0;
                        qMatrisi[k, m].gidilenj = m;
                    }
                    else if (m < 20)
                    {
                        qMatrisi[k, m].gidileni = 1;
                        qMatrisi[k, m].gidilenj = m % 10;
                    }
                    else if (m < 30)
                    {
                        qMatrisi[k, m].gidileni = 2;
                        qMatrisi[k, m].gidilenj = m % 10;
                    }
                    else if (m < 40)
                    {
                        qMatrisi[k, m].gidileni = 3;
                        qMatrisi[k, m].gidilenj = m % 10;
                    }
                    else if (m < 50)
                    {
                        qMatrisi[k, m].gidileni = 4;
                        qMatrisi[k, m].gidilenj = m % 10;
                    }
                }
            }
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    qMatrisi[i, j].odul = 0;
                }
            }
        }
        List<Hucre> listecik = new List<Hucre>();
        List<Hucre> listecikGelecek = new List<Hucre>();

        private void button1_Click(object sender, EventArgs e)
        {
            iterasyonSayisi = Convert.ToInt32(textBox1.Text);
        }

        public void BitisOlustur(string butonadi,string butonadi2)
        {
            foreach (Button btn in this.Controls.OfType<Button>())
            {
                if (btn.Name == butonadi)
                {
                    btn.Text = "X";
                }
                if (btn.Name == butonadi2)
                {
                    btn.Text = "O";
                }
            }
        }
        public void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            //Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                //Console.WriteLine("stop wait timer");
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        int iterasyonSayisi = 5;

        public void Calis()
        {
            string butonadi2;
            string butonadi;
            string ilki;
            float q;
            int giti, gitj;
            int gitiGelecek, gitjGelecek;
            float max = 0;
            float maxGelecek = 0;
            float eklencekOdul = 0;
            float qst = 0;
            int index;
            for (int caliss = 0; caliss < iterasyonSayisi; caliss++) //HER LEARN BUTONUNA BASILINCA ÇALIŞAN EĞİTİM İTERASYON SAYISI
            {
                isimdi = 0; jsimdi = 0;
                isuan = 0;
                bitisi = 4; bitisj = 9;
                array[isimdi, jsimdi].k = 1;
                array[bitisi, bitisj].k = -1;
                butonadi = "button" + bitisi.ToString() + bitisj.ToString();
                butonadi2 = "button" + isimdi.ToString() + jsimdi.ToString();
                BitisOlustur(butonadi, butonadi2);
                Random random = new Random(Guid.NewGuid().GetHashCode());
                giti = 0; gitj = 0;
                gitiGelecek = 0; gitjGelecek = 0;
                for (int say = 0; 1<2; say++)
                {
                    listecik.Clear();
                    listecikGelecek.Clear();
                    wait(1); // HAREKET İZLEMEK İÇİN HIZ AYARI
                    max = 0;
                    maxGelecek = 0;
                    eklencekOdul = 0;
                    qst = 0;
                    for (int m = 0; m < 50; m++)
                    {
                        if (0 <= odulMatrisi[isuan, m].odul)
                        {
                            if (max <= qMatrisi[isuan, m].odul)
                            {
                                max = odulMatrisi[isuan, m].odul;
                                giti = qMatrisi[isuan, m].gidileni;
                                gitj = qMatrisi[isuan, m].gidilenj;
                                listecik.Add(odulMatrisi[isuan, m]);
                                qst=qMatrisi[isuan, secilen].odul;
                            }

                        }
                    }
                    if (max == 0)
                    {
                        index = random.Next(listecik.Count);
                        giti = listecik[index].gidileni;
                        gitj = listecik[index].gidilenj;
                        max = listecik[index].odul;
                        qst = listecik[index].odul;
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (giti == i && gitj == j)
                            {
                                if (giti == 0)
                                {
                                    ilki = "0" + (gitj % 10).ToString();
                                    secilen = Int16.Parse(ilki);
                                }
                                else if (giti == 1)
                                {
                                    ilki = "1" + (gitj % 10).ToString();
                                    secilen = Int16.Parse(ilki);
                                }
                                else if (giti == 2)
                                {
                                    ilki = "2" + (gitj % 10).ToString();
                                    secilen = Int16.Parse(ilki);
                                }
                                else if (giti == 3)
                                {
                                    ilki = "3" + (gitj % 10).ToString();
                                    secilen = Int16.Parse(ilki);
                                }
                                else if (giti == 4)
                                {
                                    ilki = "4" + (gitj % 10).ToString();
                                    secilen = Int16.Parse(ilki);
                                }
                            }

                        }
                    }

                    for (int m = 0; m < 50; m++)
                    {
                        if (0 <= odulMatrisi[secilen, m].odul)
                        {
                            if (maxGelecek <= qMatrisi[secilen, m].odul)
                            {
                                maxGelecek = qMatrisi[secilen, m].odul;
                                gitiGelecek = qMatrisi[secilen, m].gidileni;
                                gitjGelecek = qMatrisi[secilen, m].gidilenj;
                                listecikGelecek.Add(qMatrisi[secilen, m]);
                                eklencekOdul = odulMatrisi[secilen, m].odul;
                            }

                        }
                    }
                    if (maxGelecek == 0)
                    {
                        index = random.Next(listecikGelecek.Count);
                        gitiGelecek = listecikGelecek[index].gidileni;
                        gitjGelecek = listecikGelecek[index].gidilenj;
                        maxGelecek = listecikGelecek[index].odul;
                        eklencekOdul = listecikGelecek[index].odul;
                    }

                    array[isimdi, jsimdi].k = 0;
                    butonadi = "button" + isimdi.ToString() + jsimdi.ToString();
                    foreach (Button btn in this.Controls.OfType<Button>())
                    {
                        if (btn.Name == butonadi)
                        {
                            btn.Text = " ";
                        }
                    }

                    butonadi = "button" + giti.ToString() + gitj.ToString();
                    foreach (Button btn in this.Controls.OfType<Button>())
                    {
                        if (btn.Name == butonadi)
                        {
                            btn.Text = "O";
                        }
                    }
                    q = qst + learning*( max + ((float)0.8 * maxGelecek) - qst);
                    qMatrisi[isuan, secilen].odul = q;
                    isimdi = giti;
                    jsimdi = gitj;
                    isuan = secilen;
                    if (array[giti, gitj].k == -1)
                    {
                        break;
                    }
                }

            }
            label1.Text = "Q Matrisi:\n";
            for (int k = 0; k < 50; k++)
            {
                for (int m = 0; m < 50; m++)
                {
                    yazdir = (int)Math.Ceiling(qMatrisi[k, m].odul);
                    label1.Text += yazdir.ToString() + "    ";
                }
                label1.Text += "\n";
            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            Calis();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Hucre[,] array = new Hucre[5, 10];
            Baslat(array);
            textBox1.Text = iterasyonSayisi.ToString();
            label1.Text = "Q Matrisi:\n";
            for (int k = 0; k < 50; k++)
            {
                for (int m = 0; m < 50; m++)
                {
                    yazdir = (int)Math.Ceiling(qMatrisi[k, m].odul);
                    label1.Text += yazdir.ToString() + "    ";
                }
                label1.Text += "\n";
            }
        }
    }
    public class Hucre
    {
        public int k = 0;
        public int l;
        public List<Hucre> liste;
        public int i;
        public int j;
        public float odul;
        public int gideni, gidenj;
        public int gidileni, gidilenj;
    }
}
