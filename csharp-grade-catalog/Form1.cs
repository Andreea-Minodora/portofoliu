using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatalogDeNoteApp
{
    public partial class FrmCatalog : MaterialSkin.Controls.MaterialForm
    {
        public FrmCatalog()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDiscipline_Click(object sender, EventArgs e)
        {
            FormDiscipline formDisc = new FormDiscipline();
            formDisc.Show();
        }

        private void btnStudenti_Click(object sender, EventArgs e)
        {
            FormStudenti studenti = new FormStudenti();

            studenti.Show();

        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnCatalog_Click(object sender, EventArgs e)
        {
          
            FormCatalogNote catalog = new FormCatalogNote();
            catalog.Show(); // deschide catalogul non-modal (poți lucra cu ambele formulare)
                            // sau, dacă vrei să blochezi formularul principal cât catalogul e deschis:
                            // catalog.ShowDialog();
        }
    }
    }

