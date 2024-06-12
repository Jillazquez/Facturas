using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Facturas
{
    public partial class Form1 : Form
    {
        private static string folder = ("facturas");
        private string csvFilePath = Path.Combine( folder,"facturas.csv");
        private string iconoEmpresa = Path.Combine("assets", "icono.png");

        public Form1()
        {
            InitializeComponent();
            LoadFacturas();
            if (!Directory.Exists("assets"))
            {
                Directory.CreateDirectory("assets");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFacturas();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = productName.Text;
                int unit = Int32.Parse(priceUnit.Text);
                int quantity = Int32.Parse(qty.Text);
                int total = unit * quantity;

                lista.Rows.Add(name, unit, quantity, total);

                ClearInputs();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Por favor, asegúrese de que todos los campos numéricos tienen valores válidos.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SaveFacturas();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFacturas();
        }

        private void LoadFacturas()
        {
            if (File.Exists(csvFilePath))
            {
                var lines = File.ReadAllLines(csvFilePath);
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    lista.Rows.Add(values[0], int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]));
                }
            }
        }

        private void SaveFacturas()
        {
            try
            {
                var lines = new List<string>
                {
                    "Nombre,PrecioUnitario,Cantidad,Total" // Header
                };
                foreach (DataGridViewRow row in lista.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value.ToString());
                        lines.Add(string.Join(",", cells));
                    }
                }
                Directory.CreateDirectory("Facturas");

                //Delete the file to avoid dupes
                if (File.Exists(csvFilePath))
                {
                    File.Delete(csvFilePath);
                }
                
                // Print the file path for debugging
                Console.WriteLine("Saving to: " + csvFilePath);
                File.WriteAllLines(csvFilePath, lines);
                

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar las facturas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarPDF()
        {
            // Crear un nuevo documento PDF
            Document doc = new Document();
            if(lista.SelectedRows.Count < 1)
            {
                MessageBox.Show($"Selecciona al menos una fila para generar la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // Definir la ruta donde se guardará el PDF
                    string dia = DateTime.Now.ToString("dd-MM-yyyy");
                    string pdfFilePath = "factura" + dia + ".pdf";

                    // Crear un objeto PdfWriter para escribir en el PDF
                    PdfWriter.GetInstance(doc, new FileStream(pdfFilePath, FileMode.Create));

                    // Abrir el documento para escribir contenido
                    doc.Open();

                    if (File.Exists(iconoEmpresa))
                    {
                        Image logo = Image.GetInstance(iconoEmpresa);
                        logo.ScaleAbsolute(100f, 100f); // Ajustar el tamaño del logo según sea necesario
                        logo.Alignment = Element.ALIGN_LEFT;
                        doc.Add(logo);
                    }

                    // Agregar un título al documento
                    Paragraph title = new Paragraph("Factura");
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    /// Crear una tabla con el número de columnas adecuado
                    PdfPTable table = new PdfPTable(4); // 4 columnas: Nombre, Precio Unitario, Cantidad, Total
                    table.WidthPercentage = 100;

                    // Agregar los encabezados de la tabla
                    table.AddCell("Nombre");
                    table.AddCell("Precio Unitario");
                    table.AddCell("Cantidad");
                    table.AddCell("Total");

                    // Agregar los datos de las filas seleccionadas a la tabla
                    foreach (DataGridViewRow row in lista.SelectedRows)
                    {
                        table.AddCell(row.Cells["Producto"].Value.ToString());
                        table.AddCell(row.Cells["PrecioUnitario"].Value.ToString());
                        table.AddCell(row.Cells["Cantidad"].Value.ToString());
                        table.AddCell(row.Cells["Total"].Value.ToString());
                    }

                    // Agregar la tabla al documento
                    doc.Add(table);

                    // Cerrar el documento
                    doc.Close();

                    // Mostrar un mensaje de éxito
                    MessageBox.Show($"PDF generado exitosamente: {pdfFilePath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Mostrar un mensaje de error si ocurre algún problema
                    MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void borrarLineas()
        {
            foreach (DataGridViewRow row in lista.SelectedRows)
            {
                try
                {
                    lista.Rows.Remove(row);
                }
                catch (Exception ex)
                {
                    // Mostrar un mensaje de error si ocurre algún problema
                    MessageBox.Show($"Error borrando linea", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }   
        }


        private void ClearInputs()
        {
            productName.Clear();
            priceUnit.Clear();
            qty.Clear();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            LoadFacturas();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFacturas();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GenerarPDF();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            borrarLineas();
        }
    }
}
