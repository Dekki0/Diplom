using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace SchoolFeeding.ViewModel.Services;
public static class Logger
{
    public static void LogError(string errorMessage)
    {
        try
        {
            string filePath = "Log.txt";

            // Добавляем информацию об ошибке в файл
            File.AppendAllText(filePath, $"{DateTime.Now}: {errorMessage}\n");

            MessageBox.Show($"Произошла ошибка. Подробности записаны в файле:\n{Path.GetFullPath(filePath)}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при записи ошибки в файл: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}