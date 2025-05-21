using Gestor.Clases;

namespace Gestor.Views;

public partial class RecuperarContraseña : ContentPage
{
    private readonly UsuarioDatabase _database;
    private string _correoValidado;
    public RecuperarContraseña()
	{
		InitializeComponent();
        _database = new UsuarioDatabase();
    }

    // Comprueba en SQLite si ya existe un usuario con ese correo
    private async Task<bool> EsCorrectoCorreoAsync(string correo)
    {
        // Podrías crear un método específico en UsuarioDatabase,
        // pero aquí simplemente filtramos la lista completa:
        var lista = await _database.ObtenerUsuariosAsync();
        return lista.Any(u =>
            string.Equals(u.Correo, correo, StringComparison.OrdinalIgnoreCase)
        );
    }
    private async void btnAceptar_Clicked(object sender, EventArgs e)
    {
        var correo = EntradaCorreo.Text?.Trim();
        if (await EsCorrectoCorreoAsync(correo))
        {
            _correoValidado = correo;
            await DisplayAlert("Correo", "Su correo se verificó correctamente", "OK");

            FrameCorreo.IsVisible = false;
            FrameContraseña.IsVisible = true;
            btnAceptar.IsVisible = false;
            btnRecuperar.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Error", "El correo no existe", "OK");
        }
    }

    private async void btnRecuperar_Clicked(object sender, EventArgs e)
    {
        var nuevaPass = EntradaContraseña.Text;
        if (ValidadorDeContraseña.IsNotValid)
        {
            await DisplayAlert("Error", "Se necesita introducir una contraseña", "OK");
            return;
        }

        // Obtenemos el usuario del correo validado
        var lista = await _database.ObtenerUsuariosAsync();
        var usuario = lista.FirstOrDefault(u =>
            string.Equals(u.Correo, _correoValidado, StringComparison.OrdinalIgnoreCase)
        );

        if (usuario != null)
        {
            // Actualizamos la contraseña y guardamos
            usuario.Contraseña = nuevaPass;
            await _database.GuardarUsuarioAsync(usuario);

            await DisplayAlert("Éxito", "Contraseña actualizada correctamente", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            // Esto no debería pasar si EsCorrectoCorreoAsync ya pasó
            await DisplayAlert("Error", "No se pudo encontrar el usuario", "OK");
        }
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("..");
    }
}