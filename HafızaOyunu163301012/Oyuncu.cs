using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HafızaOyunu163301012
{
    public class Oyuncu
    {
        public String OyuncuAdi { get; set; }
        public int Puan { get; set; }
        public int dogruCiftSayisi { get; set; }
        public Oyuncu(string oyuncuAdi)
        {
            OyuncuAdi = oyuncuAdi;
            Puan = 0;
            dogruCiftSayisi = 0;
        }
        





    }
}
