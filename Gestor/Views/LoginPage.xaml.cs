using Gestor.Clases;
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
            await DisplayAlert("Login failed", "Username or password is invalid", "Try again");
        }
    }

    

    
    bool IsCredentialCorrect(string Usuario, string Contraseña)
    {
        var match = RepositorioUsuarios
                            .ObtenerUsuarios()
                            .FirstOrDefault(u =>
                                string.Equals(u.Nombre, Usuario, StringComparison.OrdinalIgnoreCase)
                                && u.Contraseña == Contraseña);
        return match != null;
    }

    private  void btnRegistro_Clicked(object sender, EventArgs e)
    {
         Shell.Current.GoToAsync(nameof(RegistroPage));
    }

    private void btnRecuperar_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(RecuperarContraseña));
    }
}