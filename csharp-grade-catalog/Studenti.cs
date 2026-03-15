using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;

namespace CatalogDeNoteApp
{
    public partial class FormStudenti : MaterialSkin.Controls.MaterialForm

    {
        SqlDataAdapter dataAdpt;
        DataTable dt;
        Conectare conectare = new Conectare();

        public FormStudenti()
        {
            InitializeComponent();
            btnActualizare.Enabled = false;
            btnStergere.Enabled = false;
            Anul();
            IncarcaGrupe();
        }
        public void AfisareJudetelsd()
        {

        }
        private void Studenti_Load(object sender, EventArgs e)
        {
            dataAdpt = new SqlDataAdapter("SELECT * FROM Judet", conectare.DeschidereConectare());
            dt = new DataTable();
            dt.Clear();
            dataAdpt.Fill(dt);
            cmbJudet.DataSource = dt;
            cmbJudet.DisplayMember = "numeJudet";
            cmbJudet.ValueMember = "judetID";

            conectare.InchidereConectare();

        }
        public void IncarcaGrupe()
        {
            try
            {
                string query = "SELECT GrupaID, numeGrupa FROM Grupa";
                SqlDataAdapter adpt = new SqlDataAdapter(query, conectare.DeschidereConectare());
                DataTable dt = new DataTable();
                adpt.Fill(dt);

                cmbGrupa.DataSource = dt;
                cmbGrupa.DisplayMember = "numeGrupa";
                cmbGrupa.ValueMember = "GrupaID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea grupelor: " + ex.Message);
            }
            conectare.InchidereConectare();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cmbJudet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int valoare;

            try
            {
                int.TryParse(cmbJudet.SelectedValue.ToString(), out valoare);
                dataAdpt = new SqlDataAdapter("SELECT * FROM Oras WHERE JudetID = " + valoare + "", conectare.DeschidereConectare());
                dt = new DataTable();
                dt.Clear();
                dataAdpt.Fill(dt);


                cmbOras.DataSource = dt;
                cmbOras.DisplayMember = "numeOras";
                cmbOras.ValueMember = "orasID";
            }
            catch (Exception)
            {

            }
            conectare.InchidereConectare();
        }
        public void Anul()
        {
            try
            {
                dataAdpt = new SqlDataAdapter("SELECT * FROM Anul", conectare.DeschidereConectare());
                dt = new DataTable();
                dt.Clear();
                dataAdpt.Fill(dt);


                cmbClasa.DataSource = dt;
                cmbClasa.DisplayMember = "numeAn";
                cmbClasa.ValueMember = "anID";
            }

            catch (Exception)
            {

            }
            conectare.InchidereConectare();
        }

        private void btnSalvare_Click(object sender, EventArgs e)
        {
            int AnulID = Convert.ToInt32(cmbClasa.SelectedValue);
            int JudetID = Convert.ToInt32(cmbJudet.SelectedValue);
            int OrasID = Convert.ToInt32(cmbOras.SelectedValue);

            if (cmbGrupa.SelectedValue == null)
            {
                MessageBox.Show("Selectați o grupă!");
                return;
            }
            int GrupaID = Convert.ToInt32(cmbGrupa.SelectedValue);

            try
            {
                string sex = rdMasculin.Checked ? "Masculin" : "Feminin";

                if (!string.IsNullOrWhiteSpace(txtNume.Text) &&
                    !string.IsNullOrWhiteSpace(txtPrenume.Text) &&
                    !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                    !string.IsNullOrWhiteSpace(txtTelefon.Text) &&
                    !string.IsNullOrWhiteSpace(txtAdresa.Text) &&
                    cmbJudet.SelectedIndex != -1 &&
                    cmbOras.SelectedIndex != -1)
                {
                    using (SqlConnection conn = conectare.DeschidereConectare())
                    {
                        string query = "INSERT INTO Studenti (Sex, Nume, Prenume, Email, Telefon, Adresa, AnID, JudetID, OrasID, GrupaID) " +
                                       "VALUES (@Sex, @Nume, @Prenume, @Email, @Telefon, @Adresa, @AnID, @JudetID, @OrasID, @GrupaID)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Sex", sex);
                            cmd.Parameters.AddWithValue("@Nume", txtNume.Text.Trim());
                            cmd.Parameters.AddWithValue("@Prenume", txtPrenume.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@Telefon", txtTelefon.Text.Trim());
                            cmd.Parameters.AddWithValue("@Adresa", txtAdresa.Text.Trim());
                            cmd.Parameters.AddWithValue("@AnID", AnulID);
                            cmd.Parameters.AddWithValue("@JudetID", JudetID);
                            cmd.Parameters.AddWithValue("@OrasID", OrasID);
                            cmd.Parameters.AddWithValue("@GrupaID", GrupaID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Student adăugat cu succes!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Toate câmpurile sunt obligatorii.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
            }
            conectare.InchidereConectare();
        }

        private void cmbGrupa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdaugare_Click(object sender, EventArgs e)
        {
            cmbClasa.Text = "";
            txtNume.Clear();
            txtPrenume.Clear();
            txtAdresa.Clear();
            txtTelefon.Clear();
            txtEmail.Clear();
            cmbJudet.Text = " ";
            cmbOras.Text = " ";
            cmbGrupa.Text = " ";


        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            FrmAfisareDateStudenti studenti = new FrmAfisareDateStudenti();
            studenti.Show();
            Hide();
        }

        private void btnActualizare_Click(object sender, EventArgs e)
        {

            string sex = "";
            if (rdMasculin.Checked)
            {
                sex = "Masculin";
            }
            else
            {
                sex = "Feminin";
            }

            try
            {
                int judetID = Convert.ToInt32(cmbJudet.SelectedValue);
                int orasID = Convert.ToInt32(cmbOras.SelectedValue);
                int anID = Convert.ToInt32(cmbClasa.SelectedValue);
                int GrupaID = Convert.ToInt32(cmbGrupa.SelectedValue);

                if (txtNume.Text != "" && txtPrenume.Text != "" && txtAdresa.Text != ""
                    && txtTelefon.Text != "" && txtEmail.Text != "" && cmbJudet.Text != "" && cmbOras.Text != "")
                {
                    SqlConnection conn = conectare.DeschidereConectare(); // 🔥 Deschide conexiunea
                    SqlCommand cmd = new SqlCommand("UPDATE Studenti SET Sex = '" + sex + "', Nume = '" + txtNume.Text +
                        "', Prenume = '" + txtPrenume.Text + "', Adresa = '" + txtAdresa.Text + "', Telefon = '" + txtTelefon.Text +
                        "', Email = '" + txtEmail.Text + "', AnID = '" + anID + "', JudetID = '" + judetID +
                        "', OrasID = '" + orasID + "', GrupaID = '" + GrupaID +
                        "' WHERE StudentiID = '" + FrmAfisareDateStudenti.studentID + "'", conn);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Datele au fost actualizate cu succes");
                    conectare.InchidereConectare(); // ✅ Închide conexiunea aici
                }
                else
                {
                    MessageBox.Show("Te rog completează toate câmpurile.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message);
                conectare.InchidereConectare();
            }
        }

        private void btnStergere_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete from studenti where studentiID ='" + FrmAfisareDateStudenti.studentID + " '", conectare.DeschidereConectare());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Datele au fost sterse cu succes");
            }
            catch(Exception)
            {

            }
            conectare.InchidereConectare();
        }

        private void cmbClasa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}            