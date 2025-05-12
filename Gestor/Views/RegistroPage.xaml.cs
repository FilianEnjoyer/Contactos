using CommunityToolkit.Maui.Behaviors;
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
            foreach (var error in ValidadorDeCorreo.Errors)
            {
                DisplayAlert("Error", error.ToString(), "OK");
                return;
            }
        }
        
        RepositorioUsuarios.AgregarUsuario(new Usuarios
        {
            Nombre = EntradaUsuario.Text,
            Correo = EntradaCorreo.Text,
            Contraseña = EntradaContraseña.Text
        });

        Shell.Current.GoToAsync("..");
        
        
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {

    }
    
}