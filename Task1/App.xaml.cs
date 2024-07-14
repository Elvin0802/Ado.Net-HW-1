using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Task1;


public partial class App : Application
{
	private static RegisterWindow? _registerWindow;
	private static LoginWindow? _loginWindow;

	public static IConfigurationRoot? Configuration { get; set; }
	public static RegisterWindow? RegisterWindow { get => _registerWindow; set => _registerWindow = value; }
	public static LoginWindow? LoginWindow { get => _loginWindow; set => _loginWindow = value; }

	private void StartApp(object sender, StartupEventArgs e)
	{
		Configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("Resources/appsettings.json")
			.Build();

		RegisterWindow = new();
		LoginWindow = new();

		LoginWindow.ShowDialog();
	}

	public static void AppExit()
	{
		LoginWindow?.Close();
		RegisterWindow?.Close();

		App.Current.Shutdown();
	}

	public static void MoveWindow(MouseButtonEventArgs eventArgs, Window currentWindow)
	{
		if (eventArgs.ChangedButton == MouseButton.Left)
		{
			currentWindow.DragMove();
		}
	}
}
