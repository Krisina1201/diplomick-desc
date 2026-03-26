using Avalonia.Controls;
using QRCoder;
using System.IO;
using Avalonia.Media.Imaging;
using System;
using Tmds.DBus.Protocol;
namespace Diplom;

public partial class QrWindow : Window
{
    public QrWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    public Dialog dialog = new Dialog();

    // Делаем свойство публичным для привязки
    //public Bitmap? QrCodeImage;

    public void GenerateQrCode(string text)
    {
        try
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);

            var qrCodeBytes = qrCode.GetGraphic(20);
            using var stream = new MemoryStream(qrCodeBytes);

            //QrCodeImage.Source = new Bitmap(stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка генерации QR: {ex.Message}");
            dialog.ShowErrorDialog("ОШибка", "Произошла ошибка при генерации qr кода, попробуйте позже", this);
        }
    }

    private void Generate_qr_click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Генерируем QR-код при нажатии кнопки
        GenerateQrCode("https://github.com/Krisina1201/Practica/blob/master/MainWindow.axaml");

    }

    private void ClassroomClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ClassroomWindow classroomWindow = new ClassroomWindow();
        classroomWindow.Show();
        this.Close();
    }
    
    private void PeopleClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ResponsiblePersonWindow responsiblePersonWindow = new();
        responsiblePersonWindow.Show();
        this.Close();
    }
    
    private void InventoryClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        InventoryWindow inventoryWindow = new InventoryWindow();
        inventoryWindow.Show();
        this.Close();
    }
}