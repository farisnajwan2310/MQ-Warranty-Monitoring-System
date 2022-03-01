using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MqWebApp2.KD_Supplier_Part
{
    public partial class KD_ExportData : System.Web.UI.Page
    {

        //Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Binding data based on the month selected
            SqlCommand cmd = new SqlCommand("Select * from claim_record where month_year='" + DropDownList1.Text + "' AND supplier_type='KD' ORDER BY status_claim DESC ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void BtnExportData_Click(object sender, EventArgs e)
        {
            //if no application month selected, remind user to select the month
            if (DropDownList1.SelectedValue == string.Empty)
            {
                Response.Write("<script>alert('No month selected. Please set application month')</script>");
            }
            else
            {
                ExportGridToExcel();
            }

        }

        private void ExportGridToExcel()
        {
            //This function specified to execute the download operation of excel file
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Data KD -" + DropDownList1.Text + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Showing the date format in day/month/year
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                try
                {
                    if ((e.Row.Cells[16].Text != null) && (e.Row.Cells[26].Text != string.Empty) && (e.Row.Cells[36].Text != string.Empty))
                    {
                        e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString("dd/MM/yyyy");
                        e.Row.Cells[16].Text = Convert.ToDateTime(e.Row.Cells[16].Text).ToString("dd/MM/yyyy");
                        e.Row.Cells[26].Text = Convert.ToDateTime(e.Row.Cells[26].Text).ToString("dd/MM/yyyy");
                        e.Row.Cells[36].Text = Convert.ToDateTime(e.Row.Cells[36].Text).ToString("dd/MM/yyyy");
                    }
                }
                //If the date has null/empty string/unsuitable format, set to the original value.
                catch (FormatException)
                {
                    e.Row.Cells[4].Text = e.Row.Cells[4].Text;
                    e.Row.Cells[6].Text = e.Row.Cells[6].Text;
                    e.Row.Cells[16].Text = e.Row.Cells[16].Text;
                    e.Row.Cells[26].Text = e.Row.Cells[26].Text;
                    e.Row.Cells[36].Text = e.Row.Cells[36].Text;

                }

                //Display table in colour following submission level column
                if (e.Row.Cells[4].Text != string.Empty)
                {

                    e.Row.Cells[8].CssClass = "color-1stsubmission";
                    e.Row.Cells[9].CssClass = "color-1stsubmission";
                    e.Row.Cells[10].CssClass = "color-1stsubmission";
                    e.Row.Cells[11].CssClass = "color-1stsubmission";
                    e.Row.Cells[12].CssClass = "color-1stsubmission";
                    e.Row.Cells[13].CssClass = "color-1stsubmission";
                    e.Row.Cells[14].CssClass = "color-1stsubmission";
                    e.Row.Cells[15].CssClass = "color-1stsubmission";
                    e.Row.Cells[16].CssClass = "color-1stsubmission";
                    e.Row.Cells[17].CssClass = "color-1stsubmission";

                    e.Row.Cells[18].CssClass = "color-2ndsubmission";
                    e.Row.Cells[19].CssClass = "color-2ndsubmission";
                    e.Row.Cells[20].CssClass = "color-2ndsubmission";
                    e.Row.Cells[21].CssClass = "color-2ndsubmission";
                    e.Row.Cells[22].CssClass = "color-2ndsubmission";
                    e.Row.Cells[23].CssClass = "color-2ndsubmission";
                    e.Row.Cells[24].CssClass = "color-2ndsubmission";
                    e.Row.Cells[25].CssClass = "color-2ndsubmission";
                    e.Row.Cells[26].CssClass = "color-2ndsubmission";
                    e.Row.Cells[27].CssClass = "color-2ndsubmission";

                    e.Row.Cells[28].CssClass = "color-3rdsubmission";
                    e.Row.Cells[29].CssClass = "color-3rdsubmission";
                    e.Row.Cells[30].CssClass = "color-3rdsubmission";
                    e.Row.Cells[31].CssClass = "color-3rdsubmission";
                    e.Row.Cells[32].CssClass = "color-3rdsubmission";
                    e.Row.Cells[33].CssClass = "color-3rdsubmission";
                    e.Row.Cells[34].CssClass = "color-3rdsubmission";
                    e.Row.Cells[35].CssClass = "color-3rdsubmission";
                    e.Row.Cells[36].CssClass = "color-3rdsubmission";
                    e.Row.Cells[37].CssClass = "color-3rdsubmission";
                }

            }

            //Rename column header
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Application Month";
                e.Row.Cells[1].Text = "Supplier Code";
                e.Row.Cells[2].Text = "Supplier Name";
                e.Row.Cells[3].Text = "Status Claim";
                e.Row.Cells[4].Text = "Collection Date";
                e.Row.Cells[5].Text = "Remarks";
                e.Row.Cells[6].Text = "Total Amount(RM)";
                e.Row.Cells[7].Text = "Quantity Cases";

                e.Row.Cells[8].Text = "1st Submission Total Amount(RM)";
                e.Row.Cells[9].Text = "1st Submission Cases";
                e.Row.Cells[10].Text = "1st Submission Approved Amount (RM)";
                e.Row.Cells[11].Text = "1st Submission Approved Cases";
                e.Row.Cells[12].Text = "1st Submission Reject Amount (RM)";
                e.Row.Cells[13].Text = "1st Submission Reject Cases";
                e.Row.Cells[14].Text = "1st Submission Pending Amount(RM)";
                e.Row.Cells[15].Text = "1st Submission Pending Cases";
                e.Row.Cells[16].Text = "1st Submission Date";
                e.Row.Cells[17].Text = "1st Submission Remark";

                e.Row.Cells[18].Text = "2nd Submission Total Amount(RM)";
                e.Row.Cells[19].Text = "2nd Submission Cases";
                e.Row.Cells[20].Text = "2nd Submission Approved Amount (RM)";
                e.Row.Cells[21].Text = "2nd Submission Approved Cases";
                e.Row.Cells[22].Text = "2nd Submission Reject Amount (RM)";
                e.Row.Cells[23].Text = "2nd Submission Reject Cases";
                e.Row.Cells[24].Text = "2nd Submission Pending Amount(RM)";
                e.Row.Cells[25].Text = "2nd Submission Pending Cases";
                e.Row.Cells[26].Text = "2nd Submission Date";
                e.Row.Cells[27].Text = "2nd Submission Remark";

                e.Row.Cells[28].Text = "3rd Submission Total Amount(RM)";
                e.Row.Cells[29].Text = "3rd Submission Cases";
                e.Row.Cells[30].Text = "3rd Submission Approved Amount (RM)";
                e.Row.Cells[31].Text = "3rd Submission Approved Cases";
                e.Row.Cells[32].Text = "3rd Submission Reject Amount (RM)";
                e.Row.Cells[33].Text = "3rd Submission Reject Cases";
                e.Row.Cells[34].Text = "3rd Submission Pending Amount(RM)";
                e.Row.Cells[35].Text = "3rd Submission Pending Cases";
                e.Row.Cells[36].Text = "3rd Submission Date";
                e.Row.Cells[37].Text = "3rd Submission Remark";
                e.Row.Cells[38].Text = "Supplier Type";
            }
        }
    }
}