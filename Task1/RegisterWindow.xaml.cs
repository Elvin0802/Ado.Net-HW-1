using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Task1;

public partial class RegisterWindow : Window
{
	public string? ConnectionString { get; set; }
	public SqlConnection? Connection { get; set; }
	public SqlCommand? Cmd { get; set; }

	public RegisterWindow()
	{
		InitializeComponent();

		DataContext = this;

		ConnectionString = App.Configuration!.GetConnectionString("DefaultConnection")!;
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

		using (Connection = new SqlConnection(ConnectionString))
		{
			Connection.Open();

			string? query = $@"
				USE [AppUsers];

				INSERT INTO [MainUsers] 
					([Name], [Surname], [Age], [Username], [Password])
				VALUES 
					('{NameTBox.Text}', '{SurnameTBox.Text}', {AgeTBox.Text}, '{UsernameTBox.Text}','{PasswordTBox.Text}');
			";

			Cmd = new(query, Connection);

			Cmd.ExecuteNonQuery();

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
