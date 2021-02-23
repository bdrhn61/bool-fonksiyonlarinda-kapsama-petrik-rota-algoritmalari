using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TextBox[,] TextKutu;
        int x = 0;
        int y = 0;
        String alfabe = "abcdefghijklmnoprstuvyzwqx123456789";
        bool kontrol = false;
        ArrayList pi = new ArrayList();
        String sonuc = "";
        private void button1_Click(object sender, EventArgs e)
        {
           
            int coor_x = 50;
            int coor_y = 50;
            x=Int32.Parse(kolon.Text);
            y=Int32.Parse(satir.Text);
            TextKutu = new TextBox[100, 100];


            for (int i = 0; i < x; i++)
            {
                for (int k = 0; k < y; k++)
                {
                    TextKutu[i, k] = new TextBox();
                   
                    TextKutu[i, k].Location = new Point(coor_x, coor_y);
                    TextKutu[i, k].Size = new Size(20, 20);
                    this.Controls.Add(TextKutu[i, k]);
                    coor_y = coor_y + 25;


                }
                coor_x = coor_x + 25;
                coor_y = 50;

            }
        }
            

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        class alf{
            char a;
        }
        private void button2_Click(object sender, EventArgs e)
        {
         
            // tablodan verilerin alınıp yazılması  "" ****""label1""*** "

            for (int i = 0; i < x; i++)
            {
                sonuc = sonuc + "(";
                for (int k = 0; k < y; k++)
                {
                    if (TextKutu[i, k].Text == "1")
                    {
                        //k.ToString() + "+";
                        sonuc = sonuc + alfabe[k].ToString() + "+";

                    }

                }
                sonuc = sonuc.Substring(0, sonuc.Length - 1); //  Sonuncu elemanı sil
                sonuc = sonuc + ")*";

            }
            sonuc = sonuc.Substring(0, sonuc.Length - 1);
            label1.Text = sonuc;

            int ss = 0, wile = 0;
            for(int i = 0; i < sonuc.Length; i++)
            {
                if (sonuc[i].ToString() == "*")
                    ss++;
            }
            
                while(wile<ss) {    // Çarpma İŞLEMİ  paranteze dağıt
                sonuc = rekursif(sonuc);
                wile++;
                
            }
            label2.Text = sonuc;

            string sade_sonuc = toplam_sadelestir1(sonuc);
            label3.Text = sade_sonuc;

        }

        String toplam_sadelestir1(String param)
        {
            String aradeger = "";
            ArrayList toplamarray = new ArrayList();

            for (int i = 0; i < param.Length; i++)      // ArrayList  e  alma
            {
                if (param[i].ToString() != "+" && param[i].ToString() != "(" && param[i].ToString() != ")")
                {
                    aradeger += param[i].ToString();
                }
                else
                {
                    toplamarray.Add(aradeger);
                    aradeger = "";

                }
            }


            for (int i = 0; i < toplamarray.Count; i++)
            {
                for (int j = i + 1; j < toplamarray.Count; j++)
                {


                    string charsStr = "";
                    charsStr = MetinTersCevir(toplamarray[i].ToString());

                    if (toplamarray[i].ToString() == toplamarray[j].ToString() || charsStr == toplamarray[j].ToString())    // 
                    {
                        toplamarray[j] = "";

                    }


                }
            }
            for (int i = 0; i < toplamarray.Count; i++)
            {
                if (toplamarray[i].ToString() != "")
                {
                    for (int j = i+1; j < toplamarray.Count; j++)
                    {
                       if( icinde_var_mi(toplamarray[j].ToString() , toplamarray[i].ToString()))
                            toplamarray[j] = "";
                    }
                        
                }
                   
            }


            String sade_sonuc = "";

            for (int i = 0; i < toplamarray.Count; i++)
            {
                if (toplamarray[i].ToString() != "")
                    sade_sonuc += toplamarray[i].ToString() + "+";
            }

            sade_sonuc = sade_sonuc.Substring(0, sade_sonuc.Length - 1); //  Sonuncu elemanı sil
            return sade_sonuc;
        }
       
      bool icinde_var_mi(String p1, String p2)
        {
            String gecici = "";

                for (int i = 0; i < p1.Length; i++)
                {
                 for (int j = 0; j < p1.Length; j++)
                 {
                    if (p1[i] < p1[j])
                    {
                        gecici = p1[i].ToString();
                        p1 = chreplace(p1.ToString(), i, p1[j].ToString());
                        p1 = chreplace(p1.ToString(), j, gecici);
                    }
                 }
                }
             gecici = "";
            for (int i = 0; i < p2.Length; i++)
            {
                for (int j = 0; j < p2.Length; j++)
                {
                    if (p2[i] < p2[j])
                    {
                        gecici = p2[i].ToString();
                        p2 = chreplace(p2.ToString(), i, p2[j].ToString());
                        p2 = chreplace(p2.ToString(), j, gecici);
                    }
                }
            }

            if (p1.IndexOf(p2) == 0)
            {
                return true;
            }
            //p1.IndexOf(p2);

            return false;
        }
        public static string MetinTersCevir(string metin)
        {
            char[] karakter_dizisi = metin.ToCharArray();
            Array.Reverse(karakter_dizisi);
           
            return new string(karakter_dizisi);
        }
        String rekursif(String sonuc)
        {
            kontrol=false;
            String sol = "";
            String sag = "";
            int carp_son = 0;


           

            for (int i = 1; i < sonuc.Length - 1; i++)
            {

                if (sonuc[i - 1].ToString() == ")" && sonuc[i].ToString() == "*" && sonuc[i + 1].ToString() == "(")
                {
                    kontrol = true;
                    int j = i;
                    int k = i;
                    while (sonuc[i].ToString() != "(")
                    {
                        i--;
                    }
                    sol = sonuc.Substring(i + 1, j - 2);
                    //label2.Text = sol;
                    break;
                }
            }
            for (int i = 1; i < sonuc.Length - 1; i++)
            {

                if (sonuc[i - 1].ToString() == ")" && sonuc[i].ToString() == "*" && sonuc[i + 1].ToString() == "(")
                {
                    kontrol = true;
                    int j = i;
                    int k = i;
                    while (sonuc[i].ToString() != ")")
                    {
                        i++;
                    }
                    sag = sonuc.Substring(j + 2, i - j - 2);
                    
                    carp_son = i;
                   
                    break;
                }
            }


           
                sonuc = "(" + sag_sol_pars(sol, sag) + ")" + sonuc.Substring(carp_son + 1, sonuc.Length - carp_son - 1);
                sag_sol_pars(sol, sag);
            //sonuc = sag_sol(sol, sag, sonuc) + sonuc.Substring(carp_son + 1, sonuc.Length - carp_son - 1);
            //label3.Text = sonuc;
            
            // if(kontrol)
            //  rekursif(sonuc);
            //if(kontrol==true)
            //rekursif(sonuc);
           

            return sonuc;
        }

        String sag_sol_pars(String so, String sa)
        {
            ArrayList sol_taraf = new ArrayList();
            ArrayList sag_taraf = new ArrayList();
            String temp = "";
            String temp2 = "";
            String carpim = "";
            so = so + "+";
            sa = sa + "+";
            for (int i = 0; i < so.Length; i++)
            {
                if (so[i].ToString() != "+")
                {
                    temp += so[i];
                }
                else
                {
                    temp= sadelestir(temp);
                    sol_taraf.Add(temp);
                    temp = "";
                }
            }
            for (int i = 0; i < sa.Length; i++)
            {
                if (sa[i].ToString() != "+")
                {
                    temp2 += sa[i];
                }
                else
                {
                    temp2= sadelestir(temp2);
                    sag_taraf.Add(temp2);
                    temp2 = "";
                }
            }



            for(int i = 0; i < sol_taraf.Count; i++)    //   ÇArPMA İŞLEMİ
            {
                for (int j = 0; j < sag_taraf.Count; j++)
                {
                    carpim += sadelestir(sol_taraf[i].ToString() + sag_taraf[j].ToString())+"+";
                }
            }
            carpim = carpim.Substring(0, carpim.Length - 1);
           
            return carpim;
        }
        String toplam_sadelestir(String param)
        {
            String aradeger = "";
            ArrayList toplamarray = new ArrayList();

            for (int i = 0; i < param.Length; i++)      // ArrayList  e  alma
            {
                if (param[i].ToString() != "+" && param[i].ToString() != "(" && param[i].ToString() != ")")
                {
                    aradeger += param[i].ToString();
                }
                else
                {
                    toplamarray.Add(aradeger);
                    aradeger = "";

                }
            }


            for (int i = 0; i < toplamarray.Count; i++)
            {
                for (int j = i + 1; j < toplamarray.Count; j++)
                {


                    string charsStr = "";
                    charsStr = MetinTersCevir(toplamarray[i].ToString());

                    if (toplamarray[i].ToString() == toplamarray[j].ToString() || charsStr == toplamarray[j].ToString())    // 
                    {
                        toplamarray[j] = "";

                    }


                }
            }

            //label3.Text = toplamarray[0].ToString();
            //label4.Text = toplamarray[1].ToString();
            //label2.Text = sonuc.ToString();
            String sade_sonuc = "";

            for (int i = 0; i < toplamarray.Count; i++)
            {
                if (toplamarray[i].ToString() != "")
                    sade_sonuc += toplamarray[i].ToString() + "+";
            }
            sade_sonuc = sade_sonuc.Substring(0, sade_sonuc.Length - 1); //  Sonuncu elemanı sil
            return sade_sonuc;
        }
        String sadelestir(String param)
        {
            String temp = "";
            for (int i = 0; i < param.Length; i++)
            {
                for (int j = i + 1; j < param.Length; j++)
                {
                    if (param[i].ToString() == param[j].ToString())
                    {
                        temp = param[i].ToString();
                        param = param.Replace(param[i].ToString(), "");
                        param = param + temp;
                    }
                }
            }


            return param;
        }
        String sag_sol(String so, String sa, String son)
        {
            String carpim="(";
            so = so.Replace("+", "");
            sa = sa.Replace("+", "");
         
            
            for(int i = 0; i < so.Length; i++)
            {
                for (int j = 0; j < sa.Length; j++)
                {
                    carpim += so[i].ToString()+ sa[j].ToString();
                    carpim += "+";
                }

            }
            carpim = carpim.Substring(0, carpim.Length - 1);
            carpim += ")";
            return carpim;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String min_kapsama="";
           mutlak_satir();
           satir_kapsama();
           mutlak_satir();
           sutun_kapsama();

            for (int i = 0; i < pi.Count; i++)
            {
                min_kapsama += pi[i];
            }
            label4.Text = min_kapsama;

        }
        void sutun_kapsama()
        {
            int a = 0;
            int b = 0;
            y = Int32.Parse(kolon.Text);
            x = Int32.Parse(satir.Text);
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < y; k++)
                {
                    bool kapsa = false;
                    if (k != j)
                    {
                        for (int i = 0; i < x; i++)
                        {
                            if (TextKutu[j, i].Text == "1")
                            {
                                if (TextKutu[j, i].Text == TextKutu[k, i].Text)
                                {
                                    kapsa = true;
                                }
                                else
                                {
                                    kapsa = false;
                                    break;
                                }
                            }
                        }
                        if (kapsa == true)
                        {
                            for (int l = 0; l < x; l++)
                            {
                                if (TextKutu[k, l].Text == "1")
                                    a++;
                                if (TextKutu[j, l].Text == "1")
                                    b++;

                            }
                            if (a > b)
                            {
                                for (int l = 0; l < x; l++)
                                {
                                    TextKutu[k, l].Text = "";
                                }
                            }
                            else
                            {
                                for (int l = 0; l < x; l++)
                                {
                                    TextKutu[j, l].Text = "";
                                }
                            }
                        }
                    }
                }
            }
        }
        void satir_kapsama()
        {
            int a = 0;
            int b = 0;
            x = Int32.Parse(kolon.Text);
            y = Int32.Parse(satir.Text);
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < y; k++)
                {
                    bool kapsa = false;
                    if (k != j)
                    {
                        for (int i = 0; i < x; i++)
                        {
                            if (TextKutu[i, j].Text == "1")
                            {
                                if (TextKutu[i, j].Text == TextKutu[i, k].Text)
                                {
                                    kapsa = true;
                                }
                                else
                                {
                                    kapsa = false;
                                    break;
                                }
                            }
                        }
                        if (kapsa == true)
                        {
                            for (int l = 0; l < x; l++)
                            {
                                if (TextKutu[l, k].Text == "1")
                                    a++;
                                if (TextKutu[l, j].Text == "1")
                                    b++;

                            }
                            if (a < b)
                            {
                                for (int l = 0; l < x; l++)
                                {
                                    TextKutu[l, k].Text = "";
                                }
                            }
                            else
                            {
                                for (int l = 0; l < x; l++)
                                {
                                    TextKutu[l, j].Text = "";
                                }
                            }
                        }
                    }
                }
            }
        }
        void mutlak_satir()
        {
            int sayac = 0;
            y = Int32.Parse(kolon.Text);
            x = Int32.Parse(satir.Text);
            int temp = 0;



            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (TextKutu[i, j].Text == "1")
                    {
                        sayac++;
                        temp = j;
                    }
                }
                if (sayac == 1)
                {

                    pi.Add(alfabe[temp].ToString());
                    for (int k = 0; k < y; k++)
                    {
                        if (TextKutu[k, temp].Text == "1")
                        {
                            for (int l = 0; l < x; l++)
                            {
                                TextKutu[k, l].Text = "";
                            }
                        }

                    }
                }

                sayac = 0;
            }
            //label3.Text = pi[0].ToString();
            //label4.Text = pi[1].ToString();
        }
        static string chreplace(string metin, int indis, string yenideger)
        {
            metin = metin.Remove(indis, 1);
            return metin.Insert(indis, yenideger);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            y = Int32.Parse(kolon.Text);
            x = Int32.Parse(satir.Text);

            double[] sutun_agirliklari = new double[100];
            double[] satir_agirliklari = new double[100];

            for(int k = 0; k < sutun_agirliklari.Length ; k++)  //ilk deger ataması
            {
                satir_agirliklari[k] = 0;
                sutun_agirliklari[k] = 0;
            }

            for(int i = 0; i < y; i++)     // sutun agirliklari hesaplandı
            {
                int sayac = 0;
                for (int j = 0; j < x; j++)
                {
                    if (TextKutu[i, j].Text == "1")
                    {
                        sayac++;
                      
                    }
                }
                sutun_agirliklari[i] = sayac;
            }

            for (int j = 0; j < x; j++)     //satir agirliklari hesaplandı
            {
              
                for (int i = 0; i < y; i++)
                {
                    if(TextKutu[i, j].Text == "1")
                    {
                        satir_agirliklari[j] = (1 / sutun_agirliklari[i]) + satir_agirliklari[j];
                    }
                }
                satir_agirliklari[j] = x * satir_agirliklari[j];
            }

            double temp = 0;
            int temp2 = 0;
            for (int i = 1; i < x; i++)
            {
                if (satir_agirliklari[i] != 0)       //en kuçuk agirlikli satiri bulma
                {
                    temp = satir_agirliklari[i];
                    break;
                }
            }
                for (int i = 1; i < x; i++)          //en kuçuk agirlikli satiri bulma
            {
                if(temp > satir_agirliklari[i] && satir_agirliklari[i]!=0)
                {
                    temp = satir_agirliklari[i];
                    temp2 = i;
                }
            }
            label4.Text = temp.ToString();
            label3.Text = alfabe[temp2].ToString();
            for(int i = 0; i < y; i++)
            {
                TextKutu[i, temp2].Text = "";
            }
        }


        
    }
}
