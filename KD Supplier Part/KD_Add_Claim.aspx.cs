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
    public partial class KD_Add_Claim : System.Web.UI.Page
    {

        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            AmountCalcDisplay();
        }

        protected void DropDownMonthYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Set column Collection Date from the application month selected
            con.Open();
            SqlCommand DropDownMonthToCollectDate = new SqlCommand("select collection_date from month_detail where month_year='" + DropDownMonthYear.Text + "' AND supplier_type='KD' ", con);
            DropDownMonthToCollectDate.ExecuteNonQuery();
            String collectdate = Convert.ToDateTime((DropDownMonthToCollectDate.ExecuteScalar())).ToString("yyyy-MM-dd");
            date_claim.Text = collectdate;
            con.Close();
        }

        protected void DropDownSupplierCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand DropDownCodeToSupplierName = new SqlCommand("select supplier_name from supplier_details where supplier_code='" + DropDownSupplierCode.Text + "' AND supplier_type='KD' ", con);
            DropDownCodeToSupplierName.ExecuteNonQuery();
            String suppliernametextbox = Convert.ToString((DropDownCodeToSupplierName.ExecuteScalar()));
            DropDownSupplierName.SelectedValue = suppliernametextbox;
            con.Close();
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

        protected void AddClaimRecord(object sender, EventArgs e)
        {
            //Detect column amount and quantity, if there is new value, add the amount and calculate
            if (approved_1st.Text != string.Empty && rejected_1st.Text != string.Empty && approved_amount_1st.Text != string.Empty && rejected_amount_1st.Text != string.Empty)
            {
                CalculationFirst();
            }

            if (approved_2nd.Text != string.Empty && rejected_2nd.Text != string.Empty && amount_approved_2nd.Text != string.Empty && amount_rejected_2nd.Text != string.Empty)
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
            else
            {
                status_claim.Text = "Pending";
            }


            //if user leave the form column become empty(null)
            CheckNullFormColumn();

            try
            {


                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into claim_record(month_year,supplier_code,supplier_name,status_claim,date_claim_submit,claim_remarks,total_amount_claim,total_claim,first_total_amount,first_quantity,first_approve_amount,first_approve_quantity,first_reject_amount,first_reject_quantity,first_pending_amount,first_pending_quantity,first_submit_date,first_remarks,second_total_amount,second_quantity,second_approve_amount,second_approve_quantity,second_reject_amount,second_reject_quantity,second_pending_amount,second_pending_quantity,second_submit_date,second_remarks,third_total_amount,third_quantity,third_approve_amount,third_approve_quantity,third_reject_amount,third_reject_quantity,third_pending_amount,third_pending_quantity,third_submit_date,third_remarks,supplier_type)"
                    + "values('" + DropDownMonthYear.Text + "','" + DropDownSupplierCode.Text + "','" + DropDownSupplierName.Text + "','" + status_claim.Text + "','" + date_claim.Text + "','" + claim_remarks.Text + "','" + total_amount.Text + "','" + total_claim.Text + "','" + amount_1st.Text + "','" + quantity_1st.Text + "','" + approved_amount_1st.Text + "','" + approved_1st.Text + "','" + rejected_amount_1st.Text + "','" + rejected_1st.Text + "','" + pending_amount_1st.Text + "','" + pending_1st.Text + "','" + date_1st.Text + "','" + remarks_1st.Text + "','" + amount_2nd.Text + "','" + quantity_2nd.Text + "','" + amount_approved_2nd.Text + "','" + approved_2nd.Text + "','" + amount_rejected_2nd.Text + "','" + rejected_2nd.Text + "','" + pending_amount_2nd.Text + "','" + pending_2nd.Text + "','" + date_2nd.Text + "','" + remarks_2nd.Text + "','" + amount_3rd.Text + "','" + quantity_3rd.Text + "','" + amount_approved_3rd.Text + "','" + approved_3rd.Text + "','" + amount_rejected_3rd.Text + "','" + rejected_3rd.Text + "','" + pending_amount_3rd.Text + "','" + pending_3rd.Text + "','" + date_3rd.Text + "','" + remarks_3rd.Text + "','" + supplier_type1.Text + "')";

                cmd.ExecuteNonQuery();

                status_claim.Text = "";
                date_claim.Text = "";
                claim_remarks.Text = "";
                total_claim.Text = "";
                total_amount.Text = "";
                amount_1st.Text = "";
                quantity_1st.Text = "";
                approved_amount_1st.Text = "";
                approved_1st.Text = "";
                rejected_amount_1st.Text = "";
                rejected_1st.Text = " ";
                pending_amount_1st.Text = " ";
                pending_1st.Text = "";
                date_1st.Text = "";
                remarks_1st.Text = "";
                amount_2nd.Text = "";
                quantity_2nd.Text = "";
                amount_approved_2nd.Text = "";
                approved_2nd.Text = "";
                amount_rejected_2nd.Text = "";
                rejected_2nd.Text = "";
                pending_amount_2nd.Text = "";
                pending_2nd.Text = "";
                date_2nd.Text = "";
                remarks_2nd.Text = "";
                amount_3rd.Text = "";
                quantity_3rd.Text = "";
                amount_approved_3rd.Text = "";
                approved_3rd.Text = "";
                amount_rejected_3rd.Text = "";
                rejected_3rd.Text = "";
                pending_amount_3rd.Text = "";
                pending_3rd.Text = "";
                date_3rd.Text = "";
                remarks_3rd.Text = "";
                supplier_type1.Text = "";

                con.Close();
                Response.Write("<script>alert('The record has been added.')</script>");
                Response.AddHeader("REFRESH", "1;URL=KD_Add_Claim");
            }
            catch (Exception)
            {
                con.Close();
                Response.Write("<script>alert('Data Duplicate/Error.')</script>");
                Response.AddHeader("REFRESH", "1;URL=KD_Add_Claim");
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

    }
}