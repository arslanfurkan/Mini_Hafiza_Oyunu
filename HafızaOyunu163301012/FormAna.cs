using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace HafızaOyunu163301012
{
    public partial class FormAna : Form
    {
        public FormAna()
        {
            InitializeComponent();
        }
        Oyuncu oyuncu1;
        Oyuncu oyuncu2;
        PictureBox birOncekiPB=null;
        YeniOyun yeniOyun;

        private void buttonBasla_Click(object sender, EventArgs e)
        {
            oyuncu1 = new Oyuncu("Oyuncu1");
            oyuncu2 = new Oyuncu("Oyuncu2");
            yeniOyun = new YeniOyun();
            labelSıraSende1.Show();

            OyuncuPanelGuncelle();
            
            panelOyun.Show();
            TagAta();
            ResimleriYerlestir();
            timerBasla.Enabled = true;
            timerBasla.Start();
            buttonBasla.Hide();
           
        }
        //Oyuncu Panellerini Günceller
        public void OyuncuPanelGuncelle()
        {
            label1.Text = oyuncu1.OyuncuAdi;
            label2.Text = oyuncu2.OyuncuAdi;

            labelOyuncu1Dogru.Text = oyuncu1.dogruCiftSayisi.ToString();
            labelOyuncu2Dogru.Text = oyuncu2.dogruCiftSayisi.ToString();

            labelPuan1.Text = oyuncu1.Puan.ToString();
            labelPuan2.Text = oyuncu2.Puan.ToString();

        }

        //pictureboxlar 40 tane resim adeti 20 tane bu her 2 picture boxun taglari aynı olmak zorunda ve
        //bu taglari ramdom bir sekilde pictureboxlara yerleştiren method

        public void TagAta()
        {
            List<int> taglar = new List<int>();
            for (int i = 1; i <= 40; i++)
            {
                taglar.Add((i%20)+1);
            }
            Random rn = new Random();
            foreach (PictureBox pb in panelOyun.Controls)
            {
                int rastgele = rn.Next(taglar.Count);
                pb.Tag = taglar[rastgele];
                taglar.RemoveAt(rastgele);
            }
        }
      


        //pictureBoxlara rastgele atanan taglara göre sirayla resim yerleştiren metod
        public void ResimleriYerlestir()
        {
            foreach (PictureBox pb in panelOyun.Controls)
            {
                pb.Image = ımageList1.Images[(int)pb.Tag];
                //pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }  
        }

      
        //resimlerle ilgili metodlar
        public void ResimleriGizle()
        {
            foreach (PictureBox pb in panelOyun.Controls)
            {
                pb.Image = ımageList1.Images[0];
            }
        }
        public void PictureBoxlariGoster()
        {
            foreach (PictureBox pb in panelOyun.Controls)
            {
                pb.Show();
            }
        }
        

        //PictureboxClick Event
        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            labelSure.Show();

            PictureBox pb = (sender as PictureBox);
            

            if (birOncekiPB == null)
            {
                birOncekiPB = pb;
                birOncekiPB.Image = ımageList1.Images[(int)pb.Tag];
             
                timerTıklama.Enabled = true;
                timerTıklama.Start();

            }
            else if(birOncekiPB == pb)
            {
                Uyarılar.AynıResmeTıkladı();   
            }
            else
            {
                
                timerTıklama.Enabled = false;
                timerTıklama.Stop();
                labelSure.Text = "5";
                labelSure.Hide();

                pb.Image = ımageList1.Images[(int)pb.Tag];
                //burada biraz bekletilebilir ki 2. tıklanılan resmi oyuncular görsün.
                panelOyun.Enabled = false;
                await Task.Delay(2000);
                panelOyun.Enabled = true;

                if (birOncekiPB.Tag.ToString() == pb.Tag.ToString())
                {
                    yeniOyun.kalanResimSayisi = yeniOyun.kalanResimSayisi- 2;
                    //Asagidaki 2 satır kod bilinen resimleri kaldirir.
                    
                    birOncekiPB.Hide();
                    pb.Hide();
                   

                    //bilen kisiye 3 puan ekleyip sırayı değiştirir.
                    if (yeniOyun.SıraOyuncu1deMi) {
                        oyuncu1.Puan = oyuncu1.Puan + 3;
                        oyuncu1.dogruCiftSayisi = oyuncu1.dogruCiftSayisi + 1;
                    }

                    else {
                        oyuncu2.Puan = oyuncu2.Puan+ 3;
                        oyuncu2.dogruCiftSayisi = oyuncu2.dogruCiftSayisi + 1;
                    } 
                    
                    SıraDegistir();
                 

                }
                else
                {
                  
                    PuanEkle();
                    ResimleriGizle();
                   

                }
                
                birOncekiPB = null;
                OyuncuPanelGuncelle();

                if (OyunBittiMiKontrol())
                {
                    DialogResult dr = Uyarılar.OyunBittiginde();
                    if (dr == DialogResult.Yes)
                    {
                        FabrikaAyarlarınaDon();
                    }
                    else
                    {
                        this.Close();
                    }

                }

            }

        }
        //timerlar
        private void timerBasla_Tick(object sender, EventArgs e)
        {
            int sayac = int.Parse(labelSure.Text);
            sayac--;
            labelSure.Text = sayac.ToString();
            if (sayac == 0)
            {
                labelSure.Text = "5";
                labelSure.Hide();
                
                panelOyun.Enabled = true;
                ResimleriGizle();
                timerBasla.Enabled = false;
                timerBasla.Stop();
            }

        }
        private void timerTıklama_Tick(object sender, EventArgs e)
        {
            labelSure.Show();
            int sayac = int.Parse(labelSure.Text);
            sayac--;
            labelSure.Text = sayac.ToString();
            if (sayac == 0)
            {
                labelSure.Text = "5";
                labelSure.Hide();        
                
                ResimleriGizle();                                             
                birOncekiPB = null;

                PuanEkle();
                OyuncuPanelGuncelle();

                timerTıklama.Enabled = false;
                timerTıklama.Stop();
            }

        }
        public void PuanEkle()
        {
            if (yeniOyun.SıraOyuncu1deMi)
            {
                oyuncu2.Puan = oyuncu2.Puan + 1;
            }
            else
            {
                oyuncu1.Puan = oyuncu1.Puan+ 1;
            }
            SıraDegistir();
        }

        public void SıraDegistir()
        {
            yeniOyun.SıraOyuncu1deMi = !(yeniOyun.SıraOyuncu1deMi);
            if (yeniOyun.SıraOyuncu1deMi)
            {
                labelSıraSende1.Show();
                labelSıraSende2.Hide();
            }
            else
            {
                labelSıraSende2.Show();
                labelSıraSende1.Hide();
            }
        }

        public bool OyunBittiMiKontrol()
        {
            if(yeniOyun.kalanResimSayisi==0 && oyuncu1.dogruCiftSayisi == oyuncu2.dogruCiftSayisi)
            {
                if(oyuncu1.Puan > oyuncu2.Puan)
                {
                    Uyarılar.Kazanan(oyuncu1);
                }
                else if(oyuncu1.Puan == oyuncu2.Puan)
                {
                    Uyarılar.Berabere();
                }
                else
                {
                    Uyarılar.Kazanan(oyuncu2);
                }
                return true;
            }
            else if(oyuncu1.dogruCiftSayisi >10)
            {
                Uyarılar.Kazanan(oyuncu1);
                return true;
            }
            else if (oyuncu2.dogruCiftSayisi > 10)
            {
                Uyarılar.Kazanan(oyuncu2);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FabrikaAyarlarınaDon()
        {
            oyuncu1=null;
            oyuncu2=null;
            birOncekiPB = null;
            yeniOyun=null;
            PictureBoxlariGoster();
            panelOyun.Enabled = false;
            panelOyun.Hide();
            buttonBasla.Show();
            labelOyuncu1Dogru.Text = "0";
            labelOyuncu2Dogru.Text = "0";
            labelPuan1.Text = "0";
            labelPuan2.Text = "0";

            labelSıraSende1.Hide();
            labelSıraSende2.Hide();

            
        }
        public FormOynanis _oynanis = null;

        private void oynanısToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _oynanis = FormOynanis.Olustur(this);
            _oynanis.Show();
            _oynanis.Activate();
        }
    }
   
}
