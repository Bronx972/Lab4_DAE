using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Wpf_Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string connectionString = "Data Source=LAB1504-17\\SQLEXPRESS;Initial Catalog=Neptuno;User ID=userTecsup;Password=123456";
        public MainWindow()
        {
            InitializeComponent();
            McDataGrid.ItemsSource = ListarProveedoresPor("","");
        }

        private static List<Proveedor> ListarProveedoresPor(string nombrecontacto, string ciudad)
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();
                string query = "usp_ListarProveedoresPor";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    SqlParameter parameter = new SqlParameter("@nombrecontacto", nombrecontacto);
                    SqlParameter parameter2 = new SqlParameter("@ciudad", ciudad);
                    command.Parameters.Add(parameter);
                    command.Parameters.Add(parameter2);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verificar si hay filas
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Leer los datos de cada fila
                                proveedores.Add(new Proveedor
                                {
                                    IdProveedor = (int)reader["IdProveedor"],
                                    NombreCompañia = reader["nombreCompañia"].ToString(),
                                    NombreContacto = reader["nombrecontacto"].ToString(),
                                    CargoContacto = reader["cargocontacto"].ToString(),
                                    Direccion = reader["direccion"].ToString(),
                                    Ciudad = reader["ciudad"].ToString(),
                                    Region = reader["pais"].ToString(),
                                    CodPostal = reader["codPostal"].ToString(),
                                    Pais = reader["pais"].ToString(),
                                    Telefono = reader["telefono"].ToString(),
                                    Fax = reader["fax"].ToString()
                                });

                            }
                        }
                    }
                }

                // Cerrar la conexión
                connection.Close();
            }
            return proveedores;

        }
    }
}
