
using Gestor.Clases;
using Gestor = Gestor.Clases.ClaseContactos;
namespace Gestor.Views;
[QueryProperty(nameof(IDContacto),"ID")]
public partial class EditarContactosPage : ContentPage
{
    private ClaseContactos contacto;
    public EditarContactosPage()
	{
		InitializeComponent();
	}
	public string IDContacto
	{
		set
		{
			contacto = RepositorioContactos.ObtenerContactoConID(int.Parse(value));
            if (contacto != null)
            {
                EntradaNombre.Text = contacto.Nombre;
                EntradaTelefono.Text = contacto.Telefono;
                EntradaCorreo.Text = contacto.Correo;
                EntradaDireccion.Text = contacto.Direccion;
            }
        }
	}

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        if (ValidadorDeNombre.IsNotValid)
        {
            DisplayAlert("Error", "Se requiere un nombre", "OK");
            return;
        }

        if (ValidadorDeCorreo.IsNotValid)
        {
            foreach (var error in ValidadorDeCorreo.Errors)
            {
                DisplayAlert("Error", error.ToString(), "Ok");
            }
            return;
        }


        contacto.Nombre = EntradaNombre.Text;
        contacto.Telefono = EntradaTelefono.Text;
        contacto.Correo = EntradaCorreo.Text;
        contacto.Direccion = EntradaDireccion.Text;

        RepositorioContactos.ActualizarContacto(contacto.Id, contacto);
        Shell.Current.GoToAsync("..");
    }
}