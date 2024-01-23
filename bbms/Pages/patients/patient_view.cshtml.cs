using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bbms.Pages.patients
{
    public class patient_viewModel : PageModel
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
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from patient";
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

                    patientinfo.id = reader.GetInt32(0);
                    patientinfo.name = reader.GetString(1);
                    patientinfo.age = reader.GetString(2);
                    patientinfo.gender = reader.GetString(3);
                    patientinfo.address = reader.GetString(4);
                    patientinfo.contact = reader.GetString(5);
                    patientinfo.bgroup = reader.GetString(6);
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
            try
            {
                //update
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "Update patient set PatName = '" + name + "',Gender = '" + gender + "',Address = '" + address + "',Phone ='" + phone + "', Age = '"+age+"', BGroup = '"+bgroup+"' where PatID = '" + id + "'";
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

            successmsg = "updated successfully";
            Response.Redirect("/patients/patient_view");
        
        }
        
        public void OnPost()
        {
            string name = Request.Form["name"];
            string age = Request.Form["age"];
            string gender = Request.Form["gender"];
            string address = Request.Form["address"];
            string phone = Request.Form["phone"];
            string bgroup = Request.Form["bgroup"];
            try
            {
                //sava
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string sql = "INSERT INTO patient (PatName,Age,Gender,Address,Phone,BGroup) VALUES('" + name + "', '" + age + "', '" + gender + "', '" + address + "', '" + phone + "', '" + bgroup + "')";
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                // end save
            }

            catch (Exception ex)
            {
                errormsg = ex.Message;

            }

            successmsg = "saved successfully";
            Response.Redirect("/patients/patient_view");

        }


    }
    public class patientinfo
    {
        public int id;
        public string name;
        public string age;
        public string gender;
        public string address;
        public string contact;
        public string bgroup;
    }
}
