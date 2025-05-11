
using Gestor.Clases;
using Gestor.Views.Controles;
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
                ContactoCntrl.Nombre = contacto.Nombre;
                ContactoCntrl.Telefono = contacto.Telefono;
                ContactoCntrl.Correo = contacto.Correo;
                ContactoCntrl.Direccion = contacto.Direccion;
            }
        }
	}

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {



        contacto.Nombre = ContactoCntrl.Nombre;
        contacto.Telefono = ContactoCntrl.Telefono;
        contacto.Correo = ContactoCntrl.Correo;
        contacto.Direccion = ContactoCntrl.Direccion;

        RepositorioContactos.ActualizarContacto(contacto.Id, contacto);
        Shell.Current.GoToAsync("..");
    }

    private void ContactoCntrl_EnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}