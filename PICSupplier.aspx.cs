using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MqWebApp2
{
    public partial class PICSupplier : System.Web.UI.Page
    {
        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (supplier_code.Text != string.Empty)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE supplier_details SET pic1_name='" + pic1name.Text + "', pic1_tel='" + pic1tel.Text + "', pic1_email='" + pic1email.Text + "', pic2_name='" + pic2name.Text + "', pic2_tel='" + pic2tel.Text + "', pic2_email='" + pic2email.Text + "' WHERE supplier_code='" + supplier_code.Text + "' ";

                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Write("<script>alert('The supplier person in charge has been updated.')</script>");
                    Response.AddHeader("REFRESH", "1;URL=PICSupplier");
                }
                else
                {
                    Response.Write("<script>alert('The supplier code is not selected. Please try again')</script>");
                    Response.AddHeader("REFRESH", "1;URL=PICSupplier");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Data Error. Please try again')</script>");
                Response.AddHeader("REFRESH", "1;URL=PICSupplier");
            }




        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            supplier_code.Text = DropDownList1.SelectedValue;

            con.Open();
            //get name based on the selected dropdownlist value
            SqlCommand getSuppName = new SqlCommand("SELECT supplier_name from supplier_details WHERE supplier_code='"+DropDownList1.SelectedValue+"' ", con);
            String SuppName = Convert.ToString(getSuppName.ExecuteScalar());
            con.Close();
            supplier_name.Text = SuppName;

            //Pic1Name
            try
            {
                con.Open();
                //get pic1name based on the selected dropdownlist value
                SqlCommand getPic1name = new SqlCommand("SELECT pic1_name from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic1Name = Convert.ToString(getPic1name.ExecuteScalar());
                con.Close();
                pic1name.Text = Pic1Name;
            }
            catch (Exception)
            {
                pic1name.Text = "-";
            }

            //Pic1Telephone
            try
            {
                con.Open();
                //get pic1tel based on the selected dropdownlist value
                SqlCommand getPic1Tel = new SqlCommand("SELECT pic1_tel from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic1Tel = Convert.ToString(getPic1Tel.ExecuteScalar());
                con.Close();
                pic1tel.Text = Pic1Tel;
            }
            catch (Exception)
            {
                pic1tel.Text = "-";
            }

            //Pic1Email
            try
            {
                con.Open();
                //get pic1tel based on the selected dropdownlist value
                SqlCommand getPic1Email = new SqlCommand("SELECT pic1_email from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic1Email = Convert.ToString(getPic1Email.ExecuteScalar());
                con.Close();
                pic1email.Text = Pic1Email;
            }
            catch (Exception)
            {
                pic1email.Text = "-";
            }

            //Pic2Name
            try
            {
                con.Open();
                //get pic2name based on the selected dropdownlist value
                SqlCommand getPic2name = new SqlCommand("SELECT pic2_name from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic2Name = Convert.ToString(getPic2name.ExecuteScalar());
                con.Close();
                pic2name.Text = Pic2Name;
            }
            catch (Exception)
            {
                pic2name.Text = "-";
            }

            //Pic2Telephone
            try
            {
                con.Open();
                //get pic2tel based on the selected dropdownlist value
                SqlCommand getPic2Tel = new SqlCommand("SELECT pic2_tel from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic2Tel = Convert.ToString(getPic2Tel.ExecuteScalar());
                con.Close();
                pic2tel.Text = Pic2Tel;
            }
            catch (Exception)
            {
                pic2tel.Text = "-";
            }

            //Pic2Email
            try
            {
                con.Open();
                //get pic2tel based on the selected dropdownlist value
                SqlCommand getPic2Email = new SqlCommand("SELECT pic2_email from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic2Email = Convert.ToString(getPic2Email.ExecuteScalar());
                con.Close();
                pic2email.Text = Pic2Email;
            }
            catch (Exception)
            {
                pic2email.Text = "-";
            }

            //Pic3Name
            try
            {
                con.Open();
                //get pic3name based on the selected dropdownlist value
                SqlCommand getPic3name = new SqlCommand("SELECT pic3_name from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic3Name = Convert.ToString(getPic3name.ExecuteScalar());
                con.Close();
                pic3name.Text = Pic3Name;
            }
            catch (Exception)
            {
                pic3name.Text = "-";
            }

            //Pic3Telephone
            try
            {
                con.Open();
                //get pic3tel based on the selected dropdownlist value
                SqlCommand getPic3Tel = new SqlCommand("SELECT pic3_tel from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic3Tel = Convert.ToString(getPic3Tel.ExecuteScalar());
                con.Close();
                pic3tel.Text = Pic3Tel;
            }
            catch (Exception)
            {
                pic3tel.Text = "-";
            }

            //Pic3Email
            try
            {
                con.Open();
                //get pic3tel based on the selected dropdownlist value
                SqlCommand getPic3Email = new SqlCommand("SELECT pic3_email from supplier_details WHERE supplier_code='" + DropDownList1.SelectedValue + "' ", con);
                String Pic3Email = Convert.ToString(getPic3Email.ExecuteScalar());
                con.Close();
                pic3email.Text = Pic3Email;
            }
            catch (Exception)
            {
                pic3email.Text = "-";
            }



        }

        
    }
}