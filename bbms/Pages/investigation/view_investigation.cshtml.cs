using bbms.Pages.patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace bbms.Pages.transfare
{
    public class view_transfareModel : PageModel
    {
        //string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string errormsg = "";
        public string successmsg = "";
        public List<donorinfo> DonorList { get; set; }

        public int SelectedDonorId { get; set; }
        public string SelectedDonorName { get; set; }
        public string SelectedDonorAge { get; set; }
        public string SelectedDonorGender { get; set; }



       public IActionResult OnGet()
        {
            string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
            List<donorinfo> donorlist = new List<donorinfo>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT DonorID , DonorName , Age , Gender FROM donors";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        donorinfo info = new donorinfo();
                        info.id = reader.GetInt32(0);
                        info.name = reader.GetString(1);
						info.age = reader.GetString(2);
						info.gender = reader.GetString(3);

						donorlist.Add(info);
                    }
                }

                DonorList = donorlist;
                return Page();
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return Page();
            }


        }

        }



       
    public class donorinfo
    {
        public int id;
        public string name;
        public string age;
        public string gender;
        public string address;
        public string contact;
        public string bgroup;
        public string date;
    }
}
