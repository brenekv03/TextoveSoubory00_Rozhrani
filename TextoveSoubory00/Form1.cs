using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Soubory
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        //Pro práci se soubory v CSharp vždy potřebujeme nějaký stream.

        //Textový soubor je speciální typ souboru, kde jsou znaky uspořádány do řádků (pomocí řídících znaků CR a LF)
        //Textové soubory můžeme zpracovávat obecně, jako kterýkoliv jiný soubor, ale pokud chceme
        //zpracovávat text, práci nám usnadní použití speciálních streamů určených právě jen pro textové soubory: 
        //          StreamWriter 
        //          StreamReader
        
        //Textové soubory lze zpracovávat jedině sekvenčně - od začátku do konce, při zpracování se nelze v souboru vracet.
        //Proto můžeme do textového souboru buďto zapisovat nebo z něj číst. Nelze obojí současně.
        private void button1_Click(object sender, EventArgs e)
        {
            //Pro zápis do textového souboru použijeme třídu StreamWriter
            //- slouží pro otevření konkrétního textového souboru (pokud neexistuje - vytvoří se)
            //  a má metody pro zápis do textového souboru (Write a WriteLine) 
            StreamWriter streamWriter = new StreamWriter("Text.txt");
            streamWriter.WriteLine("První");
            streamWriter.WriteLine("Druhý");
            streamWriter.WriteLine("Bohdan je rasista");
            streamWriter.Write('A');
            streamWriter.Write("***");
            streamWriter.Write('X');
            streamWriter.WriteLine();
            streamWriter.Write("Bohdan je kořeň");
            streamWriter.Close(); //toto musim !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("Text.txt", true);
            streamWriter.WriteLine("LOL");
            streamWriter.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Přečte textový soubor po řádcích 
            //a každyý přečtený řádek zobrazí v listBox1

            listBox1.Items.Clear();
            StreamReader streamReader = new StreamReader("Text.txt");
            /*while(!streamReader.EndOfStream)
            {
                string s = streamReader.ReadLine();
                listBox1.Items.Add(s);
            }*/
            while(!streamReader.EndOfStream)
            {
                char c = (char)streamReader.Read();
                listBox1.Items.Add(c);
            }
            streamReader.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Přečtené řádky budeme zobrazovat v listbox

            listBox1.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Na rozdíl od řetězce nemůžeme číst znak na konci souboru
            //Metoda Peek - vrací kód znaku, který je na řadě pro další čtení - zeptáme se, zda je ještě něco ke čtení,
            //pokud žádný další znak není (konec souboru) - vrátí Peek kód -1

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //(Vytvoříme prázdné textové soubory)

            //Různé způsoby zápisu cesty: (Nelze jednoduše psát opačné lomítko)
            //Při programování aplikace používejte skoro vždy relativní cesty!!!
            StreamWriter streamWriter = new StreamWriter("Text1.txt");//v aktualní složce
            streamWriter.Close();
            streamWriter = new StreamWriter("..\\..\\Text2.txt");
            streamWriter.Close();
            streamWriter = new StreamWriter("../../Text3.txt");
            streamWriter.Close();
            streamWriter = new StreamWriter(@"..\..\Text4.txt");
            streamWriter.Close();
            streamWriter = new StreamWriter(@"..\..\soubory\Text5.txt");
            streamWriter.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Zobrazí vybraný soubor
            //Soubor vybereme pomocí komponenty OpenFileDialog nebo SaveFileDialog
            //Tyto dialogy nic neotevírají ani neukládají, jen nám umožní vybrat soubor, ostatní musíme naprogramovat sami

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox3.Items.Clear();
                StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                while (!streamReader.EndOfStream)
                {
                    string s = streamReader.ReadLine();
                    listBox3.Items.Add(s);
                }
            }
            else MessageBox.Show("Nebyl vybrán soubor");
            //Vyzkoušet jiný způsob obsloužení dialogu!!!!

            //savefiledialog je to samé
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //V textovém souboru vybraném pomocí OpenFileDialogu zapíšeme
            //na konec každého řádku *

            //Textový soubor opravíme tak, že jej celý čteme, opravené řádky nebo znaky zapisujeme
            //do pomocného textového souboru.
            //Oba streamy zavřeme, původní soubor smažeme 
            //a pomocný soubor přejmenujeme na jméno původního souboru (včetně umístění)
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox4.Items.Clear();
                listBox5.Items.Clear();
                StreamWriter streamWriter = new StreamWriter("Pomocnik.txt");
                StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                while(!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    listBox4.Items.Add(line);
                    line += "*";
                    streamWriter.WriteLine(line);

                }
                streamReader.Close();
                streamWriter.Close();
                File.Delete(openFileDialog1.FileName);
                File.Move("pomocnik.txt", openFileDialog1.FileName);
                //zobrazení opraveného souboru
                streamReader = new StreamReader(openFileDialog1.FileName);
                while(!streamReader.EndOfStream)
                {
                    listBox5.Items.Add(streamReader.ReadLine());
                }
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Pokud neurčíme kódování, bude Unicode
            StreamWriter streamWriterU = new StreamWriter("KodovaniNeurceno.txt");
            streamWriterU.WriteLine("vizáž");
            streamWriterU.WriteLine("přešívaný");
            streamWriterU.Close();



            StreamWriter sw1250 = new StreamWriter("W1250.txt", false, Encoding.GetEncoding("Windows-1250"));
            sw1250.WriteLine("vizáž");
            sw1250.Close();

            StreamWriter swDF = new StreamWriter("Default.txt", false, Encoding.Default);
            swDF.WriteLine("vizáž");
            swDF.Close();
        }
    }
}
