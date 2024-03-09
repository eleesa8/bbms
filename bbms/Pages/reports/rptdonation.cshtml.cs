using bbms.Pages.donation;
using bbms.Pages.donors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages.reports
{
    public class rptdonationModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string savemsg = "";
        public string updatemsg = "";
        public string deletemsg = "";
        public List<donorinfo> donorlist = new List<donorinfo>();
        public donorinfo donorinfo = new donorinfo();
        public void OnGet()
        {
            try
            {
                string id = Request.Query["id"];
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from donors where DonorID = '"+id+"'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    donorinfo info = new donorinfo();
                    info.id = reader.GetInt32(0);
                    info.name = reader.GetString(1);
                    info.age = reader.GetString(2);
                    info.gender = reader.GetString(3);
                    info.address = reader.GetString(4);
                    info.contact = reader.GetString(5);
                    info.bgroup = reader.GetString(6);
                    info.date = reader.GetString(7);
                    info.status = reader.GetString(8);
                    donorlist.Add(info);
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
