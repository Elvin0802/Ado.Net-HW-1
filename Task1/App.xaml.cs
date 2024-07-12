using Microsoft.Extensions.Configuration;
using System.Windows;
using System.Windows.Input;

namespace Task1;


public partial class App : Application
{
	public static IConfigurationRoot? _configuration;

	private static RegisterWindow? _registerWindow;
	public static RegisterWindow? RegisterWindow { get => _registerWindow; set => _registerWindow = value; }

	private static LoginWindow? _loginWindow;
	public static LoginWindow? LoginWindow { get => _loginWindow; set => _loginWindow = value; }

	private void StartApp(object sender, StartupEventArgs e)
	{
		_configuration = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json")
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
