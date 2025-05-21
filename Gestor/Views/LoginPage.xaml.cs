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
        // Ahora llamamos a la versi�n as�ncrona de validaci�n
        if (await IsCredentialCorrectAsync(Usuario.Text, Contrase�a.Text))
        {
            Preferences.Set("UsuarioActual", Usuario.Text.Trim());
            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("//ContactosPage");
        }
        else
        {
            Preferences.Remove("UsuarioActual");
            await DisplayAlert("Inicio de sesion fallido",
                               "Usuario o contrase�a invalidos",
                               "Intentalo de nuevo");
        }
    }

    private async Task<bool> IsCredentialCorrectAsync(string nombre, string contrase�a)
    {
        var lista = await _database.ObtenerUsuariosAsync();
        var match = lista.FirstOrDefault(u =>
            string.Equals(u.Nombre, nombre?.Trim(), StringComparison.OrdinalIgnoreCase) &&
            u.Contrase�a == contrase�a);

        return match != null;
    }

    private async  void btnRegistro_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistroPage));
    }

    private async void btnRecuperar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RecuperarContrase�a));
    }
}