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
    public partial class LP_Supplier : System.Web.UI.Page
    {
        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //Below is to set session to the previous selected month. 
            if(Session["monthLP"] != null)
            {
                con.Close();
                //Checking month value exist in record database or not
                con.Open();
                string sessioname = Session["monthLP"].ToString();
                SqlCommand checkSession = new SqlCommand("select count(month_year) from month_detail where month_year='" + sessioname + "' AND supplier_type='LP'", con);
                int totalrec0rdmonth = Convert.ToInt32(checkSession.ExecuteScalar());
                con.Close();

                if (totalrec0rdmonth == 1)
                {
                    //If the record month is found, then it will execute session set up
                    DropDownList1.Text = Session["monthLP"].ToString();
                    SessionBind();
                }
                else
                {
                    //If the record month not found, it will ask user to set new session 

                    Response.Write("<script>alert('Please set application month')</script>");
                }
            }
        }

        //Method below to bind data from database with the gridview
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {


             Session["monthLP"] = DropDownList1.Text;
            
            SqlCommand cmd = new SqlCommand("Select month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim, sum(first_approve_amount+second_approve_amount+third_approve_amount) as Amount_Collected from claim_record where month_year='" + DropDownList1.Text + "' AND supplier_type='LP' GROUP BY month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim ORDER BY status_claim DESC,supplier_code ASC ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //MonthDisplay.Text = DropDownList1.Text;

            try
            {
                //Display Collection Date
                con.Open();
                SqlCommand collectiondatedata = new SqlCommand("Select collection_date from month_detail where month_year='" + DropDownList1.Text + "'  AND supplier_type='LP'", con);
                String cdd3 = Convert.ToString((collectiondatedata.ExecuteScalar()));
                Collection_Date.Text = Convert.ToDateTime(cdd3).ToString("dd/MM/yyyy");
                con.Close();

                //Display Supplier Dateline
                con.Open();
                SqlCommand supplierdatelinedata = new SqlCommand("Select claim_dateline from month_detail where month_year='" + DropDownList1.Text + "'  AND supplier_type='LP'", con);
                String sdd = Convert.ToString((supplierdatelinedata.ExecuteScalar()));
                Supplier_Dateline.Text = Convert.ToDateTime(sdd).ToString("dd/MM/yyyy");
                con.Close();

                //Display PDCA Date
                con.Open();
                SqlCommand pdcadatedata = new SqlCommand("Select pdca_date from month_detail where month_year='" + DropDownList1.Text + "'  AND supplier_type='LP'", con);
                String pdd = Convert.ToString((pdcadatedata.ExecuteScalar()));
                Pdca_date.Text = pdd;
                con.Close();

                //Display Total Pending
                con.Open();
                SqlCommand totalpending = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + DropDownList1.Text + "' AND status_claim='Pending'  AND supplier_type='LP' ", con);
                String totalpendingtextbox = Convert.ToString((totalpending.ExecuteScalar()));
                TotalPendingLabel.Text = totalpendingtextbox;
                con.Close();

                //Display Total Incomplete
                con.Open();
                SqlCommand totalothers = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + DropDownList1.Text + "' AND status_claim='Others'  AND supplier_type='LP'", con);
                String totalotherstextbox = Convert.ToString((totalothers.ExecuteScalar()));
                TotalOthersLabel.Text = totalotherstextbox;
                con.Close();

                //Display Total Incomplete
                con.Open();
                SqlCommand totalincomplete = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + DropDownList1.Text + "' AND status_claim='Incomplete'  AND supplier_type='LP'", con);
                String totalincompletetextbox = Convert.ToString((totalincomplete.ExecuteScalar()));
                TotalIncompleteLabel.Text = totalincompletetextbox;
                con.Close();

                //Display Total Completed
                con.Open();
                SqlCommand totalcompleted = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + DropDownList1.Text + "' AND status_claim='Completed'  AND supplier_type='LP' ", con);
                String totalcompletedtextbox = Convert.ToString((totalcompleted.ExecuteScalar()));
                TotalCompleteLabel.Text = totalcompletedtextbox;
                con.Close();

                //Display All Record Total (LP)
                con.Open();
                SqlCommand totalallrecord = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + DropDownList1.Text + "'  AND supplier_type='LP' ", con);
                String totalallrecordtextbox = Convert.ToString((totalallrecord.ExecuteScalar()));
                TotalAllLPLabel.Text = totalallrecordtextbox;
                con.Close();
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Please Try Again')</script>");
                Response.AddHeader("REFRESH", "1;URL=LP_Supplier");

            }

        }


        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            //The method to delete record selected at the gridview1 display
            
            int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            String month_application = GridView1.Rows[rowindex].Cells[2].Text;
            String supplier_code = GridView1.Rows[rowindex].Cells[4].Text;
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete FROM claim_record WHERE month_year='" + month_application + "' AND supplier_code='" + supplier_code + "'  AND supplier_type='LP' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect(Request.Url.AbsoluteUri); //Refresh page after update data
        }


        //Method below to refresh grid after next page selected
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //For Page Number and Page Changing
            PageNumber.Visible = true;//Show page number
            GridView1.PageIndex = e.NewPageIndex; //When number page click, it will redirect to the page( clicked number)
            SqlCommand cmd = new SqlCommand("Select month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim, sum(first_approve_amount+second_approve_amount+third_approve_amount) as Amount_Collected from claim_record where month_year='" + DropDownList1.Text + "' AND supplier_type='LP' GROUP BY month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim ORDER BY status_claim DESC,supplier_code ASC ", con);
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Set colour of status
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "Pending")
                {
                    e.Row.Cells[4].CssClass = "status-color1";
                }
                else if (e.Row.Cells[4].Text == "Incomplete")
                {
                    e.Row.Cells[4].CssClass = "status-color2";
                }
                else if (e.Row.Cells[4].Text == "Completed")
                {
                    e.Row.Cells[4].CssClass = "status-color3";
                }

                try//setting the amount column in suitable format
                {
                    e.Row.Cells[6].Text = Convert.ToDecimal(e.Row.Cells[6].Text).ToString("N2"); //Display amount column in digit format
                    e.Row.Cells[8].Text = Convert.ToDecimal(e.Row.Cells[8].Text).ToString("N2"); //Display amount column in digit format
                }
                catch (Exception)
                {
                    e.Row.Cells[8].Text = " 0.00 ";
                }

            }

            if (e.Row.RowType == DataControlRowType.Header)//Rename header column through number cell
            {
                e.Row.Cells[1].Text = "Application Month";
                e.Row.Cells[2].Text = "Supplier Name";
                e.Row.Cells[3].Text = "Supplier Code";
                e.Row.Cells[4].Text = "Status Claim";
                e.Row.Cells[5].Text = "Remarks";
                e.Row.Cells[6].Text = "Total Amount(RM)";
                e.Row.Cells[7].Text = "Quantity Cases";
                e.Row.Cells[8].Text = "Amount Collected (RM)";
            }
        }

        protected void SessionBind()
        {
            //This event actually same with above method (DropDownListIndexSelectedChanged),but it is for the session 
            DropDownList1.SelectedValue = Session["monthLP"].ToString();
            string CurrentMonth = Session["monthLP"].ToString();


            SqlCommand cmd = new SqlCommand("Select month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim  , sum(first_approve_amount+second_approve_amount+third_approve_amount) as Amount_Collected from claim_record where month_year='" + CurrentMonth + "' AND supplier_type='LP' GROUP BY month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim ORDER BY status_claim DESC ,supplier_code ASC ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            MonthDisplay.Text =  Convert.ToString(Session["monthLP"]);

            try
            {
                //Display Collection Date
                con.Open();
                SqlCommand collectiondatedata = new SqlCommand("Select collection_date from month_detail where month_year='" + CurrentMonth + "'  AND supplier_type='LP'", con);
                collectiondatedata.ExecuteNonQuery();
                String cdd3 = Convert.ToString((collectiondatedata.ExecuteScalar()));
                Collection_Date.Text = Convert.ToDateTime(cdd3).ToString("dd/MM/yyyy");
                con.Close();

                //Display Supplier Dateline
                con.Open();
                SqlCommand supplierdatelinedata = new SqlCommand("Select claim_dateline from month_detail where month_year='" + CurrentMonth + "'  AND supplier_type='LP'", con);
                supplierdatelinedata.ExecuteNonQuery();
                String sdd = Convert.ToString((supplierdatelinedata.ExecuteScalar()));
                Supplier_Dateline.Text = Convert.ToDateTime(sdd).ToString("dd/MM/yyyy");
                con.Close();

                //Display PDCA Date
                con.Open();
                SqlCommand pdcadatedata = new SqlCommand("Select pdca_date from month_detail where month_year='" + CurrentMonth + "'  AND supplier_type='LP'", con);
                pdcadatedata.ExecuteNonQuery();
                String pdd = Convert.ToString((pdcadatedata.ExecuteScalar()));
                Pdca_date.Text = pdd;
                con.Close();

                //Display Total Pending
                con.Open();
                SqlCommand totalpending = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + CurrentMonth + "' AND status_claim='Pending'  AND supplier_type='LP' ", con);
                totalpending.ExecuteNonQuery();
                String totalpendingtextbox = Convert.ToString((totalpending.ExecuteScalar()));
                TotalPendingLabel.Text = totalpendingtextbox;
                con.Close();

                //Display Total Pending
                con.Open();
                SqlCommand totalincomplete = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + CurrentMonth + "' AND status_claim='Incomplete'  AND supplier_type='LP'", con);
                totalincomplete.ExecuteNonQuery();
                String totalincompletetextbox = Convert.ToString((totalincomplete.ExecuteScalar()));
                TotalIncompleteLabel.Text = totalincompletetextbox;
                con.Close();

                //Display Total Completed
                con.Open();
                SqlCommand totalcompleted = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + CurrentMonth + "' AND status_claim='Completed'  AND supplier_type='LP' ", con);
                totalcompleted.ExecuteNonQuery();
                String totalcompletedtextbox = Convert.ToString((totalcompleted.ExecuteScalar()));
                TotalCompleteLabel.Text = totalcompletedtextbox;
                con.Close();

                //Display All Record Total (LP)
                con.Open();
                SqlCommand totalallrecord = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + CurrentMonth + "'  AND supplier_type='LP' ", con);
                totalallrecord.ExecuteNonQuery();
                String totalallrecordtextbox = Convert.ToString((totalallrecord.ExecuteScalar()));
                TotalAllLPLabel.Text = totalallrecordtextbox;
                con.Close();
            }
            catch (Exception)
            {
                Collection_Date.Text = "-";
                Supplier_Dateline.Text = "-";
                Pdca_date.Text = "-";
                TotalPendingLabel.Text = "-";
                TotalIncompleteLabel.Text = "-";
                TotalCompleteLabel.Text = "-";
                TotalAllLPLabel.Text = "-";
            }

        }

       
    }
}