using Gestor.Clases;
namespace Gestor.Views;

public partial class ContactosPage : ContentPage
{
	public ContactosPage()
	{
		InitializeComponent();
		List<ClaseContactos> Contactos = new List<ClaseContactos>()
		{ 
			new ClaseContactos { Nombre= "Alonso1", Correo="ads@algo.es1", Direccion="algun lugar1", Telefono = "1234561"},
			new ClaseContactos { Nombre= "Alonso2", Correo="ads@algo.es2", Direccion="algun lugar2", Telefono = "1234562"},
            new ClaseContactos { Nombre= "Alonso3", Correo="ads@algo.es3", Direccion="algun lugar3", Telefono = "1234563"}
        };
        listContacts.ItemsSource = Contactos;
    }
}