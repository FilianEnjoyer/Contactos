namespace Gestor.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
    private async void BotonLogin_Clicked(object sender, EventArgs e)
    {
        if (IsCredentialCorrect(Usuario.Text, Contraseña.Text))
        {
            Preferences.Set("UsuarioActual", Usuario.Text.Trim());
            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("//ContactosPage");
        }
        else
        {
            Preferences.Remove("UsuarioActual");
            await DisplayAlert("Login failed", "Username or password if invalid", "Try again");
        }
    }

    

    
    bool IsCredentialCorrect(string username, string password)
    {
        return Usuario.Text == "admin" && Contraseña.Text == "1234";
    }

    private  void btnRegistro_Clicked(object sender, EventArgs e)
    {
         Shell.Current.GoToAsync(nameof(RegistroPage));
    }

    private void btnRecuperar_Clicked(object sender, EventArgs e)
    {

    }
}