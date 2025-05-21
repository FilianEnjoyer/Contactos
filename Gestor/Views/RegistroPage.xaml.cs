using CommunityToolkit.Maui.Behaviors;
using Gestor.Clases;
namespace Gestor.Views;

public partial class RegistroPage : ContentPage
{
    private readonly UsuarioDatabase _database;
    public event EventHandler<string> EnError;
    public RegistroPage()
	{
		InitializeComponent();
        _database = new UsuarioDatabase();
    }
    
    private async void btnRegistrarse_Clicked(object sender, EventArgs e)
    {
        // Validaciones locales
        if (ValidadorDeNombre.IsNotValid)
        {
            await DisplayAlert("Error", "Se necesita un usuario", "OK");
            return;
        }

        if (ValidadorDeCorreo.IsNotValid)
        {
            foreach (var error in ValidadorDeCorreo.Errors)
            {
                await DisplayAlert("Error", error.ToString(), "OK");
                return;
            }
        }

        // Crear la entidad y guardarla en SQLite
        var nuevoUsuario = new Usuarios
        {
            Nombre = EntradaUsuario.Text?.Trim(),
            Correo = EntradaCorreo.Text?.Trim(),
            Contraseña = EntradaContraseña.Text
        };

        try
        {
            await _database.GuardarUsuarioAsync(nuevoUsuario);
            // Navegar de regreso
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            // Dispara el evento de error si alguien está suscrito
            EnError?.Invoke(this, ex.Message);
            await DisplayAlert("Error al guardar", ex.Message, "OK");
        }


    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
    
}