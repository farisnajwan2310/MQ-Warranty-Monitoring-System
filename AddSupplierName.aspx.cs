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
    public partial class AddSupplierName : System.Web.UI.Page
    {
        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddSupplierDetails(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into supplier_details(supplier_code,supplier_name,supplier_type,supplier_address,supplier_tel,supplier_fax)" + "values('" + supplier_code.Text + "','" + supplier_name.Text + "','" + supplier_type.Text + "','" + supplier_address.Text + "','" + supplier_telephone.Text + "','" + supplier_fax.Text + "')";
                cmd.ExecuteNonQuery();

                supplier_code.Text = " ";
                supplier_name.Text = " ";
                supplier_type.Text = " ";
                supplier_address.Text = " ";
                supplier_telephone.Text = " ";
                supplier_fax.Text = " ";
                con.Close();
                Response.Write("<script>alert('The supplier details has been added.')</script>");
                Response.AddHeader("REFRESH", "1;URL=AddSupplierName");
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Data Duplication/Error.')</script>");
                Response.AddHeader("REFRESH", "1;URL=AddSupplierName");
            }
            

        }

        protected void GridViewSupplierDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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