using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Calculator_de_medii_FInal
{

    public partial class Calculator_fereastra : Form
    {
        Random random = new Random();
        object syncLock = new object();
        static List<CheckBox> lista_casute_materii = new List<CheckBox>();
        static List<Button> lista_butoane_m1_calculeaza = new List<Button>();
        static List<Button> lista_butoane_m2_calculeaza = new List<Button>();
        static List<double> lista_medii_materii_1 = new List<double>();
        static List<double> lista_medii_materii_2 = new List<double>();
        static List<double> lista_medii_materii_generale = new List<double>();
        static List<Label> lista_label_medii_materii_1 = new List<Label>();
        static List<Label> lista_label_medii_materii_2 = new List<Label>();
        static List<Label> lista_label_medii_materii_generale = new List<Label>();
        static List<CheckBox> lista_casute_teze = new List<CheckBox>();
        static List<ComboBox> lista_note_teze = new List<ComboBox>();
        static List<List<CheckBox>> lista_toate_casute_note_materii = new List<List<CheckBox>>();
        static List<List<ComboBox>> lista_toate_note_materii = new List<List<ComboBox>>();
        static List<bool> lista_este_secundara_sau_nu = new List<bool>();
        static List<Label> lista_label_indicatie = new List<Label>();

        static List<Button> lista_un_element_buton_plus = new List<Button>();
        static List<RadioButton> lista_un_element_radio_materie_principala = new List<RadioButton>();
        static List<RadioButton> lista_un_element_radio_materie_secundara = new List<RadioButton>();
        static List<Label> lista_un_element_label_indicatie_buton_plus = new List<Label>();
        static List<Label> lista_un_element_label_medie_generala_anuala = new List<Label>();
        static List<double> lista_un_element_medie_generala_anuala = new List<double>();
        int start_X = 10;
        int start_Y = 70;
        int y_ultimul_element = 0;
        public Calculator_fereastra()
        {
            InitializeComponent();
            Creeare_Controale();
        }
        void Creeare_Controale()
        {
            List<String> nume_materii = new List<String>();
            nume_materii.Add("Limba română");
            nume_materii.Add("Limba străină 1");
            nume_materii.Add("Limba străină 2");
            nume_materii.Add("Matematică");
            nume_materii.Add("Fizică");
            nume_materii.Add("Chimie");
            nume_materii.Add("Biologie");
            nume_materii.Add("Geografie");
            nume_materii.Add("Istorie");
            nume_materii.Add("Informatică");
            nume_materii.Add("Muzică");
            nume_materii.Add("Desen");
            nume_materii.Add("Științe sociale");
            nume_materii.Add("Educație fizică");

            for (int i = 0; i < nume_materii.Count; i++)
            {
                Adauga_Controale_Materie(nume_materii[i]);

            }
            Adauga_Controale_Materie_Secundara("Purtare");
            Adauga_Controale_Finale();
        }
        void Adauga_Controale_Finale()
        {
            Button buton_plus = new Button();
            lista_un_element_buton_plus.Add(buton_plus);
            buton_plus.Width = buton_plus.Height = 36;
            buton_plus.Text = "+";
            buton_plus.Location = new System.Drawing.Point(start_X, start_Y);
            buton_plus.Font = new Font(buton_plus.Font.FontFamily, 16);
            int index_curent = lista_casute_materii.Count;
            buton_plus.Click += new System.EventHandler((sender, e) => this.Buton_plus_click(sender, e, index_curent));
            this.Controls.Add(buton_plus);
            RadioButton radio_materie_principala = new RadioButton();
            lista_un_element_radio_materie_principala.Add(radio_materie_principala);
            radio_materie_principala.Location = new System.Drawing.Point(buton_plus.Left + 50, buton_plus.Top);
            radio_materie_principala.Text = "Principală";
            radio_materie_principala.Width = 80;
            radio_materie_principala.Height = 25;
            this.Controls.Add(radio_materie_principala);

            RadioButton radio_materie_secundara = new RadioButton();
            lista_un_element_radio_materie_secundara.Add(radio_materie_secundara);
            radio_materie_secundara.Location = new System.Drawing.Point(buton_plus.Left + 50, buton_plus.Top + 25);
            radio_materie_secundara.Text = "Secundară";
            radio_materie_secundara.Width = 80;
            radio_materie_secundara.Height = 25;
            this.Controls.Add(radio_materie_secundara);

            Label label_indicatie_buton_plus = new Label();
            lista_un_element_label_indicatie_buton_plus.Add(label_indicatie_buton_plus);
            label_indicatie_buton_plus.Text = "?";
            label_indicatie_buton_plus.Font = new Font(label_indicatie_buton_plus.Font.FontFamily, 10, FontStyle.Bold);
            label_indicatie_buton_plus.Width = label_indicatie_buton_plus.Height = 16;
            label_indicatie_buton_plus.Location = new System.Drawing.Point(radio_materie_principala.Left + 85, radio_materie_principala.Top + 12);
            label_indicatie_buton_plus.BackColor = Color.DodgerBlue;
            label_indicatie_buton_plus.ForeColor = Color.White;
            ToolTip tooltip_indicatie_buton_plus = new ToolTip();
            tooltip_indicatie_buton_plus.SetToolTip(label_indicatie_buton_plus, @"Butonul ""+"" va adăuga o nouă materie" + "\n" +
                                                                                "pentru a fi ulterior completată." + "\n" +
                                                                                "Trebuie bifat dacă va fi materie principală," + "\n" +
                                                                                "adică să aibă mai multe note și o potențială teză" + "\n" +
                                                                                "sau să fie secundară, doar cu o notă." + "\n\n" +
                                                                                "Exemplu: Materie secundară - Purtare");
            tooltip_indicatie_buton_plus.AutoPopDelay = 30000;
            this.Controls.Add(label_indicatie_buton_plus);

            double medie_generala_anuala = 0.00;
            lista_un_element_medie_generala_anuala.Add(medie_generala_anuala);

            Label label_medie_generala_anuala = new Label();
            lista_un_element_label_medie_generala_anuala.Add(label_medie_generala_anuala);
            label_medie_generala_anuala.Text = "MGA: " + medie_generala_anuala.ToString("F");
            label_medie_generala_anuala.Location = new System.Drawing.Point(buton_plus.Left + 325, label_indicatie_buton_plus.Top);
            this.Controls.Add(label_medie_generala_anuala);
        }
        void Adauga_Controale_Materie(string nume_materie)
        {
            CheckBox casuta_materie = new CheckBox();
            lista_casute_materii.Add(casuta_materie);
            int index_curent = lista_casute_materii.IndexOf(casuta_materie);
            casuta_materie.Text = nume_materie;
            casuta_materie.Location = new System.Drawing.Point(start_X, start_Y);
            casuta_materie.Checked = true;
            casuta_materie.CheckedChanged += new System.EventHandler((sender, e) => this.Casuta_materie_check(sender, e, index_curent));
            this.Controls.Add(casuta_materie);

            bool este_secundar_sau_nu = false;
            lista_este_secundara_sau_nu.Add(este_secundar_sau_nu);

            Button buton_calculeaza_m1 = new Button();
            lista_butoane_m1_calculeaza.Add(buton_calculeaza_m1);
            buton_calculeaza_m1.Text = "Calculează M1";
            buton_calculeaza_m1.Width = 85;
            buton_calculeaza_m1.Location = new System.Drawing.Point(casuta_materie.Left + 110, casuta_materie.Top);
            buton_calculeaza_m1.Click += new System.EventHandler((sender, e) => this.Buton_calculeaza_m1_click(sender, e, index_curent));
            this.Controls.Add(buton_calculeaza_m1);


            Button buton_calculeaza_m2 = new Button();
            lista_butoane_m2_calculeaza.Add(buton_calculeaza_m2);
            buton_calculeaza_m2.Text = "Calculează M2";
            buton_calculeaza_m2.Width = 85;
            buton_calculeaza_m2.Location = new System.Drawing.Point(buton_calculeaza_m1.Left, buton_calculeaza_m1.Top + 25);
            buton_calculeaza_m2.Click += new System.EventHandler((sender, e) => this.Buton_calculeaza_m2_click(sender, e, index_curent));
            this.Controls.Add(buton_calculeaza_m2);

            Label medie_materie_1 = new Label();
            lista_label_medii_materii_1.Add(medie_materie_1);
            double medie_1 = 0.00;
            lista_medii_materii_1.Add(medie_1);
            medie_materie_1.Text = "M1: " + medie_1.ToString("F");
            medie_materie_1.Location = new System.Drawing.Point(casuta_materie.Left + 325, casuta_materie.Top + 5);
            this.Controls.Add(medie_materie_1);

            Label medie_materie_2 = new Label();
            lista_label_medii_materii_2.Add(medie_materie_2);
            double medie_2 = 0.00;
            lista_medii_materii_2.Add(medie_2);
            medie_materie_2.Text = "M2: " + medie_2.ToString("F");
            medie_materie_2.Location = new System.Drawing.Point(medie_materie_1.Left, medie_materie_1.Top + 25);
            this.Controls.Add(medie_materie_2);

            Label medie_materie_generala = new Label();
            lista_label_medii_materii_generale.Add(medie_materie_generala);
            double medie_generala = 0.00;
            lista_medii_materii_generale.Add(medie_generala);
            medie_materie_generala.Text = "MG: " + medie_generala.ToString("F");
            medie_materie_generala.Location = new System.Drawing.Point(medie_materie_1.Left, medie_materie_1.Top + 75);
            this.Controls.Add(medie_materie_generala);


            CheckBox casuta_teza = new CheckBox();
            lista_casute_teze.Add(casuta_teza);
            casuta_teza.Text = "Teză";
            casuta_teza.Width = 50;
            casuta_teza.Location = new System.Drawing.Point(casuta_materie.Left + 20, casuta_materie.Top + 50);
            casuta_teza.ForeColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(casuta_teza);


            ComboBox nota_teza = new ComboBox();
            lista_note_teze.Add(nota_teza);
            nota_teza.Location = new System.Drawing.Point(lista_casute_teze[index_curent].Left + 95, lista_casute_teze[index_curent].Top);
            for (int j = 1; j <= 10; j++)
                nota_teza.Items.Add(j.ToString()); // adauga cele 10 note in caseta ComboBox
            nota_teza.Text = RandomNumber(1, 10).ToString(); // intre 1 si 10
            nota_teza.Width = 40;
            nota_teza.Height = 20;
            this.Controls.Add(nota_teza);


            List<CheckBox> lista_casute_note_materii = new List<CheckBox>();
            for (int j = 1; j <= 5; j++)
            {
                CheckBox casuta_nota_materie = new CheckBox();
                lista_casute_note_materii.Add(casuta_nota_materie);
                casuta_nota_materie.Text = "Nota " + j.ToString();
                casuta_nota_materie.Location = new System.Drawing.Point(lista_casute_teze[index_curent].Left, lista_casute_teze[index_curent].Top + j * 25);
                casuta_nota_materie.Width = 65;
                if (j <= 2)
                    casuta_nota_materie.Checked = true;
                casuta_nota_materie.Height = 21;
                casuta_nota_materie.ForeColor = System.Drawing.Color.DarkGray;
                this.Controls.Add(casuta_nota_materie);
            }
            lista_toate_casute_note_materii.Add(lista_casute_note_materii);


            List<ComboBox> lista_note_materii = new List<ComboBox>();
            for (int j = 0; j < 5; j++)
            {
                ComboBox note_materie = new ComboBox();
                lista_note_materii.Add(note_materie);
                note_materie.Location = new System.Drawing.Point(lista_casute_note_materii[j].Left + 95, lista_casute_note_materii[j].Top);
                note_materie.Width = lista_note_teze[index_curent].Width;
                note_materie.Height = lista_note_teze[index_curent].Height;
                for (int k = 1; k <= 10; k++)
                    note_materie.Items.Add(k.ToString());
                note_materie.Text = RandomNumber(1, 10).ToString();
                this.Controls.Add(note_materie);

                y_ultimul_element = note_materie.Top;
            }
            lista_toate_note_materii.Add(lista_note_materii);
            start_Y = y_ultimul_element + 40;
        }
        void Adauga_Controale_Materie_Secundara(string nume_materie)
        {
            CheckBox casuta_materie_secundara = new CheckBox();
            lista_casute_materii.Add(casuta_materie_secundara);
            int index_curent = lista_casute_materii.IndexOf(casuta_materie_secundara);
            casuta_materie_secundara.Text = nume_materie;
            casuta_materie_secundara.Location = new System.Drawing.Point(start_X, start_Y);
            casuta_materie_secundara.Checked = true;
            casuta_materie_secundara.CheckedChanged += new System.EventHandler((sender, e) => this.Casuta_materie_check(sender, e, index_curent));
            this.Controls.Add(casuta_materie_secundara);

            bool este_secundara_sau_nu = true;
            lista_este_secundara_sau_nu.Add(este_secundara_sau_nu);

            Button buton_adauga_m1 = new Button();
            lista_butoane_m1_calculeaza.Add(buton_adauga_m1);
            buton_adauga_m1.Text = "Adaugă M1";
            buton_adauga_m1.Width = 85;
            buton_adauga_m1.Location = new System.Drawing.Point(casuta_materie_secundara.Left + 110, casuta_materie_secundara.Top);
            buton_adauga_m1.Click += new System.EventHandler((sender, e) => this.Buton_calculeaza_m1_click(sender, e, index_curent));
            this.Controls.Add(buton_adauga_m1);


            Button buton_adauga_m2 = new Button();
            lista_butoane_m2_calculeaza.Add(buton_adauga_m2);
            buton_adauga_m2.Text = "Adaugă M2";
            buton_adauga_m2.Width = 85;
            buton_adauga_m2.Location = new System.Drawing.Point(buton_adauga_m1.Left, buton_adauga_m1.Top + 25);
            buton_adauga_m2.Click += new System.EventHandler((sender, e) => this.Buton_calculeaza_m2_click(sender, e, index_curent));
            this.Controls.Add(buton_adauga_m2);

            Label medie_materie_1 = new Label();
            lista_label_medii_materii_1.Add(medie_materie_1);
            double medie_1 = 0.00;
            lista_medii_materii_1.Add(medie_1);
            medie_materie_1.Text = "M1: " + medie_1.ToString("F");
            medie_materie_1.Location = new System.Drawing.Point(casuta_materie_secundara.Left + 325, casuta_materie_secundara.Top + 5);
            this.Controls.Add(medie_materie_1);

            Label medie_materie_2 = new Label();
            lista_label_medii_materii_2.Add(medie_materie_2);
            double medie_2 = 0.00;
            lista_medii_materii_2.Add(medie_2);
            medie_materie_2.Text = "M2: " + medie_2.ToString("F");
            medie_materie_2.Location = new System.Drawing.Point(medie_materie_1.Left, medie_materie_1.Top + 25);
            this.Controls.Add(medie_materie_2);

            Label medie_materie_generala = new Label();
            lista_label_medii_materii_generale.Add(medie_materie_generala);
            double medie_generala = 0.00;
            lista_medii_materii_generale.Add(medie_generala);
            medie_materie_generala.Text = "MG: " + medie_generala.ToString("F");
            medie_materie_generala.Location = new System.Drawing.Point(medie_materie_1.Left, medie_materie_1.Top + 75);
            this.Controls.Add(medie_materie_generala);

            // astea sunt false fiindca vreau sa pastrez index simetric in toate listele
            CheckBox casuta_teza_falsa = new CheckBox();
            lista_casute_teze.Add(casuta_teza_falsa);

            ComboBox nota_teza_falsa = new ComboBox();
            lista_note_teze.Add(nota_teza_falsa);

            List<CheckBox> lista_casuta_materii_falsa = new List<CheckBox>();
            CheckBox casuta_materie_falsa = new CheckBox();
            lista_casuta_materii_falsa.Add(casuta_materie_falsa);
            lista_toate_casute_note_materii.Add(lista_casute_materii);
            //

            List<ComboBox> lista_note_materii = new List<ComboBox>(); // are doar un element totusi
            ComboBox medie_materie = new ComboBox();
            lista_note_materii.Add(medie_materie);
            medie_materie.Location = new System.Drawing.Point(casuta_materie_secundara.Left + 115, casuta_materie_secundara.Top + 75);
            medie_materie.Width = 40;
            medie_materie.Height = 20;
            for (int k = 1; k <= 10; k++)
                medie_materie.Items.Add(k.ToString());
            medie_materie.Text = RandomNumber(1, 10).ToString();
            this.Controls.Add(medie_materie);
            lista_toate_note_materii.Add(lista_note_materii);

            y_ultimul_element = medie_materie.Top;
            start_Y = y_ultimul_element + 40;
        }

        int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max - 1); // nu lua si ultimul element in calcul
            }
        }

        private void Buton_calculeaza_m1_click(Object sender, EventArgs e, int index_curent)
        {
            double suma = 0, contor = 0;
            double medie_1 = lista_medii_materii_1[index_curent];
            double medie_2 = lista_medii_materii_2[index_curent];
            double medie_generala = 0;
            var lista_auxiliara = lista_toate_note_materii[index_curent];
            if (lista_este_secundara_sau_nu[index_curent] == false)
            {
                var lista_auxiliara_2 = lista_toate_casute_note_materii[index_curent];
                foreach (var nota_materie in lista_auxiliara)
                {
                    int index_nota_actuala = lista_auxiliara.IndexOf(nota_materie);
                    var eticheta_nota_materie = lista_auxiliara_2[index_nota_actuala];
                    if (eticheta_nota_materie.Checked == true)
                    {
                        suma += Convert.ToDouble(nota_materie.Text);
                        contor++;
                    }
                }
                medie_1 = suma / contor;
                if (lista_casute_teze[index_curent].Checked == true)
                {
                    double nota_in_teza = Convert.ToDouble(lista_note_teze[index_curent].Text);
                    medie_1 = (medie_1 * 3 + nota_in_teza) / 4;
                }
                medie_1 = Math.Round(medie_1, 2);
                lista_label_medii_materii_1[index_curent].Text = "M1: " + medie_1.ToString("F");
                if (medie_1 % 1 > 0 && medie_1 % 1 < 0.50)
                    lista_label_medii_materii_1[index_curent].Text += " => " + (int)Math.Floor(medie_1);
                else if (medie_1 % 1 >= 0.50)
                    lista_label_medii_materii_1[index_curent].Text += " => " + (int)Math.Ceiling(medie_1);
            }
            else if (lista_este_secundara_sau_nu[index_curent] == true)
            {
                foreach (var nota_materie in lista_auxiliara) // pare mai elegant asa, dar se stie ca are doar o nota asemenea materie
                {
                    suma += Convert.ToDouble(nota_materie.Text);
                    contor++;
                }
                medie_1 = suma / contor;
                medie_1 = Math.Round(medie_1, 2);
                lista_label_medii_materii_1[index_curent].Text = "M1: " + medie_1.ToString("F");
            }
            if (medie_1 > 0.00 && medie_2 > 0.00)
            {
                medie_generala = (Math.Round(medie_1, 0, MidpointRounding.AwayFromZero) + Math.Round(medie_2, 0, MidpointRounding.AwayFromZero)) / 2;
                lista_label_medii_materii_generale[index_curent].Text = "MG: " + medie_generala.ToString("F");
                if (medie_generala % 1 < 0.50 && medie_generala % 1 != 0.00)
                {
                    medie_generala = Math.Floor(medie_generala);
                    lista_label_medii_materii_generale[index_curent].Text += " => " + medie_generala;
                }
                else if (medie_generala % 1 > 0.50)
                {
                    medie_generala = Math.Ceiling(medie_generala);
                    lista_label_medii_materii_generale[index_curent].Text += " => " + medie_generala;
                }
                lista_medii_materii_generale[index_curent] = medie_generala;
            }
            lista_medii_materii_1[index_curent] = medie_1;
            lista_medii_materii_2[index_curent] = medie_2;
            Recalculeaza_media_genereala_anuala();
        }
        private void Buton_calculeaza_m2_click(Object sender, EventArgs e, int index_curent)
        {
            double suma = 0, contor = 0;
            double medie_1 = lista_medii_materii_1[index_curent];
            double medie_2 = lista_medii_materii_2[index_curent];
            double medie_generala = 0;
            var lista_auxiliara = lista_toate_note_materii[index_curent];
            if (lista_este_secundara_sau_nu[index_curent] == false)
            {
                var lista_auxiliara_2 = lista_toate_casute_note_materii[index_curent];
                foreach (var nota_materie in lista_auxiliara)
                {
                    int index_nota_actuala = lista_auxiliara.IndexOf(nota_materie);
                    var eticheta_nota_materie = lista_auxiliara_2[index_nota_actuala];
                    if (eticheta_nota_materie.Checked == true)
                    {
                        suma += Convert.ToDouble(nota_materie.Text);
                        contor++;
                    }
                }
                medie_2 = suma / contor;
                if (lista_casute_teze[index_curent].Checked == true)
                {
                    double nota_in_teza = Convert.ToDouble(lista_note_teze[index_curent].Text);
                    medie_2 = (medie_2 * 3 + nota_in_teza) / 4;
                }
                medie_2 = Math.Round(medie_2, 2);
                lista_label_medii_materii_2[index_curent].Text = "M2: " + medie_2.ToString("F");
                if (medie_2 % 1 > 0 && medie_2 % 1 < 0.50)
                    lista_label_medii_materii_2[index_curent].Text += " => " + (int)Math.Floor(medie_2);
                else if (medie_2 % 1 >= 0.50)
                    lista_label_medii_materii_2[index_curent].Text += " => " + (int)Math.Ceiling(medie_2);
            }
            else if (lista_este_secundara_sau_nu[index_curent] == true)
            {
                foreach (var nota_materie in lista_auxiliara) // pare mai elegant asa, dar se stie ca are doar o nota asemenea materie
                {
                    suma += Convert.ToDouble(nota_materie.Text);
                    contor++;
                }
                medie_2 = suma / contor;
                medie_2 = Math.Round(medie_2, 2);
                lista_label_medii_materii_2[index_curent].Text = "M2: " + medie_2.ToString("F");
            }


            if (medie_1 > 0.00 && medie_2 > 0.00)
            {
                medie_generala = (Math.Round(medie_1, 0, MidpointRounding.AwayFromZero) + Math.Round(medie_2, 0, MidpointRounding.AwayFromZero)) / 2;
                lista_label_medii_materii_generale[index_curent].Text = "MG: " + medie_generala.ToString("F");
                if (medie_generala % 1 < 0.50 && medie_generala % 1 != 0.00)
                {
                    medie_generala = Math.Floor(medie_generala);
                    lista_label_medii_materii_generale[index_curent].Text += " => " + medie_generala;
                }
                else if (medie_generala % 1 > 0.50)
                {
                    medie_generala = Math.Ceiling(medie_generala);
                    lista_label_medii_materii_generale[index_curent].Text += " => " + medie_generala;
                }
                lista_medii_materii_generale[index_curent] = medie_generala;
            }
            lista_medii_materii_1[index_curent] = medie_1;
            lista_medii_materii_2[index_curent] = medie_2;
            Recalculeaza_media_genereala_anuala();
        }
        void Recalculeaza_media_genereala_anuala()
        {
            double contor_medii_generale_nenule = 0;
            foreach (var medie in lista_medii_materii_generale)
                if (medie > 0)
                    contor_medii_generale_nenule++;
            if (contor_medii_generale_nenule > 0)
            {
                double suma_medii_generale = 0;
                double contor_medii_generale = 0;
                foreach (double medie in lista_medii_materii_generale)
                {
                    if (medie > 0)
                    {
                        suma_medii_generale += medie;
                        contor_medii_generale++;
                    }
                }
                double medie_generala_finala = suma_medii_generale / contor_medii_generale;
                if (medie_generala_finala > 0.00)
                {
                    lista_un_element_medie_generala_anuala[0] = medie_generala_finala;
                    lista_un_element_label_medie_generala_anuala[0].Text = "MGA: " + medie_generala_finala.ToString("F");
                }
            }
        }
        private void Casuta_materie_check(Object sender, EventArgs e, int index_curent)
        {
            CheckBox casuta = sender as CheckBox;
            if (casuta == null)
                return;
            if (casuta.Checked == false)
            {
                lista_butoane_m1_calculeaza[index_curent].Enabled = false;
                lista_butoane_m2_calculeaza[index_curent].Enabled = false;
                lista_medii_materii_1[index_curent] = 0.00;
                lista_medii_materii_2[index_curent] = 0.00;
                lista_medii_materii_generale[index_curent] = 0.00;
                lista_label_medii_materii_1[index_curent].Text = "M1: " + lista_medii_materii_1[index_curent].ToString("F");
                lista_label_medii_materii_2[index_curent].Text = "M2: " + lista_medii_materii_2[index_curent].ToString("F");
                lista_label_medii_materii_generale[index_curent].Text = "MG: " + lista_medii_materii_generale[index_curent].ToString("F");
                foreach (var nota_materie in lista_toate_note_materii[index_curent])
                    nota_materie.Enabled = false;
            }
            else if (casuta.Checked == true)
            {
                lista_butoane_m1_calculeaza[index_curent].Enabled = true;
                lista_butoane_m2_calculeaza[index_curent].Enabled = true;
                lista_medii_materii_1[index_curent] = 0.00;
                lista_medii_materii_2[index_curent] = 0.00;
                lista_medii_materii_generale[index_curent] = 0.00;
                lista_label_medii_materii_1[index_curent].Text = "M1: " + lista_medii_materii_1[index_curent].ToString("F");
                lista_label_medii_materii_2[index_curent].Text = "M2: " + lista_medii_materii_2[index_curent].ToString("F");
                lista_label_medii_materii_generale[index_curent].Text = "MG: " + lista_medii_materii_generale[index_curent].ToString("F");
                foreach (var nota_materie in lista_toate_note_materii[index_curent])
                    nota_materie.Enabled = true;
            }

            if (lista_este_secundara_sau_nu[index_curent] == false)
            {
                if (casuta.Checked == false)
                {
                    lista_casute_teze[index_curent].Enabled = false;
                    lista_note_teze[index_curent].Enabled = false;
                    foreach (var eticheta in lista_toate_casute_note_materii[index_curent])
                        eticheta.Enabled = false;
                }
                else if (casuta.Checked == true)
                {
                    lista_casute_teze[index_curent].Enabled = true;
                    lista_note_teze[index_curent].Enabled = true;
                    foreach (var eticheta in lista_toate_casute_note_materii[index_curent])
                        eticheta.Enabled = true;
                }
            }
            Recalculeaza_media_genereala_anuala();
        }
        private void Buton_plus_click(Object sender, EventArgs e, int index_curent)
        {
            string nume_materie_noua = Interaction.InputBox("Care este numele materiei care va fi adăugată?", "Nume materie", "Introdu numele aici");
            if (nume_materie_noua == "")
                return;
            y_ultimul_element = lista_un_element_label_indicatie_buton_plus[0].Top;
            start_Y = y_ultimul_element;
            var buton_plus = lista_un_element_buton_plus[0];
            var radio_materie_principala = lista_un_element_radio_materie_principala[0];
            var radio_materie_secundara = lista_un_element_radio_materie_secundara[0];
            var label_indicatie_buton_plus = lista_un_element_label_indicatie_buton_plus[0];
            var label_medie_generala_anuala = lista_un_element_label_medie_generala_anuala[0];
            if (radio_materie_principala.Checked == true && radio_materie_secundara.Checked == false)
                Adauga_Controale_Materie(nume_materie_noua);
            else if (radio_materie_secundara.Checked == true && radio_materie_principala.Checked == false)
                Adauga_Controale_Materie_Secundara(nume_materie_noua);
            else
            { MessageBox.Show("Nu ai bifat nicio opțiune. Încearcă din nou !"); return; }
            // Mutare elementele butonului plus la noul capat
            buton_plus.Location = new System.Drawing.Point(start_X, start_Y);
            radio_materie_principala.Location = new System.Drawing.Point(buton_plus.Left + 50, buton_plus.Top);
            radio_materie_secundara.Location = new System.Drawing.Point(buton_plus.Left + 50, buton_plus.Top + 25);
            label_indicatie_buton_plus.Location = new System.Drawing.Point(radio_materie_principala.Left + 85, radio_materie_principala.Top + 12);
            label_medie_generala_anuala.Location = new System.Drawing.Point(buton_plus.Left + 325, buton_plus.Top);
        }

        private void despreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Despre_form = new Despre();
            Despre_form.Show();
        }
    }
}