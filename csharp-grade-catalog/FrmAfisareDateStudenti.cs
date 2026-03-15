using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatalogDeNoteApp
{
    public partial class FrmAfisareDateStudenti : MaterialSkin.Controls.MaterialForm
    {

        Conectare con = new Conectare();
        SqlDataAdapter dtAp;
        DataTable dt;
        public static int studentID;


        public FrmAfisareDateStudenti()
        {
            InitializeComponent();
        }

        private void FrmAfisareDateStudenti_Load(object sender, EventArgs e)
        {
            dtAp = new SqlDataAdapter(
                "SELECT Studenti.studentiID, Studenti.nume, Studenti.prenume, Studenti.sex, " +
                "Studenti.adresa, Studenti.telefon,Studenti.email, Studenti.GrupaID, Anul.numeAn, Judet.numeJudet, Oras.numeOras " +
                "FROM Studenti " +
                "INNER JOIN Anul ON Studenti.AnID = Anul.anID " +
                "INNER JOIN Judet ON Studenti.JudetID = Judet.judetID " +
                "INNER JOIN Oras ON Studenti.OrasID = Oras.orasID",
                con.DeschidereConectare());

            dt = new DataTable();
            dtAp.Fill(dt);

            GDAfisareStudenti.DataSource = dt;

            con.InchidereConectare();
        }

        private void GDAfisareStudenti_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FormStudenti incarcareDateStudenti = new FormStudenti();

            studentID = Convert.ToInt32(GDAfisareStudenti.Rows[e.RowIndex].Cells[0].Value.ToString());
            incarcareDateStudenti.txtNume.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[1].Value.ToString());
            incarcareDateStudenti.txtPrenume.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[2].Value.ToString());
            incarcareDateStudenti.rdFeminin.Checked = true;
            incarcareDateStudenti.rdMasculin.Checked = false;
            if (GDAfisareStudenti.Rows[e.RowIndex].Cells[3].Value.ToString() == "Feminin")
            {
                incarcareDateStudenti.rdMasculin.Checked = false;
                incarcareDateStudenti.rdFeminin.Checked = true;
            }
            incarcareDateStudenti.txtAdresa.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[4].Value.ToString());
            incarcareDateStudenti.txtTelefon.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[5].Value.ToString());
            incarcareDateStudenti.txtEmail.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[6].Value.ToString());
            incarcareDateStudenti.cmbGrupa.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[7].Value.ToString());
            incarcareDateStudenti.cmbClasa.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[8].Value.ToString());
            incarcareDateStudenti.cmbJudet.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[9].Value.ToString());
            incarcareDateStudenti.cmbOras.Text = (GDAfisareStudenti.Rows[e.RowIndex].Cells[10].Value.ToString());

            incarcareDateStudenti.Show();
            incarcareDateStudenti.btnActualizare.Enabled = true;
            incarcareDateStudenti.btnStergere.Enabled = true;
        }
         public void CautareDupaPrenume(string prenume)
        {
            string cautare = "  select* from studenti where Prenume like '%" + prenume + "%'";
            SqlCommand cmd = new SqlCommand(cautare, con.DeschidereConectare());
            dtAp = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dtAp.Fill(dt);
            GDAfisareStudenti.DataSource = dt;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CautareDupaPrenume(txtCautare.Text);


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
