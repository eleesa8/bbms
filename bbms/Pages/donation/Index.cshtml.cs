using bbms.Pages.BStock;
using bbms.Pages.transfare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace bbms.Pages.donation
{
    public class IndexModel : PageModel
    {

        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string datamsg = "";
        public string successmsg = "";

		public List<binfo> blist = new List<binfo>();
        public List<dinfo> dlist = new List<dinfo>();
        public dinfo donate = new dinfo();

        int oldstock;
        public void OnGet()
        {
			try
			{
				SqlConnection con = new SqlConnection(connectionString);
				con.Open();
				string sql = "select * from bstock";
				SqlCommand cmd = new SqlCommand(sql, con);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					binfo info = new binfo();
					info.id = reader.GetInt32(0);
					info.bgroup = reader.GetString(1);
					info.amount = reader.GetString(2);
					blist.Add(info);
				}
			}
			catch (Exception ex)
			{
				//errormsg = ex.Message;
				return;
			}

            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from donors where Status = 'Accepted'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dinfo info = new dinfo();
                    info.id = reader.GetInt32(0);
                    info.name = reader.GetString(1);
                    info.bgroup = reader.GetString(6);
                    dlist.Add(info);
                }
            }
            catch (Exception ex)
            {
                //errormsg = ex.Message;
                return;
            }

            try
            {

                string id = Request.Query["id"];

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "SELECT * FROM donors where DonorID = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                   donate.id = reader.GetInt32(0);
                   donate.name = reader.GetString(1);
                   donate.bgroup = reader.GetString(6);
                }

            }
            catch (Exception ex)
            {
                //errormsg = ex.Message;
            }

            
        }
        
        public void OnPostDonate(string bgroup)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM bstock where BGrouo = '" + bgroup + "'";
            SqlCommand cmd1 = new SqlCommand(query, connection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                oldstock = Convert.ToInt32(dr["Amount"].ToString());
            }
            connection.Close();

            try
            {
                string blodgroup = Request.Form["bgroup"];
                int stock = oldstock +1;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                string sql = "UPDATE bstock SET Amount = '" + stock + "' where BGrouo = '" + blodgroup + "' ";
                SqlCommand command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                datamsg = ex.Message;
            }
        }
    }

	public class binfo
	{
		public int id;
		public string bgroup;
		public string amount;

	}

    public class dinfo
    {
        public int id;
        public string name;
        public string bgroup;

    }
}
