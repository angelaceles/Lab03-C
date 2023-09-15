// See https://aka.ms/new-console-template for more information
using System.Data;
//Librerias del ADO .NET
using System.Data.SqlClient;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-14\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=12345678";


    static void Main()
    {
        var list = ListarProductosListaObjetos();
        foreach (var item in list) 
        {
            Console.WriteLine(item.Id + item.Nombre + item.Categoria + item.Precio + item.FechaVencimiento);
        };
    }

    //De forma desconectada
    private static DataTable ListarProductosDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Producto";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }

    //De forma conectada
    private static List<Producto> ListarProductosListaObjetos()
    {
        List<Producto> productos = new List<Producto>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT idProducto,Nombre,Categoria,Precio,FechaVencimiento FROM Producto";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            productos.Add(new Producto
                            {
                                Id = (int)reader["idProducto"],
                                Nombre = reader["Nombre"].ToString(),
                                Categoria = reader["Categoria"].ToString(),
                                Precio = (decimal)reader["Precio"],
                                FechaVencimiento = (DateTime)reader["FechaVencimiento"]
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return productos;

    }


}