namespace Gestor.Views.Controles;

public partial class ControlContacto : ContentView
{
    public event EventHandler<string> EnError;
    public event EventHandler<EventArgs> AlGuardar;
    public event EventHandler<EventArgs> AlCancelar;
    public ControlContacto()
	{
		InitializeComponent();
	}
	public string Nombre
    {
        get 
		{
			return EntradaNombre.Text;
		}
		set
		{
			EntradaNombre.Text = value;
        }
    }
    public string Telefono
    {
        get
        {
            return EntradaTelefono.Text;
        }
        set
        {
            EntradaTelefono.Text = value;
        }
    }
    public string Correo
    {
        get
        {
            return EntradaCorreo.Text;
        }
        set
        {
            EntradaCorreo.Text = value;
        }
    }
    public string Direccion
    {
        get
        {
            return EntradaDireccion.Text;
        }
        set
        {
            EntradaDireccion.Text = value;
        }
    }

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
        if (ValidadorDeNombre.IsNotValid)
        {
            EnError?.Invoke(sender, "Se requiere un nombre");
            return;
        }

        if (ValidadorDeCorreo.IsNotValid)
        {
            foreach (var error in ValidadorDeCorreo.Errors)
            {
                EnError?.Invoke(sender, error.ToString());
            }
            return;
        }
        AlGuardar?.Invoke(sender, e);
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        AlCancelar?.Invoke(sender, e);
    }
}