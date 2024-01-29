using bbms.Pages.patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages.users
{
    public class view_usersModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string successmsg = "";
        public string updatemsg = "";
        public List<userinfo> userlist = new List<userinfo>();
        public userinfo userinfo = new userinfo();
        public void OnGet()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from users";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userinfo info = new userinfo();
                    info.id = reader.GetInt32(0);
                    info.name = reader.GetString(1);
                    info.full = reader.GetString(2);
                    info.password = reader.GetString(3);
                    info.role = reader.GetString(4);
                    userlist.Add(info);
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
                string sql = "SELECT * FROM users where UserID = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    userinfo.id = reader.GetInt32(0);
                    userinfo.name = reader.GetString(1);
                    userinfo.full = reader.GetString(2);
                    userinfo.password = reader.GetString(3);
                    userinfo.role = reader.GetString(4);
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
            string name = Request.Form["name"];
            string full = Request.Form["full"];
            string password = Request.Form["password"];
            string role = Request.Form["role"];
            try
            {
                //update
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "Update users set UserName = '" + name + "',FullName = '" + full + "',Password = '" + password + "',Role ='" + role + "' where UserID = '" + id + "'";
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                updatemsg = "updated successfully";
                // end update
            }

            catch (Exception ex)
            {
                errormsg = ex.Message;
                return;

            }

        }

        public void OnPost()
        {
            string name = Request.Form["name"];
            string full = Request.Form["full"];
            string password = Request.Form["password"];
            string role = Request.Form["role"];
            try
            {
                //sava
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "INSERT INTO users (UserName,FullName,Password,Role) VALUES('" + name + "', '" + full + "', '" + password + "', '" + role + "')";
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                successmsg = "saved successfully";
                // end save
            }

            catch (Exception ex)
            {
                errormsg = ex.Message;

            }

            
            
        }
    }

    public class userinfo
    {
        public int id;
        public string name;
        public string full;
        public string password;
        public string role;
    }
}
