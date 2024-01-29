using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace bbms.Pages.transfare
{
    public class view_transfareModel : PageModel
    {
        string connectionString = "Data Source=DESKTOP-9DNNVCS\\SQLEXPRESS;Initial Catalog=blooddb;Integrated Security=True";
        public string datamsg = "";
        public string successmsg = "";


        public List<donorsinfo> donorslist = new List<donorsinfo>();
        public donorsinfo donorsinfo = new donorsinfo();




        public void OnGet()
        {


            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from donors";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    donorsinfo info = new donorsinfo();
                    info.id = reader.GetInt32(0);
                    info.name = reader.GetString(1);
                    info.age = reader.GetString(2);
                    info.gender = reader.GetString(3);
                    
                    donorslist.Add(info);
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

                    donorsinfo.id = reader.GetInt32(0);
                    donorsinfo.name = reader.GetString(1);
                    donorsinfo.age = reader.GetString(2);
                    donorsinfo.gender = reader.GetString(3);

                }

            }
            catch (Exception ex)
            {
                //errormsg = ex.Message;
            }


        }

        public void OnPostTest()
        {
            string id = Request.Form["id"];
            string name = Request.Form["name"];
            string dage = Request.Form["age"];
            string gender = Request.Form["gender"];
            string weight = Request.Form["weight"];
            string amount = Request.Form["amount"];
            string hbv = Request.Form["hbv"];
            string hcv = Request.Form["hcv"];
            string rvi = Request.Form["rvi"];
            string cbc = Request.Form["cbc"];
            string syphilis = Request.Form["syphilis"];
            string malaria = Request.Form["malaria"];
            string Accepted = Request.Form["Accepted"];
            string Rejected = Request.Form["Rejected"];


            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "SELECT * FROM inves WHERE Age = '" + dage + "' UNION SELECT * FROM inves WHERE Weight = '"+weight+ "'  UNION SELECT * FROM inves WHERE Amount = '"+amount+ "'  UNION SELECT * FROM inves WHERE HBV = '"+hbv+ "'  UNION SELECT * FROM inves WHERE HCV = '"+hcv+ "'  UNION SELECT * FROM inves WHERE RVI = '"+rvi+ "'  UNION SELECT * FROM inves WHERE CBC = '"+cbc+ "'  UNION SELECT * FROM inves WHERE Syphilis = '"+syphilis+ "'  UNION SELECT * FROM inves WHERE Malaria = '"+malaria+"'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    SqlConnection sqlconn = new SqlConnection(connectionString);
                    sqlconn.Open();
                    string qr = "UPDATE donors SET Status = '" + Rejected + "' WHERE DonorID = '" + id + "'";
                    SqlCommand com = new SqlCommand(qr, sqlconn);
                    com.ExecuteNonQuery();
                    datamsg = "Sorry You cannot Donate!";
                }
                else
                {

                    SqlConnection sqlcon = new SqlConnection(connectionString);
                    sqlcon.Open();
                    string qry = "UPDATE donors SET Status = '" + Accepted + "' WHERE DonorID = '" + id + "'";
                    SqlCommand comm = new SqlCommand(qry, sqlcon);
                    comm.ExecuteNonQuery();

                    successmsg = "Thank u for Donation!";


                }
                

               
            }
            catch (Exception ex)
            {
               
            }
        }

    }


    public class donorsinfo
    {
        public int id;
        public string name;
        public string age;
        public string gender;
        public string status;

    }
}
