using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MqWebApp2
{
    public partial class Add_Month_Record : System.Web.UI.Page
    {
        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);

        //OracleConnection con = new OracleConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }


        protected void AddMonthRecord(object sender, EventArgs e)
        {
            if(month_year.Text != string.Empty && collect_date.Text != string.Empty && pdca_date.Text != string.Empty && claim_dateline.Text != string.Empty)
            {

                string monthformat = Convert.ToDateTime(month_year.Text).ToString("MMM-yyyy");
                string pdca = Convert.ToDateTime(pdca_date.Text).ToString("MMM-yyyy");
                //This method to add data filled in the form into the database
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into month_detail(month_year,collection_date,claim_dateline,pdca_date,supplier_type)" + "values('" + monthformat + "','" + collect_date.Text + "','" + claim_dateline.Text + "','" + pdca + "','" + supplier_type1.Text + "')";
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('The month has been added.')</script>");
                    Response.AddHeader("REFRESH", "1;URL=Add_Month_Record");
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Data Error/Duplicated')</script>");
                }
                

                month_year.Text = " ";
                collect_date.Text = " ";
                claim_dateline.Text = " ";
                pdca_date.Text = " ";
                con.Close();
            }
            else
            {
                Response.Write("<script>alert('Please fill in the form correctly.')</script>");
                month_year.Text = " ";
                collect_date.Text = " ";
                claim_dateline.Text = " ";
                pdca_date.Text = " ";
                con.Close();
            }

            

            
        }
         

        protected void DropDownList1_DataBinding(object sender, EventArgs e)
        {
            //This method to display month selected at the dropdownlist
            SqlCommand cmd = new SqlCommand("Select * from month_detail where month_year='" + DropDownList1.Text + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView2.DataSource = dt;
            GridView2.DataBind();

        }


        protected void DeleteRecordBtn(object sender, EventArgs e)
        {
            //The method to delete record selected at the gridview2 display
            int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            String month_application = GridView2.Rows[rowindex].Cells[2].Text;
            String suppliertype = GridView2.Rows[rowindex].Cells[6].Text;
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete FROM month_detail WHERE month_year='" + month_application + "' AND supplier_type='" + suppliertype + "' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect(Request.Url.AbsoluteUri); //Refresh page after update data

        }

        protected void collect_date_TextChanged(object sender, EventArgs e)
        {
            if(collect_date.Text != string.Empty)
            {
                claim_dateline.Text = DateTime.Parse(collect_date.Text).AddDays(60).ToString("yyyy-MM-dd");
            }
           

        }

        protected void supplier_type1_TextChanged(object sender, EventArgs e)
        {
            if(supplier_type1.Text == "KD")
            {
                if (collect_date.Text != string.Empty)
                {
                    claim_dateline.Text = DateTime.Parse(collect_date.Text).AddDays(30).ToString("yyyy-MM-dd");
                }
                
            }
            else if (supplier_type1.Text == "LP")
            {
                if (collect_date.Text != string.Empty)
                {
                    claim_dateline.Text = DateTime.Parse(collect_date.Text).AddDays(60).ToString("yyyy-MM-dd");
                }

            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try//Showing the date format in day/month/year setting it through cells number
            {
                e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("dd/MM/yyyy");
            }
            //If the date has null/empty string/unsuitable format, set to the original value.
            catch (FormatException)
            {
                e.Row.Cells[1].Text = e.Row.Cells[1].Text;
                e.Row.Cells[2].Text = e.Row.Cells[2].Text;

            }


            if (e.Row.RowType == DataControlRowType.Header)//ColumnHeaderRename
            {
                e.Row.Cells[0].Text = "Month";
                e.Row.Cells[1].Text = "Collection Date";
                e.Row.Cells[2].Text = "Claim Dateline";
                e.Row.Cells[3].Text = "PDCA Date";
                e.Row.Cells[4].Text = "Supplier Type";
            }
        }
    }
}