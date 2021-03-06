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
    public partial class KD_Summary : System.Web.UI.Page
    {

        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (YearDropDown.Text == string.Empty)
            {
                if (Session["monthKD"] != null)
                {
                    //Checking month value exist in record or not
                    con.Open();
                    string sessioname = Session["monthKD"].ToString();
                    SqlCommand checkSession = new SqlCommand("select count(month_year) from month_detail where month_year='" + sessioname + "' AND supplier_type='KD'", con);
                    int totalrec0rdmonth = Convert.ToInt32(checkSession.ExecuteScalar());
                    con.Close();

                    if (totalrec0rdmonth == 1)
                    {
                        //If the record month is found, then it will execute session set up
                        YearDropDown.Text = Session["monthKD"].ToString();
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

        protected void YearDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            Session["monthKD"] = YearDropDown.Text;
            YearDropDown.Text = Session["monthKD"].ToString();

            SessionBind();
        }

        protected void SessionBind()
        {

            //Getting session value to substitute with a variable for sql query
            String currentMonth = Session["monthKD"].ToString();

            //Display Page Title for Month Label
            con.Open();
            SqlCommand page_title = new SqlCommand("select month_year from month_detail where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
            page_title.ExecuteNonQuery();
            String pagetitle = Convert.ToString((page_title.ExecuteScalar()));
            SummaryLabel.Text = pagetitle;
            con.Close();

            try
            {
                //PieChartDataSource
                SqlCommand cmdChartLP = new SqlCommand("select  status_claim,count(status_claim) as Status1 from claim_record where supplier_type='KD' AND month_year='" + currentMonth + "' group by status_claim ", con);
                SqlDataAdapter daChartLP = new SqlDataAdapter(cmdChartLP);
                DataSet dsChartLP = new DataSet();
                daChartLP.Fill(dsChartLP);
                Chart1.DataSource = dsChartLP;

                //Total Status Claim
                con.Open();
                SqlCommand totalstatusrecord = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                double ttlStatus = Convert.ToDouble((totalstatusrecord.ExecuteScalar()));
                con.Close();

                //Percentage Pending
                con.Open();
                SqlCommand totalpending = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + currentMonth + "' AND status_claim='Pending'  AND supplier_type='KD' ", con);
                double numPending = Convert.ToDouble((totalpending.ExecuteScalar()));
                double percentPending = (numPending / ttlStatus) * 100;
                LblPercentPending.Text = percentPending.ToString("N2") + "%";
                con.Close();

                //Percentage Incomplete
                con.Open();
                SqlCommand totalincomplete = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + currentMonth + "' AND status_claim='Incomplete'  AND supplier_type='KD'", con);
                double numIncomplete = Convert.ToDouble((totalincomplete.ExecuteScalar()));
                double percentIncomplete = (numIncomplete / ttlStatus) * 100;
                LblPercentIncomplete.Text = percentIncomplete.ToString("n2") + "%";
                con.Close();

                //Percentage Completed
                con.Open();
                SqlCommand totalcompleted = new SqlCommand("Select count(status_claim) from claim_record where month_year='" + currentMonth + "' AND status_claim='Completed'  AND supplier_type='KD' ", con);
                double numCompleted = Convert.ToDouble((totalcompleted.ExecuteScalar()));
                double percentCompleted = numCompleted / ttlStatus * 100;
                LblPercentCompleted.Text = percentCompleted.ToString("n2") + "%";
                con.Close();
            }
            catch (Exception)
            {
                LblPercentCompleted.Text = "-"; LblPercentIncomplete.Text = "-"; LblPercentPending.Text = "-";
            }

            //Display Total All KD Amount(RM)
            con.Open();
            try
            {
                SqlCommand total_all_amount = new SqlCommand("select sum(total_amount_claim) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                total_all_amount.ExecuteNonQuery();
                decimal totalallamount = Convert.ToDecimal((total_all_amount.ExecuteScalar()));
                String totalallamount1 = Convert.ToDecimal(totalallamount).ToString("c2");
                KDAmountAll.Text = totalallamount1;
            }
            catch (Exception)
            {
                KDAmountAll.Text = "-";
            }
            con.Close();

            //Display Total All KD Quantity
            con.Open();
            try
            {
                SqlCommand total_all_quantity = new SqlCommand("select sum(total_claim) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                total_all_quantity.ExecuteNonQuery();
                String totalallquantity = Convert.ToString((total_all_quantity.ExecuteScalar()));
                KDQuantityAll.Text = Convert.ToDecimal(totalallquantity).ToString("n0");
            }
            catch (Exception)
            {
                KDQuantityAll.Text = "-";
            }            
            con.Close();

            //Display Total KD Approve Amount(RM)
            con.Open();
            try
            {
                SqlCommand total_approve_amount = new SqlCommand("select sum(first_approve_amount+second_approve_amount+third_approve_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                total_approve_amount.ExecuteNonQuery();
                decimal totalapproveamount = Convert.ToDecimal((total_approve_amount.ExecuteScalar()));
                String totalapproveamount1 = Convert.ToDecimal(totalapproveamount).ToString("c2");
                Total_Amount.Text = totalapproveamount1;
            }
            catch (Exception)
            {
                Total_Amount.Text = "-";
            }
            con.Close();

            //Display Total KD Quantity Approve
            con.Open();
            try
            {
                SqlCommand total_approve_quantity = new SqlCommand("select sum(first_approve_quantity+second_approve_quantity+third_approve_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                total_approve_quantity.ExecuteNonQuery();
                String totalquantityapprove = Convert.ToString((total_approve_quantity.ExecuteScalar()));
                Total_Quantity_Approve.Text = Convert.ToDecimal(totalquantityapprove).ToString("n0");
            }
            catch (Exception)
            {
                Total_Quantity_Approve.Text = "-";
            }            
            con.Close();

            //Display Total KD Amount(RM) Rejected
            con.Open();
            try
            {
                SqlCommand total_rejected_amount = new SqlCommand("select sum(first_reject_amount+second_reject_amount+third_reject_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                total_rejected_amount.ExecuteNonQuery();
                decimal totalrejectedamount = Convert.ToDecimal((total_rejected_amount.ExecuteScalar()));
                String totalrejectedamount1 = Convert.ToDecimal(totalrejectedamount).ToString("c2");
                Total_Amount_Reject.Text = totalrejectedamount1;
            }
            catch (Exception)
            {
                Total_Amount_Reject.Text = "-";
            }
            con.Close();

            //Display Total KD Quantity Rejected
            con.Open();
            try
            {
                SqlCommand total_rejected_quantity = new SqlCommand("select sum(first_reject_quantity+second_reject_quantity+third_reject_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                total_rejected_quantity.ExecuteNonQuery();
                String totalrejectedquantity = Convert.ToString((total_rejected_quantity.ExecuteScalar()));
                Total_Quantity_Reject.Text = Convert.ToDecimal(totalrejectedquantity).ToString("n0");
            }
            catch (Exception)
            {
                Total_Quantity_Reject.Text = "-";
            }            
            con.Close();

            //Display Total KD Amount(RM) Pending
            con.Open();
            try
            {
                SqlCommand total_approvereject_amount = new SqlCommand("select sum(first_approve_amount+second_approve_amount+third_approve_amount+first_reject_amount+second_reject_amount+third_reject_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con); //Total Approve + Reject Amount 1st,2nd,3rd LP
                decimal totalapproverejectamount = Convert.ToDecimal((total_approvereject_amount.ExecuteScalar()));//Get All Value replied claim from supplier (Approve + Reject)
                SqlCommand total_all_amount = new SqlCommand("select sum(total_amount_claim) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con); //Total Approve + Reject 1st,2nd,3rd LP
                decimal totalallamount = Convert.ToDecimal((total_all_amount.ExecuteScalar()));//Get Value Amount Claim Submitted to KD Supplier

                //Total claim to supplier - claim (Approve + Reject) replied from supplier
                String totalpendingamount1 = (totalallamount - totalapproverejectamount).ToString("c2");
                Total_Amount_Pending.Text = totalpendingamount1;
            }
            catch (Exception)
            {
                Total_Amount_Pending.Text = "-";
            }
            con.Close();

            //Display Total KD Quantity Case Pending
            con.Open();
            try
            {
                SqlCommand total_approvereject_case = new SqlCommand("select sum(first_approve_quantity+second_approve_quantity+third_approve_quantity+first_reject_quantity+second_reject_quantity+third_reject_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);//Total Approve + Reject Case 1st,2nd,3rd LP
                SqlCommand total_all_case = new SqlCommand("select sum (total_claim) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);//Total Approve + Reject Case 1st,2nd,3rd LP

                int total_approverejectcase = Convert.ToInt32((total_approvereject_case.ExecuteScalar()));//Value Approve+reject Case
                int total_allcase = Convert.ToInt32((total_all_case.ExecuteScalar()));//Total All Case

                String totalpendingcase1 = (total_allcase - total_approverejectcase).ToString("N0");//Total All Case - (Value Approve + Reject)


                Total_Quantity_Pending.Text = totalpendingcase1;
            }
            catch (Exception)
            {
                Total_Quantity_Pending.Text = "-";
            }            
            con.Close();

            //3-Card Stacked Display
            //Display KD 1st Total Amount(RM)
            con.Open();
            try
            {
                SqlCommand first_total_amount = new SqlCommand("select sum(first_total_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                first_total_amount.ExecuteNonQuery();
                decimal firsttotalamount = Convert.ToDecimal((first_total_amount.ExecuteScalar()));
                String firsttotalamount1 = Convert.ToDecimal(firsttotalamount).ToString("c2");
                TotalAmountFirst.Text = firsttotalamount1;
            }
            catch (Exception)
            {
                TotalAmountFirst.Text = "-";
            }
            con.Close();

            //Display KD 1st Total Quantity
            con.Open();
            try
            {
                SqlCommand first_total_quantity = new SqlCommand("select sum(first_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                first_total_quantity.ExecuteNonQuery();
                String firsttotalquantity = Convert.ToString((first_total_quantity.ExecuteScalar()));
                TotalQuantityFirst.Text = firsttotalquantity;
            }
            catch (Exception)
            {
                TotalQuantityFirst.Text = "-";
            }            
            con.Close();

            //Display KD 1st Total Amount Approved
            con.Open();
            try
            {
                SqlCommand first_total_approved_amount = new SqlCommand("select sum(first_approve_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                first_total_approved_amount.ExecuteNonQuery();
                decimal firsttotal_approved_amount = Convert.ToDecimal((first_total_approved_amount.ExecuteScalar()));
                String firsttotal_approved_amount1 = Convert.ToDecimal(firsttotal_approved_amount).ToString("c2");
                TotalApprovedFirst.Text = firsttotal_approved_amount1;
            }
            catch (Exception)
            {
                TotalApprovedFirst.Text = "-";
            }
            con.Close();

            //Display KD 1st Total Approved Quantity
            con.Open();
            try
            {
                SqlCommand first_total_approved_quantity = new SqlCommand("select sum(first_approve_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                first_total_approved_quantity.ExecuteNonQuery();
                String firsttotal_approved_quantity = Convert.ToString((first_total_approved_quantity.ExecuteScalar()));
                TotalApprovedQuantityFirst.Text = firsttotal_approved_quantity;
            }
            catch (Exception)
            {
                TotalApprovedQuantityFirst.Text = "-";
            }
            con.Close();

            //Display KD 1st Total Reject Amount
            con.Open();
            try
            {
                SqlCommand first_total_amount_rejected = new SqlCommand("select sum(first_reject_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                first_total_amount_rejected.ExecuteNonQuery();
                decimal firsttotal_amount_rejected = Convert.ToDecimal((first_total_amount_rejected.ExecuteScalar()));
                String firsttotal_amount_rejected1 = Convert.ToDecimal(firsttotal_amount_rejected).ToString("c2");
                TotalRejectedFirst.Text = firsttotal_amount_rejected1;
            }
            catch (Exception)
            {
                TotalRejectedFirst.Text = "-";
            }
            con.Close();

            //Display KD 1st Total Reject Quantity
            con.Open();
            try
            {
                SqlCommand first_total_amount_quantity = new SqlCommand("select sum(first_reject_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                first_total_amount_quantity.ExecuteNonQuery();
                String firsttotal_amount_quantity = Convert.ToString((first_total_amount_quantity.ExecuteScalar()));
                TotalRejectedQuantityFirst.Text = firsttotal_amount_quantity;
            }
            catch (Exception)
            {
                TotalRejectedQuantityFirst.Text = "-";
            }            
            con.Close();

            //Display KD 2nd Total Claim Amount
            con.Open();
            try
            {
                SqlCommand second_total_amount = new SqlCommand("select sum(second_total_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                second_total_amount.ExecuteNonQuery();
                decimal secondtotalamount = Convert.ToDecimal((second_total_amount.ExecuteScalar()));
                String secondtotalamount1 = Convert.ToDecimal(secondtotalamount).ToString("c2");
                TotalAmountSecond.Text = secondtotalamount1;
            }
            catch (Exception)
            {
                TotalAmountSecond.Text = "-";
            }
            con.Close();

            //Display KD 2nd Total Claim Quantity
            con.Open();
            try
            {
                SqlCommand second_total_quantity = new SqlCommand("select sum(second_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                second_total_quantity.ExecuteNonQuery();
                String secondtotalquantity = Convert.ToString((second_total_quantity.ExecuteScalar()));
                TotalQuantitySecond.Text = secondtotalquantity;
            }
            catch (Exception)
            {
                TotalQuantitySecond.Text = "-";
            }
            con.Close();

            //Display KD 2nd Total Approved Amount
            con.Open();
            try
            {
                SqlCommand second_total_approved_amount = new SqlCommand("select sum(second_approve_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                second_total_approved_amount.ExecuteNonQuery();
                decimal secondtotal_approved_amount = Convert.ToDecimal((second_total_approved_amount.ExecuteScalar()));
                String secondtotal_approved_amount1 = Convert.ToDecimal(secondtotal_approved_amount).ToString("c2");
                TotalApprovedSecond.Text = secondtotal_approved_amount1;
            }
            catch (Exception)
            {
                TotalApprovedSecond.Text = "-";
            }
            con.Close();

            //Display KD 2nd Total Approved Quantity
            con.Open();
            try
            {
                SqlCommand second_total_approved_quantity = new SqlCommand("select sum(second_approve_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                second_total_approved_quantity.ExecuteNonQuery();
                String secondtotal_approved_quantity = Convert.ToString((second_total_approved_quantity.ExecuteScalar()));
                TotalApprovedQuantitySecond.Text = secondtotal_approved_quantity;
            }
            catch (Exception)
            {
                TotalApprovedQuantitySecond.Text = "-";
            }
            con.Close();

            //Display KD 2nd Total Reject Amount
            con.Open();
            try
            {
                SqlCommand second_total_reject_amount = new SqlCommand("select sum(second_reject_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                second_total_reject_amount.ExecuteNonQuery();
                decimal secondtotal_reject_amount = Convert.ToDecimal((second_total_reject_amount.ExecuteScalar()));
                String secondtotal_reject_amount1 = Convert.ToDecimal(secondtotal_reject_amount).ToString("c2");
                TotalRejectedSecond.Text = secondtotal_reject_amount1;
            }
            catch (Exception)
            {
                TotalRejectedSecond.Text = "-";
            }
            con.Close();

            //Display KD 2nd Total Reject Quantity
            con.Open();
            try
            {
                SqlCommand second_total_reject_quantity = new SqlCommand("select sum(second_reject_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                second_total_reject_quantity.ExecuteNonQuery();
                String secondtotal_reject_quantity = Convert.ToString((second_total_reject_quantity.ExecuteScalar()));
                TotalRejectedQuantitySecond.Text = secondtotal_reject_quantity;
            }
            catch (Exception)
            {
                TotalRejectedQuantitySecond.Text = "-";
            }
            con.Close();

            //Display KD 3rd Total Amount
            con.Open();
            try
            {
                SqlCommand third_total_amount = new SqlCommand("select sum(third_total_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                third_total_amount.ExecuteNonQuery();
                decimal thirdtotalamount = Convert.ToDecimal((third_total_amount.ExecuteScalar()));
                String thirdtotalamount1 = Convert.ToDecimal(thirdtotalamount).ToString("c2");
                TotalAmountThird.Text = thirdtotalamount1;
            }
            catch (Exception)
            {
                TotalAmountThird.Text = "-";
            }
            con.Close();

            //Display KD 3rd Total Quantity
            con.Open();
            try
            {
                SqlCommand third_total_quantity = new SqlCommand("select sum(third_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                third_total_quantity.ExecuteNonQuery();
                String thirdtotalquantity = Convert.ToString((third_total_quantity.ExecuteScalar()));
                TotalQuantityThird.Text = thirdtotalquantity;
            }
            catch (Exception)
            {
                TotalQuantityThird.Text = "-";
            }            
            con.Close();

            //Display KD 3rd Total Approved Amount
            con.Open();
            try
            {
                SqlCommand third_total_approved_amount = new SqlCommand("select sum(third_approve_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                third_total_approved_amount.ExecuteNonQuery();
                decimal thirdtotal_approved_amount = Convert.ToDecimal((third_total_approved_amount.ExecuteScalar()));
                String thirdtotal_approved_amount1 = Convert.ToDecimal(thirdtotal_approved_amount).ToString("c2");
                TotalApprovedThird.Text = thirdtotal_approved_amount1;
            }
            catch (Exception)
            {
                TotalApprovedThird.Text = "-";
            }
            con.Close();

            //Display KD 3rd Total Approved Quantity
            con.Open();
            try
            {
                SqlCommand third_total_approved_quantity = new SqlCommand("select sum(third_approve_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                third_total_approved_quantity.ExecuteNonQuery();
                String thirdtotal_approved_quantity = Convert.ToString((third_total_approved_quantity.ExecuteScalar()));
                TotalApprovedQuantityThird.Text = thirdtotal_approved_quantity;
            }
            catch (Exception)
            {
                TotalApprovedQuantityThird.Text = "-";
            }            
            con.Close();

            //Display KD 3rd Total Rejected Amount
            con.Open();
            try
            {
                SqlCommand third_total_rejected_amount = new SqlCommand("select sum(third_reject_amount) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                third_total_rejected_amount.ExecuteNonQuery();
                decimal thirdtotal_rejected_amount = Convert.ToDecimal((third_total_rejected_amount.ExecuteScalar()));
                String thirdtotal_rejected_amount1 = Convert.ToDecimal(thirdtotal_rejected_amount).ToString("c2");
                TotalRejectThird.Text = thirdtotal_rejected_amount1;
            }
            catch (Exception)
            {
                TotalRejectThird.Text = "-";
            }
            con.Close();

            //Display KD 3rd Total Rejected Quantity
            con.Open();
            try
            {
                SqlCommand third_total_rejected_quantity = new SqlCommand("select sum(third_reject_quantity) from claim_record where month_year='" + currentMonth + "'  AND supplier_type='KD' ", con);
                third_total_rejected_quantity.ExecuteNonQuery();
                String thirdtotal_rejected_quantity = Convert.ToString((third_total_rejected_quantity.ExecuteScalar()));
                TotalRejectQuantityThird.Text = thirdtotal_rejected_quantity;
            }
            catch (Exception)
            {
                TotalRejectQuantityThird.Text = "-";
            }            
            con.Close();


        }
    }
}