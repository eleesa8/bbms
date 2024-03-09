using bbms.Pages.patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages.donors
{
    public class view_donorsModel : PageModel
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
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from donors";
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

                    donorinfo.id = reader.GetInt32(0);
                    donorinfo.name = reader.GetString(1);
                    donorinfo.age = reader.GetString(2);
                    donorinfo.gender = reader.GetString(3);
                    donorinfo.address = reader.GetString(4);
                    donorinfo.contact = reader.GetString(5);
                    donorinfo.bgroup = reader.GetString(6);
                    donorinfo.date= reader.GetString(7);
                    donorinfo.status = reader.GetString(8);
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
            string age = Request.Form["age"];
            string gender = Request.Form["gender"];
            string address = Request.Form["address"];
            string phone = Request.Form["phone"];
            string bgroup = Request.Form["bgroup"];
            string date = Request.Form["date"];

            try
            {
                //update
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "Update donors set DonorName = '" + name + "',Gender = '" + gender + "',Address = '" + address + "',Phone ='" + phone + "', Age = '" + age + "', BGroup = '" + bgroup + "', Date = '"+date+"' where DonorID = '" + id + "'";
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

            
            Response.Redirect("/donors/view_donors");

        }

        public void OnPost()
        {
            string name = Request.Form["name"];
            string age = Request.Form["age"];
            string gender = Request.Form["gender"];
            string address = Request.Form["address"];
            string phone = Request.Form["phone"];
            string bgroup = Request.Form["bgroup"];
            string date = Request.Form["date"];
            try
            {
                //sava
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "INSERT INTO donors (DonorName,Age,Gender,Address,Phone,BGroup,Date) VALUES('" + name + "', '" + age + "', '" + gender + "', '" + address + "', '" + phone + "', '" + bgroup + "', '" + date + "')";
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                // end save
            }

            catch (Exception ex)
            {
                errormsg = ex.Message;

            }

           
            Response.Redirect("/donors/view_donors");

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
        public string status;
    }
}
