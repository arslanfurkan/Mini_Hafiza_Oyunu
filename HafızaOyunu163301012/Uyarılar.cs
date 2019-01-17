using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HafızaOyunu163301012
{
    class Uyarılar
    {
        public static void AynıResmeTıkladı()
        {
            MessageBox.Show("O resme zaten tıkladın.", "Baska resim sec", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Kazanan(Oyuncu oyuncu)
        {
            MessageBox.Show("Tebrikler '" + oyuncu.OyuncuAdi + "',\n" + oyuncu.dogruCiftSayisi + " adet resmi dogru bildin.\n" + oyuncu.Puan + " PUAN elde ettin.","'"+oyuncu.OyuncuAdi+"' KAZANDI!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static void Berabere()
        {
            MessageBox.Show("Şansa bak ikinizinde puan ve dogru tahmin sayısı aynı","SONUÇ BERABERE!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult OyunBittiginde()
        {
            return MessageBox.Show("Oyun bitti!\n Bir daha oynamak için Evet'i,\nÇıkmak için Hayır'ı tuşlayınız.", "OYUN BİTTİ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
