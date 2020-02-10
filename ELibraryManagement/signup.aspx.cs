using System;

using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibraryManagement
{
    public partial class signup : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["conElibrary"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (_checkMemberExist())
            {
                Response.Write("<script>alert('Your username is already registered. Try another username.')</script>");
            }
            else _userSignup();


        }



        bool _checkMemberExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl where member_id='" + txtUsername.Text.Trim() + "';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return (dt.Rows.Count > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return false;
            }
        }

        void _userSignup()
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("" +
                    "INSERT INTO member_master_tbl(full_name, dob, contract_no, email, state, city, pincode, full_adrress, member_id, password, account_status) " +
                    "VALUES(@fname, @dob, @contract_no, @email, @state, @city, @pincode, @full_adrress, @member_id, @password, @account_status)", con);
                cmd.Parameters.AddWithValue("@fname", txtFullname.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", dtpDob.Text.Trim());
                cmd.Parameters.AddWithValue("@contract_no", txtContactNo.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@state", dlState.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", txtPincode.Text.Trim());
                cmd.Parameters.AddWithValue("@full_adrress", txtAddr.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPwd.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>" +
                    "alert('Sign Up successful. You can login now')" +
                    "</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}