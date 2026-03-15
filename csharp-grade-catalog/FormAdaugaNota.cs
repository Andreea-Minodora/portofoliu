using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CatalogDeNoteApp
{
    public partial class FormAdaugaNota : MaterialSkin.Controls.MaterialForm
    {
        Conectare conectare = new Conectare();

        public FormAdaugaNota()
        {
            InitializeComponent();
            LoadStudenti();
            LoadDiscipline();
        }

        private void LoadStudenti()
        {
            string query = "SELECT StudentiID, Nume + ' ' + Prenume AS NumeComplet FROM Studenti";
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "NumeComplet";
                comboBox1.ValueMember = "StudentiID";
                comboBox1.SelectedIndex = -1; // nimic selectat implicit
            }
            conectare.InchidereConectare();
        }

        private void LoadDiscipline()
        {
            string query = "SELECT DisciplinaID, Nume FROM Disciplina";
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "Nume";
                comboBox2.ValueMember = "DisciplinaID";
                comboBox2.SelectedIndex = -1; // nimic selectat implicit
            }
            conectare.InchidereConectare();
        }

        private void buttonAddNota_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Selectează un student și o disciplină.");
                return;
            }

            int studentId = (int)comboBox1.SelectedValue;
            int disciplinaId = (int)comboBox2.SelectedValue;
            int nota = (int)numericUpDown2.Value;
            int? notaLab = numericUpDown1.Value > 0 ? (int?)numericUpDown1.Value : null;
            DateTime dataNotarii = dateTimePicker2.Value;

            string query = @"
                INSERT INTO Nota (student_id, disciplina_id, nota, nota_laborator, data_notarii)
                VALUES (@student_id, @disciplina_id, @nota, @nota_laborator, @data_notarii)";

            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@disciplina_id", disciplinaId);
                cmd.Parameters.AddWithValue("@nota", nota);
                cmd.Parameters.AddWithValue("@nota_laborator", (object)notaLab ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@data_notarii", dataNotarii);

                cmd.ExecuteNonQuery();
            }
            conectare.InchidereConectare();

            MessageBox.Show("Nota a fost adăugată.");
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
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
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Selectează un student și o disciplină.");
                return;
            }

            int studentId = (int)comboBox1.SelectedValue;
            int disciplinaId = (int)comboBox2.SelectedValue;
            int nota = (int)numericUpDown2.Value;
            int? notaLab = numericUpDown1.Value > 0 ? (int?)numericUpDown1.Value : null;
            DateTime dataNotarii = dateTimePicker2.Value;

            string query = @"
            INSERT INTO Nota (student_id, disciplina_id, nota, nota_laborator, data_notarii)
            VALUES (@student_id, @disciplina_id, @nota, @nota_laborator, @data_notarii)";

            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@disciplina_id", disciplinaId);
                cmd.Parameters.AddWithValue("@nota", nota);
                cmd.Parameters.AddWithValue("@nota_laborator", (object)notaLab ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@data_notarii", dataNotarii);

                cmd.ExecuteNonQuery();
            }
            conectare.InchidereConectare();

            MessageBox.Show("Nota a fost adăugată.");
            this.Close();
        }
    

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
    
