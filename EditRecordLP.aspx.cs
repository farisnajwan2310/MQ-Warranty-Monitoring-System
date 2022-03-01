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
    public partial class EditRecordLP : System.Web.UI.Page
    {
        //Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            AmountCalcDisplay();//To set the value of JS, calculation


        }

        protected void Page_PreRender(object sender, EventArgs e)
        {//The event will load last before the page refresh/rendered

            
            if (DropDownList3.Text == string.Empty)//if the dropdownlist not set to a value
            {
                if (Session["monthLP"] != null)//if session has been set
                {
                    //Checking month value exist in database record or not
                    con.Open();
                    string sessioname = Session["monthLP"].ToString();
                    //count record matching in the database
                    SqlCommand checkSession = new SqlCommand("select count(month_year) from month_detail where month_year='" + sessioname + "' AND supplier_type='LP'", con);
                    int totalrec0rdmonth = Convert.ToInt32(checkSession.ExecuteScalar());
                    con.Close();

                    if (totalrec0rdmonth == 1)
                    {
                        //If the record month is found, then it will execute session set up
                        DropDownList3.Text = Session["monthLP"].ToString();
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


        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Setting up session if the user select new value at the dropdownlist
            Session["monthLP"] = DropDownList3.Text;
            SessionBind();
        }

        protected void SessionBind()
        {
            //Substitute current session to dropdown value.
            String currentMonth = Session["monthLP"].ToString();

            //Binding data based on the month selected
            SqlCommand cmd = new SqlCommand("Select * from claim_record where month_year='" + currentMonth + "' AND supplier_type='LP' ORDER BY status_claim DESC,supplier_code ASC ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            //make button and label visible
            SearchCodeLabel.Visible = true;
            SearchSuppCode.Visible = true;
            SearchCodeBtn.Visible = true;
            PageNumber.Visible = false;
            RefreshBtn.Visible = true;

        }

        protected void SearchCodeBtn_Click(object sender, EventArgs e)
         {
            //Fetch supplier record via code and month
            String currentMonth = Session["monthLP"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from claim_record where month_year='" + currentMonth + "' AND supplier_code ='"+SearchSuppCode.Text+ "' AND supplier_type='LP' ORDER BY status_claim DESC,supplier_code ASC ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);          
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();                   
         }              
        protected void SelectRecord_Click(object sender, EventArgs e)
        {

            
            try
            {

                //Put the data from table to form
                int rowindex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
                UpdateMonthTextBox.Text = GridView1.Rows[rowindex].Cells[1].Text;
                UpdateSupplierName.Text = GridView1.Rows[rowindex].Cells[3].Text;
                UpdateSupplierCode.Text = GridView1.Rows[rowindex].Cells[2].Text;
                status_claim.SelectedValue = GridView1.Rows[rowindex].Cells[4].Text;
                date_claim.Text = Convert.ToDateTime(GridView1.Rows[rowindex].Cells[5].Text).ToString("yyyy-MM-dd");//Date format changing to display in the form 
                claim_remarks.Text = GridView1.Rows[rowindex].Cells[6].Text;
                total_amount.Text = GridView1.Rows[rowindex].Cells[7].Text;
                total_claim.Text = GridView1.Rows[rowindex].Cells[8].Text;

                amount_1st.Text = GridView1.Rows[rowindex].Cells[9].Text;
                quantity_1st.Text = GridView1.Rows[rowindex].Cells[10].Text;
                approved_amount_1st.Text = GridView1.Rows[rowindex].Cells[11].Text;
                approved_1st.Text = GridView1.Rows[rowindex].Cells[12].Text;
                rejected_amount_1st.Text = GridView1.Rows[rowindex].Cells[13].Text;
                rejected_1st.Text = GridView1.Rows[rowindex].Cells[14].Text;
                pending_amount_1st.Text = GridView1.Rows[rowindex].Cells[15].Text;
                pending_1st.Text = GridView1.Rows[rowindex].Cells[16].Text;
                date_1st.Text = Convert.ToDateTime(GridView1.Rows[rowindex].Cells[17].Text).ToString("yyyy-MM-dd");
                remarks_1st.Text = GridView1.Rows[rowindex].Cells[18].Text;

                amount_2nd.Text = GridView1.Rows[rowindex].Cells[19].Text;
                quantity_2nd.Text = GridView1.Rows[rowindex].Cells[20].Text;
                amount_approved_2nd.Text = GridView1.Rows[rowindex].Cells[21].Text;
                approved_2nd.Text = GridView1.Rows[rowindex].Cells[22].Text;
                amount_rejected_2nd.Text = GridView1.Rows[rowindex].Cells[23].Text;
                rejected_2nd.Text = GridView1.Rows[rowindex].Cells[24].Text;
                pending_amount_2nd.Text = GridView1.Rows[rowindex].Cells[25].Text;
                pending_2nd.Text = GridView1.Rows[rowindex].Cells[26].Text;
                date_2nd.Text = Convert.ToDateTime(GridView1.Rows[rowindex].Cells[27].Text).ToString("yyyy-MM-dd");
                remarks_2nd.Text = GridView1.Rows[rowindex].Cells[28].Text;

                amount_3rd.Text = GridView1.Rows[rowindex].Cells[29].Text;
                quantity_3rd.Text = GridView1.Rows[rowindex].Cells[30].Text;
                amount_approved_3rd.Text = GridView1.Rows[rowindex].Cells[31].Text;
                approved_3rd.Text = GridView1.Rows[rowindex].Cells[32].Text;
                amount_rejected_3rd.Text = GridView1.Rows[rowindex].Cells[33].Text;
                rejected_3rd.Text = GridView1.Rows[rowindex].Cells[34].Text;
                pending_amount_3rd.Text = GridView1.Rows[rowindex].Cells[35].Text;
                pending_3rd.Text = GridView1.Rows[rowindex].Cells[36].Text;
                date_3rd.Text = Convert.ToDateTime(GridView1.Rows[rowindex].Cells[37].Text).ToString("yyyy-MM-dd");
                remarks_3rd.Text = GridView1.Rows[rowindex].Cells[38].Text;
                supplier_type1.Text = GridView1.Rows[rowindex].Cells[39].Text;


                //Display All Record Total (LP) at top of the form
                con.Open();
                SqlCommand total_amount_collected = new SqlCommand("Select sum(first_approve_amount+second_approve_amount+third_approve_amount) from claim_record where month_year='" + DropDownList3.Text + "' AND supplier_code='" + UpdateSupplierCode.Text + "' AND supplier_type='LP' ", con);
                total_amount_collected.ExecuteNonQuery();
                String totalamountcollected = Convert.ToString((total_amount_collected.ExecuteScalar()));
                String totalamountcollected1 = Convert.ToDecimal(totalamountcollected).ToString("C2");
                TotalCollected.Text = totalamountcollected1;
                con.Close();

            }
            catch (Exception)
            {
                //when record have null value , it will set to this
                TotalCollected.Text = "Data Incomplete";

                //Clearing text box if data incomplete(no response from supplier)
                amount_1st.Text = "0.00"; quantity_1st.Text = "0"; approved_amount_1st.Text = "0.00";
                approved_1st.Text = "0"; rejected_amount_1st.Text = "0.00";
                rejected_1st.Text = "0"; pending_amount_1st.Text = "0.00";
                pending_1st.Text = "0"; date_1st.Text = "1900-01-01";
                remarks_1st.Text = "-";

                amount_2nd.Text = "0.00"; quantity_2nd.Text = "0"; amount_approved_2nd.Text = "0.00";
                approved_2nd.Text = "0"; amount_rejected_2nd.Text = "0.00";
                rejected_2nd.Text = "0"; pending_amount_2nd.Text = "0.00";
                pending_2nd.Text = "0"; date_2nd.Text = "1900-01-01";
                remarks_2nd.Text = "-";

                amount_3rd.Text = "0.00"; quantity_3rd.Text = "0"; amount_approved_3rd.Text = "0.00";
                approved_3rd.Text = "0"; amount_rejected_3rd.Text = "0.00";
                rejected_3rd.Text = "0"; pending_amount_3rd.Text = "0.00";
                pending_3rd.Text = "0"; date_3rd.Text = "1900-01-01";
                remarks_3rd.Text = "-";

            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            //Detect column amount and quantity, if there is new value, add the amount and calculate
            if (approved_1st.Text != string.Empty && rejected_1st.Text != string.Empty && approved_amount_1st.Text != string.Empty && rejected_amount_1st.Text != string.Empty)
            {
                CalculationFirst();
            }

            if(approved_2nd.Text != string.Empty && rejected_2nd.Text != string.Empty && amount_approved_2nd.Text != string.Empty && amount_rejected_2nd.Text != string.Empty)
            {
                CalculationSecond();
            }

            if (approved_3rd.Text != string.Empty && rejected_3rd.Text != string.Empty && amount_approved_3rd.Text != string.Empty && amount_rejected_3rd.Text != string.Empty)
            {
                CalculationThird();
            }

            //calculate the total submitted by supplier, if the total collected complete, change the status to completed
            int totalsubmitted;
            int quantity1 = Convert.ToInt32(quantity_1st.Text);
            int quantity2 = Convert.ToInt32(quantity_2nd.Text);
            int quantity3 = Convert.ToInt32(quantity_3rd.Text);
            totalsubmitted = quantity1 + quantity2 + quantity3;

            decimal amount1 = Convert.ToDecimal(amount_1st.Text);
            decimal amount2 = Convert.ToDecimal(amount_2nd.Text);
            decimal amount3 = Convert.ToDecimal(amount_3rd.Text);
            decimal totalamount = amount1 + amount2 + amount3;


            if (Convert.ToString(totalsubmitted) == total_claim.Text && Convert.ToString(totalamount) == total_amount.Text)
            {
                status_claim.Text = "Completed";
            }
            //if the total is not equal to total claim, set status to incomplete 
            else if (totalsubmitted > 0)
            {
                status_claim.Text = "Incomplete";
            }
            else if(status_claim.Text == "Others")
            {
                status_claim.Text = "Others";
            }
            else
            {
                status_claim.Text = "Pending";
            }


            //if user leave the form column become empty(null)
            CheckNullFormColumn();

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE claim_record SET supplier_name='" + UpdateSupplierName.Text + "', status_claim='" + status_claim.Text + "', date_claim_submit='" + date_claim.Text + "',claim_remarks='" + claim_remarks.Text + "',total_amount_claim='" + total_amount.Text + "',total_claim='" + total_claim.Text + "',first_total_amount='" + amount_1st.Text + "',first_quantity='" + quantity_1st.Text + "', first_approve_amount='" + approved_amount_1st.Text + "', first_approve_quantity='" + approved_1st.Text + "',first_reject_amount='" + rejected_amount_1st.Text + "', first_reject_quantity='" + rejected_1st.Text + "', first_pending_amount='" + pending_amount_1st.Text + "',first_pending_quantity='" + pending_1st.Text + "',first_submit_date='" + date_1st.Text + "',first_remarks='" + remarks_1st.Text + "',second_total_amount='" + amount_2nd.Text + "', second_quantity='" + quantity_2nd.Text + "', second_approve_amount='" + amount_approved_2nd.Text + "',second_approve_quantity='" + approved_2nd.Text + "',second_reject_amount='" + amount_rejected_2nd.Text + "', second_reject_quantity='" + rejected_2nd.Text + "',second_pending_amount='" + pending_amount_2nd.Text + "',second_pending_quantity='" + pending_2nd.Text + "',second_submit_date='" + date_2nd.Text + "',second_remarks='" + remarks_2nd.Text + "',third_total_amount='" + amount_3rd.Text + "',third_quantity='" + quantity_3rd.Text + "',third_approve_amount='" + amount_approved_3rd.Text + "',third_approve_quantity='" + approved_3rd.Text + "',third_reject_amount='" + amount_rejected_3rd.Text + "',third_reject_quantity='" + rejected_3rd.Text + "',third_pending_amount='" + pending_amount_3rd.Text + "',third_pending_quantity='" + pending_3rd.Text + "',third_submit_date='" + date_3rd.Text + "',third_remarks='" + remarks_3rd.Text + "' WHERE month_year='" + UpdateMonthTextBox.Text + "' AND supplier_code='" + UpdateSupplierCode.Text + "' AND supplier_type='" + supplier_type1.Text + "' ";

            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('The details has been updated.')</script>");
            Response.AddHeader("REFRESH", "1;URL=EditRecordLP");
        }



        protected void btndelete_Click(object sender, EventArgs e)
        {
            //Delete record
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM claim_record WHERE month_year='"+ UpdateMonthTextBox.Text + "' AND supplier_code='"+ UpdateSupplierCode.Text + "' AND status_claim='"+ status_claim.Text + "' ";
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('The details has been deleted.')</script>");//Confirmation Page
            Response.AddHeader("REFRESH", "1;URL=EditRecordLP");//Refresh page
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Show page number below the table
            PageNumber.Visible = true;
            GridView1.PageIndex = e.NewPageIndex;//Get index in the table for page number reference
            SqlCommand cmd = new SqlCommand("Select * from claim_record where month_year='" + DropDownList3.Text + "' AND supplier_type='LP' ORDER BY status_claim DESC,supplier_code ASC ", con);
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

        
        protected void CalculationFirst()
        {
            //if user leave the form column become empty(null)
            CheckNullFormColumn();

            //this method called to calculate the first total amount pending when the date submission are set only
            decimal totalamount = Convert.ToDecimal(total_amount.Text);
            decimal firstapproveamount = Convert.ToDecimal(approved_amount_1st.Text);
            decimal firstrejectamount = Convert.ToDecimal(rejected_amount_1st.Text);
            decimal firstpendingamount = totalamount - (firstapproveamount + firstrejectamount);
            pending_amount_1st.Text = firstpendingamount.ToString();
            
            //this method called to calculate the first total quantity pending
            int totalclaim = Convert.ToInt32(total_claim.Text);
            int firstapprove = Convert.ToInt32(approved_1st.Text);
            int firstreject = Convert.ToInt32(rejected_1st.Text);
            int firstpending = totalclaim - (firstapprove + firstreject);
            pending_1st.Text = firstpending.ToString();

            //set the total amount in first submission (approve + reject)
            decimal first_amount_submitted = firstapproveamount + firstrejectamount;
            amount_1st.Text = Convert.ToString(first_amount_submitted);

            //set the total case in first submission (approve + reject)
            int firstcasesubmitted = firstapprove + firstreject;
            quantity_1st.Text = Convert.ToString(firstcasesubmitted);
            

        }

        protected void CalculationSecond()
        {
            //if user leave the form column become empty(null)
            CheckNullFormColumn();

            //this method called to calculate the second total amount pending when the date submission are set only
            decimal secondtotalamount = Convert.ToDecimal(pending_amount_1st.Text);
            decimal secondapproveamount = Convert.ToDecimal(amount_approved_2nd.Text);
            decimal secondrejectamount = Convert.ToDecimal(amount_rejected_2nd.Text);
            decimal secondpendingamount = secondtotalamount - (secondapproveamount + secondrejectamount);
            pending_amount_2nd.Text = secondpendingamount.ToString();

            //set the total amount in second submission (approve + reject)
            decimal second_amount_submitted = secondapproveamount + secondrejectamount;
            amount_2nd.Text = Convert.ToString(second_amount_submitted);

            //this method called to calculate the second quantity pending
            int secondtotal = Convert.ToInt32(pending_1st.Text);
            int secondapprove = Convert.ToInt32(approved_2nd.Text);
            int secondreject = Convert.ToInt32(rejected_2nd.Text);

            int secondpending = secondtotal - (secondapprove + secondreject);
            pending_2nd.Text = secondpending.ToString();

            //set the total case in second submission (approve + reject)
            int second_case_submitted = secondapprove + secondreject;
            quantity_2nd.Text = Convert.ToString(second_case_submitted);
        }

        protected void CalculationThird()
        {
            //if user leave the form column become empty(null)
            CheckNullFormColumn();

            //this method called to calculate the third total amount pending when the date submission are set only
            decimal thirdtotalamount = Convert.ToDecimal(pending_amount_2nd.Text);
            decimal thirdapproveamount = Convert.ToDecimal(amount_approved_3rd.Text);
            decimal thirdrejectamount = Convert.ToDecimal(amount_rejected_3rd.Text);
            decimal thirdpendingamount = thirdtotalamount - (thirdapproveamount + thirdrejectamount);
            pending_amount_3rd.Text = thirdpendingamount.ToString();

            //set the total amount in third submission (approve + reject)
            decimal third_amount_submitted = thirdapproveamount + thirdrejectamount;
            amount_3rd.Text = Convert.ToString(third_amount_submitted);

            //this method called to calculate the third total quantity pending
            int thirdtotal = Convert.ToInt32(pending_2nd.Text);
            int thirdapprove = Convert.ToInt32(approved_3rd.Text);
            int thirdreject = Convert.ToInt32(rejected_3rd.Text);
            int thirdpending = thirdtotal - (thirdapprove + thirdreject);
            pending_3rd.Text = thirdpending.ToString();

            //set the total case in third submission (approve + reject)
            decimal third_case_submitted = thirdapprove + thirdreject;
           quantity_3rd.Text = Convert.ToString(third_case_submitted);

        }

        //This method executed when gridview is accepting data changing
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)//set format for every cell/row
            { 
                //Set colour of status
                if (e.Row.Cells[4].Text == "Pending")
                {
                    e.Row.Cells[4].CssClass = "status-color1";//status-color1 is a css class
                }
                else if (e.Row.Cells[4].Text == "Incomplete")
                {
                    e.Row.Cells[4].CssClass = "status-color2";
                }
                else if (e.Row.Cells[4].Text == "Completed")
                {
                    e.Row.Cells[4].CssClass = "status-color3";
                }

                try//Showing the date format in day/month/year setting it through cells number
                {
                    if ((e.Row.Cells[17].Text != null) && (e.Row.Cells[27].Text != string.Empty) && (e.Row.Cells[37].Text != string.Empty))
                    {
                        e.Row.Cells[5].Text = Convert.ToDateTime(e.Row.Cells[5].Text).ToString("dd/MM/yyyy");
                        e.Row.Cells[17].Text = Convert.ToDateTime(e.Row.Cells[17].Text).ToString("dd/MM/yyyy");
                        e.Row.Cells[27].Text = Convert.ToDateTime(e.Row.Cells[27].Text).ToString("dd/MM/yyyy");
                        e.Row.Cells[37].Text = Convert.ToDateTime(e.Row.Cells[37].Text).ToString("dd/MM/yyyy");
                    }
                }
                //If the date has null/empty string/unsuitable format, set to the original value.
                catch (FormatException)
                {
                    e.Row.Cells[5].Text = e.Row.Cells[5].Text;
                    e.Row.Cells[7].Text = e.Row.Cells[7].Text;
                    e.Row.Cells[17].Text = e.Row.Cells[17].Text;
                    e.Row.Cells[27].Text = e.Row.Cells[27].Text;
                    e.Row.Cells[37].Text = e.Row.Cells[37].Text;

                }

            }

            //Rename column header
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "Application Month";
                e.Row.Cells[2].Text = "Supplier Code";
                e.Row.Cells[3].Text = "Supplier Name";
                e.Row.Cells[4].Text = "Status Claim";
                e.Row.Cells[5].Text = "Collection Date";
                e.Row.Cells[6].Text = "Remarks";
                e.Row.Cells[7].Text = "Total Amount(RM)";
                e.Row.Cells[8].Text = "Quantity Cases";

                e.Row.Cells[9].Text = "1st Submission Total Amount(RM)";
                e.Row.Cells[10].Text = "1st Submission Cases";
                e.Row.Cells[11].Text = "1st Submission Approved Amount (RM)";
                e.Row.Cells[12].Text = "1st Submission Approved Cases";
                e.Row.Cells[13].Text = "1st Submission Reject Amount (RM)";
                e.Row.Cells[14].Text = "1st Submission Reject Cases";
                e.Row.Cells[15].Text = "1st Submission Pending Amount(RM)";
                e.Row.Cells[16].Text = "1st Submission Pending Cases";
                e.Row.Cells[17].Text = "1st Submission Date";
                e.Row.Cells[18].Text = "1st Submission Remark";

                e.Row.Cells[19].Text = "2nd Submission Total Amount(RM)";
                e.Row.Cells[20].Text = "2nd Submission Cases";
                e.Row.Cells[21].Text = "2nd Submission Approved Amount (RM)";
                e.Row.Cells[22].Text = "2nd Submission Approved Cases";
                e.Row.Cells[23].Text = "2nd Submission Reject Amount (RM)";
                e.Row.Cells[24].Text = "2nd Submission Reject Cases";
                e.Row.Cells[25].Text = "2nd Submission Pending Amount(RM)";
                e.Row.Cells[26].Text = "2nd Submission Pending Cases";
                e.Row.Cells[27].Text = "2nd Submission Date";
                e.Row.Cells[28].Text = "2nd Submission Remark";

                e.Row.Cells[29].Text = "3rd Submission Total Amount(RM)";
                e.Row.Cells[30].Text = "3rd Submission Cases";
                e.Row.Cells[31].Text = "3rd Submission Approved Amount (RM)";
                e.Row.Cells[32].Text = "3rd Submission Approved Cases";
                e.Row.Cells[33].Text = "3rd Submission Reject Amount (RM)";
                e.Row.Cells[34].Text = "3rd Submission Reject Cases";
                e.Row.Cells[35].Text = "3rd Submission Pending Amount(RM)";
                e.Row.Cells[36].Text = "3rd Submission Pending Cases";
                e.Row.Cells[37].Text = "3rd Submission Date";
                e.Row.Cells[38].Text = "3rd Submission Remark";
                e.Row.Cells[39].Text = "Supplier Type";
                //e.Row.Cells[].Text = "";
            }
        }

        protected void CheckNullFormColumn()//if user leave the form column become empty(null)
        {
            if (approved_amount_1st.Text == string.Empty)
            {
                approved_amount_1st.Text = "0.00";
            }
            if (approved_1st.Text == string.Empty)
            {
                approved_1st.Text = "0";
            }
            if (rejected_amount_1st.Text == string.Empty)
            {
                rejected_amount_1st.Text = "0.00";
            }
            if (rejected_1st.Text == string.Empty)
            {
                rejected_1st.Text = "0";
            }
            if (amount_approved_2nd.Text == string.Empty)
            {
                amount_approved_2nd.Text = "0.00";
            }
            if (approved_2nd.Text == string.Empty)
            {
                approved_2nd.Text = "0";
            }
            if (amount_rejected_2nd.Text == string.Empty)
            {
                amount_rejected_2nd.Text = "0.00";
            }
            if (rejected_2nd.Text == string.Empty)
            {
                rejected_2nd.Text = "0";
            }
            if (amount_approved_3rd.Text == string.Empty)
            {
                amount_approved_3rd.Text = "0.00";
            }
            if (approved_3rd.Text == string.Empty)
            {
                approved_3rd.Text = "0";
            }
            if (amount_rejected_3rd.Text == string.Empty)
            {
                amount_rejected_3rd.Text = "0.00";
            }
            if (rejected_3rd.Text == string.Empty)
            {
                rejected_3rd.Text = "0";
            }

        }


        protected void AmountCalcDisplay()
        {
            //Setvalue of javascript
            total_amount.Attributes.Add("onkeyup", "GetTotal();");
            approved_amount_1st.Attributes.Add("onkeyup", "GetTotal();");
            rejected_amount_1st.Attributes.Add("onkeyup", "GetTotal();");
            amount_1st.Attributes.Add("onkeyup", "GetTotal();");
            pending_amount_1st.Attributes.Add("onkeyup", "GetTotal();");
            total_claim.Attributes.Add("onkeyup", "GetTotal();");
            approved_1st.Attributes.Add("onkeyup", "GetTotal();");
            rejected_1st.Attributes.Add("onkeyup", "GetTotal();");
            quantity_1st.Attributes.Add("onkeyup", "GetTotal();");
            pending_1st.Attributes.Add("onkeyup", "GetTotal();");

            amount_approved_2nd.Attributes.Add("onkeyup", "GetTotal();");
            amount_rejected_2nd.Attributes.Add("onkeyup", "GetTotal();");
            amount_2nd.Attributes.Add("onkeyup", "GetTotal();");
            pending_amount_2nd.Attributes.Add("onkeyup", "GetTotal();");
            approved_2nd.Attributes.Add("onkeyup", "GetTotal();");
            rejected_2nd.Attributes.Add("onkeyup", "GetTotal();");
            quantity_2nd.Attributes.Add("onkeyup", "GetTotal();");
            pending_2nd.Attributes.Add("onkeyup", "GetTotal();");

            amount_approved_3rd.Attributes.Add("onkeyup", "GetTotal();");
            amount_rejected_3rd.Attributes.Add("onkeyup", "GetTotal();");
            amount_3rd.Attributes.Add("onkeyup", "GetTotal();");
            pending_amount_3rd.Attributes.Add("onkeyup", "GetTotal();");
            approved_3rd.Attributes.Add("onkeyup", "GetTotal();");
            rejected_3rd.Attributes.Add("onkeyup", "GetTotal();");
            quantity_3rd.Attributes.Add("onkeyup", "GetTotal();");
            pending_3rd.Attributes.Add("onkeyup", "GetTotal();");



        }


        
    }
}