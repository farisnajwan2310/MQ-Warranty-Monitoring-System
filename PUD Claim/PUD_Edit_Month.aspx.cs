using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MqWebApp2.PUD_Claim
{
    public partial class PUD_Edit_Month : System.Web.UI.Page
    {

        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownApplicationMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //This method to display month selected at the dropdownlist
            SqlCommand cmd = new SqlCommand("Select * from month_detail where month_year='" + DropDownApplicationMonth.Text + "' AND supplier_type='PUD Claim' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void SelectBtn_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            txtmonthyear.Text = GridView1.Rows[rowindex].Cells[2].Text;
            collect_date.Text = Convert.ToDateTime(GridView1.Rows[rowindex].Cells[3].Text).ToString("yyyy-MM-dd");
            claim_dateline.Text = Convert.ToDateTime(GridView1.Rows[rowindex].Cells[4].Text).ToString("yyyy-MM-dd");
            pdca_date.Text = GridView1.Rows[rowindex].Cells[5].Text;
            supplier_type.Text = GridView1.Rows[rowindex].Cells[6].Text;
            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try//Showing the date format in day/month/year setting it through cells number
            {
                e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MM/yyyy");
            }
            //If the date has null/empty string/unsuitable format, set to the original value.
            catch (FormatException)
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text;
                e.Row.Cells[4].Text = e.Row.Cells[4].Text;

            }

            //Rename Header Name
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = "Application Month";
                e.Row.Cells[3].Text = "Collection Date";
                e.Row.Cells[4].Text = "Dateline";
                e.Row.Cells[5].Text = "PDCA Date";
                e.Row.Cells[6].Text = "Supplier";
            }
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            //The method to delete record selected at the gridview2 display
            int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            String month_application = GridView1.Rows[rowindex].Cells[2].Text;
            String collectdate = Convert.ToDateTime(GridView1.Rows[rowindex].Cells[3].Text).ToString("yyyy-MM-dd");
            String suppliertype = GridView1.Rows[rowindex].Cells[6].Text;
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete FROM month_detail WHERE month_year='" + month_application + "' AND collection_date='" + collectdate + "'  AND supplier_type='" + suppliertype + "' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('The month has been deleted.')</script>");
            Response.AddHeader("REFRESH", "1;URL=PUD_Edit_Month");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //This method will execute SQL Query to enter data at form to the database
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE month_detail SET collection_date='" + collect_date.Text + "', claim_dateline='" + claim_dateline.Text + "', pdca_date='" + pdca_date.Text + "' WHERE month_year='" + txtmonthyear.Text + "' AND supplier_type='" + supplier_type.Text + "'";

            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('The month has been updated.')</script>");
            Response.AddHeader("REFRESH", "1;URL=PUD_Edit_Month");
        }

        protected void collect_date_TextChanged(object sender, EventArgs e)
        {
            if (collect_date.Text != string.Empty)
            {
                claim_dateline.Text = DateTime.Parse(collect_date.Text).AddDays(60).ToString("yyyy-MM-dd");
            }
        }
    }
}