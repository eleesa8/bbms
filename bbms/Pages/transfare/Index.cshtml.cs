using bbms.Pages.patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace bbms.Pages.transfare
{
    public class IndexModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string successmsg = "";
        public List<pinfo> plist = new List<pinfo>();
        public pinfo pinfo = new pinfo();

        int newstock;
        public void OnGet()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from patient";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pinfo info = new pinfo();
                    info.id = reader.GetInt32(0);
                    info.name = reader.GetString(1);
                    info.age = reader.GetString(2);
                    info.gender = reader.GetString(3);
                    info.bgroup = reader.GetString(6);
                    plist.Add(info);
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
                string sql = "SELECT * FROM patient where PatID = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    pinfo.id = reader.GetInt32(0);
                    pinfo.name = reader.GetString(1);
                    pinfo.age = reader.GetString(2);
                    pinfo.gender = reader.GetString(3);
                    pinfo.bgroup = reader.GetString(6);
                }

            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
            }
        }

        public void OnPostTransfare(string bgroup)
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
                newstock = Convert.ToInt32(dr["Amount"].ToString());
            }
            connection.Close();

            try
            {
                string id = Request.Form["id"];
                string name = Request.Form["name"];
                string age = Request.Form["age"];
                string gender = Request.Form["gender"];
                string blodgroup = Request.Form["bgroup"];
                string blodtran = Request.Form["blodtran"];
                string date = Request.Form["date"];
                int stock = newstock - 1;

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                if(stock > 0)
                {
                    string sql = "UPDATE bstock SET Amount = '" + stock + "' where BGrouo = '" + blodtran + "' ";
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.ExecuteNonQuery();


                    string qry = "INSERT INTO transfare (CID, CName, Gender, Age, BGroup ,Btansfare, Date) VALUES('" + id + "', '" + name + "', '" + gender + "', '" + age + "', '" + blodgroup + "', '" + blodtran + "', '" + date + "')";
                    SqlCommand sqlCommand = new SqlCommand(qry, conn);
                    sqlCommand.ExecuteNonQuery();
                    successmsg = "Blood transfusion done successfully";
                }
                else
                {
                    errormsg = "We are sorry the stock not available please choose onther one that matching";
                }

               
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
            }
        }
    }

    public class pinfo
    {
        public int id;
        public string name;
        public string age;
        public string gender;
        public string bgroup;
    }
}
