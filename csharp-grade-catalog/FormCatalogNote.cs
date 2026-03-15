using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CatalogDeNoteApp
{
    public partial class FormCatalogNote : MaterialSkin.Controls.MaterialForm
    {
        Conectare conectare = new Conectare();

        public FormCatalogNote()
        {
            InitializeComponent();
            LoadFacultati();
        }

        private void LoadFacultati()
        {
            string query = "SELECT FacultateID, Nume FROM Facultate";
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtFacultati = new DataTable();
                adapter.Fill(dtFacultati);

                cmbFacultate.DataSource = dtFacultati;
                cmbFacultate.DisplayMember = "Nume";
                cmbFacultate.ValueMember = "FacultateID";
                cmbFacultate.SelectedIndex = -1;
            }
            conectare.InchidereConectare();
        }

        private void LoadSpecializari(int facultateId)
        {
            string query = "SELECT SpecializareID, Nume FROM Specializare WHERE FacultateID = @FacultateID";
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@FacultateID", facultateId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtSpecializari = new DataTable();
                adapter.Fill(dtSpecializari);

                cmbSpecializare.DataSource = dtSpecializari;
                cmbSpecializare.DisplayMember = "Nume";
                cmbSpecializare.ValueMember = "SpecializareID";
                cmbSpecializare.SelectedIndex = -1;
            }
            conectare.InchidereConectare();
        }

        private void LoadStudenti(int specializareId)
        {
            string query = "SELECT studentiID, Nume + ' ' + Prenume AS nume_complet FROM Studenti WHERE SpecializareID = @SpecializareID";
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@SpecializareID", specializareId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtStudenti = new DataTable();
                adapter.Fill(dtStudenti);

                cmbStudent.DataSource = dtStudenti;
                cmbStudent.DisplayMember = "nume_complet";
                cmbStudent.ValueMember = "studentiID";
                cmbStudent.SelectedIndex = -1;
            }
            conectare.InchidereConectare();
        }

        private void LoadDiscipline(int specializareId)
        {
            string query = "SELECT DisciplinaID, Nume FROM Disciplina WHERE SpecializareID = @SpecializareID";
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@SpecializareID", specializareId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtDiscipline = new DataTable();
                adapter.Fill(dtDiscipline);

                cmbDisciplina.DataSource = dtDiscipline;
                cmbDisciplina.DisplayMember = "Nume";
                cmbDisciplina.ValueMember = "DisciplinaID";
                cmbDisciplina.SelectedIndex = -1;
            }
            conectare.InchidereConectare();
        }

        private void LoadNote()
        {
            string query = @"
                SELECT 
                    n.id AS NotaID, 
                    s.nume + ' ' + s.prenume AS Student,
                    d.Nume AS Disciplina,
                    n.nota AS NotaExamen,
                    n.nota_laborator AS NotaLaborator,
                    n.data_notarii AS DataNota,
                    CASE 
                        WHEN n.nota_laborator IS NOT NULL THEN (n.nota + n.nota_laborator) / 2.0 
                        ELSE n.nota 
                    END AS Media
                FROM Nota n
                INNER JOIN Studenti s ON n.student_id = s.studentiID
                INNER JOIN Disciplina d ON n.disciplina_id = d.DisciplinaID
                WHERE (@FacultateID IS NULL OR s.FacultateID = @FacultateID)
                  AND (@SpecializareID IS NULL OR s.SpecializareID = @SpecializareID)
                  AND (@StudentID IS NULL OR s.studentiID = @StudentID)
                  AND (@DisciplinaID IS NULL OR d.DisciplinaID = @DisciplinaID)
            ";

            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@FacultateID", GetSelectedValueOrDbNull(cmbFacultate));
                cmd.Parameters.AddWithValue("@SpecializareID", GetSelectedValueOrDbNull(cmbSpecializare));
                cmd.Parameters.AddWithValue("@StudentID", GetSelectedValueOrDbNull(cmbStudent));
                cmd.Parameters.AddWithValue("@DisciplinaID", GetSelectedValueOrDbNull(cmbDisciplina));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtNote = new DataTable();
                adapter.Fill(dtNote);

                dataGridViewNote.DataSource = dtNote;
            }
            conectare.InchidereConectare();
        }

        private object GetSelectedValueOrDbNull(ComboBox combo)
        {
            if (string.IsNullOrEmpty(combo.ValueMember))
                return DBNull.Value;

            if (combo.SelectedItem is DataRowView drv && drv.Row != null)
            {
                if (drv.Row.Table.Columns.Contains(combo.ValueMember))
                {
                    var val = drv.Row[combo.ValueMember];
                    if (val != DBNull.Value)
                        return val;
                }
            }
            return DBNull.Value;
        }

        private void cmbFacultate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFacultate.SelectedItem is DataRowView drv && drv["FacultateID"] != DBNull.Value)
            {
                int facultateId = Convert.ToInt32(drv["FacultateID"]);
                LoadSpecializari(facultateId);
            }
            else
            {
                cmbSpecializare.DataSource = null;
                cmbStudent.DataSource = null;
                cmbDisciplina.DataSource = null;
            }
            LoadNote();
        }

        private void cmbSpecializare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpecializare.SelectedItem is DataRowView drv && drv["SpecializareID"] != DBNull.Value)
            {
                int specializareId = Convert.ToInt32(drv["SpecializareID"]);
                LoadStudenti(specializareId);
                LoadDiscipline(specializareId);
            }
            else
            {
                cmbStudent.DataSource = null;
                cmbDisciplina.DataSource = null;
            }
            LoadNote();
        }

        private void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNote();
        }

        private void cmbDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNote();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridViewNote_CellContentClick(object sender, EventArgs e)
        {
        }

        private void btnStergeNota_Click(object sender, EventArgs e)
        {
            if (dataGridViewNote.SelectedRows.Count > 0)
            {
                int notaId = Convert.ToInt32(dataGridViewNote.SelectedRows[0].Cells["NotaID"].Value);

                var confirmResult = MessageBox.Show("Ești sigur că vrei să ștergi această notă?",
                                                    "Confirmă ștergerea",
                                                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    string query = "DELETE FROM Nota WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                    {
                        cmd.Parameters.AddWithValue("@id", notaId);
                        cmd.ExecuteNonQuery();
                    }
                    conectare.InchidereConectare();

                    MessageBox.Show("Nota a fost ștearsă.");
                    LoadNote();
                }
            }
            else
            {
                MessageBox.Show("Selectează o notă pentru a o șterge.");
            }
        }

        private void btnModificaNota_Click(object sender, EventArgs e)
        {
            if (dataGridViewNote.SelectedRows.Count > 0)
            {
                int notaId = Convert.ToInt32(dataGridViewNote.SelectedRows[0].Cells["NotaID"].Value);
                var form = new FormModificaNota(notaId);
                form.ShowDialog();
                LoadNote(); // reîncarcă datele după modificare
            }
            else
            {
                MessageBox.Show("Selectează o notă pentru a o modifica.");
            }
        }

        private void btnAdaugaNota_Click(object sender, EventArgs e)
        {
            var form = new FormAdaugaNota();
            form.ShowDialog();
            LoadNote();
        }

        private void BtnMediaGenerala_Click(object sender, EventArgs e)
        {

            if (cmbStudent.SelectedValue == null)
            {
                MessageBox.Show("Selectează un student.");
                return;
            }

            int studentId = Convert.ToInt32(cmbStudent.SelectedValue);

            string query = @"
        SELECT AVG(CAST(nota AS FLOAT)) AS MediaGenerala 
        FROM Nota 
        WHERE student_id = @student_id";

            object result = null;
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@student_id", studentId);
                result = cmd.ExecuteScalar();
                conectare.InchidereConectare();
            }

            if (result != DBNull.Value && result != null)
            {
                decimal media = Convert.ToDecimal(result);
                textBoxMediaStudent.Text = media.ToString("F2");
            }
            else
            {
                textBoxMediaStudent.Text = "N/A";
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            if (dataGridViewNote.Rows.Count == 0)
            {
                MessageBox.Show("Nu există date pentru export.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV files|*.csv", FileName = "export_note.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName))
                        {
                            // Header
                            string[] columnNames = dataGridViewNote.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText).ToArray();
                            sw.WriteLine(string.Join(",", columnNames));

                            // Rows
                            foreach (DataGridViewRow row in dataGridViewNote.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    string[] fields = row.Cells.Cast<DataGridViewCell>().Select(cell =>
                                    {
                                        if (cell.Value == null) return "";
                                        string val = cell.Value.ToString();
                                        // Escape quotes
                                        if (val.Contains(",") || val.Contains("\""))
                                            val = $"\"{val.Replace("\"", "\"\"")}\"";
                                        return val;
                                    }).ToArray();

                                    sw.WriteLine(string.Join(",", fields));
                                }
                            }
                        }
                        MessageBox.Show("Export realizat cu succes!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare la export: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridViewNote_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSituatie_Click(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedValue == null || cmbDisciplina.SelectedValue == null)
            {
                MessageBox.Show("Selectează student și disciplină.");
                return;
            }

            int studentId = (int)cmbStudent.SelectedValue;
            int disciplinaId = (int)cmbDisciplina.SelectedValue;

            string query = @"
        SELECT s.StudentiID, s.Nume + ' ' + s.Prenume AS Student,
               d.Nume AS Disciplina,
               AVG(COALESCE(n.nota, 0) + COALESCE(n.nota_laborator, 0))/2.0 AS Media
        FROM Nota n
        INNER JOIN Studenti s ON n.student_id = s.StudentiID
        INNER JOIN Disciplina d ON n.disciplina_id = d.DisciplinaID
        WHERE s.StudentiID = @studentId AND d.DisciplinaID = @disciplinaId
        GROUP BY s.StudentiID, s.Nume, s.Prenume, d.Nume";

            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
            {
                cmd.Parameters.AddWithValue("@studentId", studentId);
                cmd.Parameters.AddWithValue("@disciplinaId", disciplinaId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            conectare.InchidereConectare();

            // Adaugă o coloană „Situatie” în DataTable
            dt.Columns.Add("Situatie", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                decimal media = 0;
                if (decimal.TryParse(row["Media"].ToString(), out media))
                {
                    row["Situatie"] = media >= 5 ? "Promovat" : "Nepromovat";
                }
                else
                {
                    row["Situatie"] = "N/A";
                }
            }

            dataGridViewSituatie.DataSource = dt;
        }
    }
    }
