using Gestor.Clases;
namespace Gestor.Views;

public partial class LoginPage : ContentPage
{
    private readonly UsuarioDatabase _database;
    public LoginPage()
	{
		InitializeComponent();
        _database = new UsuarioDatabase();
    }
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
    private async void BotonLogin_Clicked(object sender, EventArgs e)
    {
        // Ahora llamamos a la versión asíncrona de validación
        if (await IsCredentialCorrectAsync(Usuario.Text, Contraseña.Text))
        {
            Preferences.Set("UsuarioActual", Usuario.Text.Trim());
            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("//ContactosPage");
        }
        else
        {
            Preferences.Remove("UsuarioActual");
            await DisplayAlert("Inicio de sesion fallido",
                               "Usuario o contraseña invalidos",
                               "Intentalo de nuevo");
        }
    }

    private async Task<bool> IsCredentialCorrectAsync(string nombre, string contraseña)
    {
        var lista = await _database.ObtenerUsuariosAsync();
        var match = lista.FirstOrDefault(u =>
            string.Equals(u.Nombre, nombre?.Trim(), StringComparison.OrdinalIgnoreCase) &&
            u.Contraseña == contraseña);

        return match != null;
    }

    private async  void btnRegistro_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistroPage));
    }

    private async void btnRecuperar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RecuperarContraseña));
    }
}