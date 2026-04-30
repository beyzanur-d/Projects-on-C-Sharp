using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Test3
{
    // Canvas üzerinde araba ve çiçek çizen, WASD ile hareket ettiren pencere sınıfı.
    public partial class MainWindow : Window
    {
        // Hareketli tüm şekilleri tek listede tutuyoruz.
        // Hareket ve sınır kontrolü bu liste üzerinden yapılır.
        private List<UIElement> childrenList;

        public MainWindow()
        {
            InitializeComponent();
            childrenList = new List<UIElement>();
            DrawCar();
            DrawFlower();
        }

        // Araba; gövde, cam ve iki tekerlekten oluşur.
        // Tüm parçalar childrenList'e eklenir, böylece birlikte hareket ederler.
        private void DrawCar()
        {
            // Araba gövdesi — aracın ana kütlesi
            Rectangle carBody = new Rectangle()
            {
                Width = 200,
                Height = 50,
                Fill = Brushes.Black
            };
            Canvas.SetLeft(carBody, 50);
            Canvas.SetTop(carBody, 300);
            canvas.Children.Add(carBody);

            // Araba camı — gövdenin hemen üstüne, biraz daha dar yerleştirilir
            Rectangle carGlass = new Rectangle()
            {
                Width = 150,
                Height = 40,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(carGlass, 75);
            Canvas.SetTop(carGlass, 260);
            canvas.Children.Add(carGlass);

            // Sol tekerlek
            Ellipse leftWheel = new Ellipse()
            {
                Width = 40,
                Height = 40,
                Fill = Brushes.Gray
            };
            Canvas.SetLeft(leftWheel, 60);
            Canvas.SetTop(leftWheel, 340);
            canvas.Children.Add(leftWheel);

            // Sağ tekerlek
            Ellipse rightWheel = new Ellipse()
            {
                Width = 40,
                Height = 40,
                Fill = Brushes.Gray
            };
            Canvas.SetLeft(rightWheel, 190);
            Canvas.SetTop(rightWheel, 340);
            canvas.Children.Add(rightWheel);

            childrenList.AddRange(new UIElement[] { carBody, carGlass, leftWheel, rightWheel });
        }

        // Çiçek; sarı bir merkez ve etrafına 45°'lik açılarla yerleştirilmiş 8 kırmızı yapraktan oluşur.
        // Merkez, yaprakların üstünde görünsün diye ZIndex ile en üst katmana alınır.
        private void DrawFlower()
        {
            // Çiçeğin sarı merkezi
            Ellipse flowerBody = new Ellipse()
            {
                Width = 40,
                Height = 40,
                Fill = Brushes.Yellow
            };
            Canvas.SetLeft(flowerBody, 290);
            Canvas.SetTop(flowerBody, 270);
            canvas.Children.Add(flowerBody);

            // 8 yaprak, merkez etrafına 45°'lik eşit aralıklarla yerleştirilir.
            // Her yaprak merkeze 30 piksel uzaklıkta konumlandırılır.
            for (int i = 0; i < 8; i++)
            {
                Ellipse petal = new Ellipse()
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Red
                };

                double angle = i * 45;
                double x = 300 + 30 * Math.Cos(Math.PI * angle / 180);
                double y = 280 - 30 * Math.Sin(Math.PI * angle / 180);

                Canvas.SetLeft(petal, x);
                Canvas.SetTop(petal, y);
                canvas.Children.Add(petal);
                childrenList.Add(petal);
            }

            // Merkez, tüm yaprakların üstünde görünsün
            Panel.SetZIndex(flowerBody, canvas.Children.Count);
            childrenList.Add(flowerBody);
        }

        // WASD ile tüm şekilleri 10 piksel hareket ettirir.
        //
        // Sınır kontrolü, tek bir referans şekle değil,
        // listedeki tüm elemanların en uç pozisyonuna göre yapılır.
        // Bu sayede hangi şekil en kenardaysa o belirler — hiçbir eleman canvas dışına çıkamaz.
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W) // Yukarı
            {
                // En yukarıdaki elemanın Top değeri 0'ın altına inmemeli
                double minTop = childrenList.Min(child => Canvas.GetTop(child));
                if (minTop - 10 >= 0)
                {
                    foreach (UIElement child in childrenList)
                        Canvas.SetTop(child, Canvas.GetTop(child) - 10);
                }
            }
            else if (e.Key == Key.A) // Sol
            {
                // En soldaki elemanın Left değeri 0'ın altına inmemeli
                double minLeft = childrenList.Min(child => Canvas.GetLeft(child));
                if (minLeft - 10 >= 0)
                {
                    foreach (UIElement child in childrenList)
                        Canvas.SetLeft(child, Canvas.GetLeft(child) - 10);
                }
            }
            else if (e.Key == Key.S) // Aşağı
            {
                // En alttaki elemanın alt kenarı canvas yüksekliğini geçmemeli
                double maxBottom = childrenList.Max(child => Canvas.GetTop(child) + child.RenderSize.Height);
                if (maxBottom + 10 <= canvas.ActualHeight)
                {
                    foreach (UIElement child in childrenList)
                        Canvas.SetTop(child, Canvas.GetTop(child) + 10);
                }
            }
            else if (e.Key == Key.D) // Sağ
            {
                // En sağdaki elemanın sağ kenarı canvas genişliğini geçmemeli
                double maxRight = childrenList.Max(child => Canvas.GetLeft(child) + child.RenderSize.Width);
                if (maxRight + 10 <= canvas.ActualWidth)
                {
                    foreach (UIElement child in childrenList)
                        Canvas.SetLeft(child, Canvas.GetLeft(child) + 10);
                }
            }
        }
    }
}