using bbms.Pages.BStock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages.reports
{
    public class rptbloodstockModel : PageModel
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
                string id = Request.Query["id"];
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from bstock where BloodID = '" + id + "' ";
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
        }
    }
}
