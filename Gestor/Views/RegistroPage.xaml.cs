using Gestor.Clases;
namespace Gestor.Views;

public partial class RegistroPage : ContentPage
{
    public event EventHandler<string> EnError;
    public RegistroPage()
	{
		InitializeComponent();
	}
    
    private void btnRegistrarse_Clicked(object sender, EventArgs e)
    {
        if (ValidadorDeNombre.IsNotValid)
        {
            DisplayAlert("Error", "Se necesita un usuario", "OK");
            return;
        }
        if (ValidadorDeCorreo.IsNotValid)
        {
            DisplayAlert("Error", "Correo invalido", "OK");
            return;
        }
        if (ValidadorDeCorreo.IsValid && ValidadorDeNombre.IsValid)
        {
            RepositorioUsuarios.AgregarUsuario(new Usuarios
            {
                Nombre = EntradaUsuario.Text,
                Correo = EntradaCorreo.Text,
                Contraseña = EntradaContraseña.Text
            });

            Shell.Current.GoToAsync("..");
        }
        
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {

    }
    
}