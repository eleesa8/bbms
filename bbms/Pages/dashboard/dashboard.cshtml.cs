using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace bbms.Login
{
    public class loginModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string successmsg = "";

        public string users { set; get; }
		public string patient { set; get; }
		public string donors { set; get; }
		public string transfare { set; get; }

        public string Aplus { set; get; }
        public string Apluscir { set; get; }
        public string AMinus { set; get; }
        public string AMinuscir { set; get; }
        public string Bplus { set; get; }
        public string Bpluscir { set; get; }
        public string BMinus { set; get; }
        public string BMinuscir { set; get; }

        public string ABplus { set; get; }
        public string ABpluscir { set; get; }

        public string ABMinus { set; get; }
        public string ABMinuscir { set; get; }

        public string Oplus { set; get; }
        public string Opluscir { set; get; }

        public string OMinus { set; get; }
        public string OMinuscir { set; get; }
        public void OnGet()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //users
            SqlDataAdapter sda = new SqlDataAdapter("SELECT count(*) from users", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            users = dt.Rows[0][0].ToString();

            //patients
			SqlDataAdapter sda1 = new SqlDataAdapter("SELECT count(*) from patient", con);
			DataTable dt1 = new DataTable();
			sda1.Fill(dt1);
			patient = dt1.Rows[0][0].ToString();

			//donors
			SqlDataAdapter sda2 = new SqlDataAdapter("SELECT count(*) from donors where Status ='Accepted'", con);
			DataTable dt2 = new DataTable();
			sda2.Fill(dt2);
			donors = dt2.Rows[0][0].ToString();

			//transfare
			SqlDataAdapter sda3 = new SqlDataAdapter("SELECT count(*) from transfare", con);
			DataTable dt3 = new DataTable();
			sda3.Fill(dt3);
			transfare = dt3.Rows[0][0].ToString();

            //blood stock
            SqlDataAdapter sda5 = new SqlDataAdapter("SELECT count(*) from bstock", con);
            DataTable dt5 = new DataTable();
            sda5.Fill(dt5);
            int BStock = Convert.ToInt32(dt5.Rows[0][0].ToString());

            //A+
            SqlDataAdapter sda4 = new SqlDataAdapter("SELECT Amount from bstock where BGrouo ='" + "A+" + "'    ", con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            Aplus = dt4.Rows[0][0].ToString();
            double AplusPercentage = (Convert.ToDouble(dt4.Rows[0][0].ToString())/ BStock) * 100;
            Apluscir = Convert.ToInt32(AplusPercentage).ToString();

            //A-
            SqlDataAdapter sda6 = new SqlDataAdapter("SELECT Amount from bstock where BGrouo ='" + "A-" + "'    ", con);
            DataTable dt6 = new DataTable();
            sda6.Fill(dt6);
            AMinus = dt6.Rows[0][0].ToString();
            double AminusPercentage = (Convert.ToDouble(dt6.Rows[0][0].ToString()) / BStock) * 100;
            AMinuscir = Convert.ToInt32(AminusPercentage).ToString();

            //B+
            SqlDataAdapter sda7 = new SqlDataAdapter("SELECT Amount from bstock where BGrouo ='" + "B+" + "'    ", con);
            DataTable dt7 = new DataTable();
            sda7.Fill(dt7);
            Bplus = dt7.Rows[0][0].ToString();
            double BplusPercentage = (Convert.ToDouble(dt7.Rows[0][0].ToString()) / BStock) * 100;
            Bpluscir = Convert.ToInt32(BplusPercentage).ToString();

            //B-
            SqlDataAdapter sda8 = new SqlDataAdapter("SELECT Amount from bstock where BGrouo ='" + "B-" + "'    ", con);
            DataTable dt8 = new DataTable();
            sda8.Fill(dt8);
            BMinus = dt8.Rows[0][0].ToString();
            double BMinusPercentage = (Convert.ToDouble(dt8.Rows[0][0].ToString()) / BStock) * 100;
            BMinuscir = Convert.ToInt32(BMinusPercentage).ToString();

            //AB+
            SqlDataAdapter sda9 = new SqlDataAdapter("SELECT Amount from bstock where BGrouo ='" + "AB+" + "'    ", con);
            DataTable dt9 = new DataTable();
            sda9.Fill(dt9);
            ABplus = dt9.Rows[0][0].ToString();
            double ABplusPercentage = (Convert.ToDouble(dt9.Rows[0][0].ToString()) / BStock) * 100;
            ABpluscir = Convert.ToInt32(ABplusPercentage).ToString();

            //AB-
            SqlDataAdapter sda10 = new SqlDataAdapter("SELECT Amount from bstock where BGrouo ='" + "AB-" + "'    ", con);
            DataTable dt10 = new DataTable();
            sda10.Fill(dt10);
            ABMinus = dt10.Rows[0][0].ToString();
            double ABMinusPercentage = (Convert.ToDouble(dt10.Rows[0][0].ToString()) / BStock) * 100;
            BMinuscir = Convert.ToInt32(ABMinusPercentage).ToString();

            //O+
            SqlDataAdapter sda11 = new SqlDataAdapter("SELECT Amount from bstock where BGrouo ='" + "O+" + "'    ", con);
            DataTable dt11 = new DataTable();
            sda11.Fill(dt11);
            Oplus = dt11.Rows[0][0].ToString();
            double OplusPercentage = (Convert.ToDouble(dt11.Rows[0][0].ToString()) / BStock) * 100;
            Opluscir = Convert.ToInt32(OplusPercentage).ToString();

            //O-
            SqlDataAdapter sda12 = new SqlDataAdapter("SELECT Amount from bstock where BGrouo ='" + "O-" + "'    ", con);
            DataTable dt12 = new DataTable();
            sda12.Fill(dt12);
            OMinus = dt12.Rows[0][0].ToString();
            double OMinusPercentage = (Convert.ToDouble(dt12.Rows[0][0].ToString()) / BStock) * 100;
            OMinuscir = Convert.ToInt32(OMinusPercentage).ToString();

            con.Close();
        }

        
    }
}
