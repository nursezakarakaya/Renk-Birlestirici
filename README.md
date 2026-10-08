## Renk Birleştirici Windows Forms Uygulaması

<p align = "center">
<img width="1000" height="578" alt="image" src="https://github.com/user-attachments/assets/047543ab-2605-4331-8a5d-6a08f1236600" />
</p>

Görsel Programlama dersi laboratuvarında kodlamış olduğum bu uygulama, seçeceğiniz iki rengi birbirine ekleyip bunun modunu alan bir algoritma kullanır. Bu sayede iki rengin birleşimini almanızı sağlar.

---

### MOD'lu renk birleştirme algoritması

İki renk birbirine eklenir ve 255'e göre mod alınır. Örneğin: 

<b>Renk1 (R, G, B) =>  25, 100,  10 

<b>Renk2 (R, G, B) => 250, 125, 255

<b>Toplam =>  275, 225, 265
Mod(255) => 20, 225,  10

<i>Oluşan yeni renk (R, G, B) => 20, 225, 10 </i>

---

## Kullanımı

Renk paletinden üzerine tıklanarak bir renk seçilip "+" butonuna tıklandığında, seçili renk bu butonun hemen yanına eklenir. Seçilen iki rengin birleşimi ise anlık olarak sağ taraftaki büyük panele yansır. Eğer listede tek renk varsa, panel arkaplanı direkt olarak o renk olacaktır. Listeye ikiden fazla renk eklemeye çalışıldığında ise sadece en son seçilen iki renk baza alınır, seçilen en eski renk ise listeden otomatik olarak çıkarılacaktır.

* Projeyi çalıştırabilecek bir arayüzünüz (Visual Studio vb.) yoksa uygulamayı direkt olarak açabileceğiniz bir exe dosyası ana klasöre mevcuttur.
