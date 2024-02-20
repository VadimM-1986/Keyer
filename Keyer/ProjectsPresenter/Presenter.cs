using Keyer.Views;
using System.Windows.Controls;


namespace Keyer.ProjectsPresenter;

public class Presenter
{
    private readonly MainWindow _mainWindow;
    private readonly Model.Image _image;

    public Presenter(MainWindow mainWindow, Model.Image image)
    {
        _image = image;
        _mainWindow = mainWindow; 
        _mainWindow.OpenImageEvent += HandlerOpenImage;
        _mainWindow.ProcessingImageEvent += HandlerProcessingImage;
        _mainWindow.SaveImageEvent += HandlerHaveImage;
        _mainWindow.SelectedListColorEvent += HandlerSelectedListColor;
    }

    // Открываем картинку 
    private void HandlerOpenImage(object? sender, EventArgs e)
    {
        _mainWindow.ImageBox.Source = _image.OpenImage();
    }

    // Выбираем цвет для кеинга
    private void HandlerSelectedListColor(object? sender, EventArgs e)
    {
        // Получите выбранный элемент
        ComboBoxItem selectedColorItem = (ComboBoxItem)_mainWindow.colorComboBox.SelectedItem;
        _image.SelectColor(selectedColorItem);
    }

    // Вырезаем фон у картинки
    private void HandlerProcessingImage(object? sender, EventArgs e)
    {
        _mainWindow.ImageBox.Source = _image.ProcessingCutImage();
    }

    // Сохраняем картинку
    private void HandlerHaveImage(object? sender, EventArgs e)
    {
        _image.SaveImage();
    }
}
