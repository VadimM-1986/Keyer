using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Keyer.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // Кнопка открытия картинки
    public event EventHandler OpenImageEvent = (sender, args) => { };
    // Список выбора цвета кеинга
    public event EventHandler SelectedListColorEvent = (sender, args) => { };
    // Кнопка обработки картинки (удаление фона)
    public event EventHandler ProcessingImageEvent = (sender, args) => { };
    // Кнопка сохранения картинки
    public event EventHandler SaveImageEvent = (sender, args) => { };

    public MainWindow()
    {
        InitializeComponent();
    }

    // Перемещение формы по экрану при нажатой левой кнопки мыши
    private void WindowMovementMouse(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    // Кнопка свернуть окно формы
    private void RollDownWindowClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    // Кнопка закрыть окно формы
    private void CloseWindowClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OpenImageButtonClick(object sender, RoutedEventArgs e)
    {
        OpenImageEvent.Invoke(sender, e);
    }

    private void SelectedListColorButton(object sender, SelectionChangedEventArgs e)
    {
        SelectedListColorEvent.Invoke(sender, e);
    }

    private void ButtonProcessingBackgroundImageClick(object sender, RoutedEventArgs e)
    {
        ProcessingImageEvent.Invoke(sender, e);
    }

    private void SaveImageClick(object sender, RoutedEventArgs e)
    {
        SaveImageEvent.Invoke(sender, e);
    }
}