using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace Keyer.Model
{
    public class Service
    {
        private BitmapImage? bitmapImagePublic;
        private WriteableBitmap? writableBitmapPublic;

        private int red, green, blue;

        // ОТКРЫТИЕ КАРТИНКИ
        public BitmapImage? OpenImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображение с расширением PNG без альфа-компонента (*.png)|*.png";
            
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Проверка альфа-компонента в изображении
                    BitmapImage originalBitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                    
                    bitmapImagePublic = originalBitmapImage;

                    if (HasAlphaChannel(originalBitmapImage))
                    {
                        MessageBox.Show("Выберите PNG изображение без альфа-компонента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (originalBitmapImage.PixelWidth != 1920 || originalBitmapImage.PixelHeight != 1080)
                    {
                        MessageBox.Show("Выберите изображение с размером 1920x1080.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки/изменения изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return bitmapImagePublic;
        }

        // Метод для проверки наличия альфа-компонента в изображении
        private bool HasAlphaChannel(BitmapImage bitmapImage)
        {
            return bitmapImage.Format == System.Windows.Media.PixelFormats.Bgra32 || bitmapImage.Format == System.Windows.Media.PixelFormats.Pbgra32;
        }


        // ВЫБОР ЦВЕТА КЕИНГА
        public void SelectColor(ComboBoxItem selectedColorItem)
        {

            // Получите текстовое название из выбранного элемента
            string colorName = ((TextBlock)((StackPanel)selectedColorItem.Content).Children[1]).Text;

            // Ваш код обработки события в зависимости от выбранного цвета
            switch (colorName)
            {
                case "Зеленый цвет":
                    red = 205;
                    green = 24;
                    blue = 0;
                    break;

                case "Синий цвет":
                    red = 0;
                    green = 0;
                    blue = 0;
                    break;

                case "Красный цвет":
                    red = 0;
                    green = 0;
                    blue = 0;
                    break;
            }
        }


        // ОБРАБОТКА КАРТИНКИ (УДАЛЕНИЕ ФОНА)
        public WriteableBitmap ProcessingCutImage()
        {
            // Создаем WriteableBitmap на основе originalBitmapImage с использованием формата Pbgra32
            WriteableBitmap writableBitmap = new WriteableBitmap(bitmapImagePublic.PixelWidth, bitmapImagePublic.PixelHeight, 96, 96, PixelFormats.Pbgra32, null);

            // Изменение цвета пикселей
            int width = writableBitmap.PixelWidth;
            int height = writableBitmap.PixelHeight;

            int bytesPerPixel = (writableBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] pixels = new byte[width * height * bytesPerPixel];

            bitmapImagePublic.CopyPixels(new Int32Rect(0, 0, width, height), pixels, width * bytesPerPixel, 0);

            // Замена зеленого цвета на прозрачность
            for (int i = 0; i < pixels.Length; i += bytesPerPixel)
            {
                if (pixels[i + 2] == blue && pixels[i] == green && pixels[i + 1] == red)
                {
                    pixels[i] = 0;     // Синий
                    pixels[i + 1] = 0; // Зеленый
                    pixels[i + 2] = 0; // Красный
                    pixels[i + 3] = 0; // Альфа-канал (прозрачность)
                }
            }

            // Копирование измененных пикселей обратно в WriteableBitmap
            writableBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * bytesPerPixel, 0);
   
            writableBitmapPublic = writableBitmap;
            return writableBitmapPublic;
        }


        // СОХРАНЕНИЕ КАРТИНКИ
        public void SaveImage()
        {
            // Открываем SaveFileDialog для выбора места сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Files (*.png)|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Сохранение WriteableBitmap в выбранный файл
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(writableBitmapPublic));
                    encoder.Save(stream);
                }
            }
        }
    }
}
