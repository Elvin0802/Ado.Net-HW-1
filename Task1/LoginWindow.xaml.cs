using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Task1;

public partial class LoginWindow : Window
{
	public static string? ConnectionString { get; set; }
	public SqlConnection? Connection { get; set; }
	public SqlDataReader? Reader { get; set; }
	public SqlCommand? Cmd { get; set; }

	public LoginWindow()
	{
		InitializeComponent();

		DataContext = this;

		ConnectionString = App.Configuration!.GetConnectionString("DefaultConnection")!;
	}

	private void LoginButtonClickExecute(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrEmpty(UsernameTBox.Text) || string.IsNullOrEmpty(PasswordTBox.Text))
		{
			MessageBox.Show($"Please fill all lines.", "Message");
			return;
		}

		using (Connection = new SqlConnection(ConnectionString))
		{
			var querry = @"
							USE [AppUsers];

							SELECT * FROM [MainUsers];
						";

			Cmd = new(querry, Connection);

			Connection!.Open();
			Reader = Cmd.ExecuteReader();

			while (Reader.Read())
			{
				if (Reader["Username"].ToString() == UsernameTBox.Text)
				{
					if (Reader["Password"].ToString() == PasswordTBox.Text)
					{
						MessageBox.Show($"Welcome {Reader["Name"]} {Reader["Surname"]}.", "Message");
						return;
					}
				}
			}

			MessageBox.Show($"Username or Password is incorrect.", "Message");
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
