using bbms.Pages.patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages.BStock
{
    public class view_bloodModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string successmsg = "";
        public List<bloodinfo> bloodlist = new List<bloodinfo>();
        public bloodinfo bloodinfo = new bloodinfo();
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
                    bloodinfo info = new bloodinfo();
                    info.id = reader.GetInt32(0);
                    info.btype = reader.GetString(1);
                    info.amount = reader.GetString(2);
                    bloodlist.Add(info);
                }
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return;
            }

            try
            {

                string id = Request.Query["id"];

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "SELECT * FROM bstock where BloodID = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    bloodinfo.id = reader.GetInt32(0);
                    bloodinfo.btype = reader.GetString(1);
                    bloodinfo.amount = reader.GetString(2);
                }

            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
            }
        }



        public void OnPostUpdate()
        {
            string id = Request.Form["id"];
            string bgroup = Request.Form["bgroup"];
            string amount = Request.Form["amount"];
            try
            {
                //update
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "Update bstock set BGrouo = '" + bgroup + "', Amount = '" + amount+ "' where BloodID = '" + id + "'";
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                // end update
            }

            catch (Exception ex)
            {
                errormsg = ex.Message;
                return;

            }

            successmsg = "updated successfully";
            Response.Redirect("/BStock/view_blood");

        }

        public void OnPost()
        {
            string bgroup = Request.Form["bgroup"];
            string amount = Request.Form["amount"];
            try
            {
                //sava
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "INSERT INTO bstock (BGrouo,Amount) VALUES('" + bgroup + "', '" + amount + "')";
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                // end save
            }

            catch (Exception ex)
            {
                errormsg = ex.Message;

            }

            
            Response.Redirect("/BStock/view_blood");

        }

    }

    public class bloodinfo
    {
        public int id;
        public string btype;
        public string amount;
    }
}
