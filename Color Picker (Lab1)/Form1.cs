using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Color_Picker__Lab1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        // son seçilen rengi tutan global değişken
        Color selectedColor = Color.Transparent;
        List<Color> secilenRenkler = new List<Color>();
        FlowLayoutPanel renkKonteyneri;
        Panel sonuc;
        Label lblRenkKodu;

        private void ColorPanel_Click(object sender, EventArgs e)
        {
            // Tıklanan nesneyi "Panel" sınıfına dönüştürüyoruz (Casting)
            Panel clickedPanel = (Panel)sender;

            // Tıklanan panelin rengini al
            selectedColor = clickedPanel.BackColor;

            // Test amaçlı arka planını formun başlığına yazdırabilirsin
            this.Text = "Seçilen Renk: " + selectedColor.ToString();
        }

        private void renkEkle(object sender, EventArgs e)
        {
            //seçili renk yoksa işlem yapma
            if (selectedColor == Color.Transparent) return;
            secilenRenkler.Add(selectedColor);

            // 3. Maksimum 2 renk sınırı ve kaydırma kuralı
            if (secilenRenkler.Count > 2)
            {
                secilenRenkler.RemoveAt(0); // İlk eklenen rengi listeden çıkar
            }

            KonteynerGuncelle();
            RenkleriBirlestir();
        }

        private void KonteynerGuncelle()
        {
            // eski kutucukları temizle
            renkKonteyneri.Controls.Clear();

            // Listede kaç renk varsa her biri için küçük bir panel oluştur
            foreach (Color renk in secilenRenkler)
            {
                Panel kucukKutucuk = new Panel();
                kucukKutucuk.Size = new Size(30, 30);
                kucukKutucuk.BackColor = renk;
                kucukKutucuk.BorderStyle = BorderStyle.FixedSingle;
                // Kutucuklar arasındaki dış boşluk (Sol: 0, Üst: 0, Sağ: 5, Alt: 0)
                kucukKutucuk.Margin = new Padding(0, 0, 5, 0);

                // FlowLayoutPanel otomatik yan yana dizecektir
                renkKonteyneri.Controls.Add(kucukKutucuk);
            }
        }

        private void RenkleriBirlestir()
        {
            if (secilenRenkler.Count == 1)
            {
                sonuc.BackColor = secilenRenkler[0];
            }
            else if (secilenRenkler.Count == 2)
            {
                Color renk1 = secilenRenkler[0];
                Color renk2 = secilenRenkler[1];

                int yeniR = (renk1.R + renk2.R) % 255;
                int yeniG = (renk1.G + renk2.G) % 255;
                int yeniB = (renk1.B + renk2.B) % 255;

                sonuc.BackColor = Color.FromArgb(yeniR, yeniG, yeniB);
            }

            // Renk kodunu Label üzerinde güncelleme (HEX formatı: #RRGGBB)
            if (secilenRenkler.Count > 0)
            {
                Color c = sonuc.BackColor;
                string hexKod = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
                lblRenkKodu.Text = $"{hexKod}\nRGB({c.R}, {c.G}, {c.B})";
            }
        }

        private void InitializeCustomComponents()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            // Sütunlar için temel renklerimizi tanımlıyoruz (Mavi, Yeşil, Sarı, Turuncu, Kırmızı, Mor, Kahverengi, Beyaz, Koyu Gri)
            Color[] baseColors = new Color[] {
            Color.CornflowerBlue, Color.LightGreen, Color.Khaki, Color.SandyBrown,
            Color.IndianRed, Color.MediumPurple, Color.Tan, Color.White, Color.DimGray
            };

            int rows = 5; // Yukarıdan aşağıya 5 satır
            int cols = 9; // Soldan sağa 9 sütun
            int panelSize = 30; // Panellerin kare boyutu
            int spacing = 5; // Paneller arası boşluk
            int startX = 20; // Sol üst köşeden başlama mesafesi
            int startY = 20;

            // Önce sütunları dönüyoruz, çünkü her sütun kendi içinde aşağı doğru koyulaşacak
            for (int col = 0; col < cols; col++)
            {
                Color currentColor = baseColors[col];

                for (int row = 0; row < rows; row++)
                {
                    // Paneli oluşturma
                    Panel colorPanel = new Panel();
                    colorPanel.Width = panelSize;
                    colorPanel.Height = panelSize;

                    // Konumu hesaplama
                    colorPanel.Left = startX + (col * (panelSize + spacing));
                    colorPanel.Top = startY + (row * (panelSize + spacing));

                    // Aşağı indikçe row değişkenine bağlı olarak koyulaşır
                    // RGB değerlerinin 0'ın altına düşmemesi için Math.Max kullanıyoruz
                    int r = Math.Max(0, currentColor.R - (row * 25));
                    int g = Math.Max(0, currentColor.G - (row * 25));
                    int b = Math.Max(0, currentColor.B - (row * 25));

                    colorPanel.BackColor = Color.FromArgb(r, g, b);

                    // Kenarlık (İsteğe bağlı, daha şık durması için)
                    colorPanel.BorderStyle = BorderStyle.FixedSingle;

                    // Tıklanma olayı. hangi rengin seçildiğini hafızaya almak için
                    colorPanel.Click += new EventHandler(ColorPanel_Click);

                    // Paneli forma ekleme
                    this.Controls.Add(colorPanel);
                }
            }

            ////////////////////////// + butonu

            Button ekleButonu = new Button();

            ekleButonu.Name = "btnTika";
            ekleButonu.Text = "+";
            ekleButonu.Location = new System.Drawing.Point(20, 200);
            ekleButonu.Size = new System.Drawing.Size(50, 30);

            ekleButonu.Click += new EventHandler(renkEkle);
            this.Controls.Add(ekleButonu);

            /////////////////////// seçilen renkler

            renkKonteyneri = new FlowLayoutPanel();
            renkKonteyneri.Name = "renkKonteyneri";
            renkKonteyneri.Location = new System.Drawing.Point(80, 200);
            renkKonteyneri.Size = new System.Drawing.Size(200, 35);
            renkKonteyneri.FlowDirection = FlowDirection.LeftToRight;   // Elemanların dizilim yönü
            renkKonteyneri.WrapContents = false; // Kutuların alt satıra kaymasını engeller
            this.Controls.Add(renkKonteyneri);

            /////////////////////// sonuç paneli

            sonuc = new Panel();
            sonuc.Location = new System.Drawing.Point(340, 20);
            sonuc.Size = new System.Drawing.Size(150, 150);
            // panel1.BackColor = System.Drawing.Color.LightBlue;
            sonuc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(sonuc);

            ////// renk kodu yazdırma
            
            lblRenkKodu = new Label();
            lblRenkKodu.Location = new System.Drawing.Point(340, 180);
            lblRenkKodu.Size = new System.Drawing.Size(150, 30);
            lblRenkKodu.TextAlign = ContentAlignment.MiddleCenter;
            lblRenkKodu.Font = new Font("Arial", 9, FontStyle.Bold);
            lblRenkKodu.Text = "Renk Kodu: -";
            this.Controls.Add(lblRenkKodu);

        }
    }
}
