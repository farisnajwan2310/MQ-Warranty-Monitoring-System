using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MqWebApp2.PUD_Claim
{
    public partial class PUD_ImportExcel : System.Web.UI.Page
    {
        //Connection file, get the connection string from web.config file
        SqlConnection SQLcon = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void TemplateFileBtn_Click(object sender, EventArgs e)
        {
            //User download excel template
            string FileName = "ImportExcelTemplate.xls";

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = ".xls";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/ImportExcelTemplate.xls"));
            response.Flush();
            response.End();
        }

        protected void UserManualBtn_Click(object sender, EventArgs e)
        {
            //User download user manual file.
            string FileName = "UserManualExcelImport.pdf";

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = ".pdf";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/ExcelUserManual.pdf"));
            response.Flush();
            response.End();
        }

        protected void BindGrid_Click(object sender, EventArgs e)
        {
            ShowDataGV();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)//set format for every cell/row
            {
                e.Row.Cells[0].Text = DropDownApplicationMonth.SelectedValue;
                e.Row.Cells[3].Text = "Pending";
                e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("yyyy-MM-dd");
                e.Row.Cells[8].Text = "PUD Claim";
            }
        }

        protected void ShowDataGV()
        {
            try
            {
                GridView1.Visible = true;

                string path1 = @"C:\Users\BSPT0773\source\repos\MqWebApp2\ExcelFile\" + FileUploadExcel.FileName;
                string path = (Server.MapPath("/ExcelFile/" + DropDownApplicationMonth.SelectedValue + FileUploadExcel.FileName));
                FileUploadExcel.SaveAs(path);

                string ExcelConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=Excel 4.0;";
                OleDbConnection OleDBcon = new OleDbConnection(ExcelConnString);

                //Using ole db to read file from excel 
                OleDbCommand cmd = new OleDbCommand("Select [Month],[Supplier Code],[Supplier Name],[Status],[Date collection],[Remarks],[Total Amount],[Total Case],[Supplier Type]   from [PUD_Claim$] WHERE [Month] IS NOT NULL", OleDBcon);

                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                objAdapter1.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception)
            {
                Response.Write("<script>alert('There is no data to show. Please input excel file.')</script>");//Confirmation Page
                Response.AddHeader("REFRESH", "1;URL=PUD_ImportExcel");//Refresh page
            }
        }

        protected void UploadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownApplicationMonth.SelectedValue == string.Empty)//Check if dropdown is null
                {
                    Response.Write("<script>alert('Month is not selected. Please try again.')</script>");//Confirmation Page
                }

                if (GridView1.Visible == true )//if the gridview is ready binded with any data, proceed to read the data from the available gridview
                {
                    foreach (GridViewRow g1 in GridView1.Rows)
                    {

                        SqlCommand SQLcon1 = new SqlCommand("insert into claim_record(month_year,supplier_code,supplier_name,status_claim,date_claim_submit,claim_remarks,total_amount_claim,total_claim,supplier_type) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "','" + g1.Cells[3].Text + "','" + g1.Cells[4].Text + "','" + g1.Cells[5].Text + "','" + g1.Cells[6].Text + "','" + g1.Cells[7].Text + "','" + g1.Cells[8].Text + "')", SQLcon);
                        SQLcon.Open();
                        SQLcon1.ExecuteNonQuery();
                        SQLcon.Close();
                    }

                    Response.Write("<script>alert('The data has been inserted.')</script>");//Confirmation Page
                    Response.AddHeader("REFRESH", "1;URL=PUD_ImportExcel");//Refresh page
                }
                else//if the user pick to add data directly from the excel file without preview 
                {
                    ShowDataGV();

                    if (FileUploadExcel.HasFile)
                    {
                        foreach (GridViewRow g1 in GridView1.Rows)
                        {

                            SqlCommand SQLcon1 = new SqlCommand("insert into claim_record(month_year,supplier_code,supplier_name,status_claim,date_claim_submit,claim_remarks,total_amount_claim,total_claim,supplier_type) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "','" + g1.Cells[3].Text + "','" + g1.Cells[4].Text + "','" + g1.Cells[5].Text + "','" + g1.Cells[6].Text + "','" + g1.Cells[7].Text + "','" + g1.Cells[8].Text + "')", SQLcon);
                            SQLcon.Open();
                            SQLcon1.ExecuteNonQuery();
                            SQLcon.Close();
                        }

                        Response.Write("<script>alert('The data has been inserted.')</script>");//Confirmation Page
                        Response.AddHeader("REFRESH", "1;URL=PUD_ImportExcel");//Refresh page
                    }
                    else
                    {
                        Response.Write("<script>alert('No excel file located.')</script>");//Confirmation Page
                    }
                }


            }
            catch (Exception errMsg)
            {
                SQLcon.Close();
                SQLcon.Open();
                SqlCommand SQLdelete = new SqlCommand("delete from claim_record where month_year='" + MonthApplication.Text + "' and supplier_type='LP' ", SQLcon);
                SQLdelete.ExecuteNonQuery();
                SQLcon.Close();
                Response.Write("<script>alert('Data Error.')</script>");//Confirmation Page
                UploadLabel.Text = errMsg.ToString();
            }

        }

        protected void ResetRecord_Click(object sender, EventArgs e)
        {
            try
            {


                if (ResetRecord.Text != string.Empty)
                {
                    //Connection file, get the connection string from web.config file
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);
                    //Delete record
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM claim_record WHERE month_year='" + MonthApplication.Text + "' AND supplier_type='PUD Claim' AND first_total_amount IS NULL ";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    //Refresh page after update data
                    Response.Write("<script>alert('Data Deleted')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please set application month')</script>");
                }

            }
            catch (Exception)
            {
                Response.Write("<script>alert('No data to delete')</script>");
            }
        }
    }
}