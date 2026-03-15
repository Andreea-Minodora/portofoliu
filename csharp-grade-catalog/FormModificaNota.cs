using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CatalogDeNoteApp
{
    public partial class FormModificaNota : MaterialSkin.Controls.MaterialForm
    {
        private int NotaID;
        Conectare conectare = new Conectare();

        public FormModificaNota(int notaId)
        {
            InitializeComponent();
            NotaID = notaId;
            LoadNota();
        }

        private void LoadNota()
        {
            string query = "SELECT student_id, disciplina_id, nota, nota_laborator, data_notarii FROM Nota WHERE id = @NotaID";
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@NotaID", NotaID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        comboBox1.SelectedValue = reader["student_id"] != DBNull.Value ? Convert.ToInt32(reader["student_id"]) : 0;
                        comboBox2.SelectedValue = reader["disciplina_id"] != DBNull.Value ? Convert.ToInt32(reader["disciplina_id"]) : 0;

                        numericUpDown2.Value = reader["nota"] != DBNull.Value ? Convert.ToDecimal(reader["nota"]) : 0;
                        numericUpDown1.Value = reader["nota_laborator"] != DBNull.Value ? Convert.ToDecimal(reader["nota_laborator"]) : 0;

                        dateTimePicker2.Value = reader["data_notarii"] != DBNull.Value ? Convert.ToDateTime(reader["data_notarii"]) : DateTime.Now;
                    }
                }
            }
            conectare.InchidereConectare();
        }


        private void btnSalveazaNota_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Selectează un student și o disciplină înainte de a salva nota.");
                return;
            }

            int studentId = (int)comboBox1.SelectedValue;
            int disciplinaId = (int)comboBox2.SelectedValue;
            decimal nota = numericUpDown2.Value;
            decimal? notaLab = numericUpDown1.Value > 0 ? (decimal?)numericUpDown1.Value : null;
            DateTime dataNotarii = dateTimePicker2.Value;

            string query = @"
        UPDATE Nota 
        SET student_id = @student_id, disciplina_id = @disciplina_id, nota = @nota, nota_laborator = @nota_laborator, data_notarii = @data_notarii
        WHERE id = @NotaID";

            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@disciplina_id", disciplinaId);
                cmd.Parameters.AddWithValue("@nota", nota);
                cmd.Parameters.AddWithValue("@nota_laborator", notaLab.HasValue ? (object)notaLab.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@data_notarii", dataNotarii);
                cmd.Parameters.AddWithValue("@NotaID", NotaID);

                cmd.ExecuteNonQuery();
            }
            conectare.InchidereConectare();

            MessageBox.Show("Nota a fost modificată.");
            this.Close();
        }


        private void btnCalculeazaMedie_Click(object sender, EventArgs e)
        {
            decimal notaExamen = numericUpDown2.Value;
            decimal notaLaborator = numericUpDown1.Value;

            decimal media;
            if (notaLaborator > 0)
                media = (notaExamen + notaLaborator) / 2;
            else
                media = notaExamen;

            textBox1.Text = media.ToString("F2");
        }
    }
}
