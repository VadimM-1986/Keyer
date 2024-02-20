using Keyer.Views;
using Keyer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Keyer.ProjectsPresenter
{
    public class Presenter
    {
        MainWindow mainWindow;
        Service service;

        public Presenter(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow; 
            this.service = new Service();
            mainWindow.openImageEvent += HendlerOpenImage;
            mainWindow.processingImageEvent += HendlerProcessingImage;
            mainWindow.saveImagr += HendlerHaveImagr;
            mainWindow.selectedListColor += HendlerSelectedListColor;
        }

        // Открываем картинку 
        private void HendlerOpenImage(object? sender, EventArgs e)
        {
            mainWindow.ImageBox.Source = service.OpenImage();
        }

        // Выбираем цвет для кеинга
        private void HendlerSelectedListColor(object? sender, EventArgs e)
        {
            // Получите выбранный элемент
                ComboBoxItem selectedColorItem = (ComboBoxItem)mainWindow.colorComboBox.SelectedItem;
                service.SelectColor(selectedColorItem);
        }

        // Вырезаем фон у картинки
        private void HendlerProcessingImage(object? sender, EventArgs e)
        {
            mainWindow.ImageBox.Source = service.ProcessingCutImage();
        }

        // Сохраняем картинку
        private void HendlerHaveImagr(object? sender, EventArgs e)
        {
            service.SaveImage();
        }
    }
}
