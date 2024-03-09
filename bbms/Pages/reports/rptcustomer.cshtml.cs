using bbms.Pages.patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages.reports
{
    public class rptcustomerModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string successmsg = "";
        public List<patientinfo> patientlist = new List<patientinfo>();
        public patientinfo patientinfo = new patientinfo();
        public void OnGet()
        {
            try
            {
                string id = Request.Query["id"];
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from patient where PatID = '"+id+"'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patientinfo info = new patientinfo();
                    info.id = reader.GetInt32(0);
                    info.name = reader.GetString(1);
                    info.age = reader.GetString(2);
                    info.gender = reader.GetString(3);
                    info.address = reader.GetString(4);
                    info.contact = reader.GetString(5);
                    info.bgroup = reader.GetString(6);
                    patientlist.Add(info);
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
