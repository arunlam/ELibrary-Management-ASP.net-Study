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
    public partial class adminlogin : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["conElibrary"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("" +
                    "SELECT * FROM member_master_tbl WHERE member_id='" +
                    txtUsername.Text.Trim() + "' AND password='" + txtPwd.Text.Trim() + "';"
                    , con);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Response.Write("<scrip>alert('" + dr.GetValue(0).ToString() + "')</script>");
                    }
                }
                else
                {
                    Response.Write("<scrip>alert('Invalid user')</script>");

                }

            }
            catch (Exception ex)
            {
                Response.Write("<scrip>alert('" + ex.Message + "')</script>");

            }
        }
    }
}