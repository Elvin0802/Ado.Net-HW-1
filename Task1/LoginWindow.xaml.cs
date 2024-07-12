using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Task1;

public partial class LoginWindow : Window
{
	public static string? connectionString { get; set; }
	public SqlConnection? connection { get; set; }
	SqlDataReader? reader { get; set; }
	SqlCommand? cmd { get; set; }
	public LoginWindow()
	{
		InitializeComponent();
		DataContext = this;

		connectionString = App._configuration!.GetConnectionString("DefaultConnection")!;
		connection = new SqlConnection(connectionString);
	}

	private void LoginButtonClickExecute(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrEmpty(UsernameTBox.Text) || string.IsNullOrEmpty(PasswordTBox.Text))
			return;

		using (connection)
		{
			var querry = @"
							USE [AppUsers];

							SELECT * FROM [MainUsers];
						";

			cmd = new(querry, connection);

			connection!.Open();
			reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				if (reader["Username"].ToString() == UsernameTBox.Text)
				{
					if (reader["Password"].ToString() == PasswordTBox.Text)
					{
						MessageBox.Show($"Welcome {reader["Name"]} {reader["Surname"]}.", "Message");
						return;
					}
				}
			}
		}
	}

	private void RegisterButtonClickExecute(object sender, RoutedEventArgs e)
	{
		this.Hide();

		App.RegisterWindow?.ShowDialog();
	}

	private void ExitButtonExecute(object sender, RoutedEventArgs e)
	{
		App.AppExit();
	}

	private void Window_MouseDown(object sender, MouseButtonEventArgs e)
	{
		App.MoveWindow(e, this);
	}
}
