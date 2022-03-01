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
    public partial class Edit_SupplierDetails : System.Web.UI.Page
    {
        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

            BindSupplierData();

            //This method to bind data from method void BindGrid()

        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)//ColumnHeaderRename
            {
                e.Row.Cells[2].Text = "Supplier Code";
                e.Row.Cells[3].Text = "Supplier Name";
                e.Row.Cells[4].Text = "Supplier Type";
                e.Row.Cells[5].Text = "Address";
                e.Row.Cells[6].Text = "Phone";
                e.Row.Cells[7].Text = "Fax";
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set PageNumber visible
            PageNumber.Visible = true;
            GridView1.PageIndex = e.NewPageIndex;
            SqlCommand cmd = new SqlCommand("Select supplier_code, supplier_name, supplier_type, supplier_address, supplier_tel, supplier_fax from supplier_details order by supplier_type desc, supplier_code asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            int pageNumber = GridView1.PageIndex;
            //since index count from 0, it need to plus one to get current page
            int currentPage = 1 + pageNumber;
            PageNumber.Text = "Page: " + currentPage.ToString();
        }


        protected void BindSupplierData()
        {
            SqlCommand cmd = new SqlCommand("Select supplier_code, supplier_name, supplier_type, supplier_address, supplier_tel, supplier_fax from supplier_details order by supplier_type desc, supplier_code asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            String suppliercodeid = GridView1.Rows[rowindex].Cells[1].Text;

            con.Open();
            SqlCommand cmd = new SqlCommand("Delete FROM supplier_details WHERE supplier_code='" + suppliercodeid + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect(Request.Url.AbsoluteUri); //Refresh page after update data
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            supplier_code.Text = GridView1.Rows[rowindex].Cells[2].Text;
            supplier_name.Text = GridView1.Rows[rowindex].Cells[3].Text;
            supplier_type.Text = GridView1.Rows[rowindex].Cells[4].Text;
            supplier_address.Text = GridView1.Rows[rowindex].Cells[5].Text;
            supplier_telephone.Text = GridView1.Rows[rowindex].Cells[6].Text;
            supplier_fax.Text = GridView1.Rows[rowindex].Cells[7].Text;

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (supplier_code.Text != string.Empty)
            {

                try
                {
                    //Delete record
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM supplier_details WHERE supplier_code='" + supplier_code.Text + "' AND supplier_type='" + supplier_type.Text + "'  ";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    supplier_code.Text = "";
                    supplier_name.Text = "";
                    supplier_type.Text = "";
                    supplier_address.Text = "";
                    supplier_telephone.Text = "";
                    supplier_fax.Text = "";
                    Response.Write("<script>alert('The supplier details has been deleted.')</script>");
                    Response.AddHeader("REFRESH", "1;URL=Edit_SupplierDetails");
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Error. Please Make Sure Data Entered Correctly.')</script>");
                }
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (supplier_name.Text != string.Empty && supplier_code.Text != string.Empty && supplier_address.Text != string.Empty)
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE supplier_details SET supplier_name='" + supplier_name.Text + "', supplier_type='" + supplier_type.Text + "', supplier_address='" + supplier_address.Text + "', supplier_tel='" + supplier_telephone.Text + "', supplier_fax='" + supplier_fax.Text + "' WHERE supplier_code='" + supplier_code.Text + "' ";

                    cmd.ExecuteNonQuery();
                    con.Close();

                    supplier_code.Text = "";
                    supplier_name.Text = "";
                    supplier_address.Text = "";
                    supplier_telephone.Text = "";
                    supplier_fax.Text = "";
                    Response.Write("<script>alert('The supplier details has been updated.')</script>");
                    Response.AddHeader("REFRESH", "1;URL=Edit_SupplierDetails");
                }
                catch (Exception sss)
                {
                    supplier_address.Text = sss.ToString();
                    Response.Write("<script>alert('Error. Please Make Sure Data Entered Correctly.')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Error. Please Make Sure Data Entered Correctly.')</script>");
            }
        }

    

        protected void SearchCodeBtn_Click(object sender, EventArgs e)
        {
            GridViewSearchResult.Visible = true;
            //Fetch supplier record via code 
            SqlCommand cmd = new SqlCommand("Select supplier_code, supplier_name, supplier_type, supplier_address, supplier_tel, supplier_fax from supplier_details where supplier_code='" + SearchSuppCode.Text + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridViewSearchResult.DataSource = dt;
            GridViewSearchResult.DataBind();
        }

        protected void RefreshBtn_Click(object sender, EventArgs e)
        {
            GridViewSearchResult.Visible = false;
            SqlCommand cmd = new SqlCommand("Select supplier_code, supplier_name, supplier_type, supplier_address, supplier_tel, supplier_fax from supplier_details ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void SelectSearchTable_Click(object sender, EventArgs e)
        {
            int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            supplier_code.Text = GridViewSearchResult.Rows[rowindex].Cells[1].Text;
            supplier_name.Text = GridViewSearchResult.Rows[rowindex].Cells[2].Text;
            supplier_type.Text = GridViewSearchResult.Rows[rowindex].Cells[3].Text;
            supplier_address.Text = GridViewSearchResult.Rows[rowindex].Cells[4].Text;
            supplier_telephone.Text = GridViewSearchResult.Rows[rowindex].Cells[5].Text;
            supplier_fax.Text = GridViewSearchResult.Rows[rowindex].Cells[6].Text;
        }

        protected void GridViewSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)//ColumnHeaderRename
            {
                e.Row.Cells[1].Text = "Supplier Code";
                e.Row.Cells[2].Text = "Supplier Name";
                e.Row.Cells[3].Text = "Supplier Type";
                e.Row.Cells[4].Text = "Address";
                e.Row.Cells[5].Text = "Phone";
                e.Row.Cells[6].Text = "Fax";
            }
        }
    }
}