using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MqWebApp2.KD_Supplier_Part
{
    
    public partial class KD_Supplier : System.Web.UI.Page
    {
        //Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (DropDownApplicationMonth.Text == string.Empty)
                {
                    if (Session["monthKD"] != null)
                    {
                        //Checking month value exist in record or not
                        con.Open();
                        string sessioname = Session["monthKD"].ToString();
                        SqlCommand checkSession = new SqlCommand("select count(month_year) from month_detail where month_year='" + sessioname + "' AND supplier_type='KD'", con);
                        int totalrec0rdmonth = Convert.ToInt32(checkSession.ExecuteScalar());
                        con.Close();

                        if(totalrec0rdmonth == 1)
                        {
                            //If the record month is found, then it will execute session set up
                            DropDownApplicationMonth.Text = Session["monthKD"].ToString();
                            SessionBind();
                        }
                        else
                        {
                            //If the record month not found, it will ask user to set new session 

                            Response.Write("<script>alert('Please set application month')</script>");
                        }
                        
                    }
                    else
                    {
                        //if the session is not set, it will give alert
                        Response.Write("<script>alert('Please set application month')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Session["monthKD"] = "";
                DropDownApplicationMonth.SelectedValue = "Select Month";
                //if the session is not set, it will give alert
                Response.Write("<script>alert('Please set application month')</script>");
            }
        }


        protected void DropDownYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Every dropdown value change will set to the new session value
            //
            MonthDisplay.Text = DropDownApplicationMonth.Text;
            Session["monthKD"] = DropDownApplicationMonth.Text;
            DropDownApplicationMonth.Text = Session["monthKD"].ToString();

            SessionBind();


        }

        protected void SessionBind()
        {
            //Below will execute data binding/fetching based on the month selected

            //Assign session value to string variable to put in the sql query
            String currentMonth = Session["monthKD"].ToString();


            //Fetch data from database and put into table display
            SqlCommand cmd = new SqlCommand("Select month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim, sum(first_approve_amount+second_approve_amount+third_approve_amount) as Amount_Collected from claim_record where month_year='" + currentMonth + "' AND supplier_type='KD ' GROUP BY month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim ORDER BY status_claim DESC ,supplier_code ASC ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();


            
            try
            {
                //Display Collection Date
                con.Open();
                SqlCommand submissiondatedata = new SqlCommand("Select collection_date from month_detail where month_year='" + currentMonth + "' AND supplier_type='KD'", con);
                submissiondatedata.ExecuteNonQuery();
                String cdd3 = Convert.ToString((submissiondatedata.ExecuteScalar()));
                Submission_Date.Text = Convert.ToDateTime(cdd3).ToString("dd/MM/yyyy");
                con.Close();

                //Display Supplier Dateline
                con.Open();
                SqlCommand supplierdatelinedata = new SqlCommand("Select claim_dateline from month_detail where month_year='" + currentMonth + "' AND supplier_type='KD' ", con);
                supplierdatelinedata.ExecuteNonQuery();
                String sdd = Convert.ToString((supplierdatelinedata.ExecuteScalar()));
                Supplier_Dateline.Text = Convert.ToDateTime(sdd).ToString("dd/MM/yyyy");
                con.Close();

                //Display PDCA Date
                con.Open();
                SqlCommand pdcadatedata = new SqlCommand("Select pdca_date from month_detail where month_year='" + currentMonth + "' AND supplier_type='KD' ", con);
                pdcadatedata.ExecuteNonQuery();
                String pdd = Convert.ToString((pdcadatedata.ExecuteScalar()));
                Pdca_date.Text = pdd;
                con.Close();

                //Display Total Pending
                con.Open();
                SqlCommand totalpending = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + currentMonth + "' AND status_claim='Pending' AND supplier_type='KD' ", con);
                totalpending.ExecuteNonQuery();
                String totalpendingtextbox = Convert.ToString((totalpending.ExecuteScalar()));
                TotalPendingLabel.Text = totalpendingtextbox;
                con.Close();

                //Display Total Incomplete
                con.Open();
                SqlCommand totalincomplete = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + currentMonth + "' AND status_claim='Incomplete' AND supplier_type='KD'", con);
                totalincomplete.ExecuteNonQuery();
                String totalincompletetextbox = Convert.ToString((totalincomplete.ExecuteScalar()));
                TotalIncompleteLabel.Text = totalincompletetextbox;
                con.Close();

                //Display Total Completed
                con.Open();
                SqlCommand totalcompleted = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + currentMonth + "' AND status_claim='Completed' AND supplier_type='KD' ", con);
                totalcompleted.ExecuteNonQuery();
                String totalcompletedtextbox = Convert.ToString((totalcompleted.ExecuteScalar()));
                TotalCompleteLabel.Text = totalcompletedtextbox;
                con.Close();

                //Display All Record Total (KD)
                con.Open();
                SqlCommand totalallrecord = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + currentMonth + "' AND supplier_type='KD' ", con);
                totalallrecord.ExecuteNonQuery();
                String totalallrecordtextbox = Convert.ToString((totalallrecord.ExecuteScalar()));
                TotalAllLPLabel.Text = totalallrecordtextbox;
                con.Close();

                //Set Text in "All list of KD" to current month
                MonthDisplay.Text = currentMonth;
            }
            catch(Exception) {

                Submission_Date.Text = "-";
                Supplier_Dateline.Text = "-";
                Pdca_date.Text = "-";
                TotalPendingLabel.Text = "-";
                TotalIncompleteLabel.Text = "-";
                TotalCompleteLabel.Text = "-";
                TotalAllLPLabel.Text = "-";

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)//set format for every cell/row
            {
                //Set colour of status
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

            if (e.Row.RowType == DataControlRowType.Header)//set header name by number cells
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //For Page Changing
            GridView1.PageIndex = e.NewPageIndex;//When number page click, it will redirect to the page( clicked number)
            SqlCommand cmd = new SqlCommand("Select month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim, sum(first_approve_amount+second_approve_amount+third_approve_amount) as Amount_Collected from claim_record where month_year='" + DropDownApplicationMonth.Text + "' AND supplier_type='KD ' GROUP BY month_year,supplier_name,supplier_code,status_claim,claim_remarks,total_amount_claim,total_claim ORDER BY status_claim DESC ,supplier_code ASC  ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}