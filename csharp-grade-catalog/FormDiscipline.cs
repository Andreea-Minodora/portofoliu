using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CatalogDeNoteApp
{
    public partial class FormDiscipline : MaterialSkin.Controls.MaterialForm
    {
        Conectare conectare = new Conectare();

        public FormDiscipline()
        {
            InitializeComponent();
            this.Load += FormDiscipline_Load;

            cmbSpecializare1.SelectedIndexChanged += cmbSpecializare1_SelectedIndexChanged;
            cmbSpecializare2.SelectedIndexChanged += cmbSpecializare2_SelectedIndexChanged;
            cmbSpecializare3.SelectedIndexChanged += cmbSpecializare3_SelectedIndexChanged;
        }

        private void FormDiscipline_Load(object sender, EventArgs e)

        {
            int facultate1Id = 1; // inginerie electrica
            int facultate2Id = 2; // drept
            int facultate3Id = 3; // litere

            LoadSpecializari(facultate1Id, cmbSpecializare1);
            LoadSpecializari(facultate2Id, cmbSpecializare2);
            LoadSpecializari(facultate3Id, cmbSpecializare3);


            DataTable tipEvaluareTable = new DataTable();
            tipEvaluareTable.Columns.Add("Text");
            tipEvaluareTable.Columns.Add("Value");

            tipEvaluareTable.Rows.Add("Examen", 1);
            tipEvaluareTable.Rows.Add("Colocviu", 2);
            tipEvaluareTable.Rows.Add("Verificare", 3);

            cmbTipEvaluare.DataSource = tipEvaluareTable;
            cmbTipEvaluare.DisplayMember = "Text";
            cmbTipEvaluare.ValueMember = "Value";

            cmbTipEvaluare2.DataSource = tipEvaluareTable.Copy();  // Copie pentru a nu lega aceleași date
            cmbTipEvaluare2.DisplayMember = "Text";
            cmbTipEvaluare2.ValueMember = "Value";

            // Similar pentru cmbTipEvaluare3, dacă ai
            cmbTipEvaluare3.DataSource = tipEvaluareTable.Copy();
            cmbTipEvaluare3.DisplayMember = "Text";
            cmbTipEvaluare3.ValueMember = "Value";
        }



        private void LoadSpecializari(int facultateId, ComboBox combo)
        {
            if (facultateId == -1)
            {
                combo.DataSource = null;
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT SpecializareID, Nume FROM Specializare WHERE FacultateID=@fid", conectare.DeschidereConectare());
                cmd.Parameters.AddWithValue("@fid", facultateId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                combo.DataSource = dt;
                combo.DisplayMember = "Nume";
                combo.ValueMember = "SpecializareID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea specializărilor: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void LoadDiscipline(int specializareId, DataGridView grid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT DisciplinaID, Nume, Acronim, TipEvaluare FROM Disciplina WHERE SpecializareID = @sid", conectare.DeschidereConectare());

                cmd.Parameters.AddWithValue("@sid", specializareId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                grid.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea disciplinelor: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void cmbSpecializare1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpecializare1.SelectedValue != null && int.TryParse(cmbSpecializare1.SelectedValue.ToString(), out int specializareId))
            {
                LoadDiscipline(specializareId, dataGridView1);
            }
        }

        private void cmbSpecializare2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpecializare2.SelectedValue != null && int.TryParse(cmbSpecializare2.SelectedValue.ToString(), out int specializareId))
            {
                LoadDiscipline(specializareId, dataGridView2);
            }
        }

        private void cmbSpecializare3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpecializare3.SelectedValue != null && int.TryParse(cmbSpecializare3.SelectedValue.ToString(), out int specializareId))
            {
                LoadDiscipline(specializareId, dataGridView3);
            }
        }



        private void button3_Click(object sender, EventArgs e)

        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează o disciplină de șters.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Ești sigur că vrei să ștergi această disciplină?", "Confirmare", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;


                int disciplinaId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["DisciplinaID"].Value);

                string query = "DELETE FROM Disciplina WHERE DisciplinaID = @id";
                using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                {
                    cmd.Parameters.AddWithValue("@id", disciplinaId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Disciplina a fost ștearsă cu succes!");
                int specializareId = (int)cmbSpecializare1.SelectedValue;
                LoadDiscipline(specializareId, dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergere: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }


        private void comboBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            try
            {
                string nume = txtNume.Text.Trim();
                string acronim = txtAcronim.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare.SelectedValue.ToString());
                int specializareId = (int)cmbSpecializare1.SelectedValue;

                string query = "INSERT INTO Disciplina (Nume, Acronim, TipEvaluare, SpecializareID) VALUES (@nume, @acronim, @tip, @specId)";
                using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                {
                    cmd.Parameters.AddWithValue("@nume", nume);
                    cmd.Parameters.AddWithValue("@acronim", acronim);
                    cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                    cmd.Parameters.AddWithValue("@specId", specializareId);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Disciplina a fost adaugata!");
                LoadDiscipline(specializareId, dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adaugare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizare_Click(object sender, EventArgs e)
        {

            try
            {

                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selectează o disciplină pentru actualizare.");
                    return;
                }


                int disciplinaId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["DisciplinaID"].Value);


                string nume = txtNume.Text.Trim();
                string acronim = txtAcronim.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare.SelectedValue.ToString());

                string query = "UPDATE Disciplina SET Nume=@nume, Acronim=@acronim, TipEvaluare=@tip WHERE DisciplinaID=@id";
                using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                {
                    cmd.Parameters.AddWithValue("@nume", nume);
                    cmd.Parameters.AddWithValue("@acronim", acronim);
                    cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                    cmd.Parameters.AddWithValue("@id", disciplinaId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Disciplina a fost actualizată cu succes.");
                    else
                        MessageBox.Show("Actualizarea nu a avut loc. Verifică datele.");
                }


                int specializareId = (int)cmbSpecializare1.SelectedValue;
                LoadDiscipline(specializareId, dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }
        private void ClearFields()
        {
            txtNume.Text = "";
            txtAcronim.Text = "";
            cmbTipEvaluare.SelectedIndex = -1;
            dataGridView1.ClearSelection();
        }

        private void btnSalveaza_Click(object sender, EventArgs e)
        {

            try
            {
                string nume = txtNume.Text.Trim();
                string acronim = txtAcronim.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare.SelectedValue.ToString());
                int specializareId = (int)cmbSpecializare1.SelectedValue;

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int disciplinaId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["DisciplinaID"].Value);

                    string updateQuery = "UPDATE Disciplina SET Nume=@nume, Acronim=@acronim, TipEvaluare=@tip WHERE DisciplinaID=@id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conectare.DeschidereConectare()))
                    {
                        cmd.Parameters.AddWithValue("@nume", nume);
                        cmd.Parameters.AddWithValue("@acronim", acronim);
                        cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                        cmd.Parameters.AddWithValue("@id", disciplinaId);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Disciplina a fost actualizată!");
                }
                else
                {
                    string insertQuery = "INSERT INTO Disciplina (Nume, Acronim, TipEvaluare, SpecializareID) VALUES (@nume, @acronim, @tip, @specId)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conectare.DeschidereConectare()))
                    {
                        cmd.Parameters.AddWithValue("@nume", nume);
                        cmd.Parameters.AddWithValue("@acronim", acronim);
                        cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                        cmd.Parameters.AddWithValue("@specId", specializareId);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Disciplina a fost adăugată!");
                }

                LoadDiscipline(specializareId, dataGridView1);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void btnAdauga2_Click(object sender, EventArgs e)
        {

            try
            {
                string nume = txtNume2.Text.Trim();
                string acronim = txtAcronim2.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare2.SelectedValue.ToString());
                int specializareId = (int)cmbSpecializare2.SelectedValue;

                string query = "INSERT INTO Disciplina (Nume, Acronim, TipEvaluare, SpecializareID) VALUES (@nume, @acronim, @tip, @specId)";
                using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                {
                    cmd.Parameters.AddWithValue("@nume", nume);
                    cmd.Parameters.AddWithValue("@acronim", acronim);
                    cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                    cmd.Parameters.AddWithValue("@specId", specializareId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Disciplina a fost adăugată!");
                LoadDiscipline(specializareId, dataGridView2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void btnActualizeaza2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selectează o disciplină pentru actualizare.");
                    return;
                }
                if (cmbTipEvaluare2.SelectedValue == null)
                {
                    MessageBox.Show("SelectedValue este null!");
                }


                int disciplinaId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["DisciplinaID"].Value);

                string nume = txtNume2.Text.Trim();
                string acronim = txtAcronim2.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare2.SelectedValue.ToString());

                string query = "UPDATE Disciplina SET Nume=@nume, Acronim=@acronim, TipEvaluare=@tip WHERE DisciplinaID=@id";
                using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                {
                    cmd.Parameters.AddWithValue("@nume", nume);
                    cmd.Parameters.AddWithValue("@acronim", acronim);
                    cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                    cmd.Parameters.AddWithValue("@id", disciplinaId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Disciplina a fost actualizată cu succes.");
                    else
                        MessageBox.Show("Actualizarea nu a avut loc. Verifică datele.");
                }

                int specializareId = (int)cmbSpecializare2.SelectedValue;
                LoadDiscipline(specializareId, dataGridView2);
                ClearFields2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void btnSterge2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează o disciplină de șters.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Ești sigur că vrei să ștergi această disciplină?", "Confirmare", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                int rowIndex = dataGridView2.SelectedRows[0].Index;
                int disciplinaId = Convert.ToInt32(dataGridView2.Rows[rowIndex].Cells["DisciplinaID"].Value);

                string query = "DELETE FROM Disciplina WHERE DisciplinaID = @id";
                using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                {
                    cmd.Parameters.AddWithValue("@id", disciplinaId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Disciplina a fost ștearsă cu succes!");
                int specializareId = (int)cmbSpecializare2.SelectedValue;
                LoadDiscipline(specializareId, dataGridView2);
                ClearFields2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergere: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void btnSalveaza2_Click(object sender, EventArgs e)
        {
            try
            {
                string nume = txtNume2.Text.Trim();
                string acronim = txtAcronim2.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare2.SelectedValue.ToString());
                int specializareId = (int)cmbSpecializare2.SelectedValue;

                if (dataGridView2.SelectedRows.Count > 0)
                {
                    int disciplinaId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["DisciplinaID"].Value);

                    string updateQuery = "UPDATE Disciplina SET Nume=@nume, Acronim=@acronim, TipEvaluare=@tip WHERE DisciplinaID=@id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conectare.DeschidereConectare()))
                    {
                        cmd.Parameters.AddWithValue("@nume", nume);
                        cmd.Parameters.AddWithValue("@acronim", acronim);
                        cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                        cmd.Parameters.AddWithValue("@id", disciplinaId);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Disciplina a fost actualizată!");
                }
                else
                {
                    string insertQuery = "INSERT INTO Disciplina (Nume, Acronim, TipEvaluare, SpecializareID) VALUES (@nume, @acronim, @tip, @specId)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conectare.DeschidereConectare()))
                    {
                        cmd.Parameters.AddWithValue("@nume", nume);
                        cmd.Parameters.AddWithValue("@acronim", acronim);
                        cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                        cmd.Parameters.AddWithValue("@specId", specializareId);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Disciplina a fost adăugată!");
                }

                LoadDiscipline(specializareId, dataGridView2);
                ClearFields2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView2.SelectedRows[0];
                txtNume2.Text = row.Cells["Nume"].Value.ToString();
                txtAcronim2.Text = row.Cells["Acronim"].Value.ToString();
                cmbTipEvaluare2.SelectedValue = Convert.ToInt32(row.Cells["TipEvaluare"].Value);
            }
        }

        private void ClearFields2()
        {
            txtNume2.Text = "";
            txtAcronim2.Text = "";
            cmbTipEvaluare2.SelectedIndex = -1;
            dataGridView2.ClearSelection();
        }

    


        
       

        private void cmbTipEvaluare3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdauga3_Click(object sender, EventArgs e)
        {
            try
            {
                string nume = txtNume3.Text.Trim();
                string acronim = txtAcronim3.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare3.SelectedValue.ToString());
                int specializareId = (int)cmbSpecializare3.SelectedValue;

                string query = "INSERT INTO Disciplina (Nume, Acronim, TipEvaluare, SpecializareID) VALUES (@nume, @acronim, @tip, @specId)";
                using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                {
                    cmd.Parameters.AddWithValue("@nume", nume);
                    cmd.Parameters.AddWithValue("@acronim", acronim);
                    cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                    cmd.Parameters.AddWithValue("@specId", specializareId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Disciplina a fost adăugată!");
                LoadDiscipline(specializareId, dataGridView3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void btnActualizeaza3_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView3.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selectează o disciplină pentru actualizare.");
                    return;
                }


                int disciplinaId = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells["DisciplinaID"].Value);


                string nume = txtNume3.Text.Trim();
                string acronim = txtAcronim3.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare3.SelectedValue.ToString());

                string query = "UPDATE Disciplina SET Nume=@nume, Acronim=@acronim, TipEvaluare=@tip WHERE DisciplinaID=@id";
                using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                {
                    cmd.Parameters.AddWithValue("@nume", nume);
                    cmd.Parameters.AddWithValue("@acronim", acronim);
                    cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                    cmd.Parameters.AddWithValue("@id", disciplinaId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Disciplina a fost actualizată cu succes.");
                    else
                        MessageBox.Show("Actualizarea nu a avut loc. Verifică datele.");
                }


                int specializareId = (int)cmbSpecializare3.SelectedValue;
                LoadDiscipline(specializareId, dataGridView3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void btnSalveaza3_Click(object sender, EventArgs e)
        {
            try
            {
                string nume = txtNume3.Text.Trim();
                string acronim = txtAcronim3.Text.Trim();
                int tipEvaluare = int.Parse(cmbTipEvaluare3.SelectedValue.ToString());
                int specializareId = (int)cmbSpecializare3.SelectedValue;

                if (dataGridView3.SelectedRows.Count > 0)
                {
                    int disciplinaId = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells["DisciplinaID"].Value);

                    string updateQuery = "UPDATE Disciplina SET Nume=@nume, Acronim=@acronim, TipEvaluare=@tip WHERE DisciplinaID=@id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conectare.DeschidereConectare()))
                    {
                        cmd.Parameters.AddWithValue("@nume", nume);
                        cmd.Parameters.AddWithValue("@acronim", acronim);
                        cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                        cmd.Parameters.AddWithValue("@id", disciplinaId);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Disciplina a fost actualizată!");
                }
                else
                {
                    string insertQuery = "INSERT INTO Disciplina (Nume, Acronim, TipEvaluare, SpecializareID) VALUES (@nume, @acronim, @tip, @specId)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conectare.DeschidereConectare()))
                    {
                        cmd.Parameters.AddWithValue("@nume", nume);
                        cmd.Parameters.AddWithValue("@acronim", acronim);
                        cmd.Parameters.AddWithValue("@tip", tipEvaluare);
                        cmd.Parameters.AddWithValue("@specId", specializareId);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Disciplina a fost adăugată!");
                }

                LoadDiscipline(specializareId, dataGridView3);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message);
            }
            finally
            {
                conectare.InchidereConectare();
            }
        }

        private void btnSterge3_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView3.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selectează o disciplină de șters.");
                    return;
                }

                if (!dataGridView3.Columns.Contains("DisciplinaID"))
                {
                    MessageBox.Show("Coloana DisciplinaID nu există în DataGridView3!");
                    return;
                }

                DialogResult confirm = MessageBox.Show("Ești sigur că vrei să ștergi această disciplină?", "Confirmare", MessageBoxButtons.YesNo);
                if (confirm != DialogResult.Yes)
                    return;

                try
                {
                    int rowIndex = dataGridView3.SelectedRows[0].Index;


                    int disciplinaId = Convert.ToInt32(dataGridView3.Rows[rowIndex].Cells["DisciplinaID"].Value);

                    string query = "DELETE FROM Disciplina WHERE DisciplinaID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conectare.DeschidereConectare()))
                    {
                        cmd.Parameters.AddWithValue("@id", disciplinaId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Disciplina a fost ștearsă cu succes!");
                    int specializareId = (int)cmbSpecializare3.SelectedValue;
                    LoadDiscipline(specializareId, dataGridView3);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la ștergere: " + ex.Message);
                }
                finally
                {
                    conectare.InchidereConectare();
                }
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
  
    


