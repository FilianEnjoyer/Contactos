using Gestor.Clases;

namespace Gestor.Views;

public partial class RecuperarContraseña : ContentPage
{
    private string _correoValidado;
    public RecuperarContraseña()
	{
		InitializeComponent();
	}

    bool EsCorrectoCorreo(string correo)
    {
        return RepositorioUsuarios
            .ObtenerUsuarios()
            .Any(u => u.Correo == correo);
    }
    private async void btnAceptar_Clicked(object sender, EventArgs e)
    {
        var correo = EntradaCorreo.Text?.Trim();
        if (EsCorrectoCorreo(correo))
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
            return;
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
        var usuario = RepositorioUsuarios
                .ObtenerUsuarios()
                .FirstOrDefault(u =>
                    string.Equals(u.Correo, _correoValidado, StringComparison.OrdinalIgnoreCase)
                );

        if (usuario != null)
        {
            // Preparo un objeto Usuarios con el mismo Id y nombre/correo, pero nueva contraseña
            var actualizado = new Usuarios
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Contraseña = nuevaPass
            };

            // Llamo al método de repositorio
            RepositorioUsuarios.ActualizarUsuario(usuario.Id, actualizado);

            await DisplayAlert("Éxito", "Contraseña actualizada correctamente", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        
        Shell.Current.GoToAsync("..");
    }
}