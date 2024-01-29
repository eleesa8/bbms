using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages
{
    public class IndexModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string successmsg = "";
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            string name = Request.Form["uname"];
            string password = Request.Form["Password"];
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = "SELECT * FROM users where UserName = '" + name + "' and Password = '" + password + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //saving user info in session
                HttpContext.Session.SetString("user", reader.GetString(1));
                HttpContext.Session.SetString("fullname", reader.GetString(2));
                Response.Redirect("/dashboard/dashboard");

            }

            else
            {
                errormsg = "Login Information Incorrect";
            }
        }
    }
}
