using Gestor.Clases;
namespace Gestor.Views;

public partial class AñadirContactosPage : ContentPage
{
	public AñadirContactosPage()
	{
		InitializeComponent();
	}

    private void ContactoCntrl_AlGuardar(object sender, EventArgs e)
    {
        RepositorioContactos.AgregarContacto(new ClaseContactos
        {
            Nombre = ContactoCntrl.Nombre,
            Telefono = ContactoCntrl.Telefono,
            Correo = ContactoCntrl.Correo,
            Direccion = ContactoCntrl.Direccion
        });
        Shell.Current.GoToAsync("..");
    }

    private void ContactoCntrl_AlCancelar(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void ContactoCntrl_EnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}