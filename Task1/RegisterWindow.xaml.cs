using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Task1;

public partial class RegisterWindow : Window
{
	public RegisterWindow()
	{
		InitializeComponent();
		DataContext = this;
	}

	private void FinishRegisterButtonClickExecute(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrEmpty(NameTBox.Text) || string.IsNullOrEmpty(SurnameTBox.Text)
			|| string.IsNullOrEmpty(AgeTBox.Text) || string.IsNullOrEmpty(UsernameTBox.Text)
			|| string.IsNullOrEmpty(PasswordTBox.Text) || string.IsNullOrEmpty(CPasswordTBox.Text))
		{
			MessageBox.Show($"All lines must be filled !", "Message");
			return;
		}

		if (PasswordTBox.Text != CPasswordTBox.Text)
		{
			MessageBox.Show($"Enter correct confirm password !", "Message");
			return;
		}

		string connectionString = App._configuration!.GetConnectionString("DefaultConnection")!;

		using (var sqlConnection = new SqlConnection(connectionString))
		{
			SqlCommand? command = new();
			sqlConnection.Open();

			string? insertQuery = $@"
				USE [AppUsers];

				INSERT INTO [MainUsers] 
					([Name], [Surname], [Age], [Username], [Password])
				VALUES 
					('{NameTBox.Text}', '{SurnameTBox.Text}', {AgeTBox.Text}, '{UsernameTBox.Text}','{PasswordTBox.Text}');";

			command.Connection = sqlConnection;
			command.CommandText = insertQuery;
			command.ExecuteNonQuery();

			MessageBox.Show("Register Successful !", "Message");
		}

		this.Hide();

		App.LoginWindow?.ShowDialog();
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
