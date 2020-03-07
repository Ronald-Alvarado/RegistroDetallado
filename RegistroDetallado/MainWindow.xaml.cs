using RegistroDetallado.BLL;
using RegistroDetallado.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistroDetallado
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TelefonosDetalle> Detalles { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Detalles = new List<TelefonosDetalle>();
        }

        private void Limpiar()
        {
            Id_Text.Text = "0";
            Nombre_Text.Text = String.Empty;
            Cedula_Text.Text = String.Empty;
            Direccion_Text.Text = String.Empty;
            Fecha_Text.SelectedDate = DateTime.Now;

            this.Detalles = new List<TelefonosDetalle>();
            CargarGrid();

        }

        private Personas LlenaClase()
        {
            Personas p = new Personas();

            p.PersonaId = Convert.ToInt32(Id_Text.Text);
            p.Nombre = Nombre_Text.Text;
            p.Cedula = Cedula_Text.Text;
            p.Direccion = Direccion_Text.Text;
            p.FechaNacimiento = Fecha_Text.DisplayDate;

            p.Telefonos = this.Detalles;

            return p;

        }

        private void LlenaCampos(Personas p)
        {
            Id_Text.Text = Convert.ToString(p.PersonaId);
            Nombre_Text.Text = p.Nombre;
            Cedula_Text.Text = p.Cedula;
            Direccion_Text.Text = p.Direccion;
            Fecha_Text.Text = Convert.ToString(p.FechaNacimiento);

            this.Detalles = p.Telefonos;
            CargarGrid();
        }

        private bool Validar()
        {
            bool paso = true;

            if(Id_Text.Text == String.Empty)
            {
                MessageBox.Show("Id Vacio","Error");
                Id_Text.Focus();
                paso = false;
            }

            if(Nombre_Text.Text == String.Empty)
            {
                MessageBox.Show("Nombre Vacio","Error");
                Nombre_Text.Focus();
                paso = false;
            }

            if(Cedula_Text.Text == String.Empty)
            {
                MessageBox.Show("Cedula Vacia","Error");
                Cedula_Text.Focus();
                paso = false;
            }

            if(Direccion_Text.Text == String.Empty)
            {
                MessageBox.Show("Direccion Vacia","Error");
                Direccion_Text.Focus();
                paso = false;
            }

            if(Fecha_Text.Text == String.Empty)
            {
                MessageBox.Show("Fecha Vacia","Error");
                Fecha_Text.Focus();
                paso = false;
            }

            if (this.Detalles.Count == 0)
            {
                MessageBox.Show("Debe Agregar un Telefono","Error");
                Telefono_Text.Focus();
                paso = false;
            }

            return paso;
        }

        private void CargarGrid()
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = this.Detalles;
        }

        private bool ExisteBase()
        {
            Personas p = PersonasBLL.Buscar((int)Convert.ToInt32(Id_Text.Text));

            return (p != null);
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int Id;
            int.TryParse(Id_Text.Text, out Id);
            Personas p = new Personas();

            Personas Anterior = PersonasBLL.Buscar(Id);
           
            Limpiar();

            if(Anterior != null)
            {
                LlenaCampos(Anterior);
                CargarGrid();
            }
            else
            {
                MessageBox.Show("No se Encontró");
            }

        }

        private void Agregar_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.DataContext != null)
            {
                this.Detalles = (List<TelefonosDetalle>)DataGrid.ItemsSource;
            }

            this.Detalles.Add(new TelefonosDetalle
            {
                Id = 0,
                PersonaId = Convert.ToInt32(Id_Text.Text),
                TipoTelefono = Tipo_Text.Text,
                Telefono = Telefono_Text.Text
            }
            );

            CargarGrid();
            Telefono_Text.Focus();
            Telefono_Text.Clear();
            Tipo_Text.Clear();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if(DataGrid.Items.Count > 1 && DataGrid.SelectedIndex < DataGrid.Items.Count - 1)
            {
                Detalles.RemoveAt(DataGrid.SelectedIndex);

                CargarGrid();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;
            Personas p;

            if (!Validar())
            {
                return;
            }

            p = LlenaClase();

            if(Id_Text.Text == "0")
            {
               paso = PersonasBLL.Guardar(p);
            }
            else
            {
                if (!ExisteBase())
                {
                    MessageBox.Show("No existe en la database");
                    return;
                }
                paso = PersonasBLL.Modificar(p);
            }

            if (paso)
            {
                MessageBox.Show("Se Guardo");
            }
            else
            {
                MessageBox.Show("No se Guardo");
            }

            Limpiar();
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int Id;
            int.TryParse(Id_Text.Text, out Id);

            Limpiar();

            if (PersonasBLL.Eliminar(Id))
            {
                MessageBox.Show("Se elimino");
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }
    }
}
