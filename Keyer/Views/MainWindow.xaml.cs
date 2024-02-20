using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Keyer.ProjectsPresenter;
using System.Windows.Controls;

namespace Keyer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new Presenter(this);
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

        // Кнопка открытия картинки
        public event EventHandler openImageEvent = (sender, args) => { };
        private void Button_Open_Image_Click(object sender, RoutedEventArgs e)
        {
            openImageEvent.Invoke(sender, e);
        }

        // Список выбора цвета кеинга
        public event EventHandler selectedListColor = (sender, args) => { };
        private void Button_SelectedListColor(object sender, SelectionChangedEventArgs e)
        {
            selectedListColor.Invoke(sender, e);
        }

        // Кнопка обработки картинки (удаление фона)
        public event EventHandler processingImageEvent = (sender, args) => { };
        private void Button_Processing_Backgraund_Image_Click(object sender, RoutedEventArgs e)
       {
            processingImageEvent.Invoke(sender, e);
       }

        // Кнопка сохранения картинки
        public event EventHandler saveImagr = (sender, args) => { };
        private void Button_Save_Image_Click(object sender, RoutedEventArgs e)
        {
            saveImagr.Invoke(sender, e);
        }
    }
}