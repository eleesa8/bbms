using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages.users
{
    public class changepassModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string successmsg = "";
        public void OnGet()
        {
        }
      
        public void OnPost()
        {
            string name = Request.Form["username"];
            string old_pass = Request.Form["old"];
            string newpass = Request.Form["new"];
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = "SELECT * FROM users where UserName = '" + name + "' and Password = '" + old_pass + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                try
                {
                    //update password
                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                    string qry = "UPDATE users SET Password = '" + newpass + "' WHERE UserName = '" + name + "'";
                    SqlCommand sqlCommand = new SqlCommand(qry, conn);
                    sqlCommand.ExecuteNonQuery();
                    Response.Redirect("/Index");
                }
                catch(Exception ex)
                {
                    errormsg = ex.Message;
                    return;
                }
            }

            else
            {
                errormsg = "Old Password Incorrect";
                return;
            }
        }
    }
}
