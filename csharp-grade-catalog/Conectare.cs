using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CatalogDeNoteApp
{
    class Conectare
    {
        SqlConnection conectare;


        public Conectare()
        {
            conectare = new SqlConnection("Data Source=ANDREEA\\SQLEXPRESS ; Initial Catalog=Catalog ; Integrated Security=True");
        }

        public SqlConnection DeschidereConectare()
        {
            try
            {
                conectare.Open();
            }
            catch (Exception)
            {

            }
            return conectare;
        }

        public void InchidereConectare()
        {
            try
            {
                conectare.Close();
            }
            catch (Exception)
            {

            }
            
        }

    }

}
