using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MqWebApp2.All_Claim
{
    public partial class All_Claim : System.Web.UI.Page
    {
        //Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownApplicationMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //LP Chart
                SqlCommand cmdChartLP = new SqlCommand("select  status_claim,count(status_claim) as Status1 from claim_record where supplier_type='LP' AND month_year='" + DropDownApplicationMonth.Text + "' group by status_claim ", con);
                SqlDataAdapter daChartLP = new SqlDataAdapter(cmdChartLP);
                DataSet dsChartLP = new DataSet();
                daChartLP.Fill(dsChartLP);
                Chart2.DataSource = dsChartLP;

                //KD Chart
                SqlCommand cmdChartKD = new SqlCommand("select  status_claim,count(status_claim) as Status1 from claim_record where supplier_type='KD' AND month_year='" + DropDownApplicationMonth.Text + "' group by status_claim ", con);
                SqlDataAdapter daChartKD = new SqlDataAdapter(cmdChartKD);
                DataSet dsChartKD = new DataSet();
                daChartKD.Fill(dsChartKD);
                Chart3.DataSource = dsChartKD;

                //Display Grand Total Collected Amount(RM)
                con.Open();
                SqlCommand total_approve_amount = new SqlCommand("select sum(first_approve_amount+second_approve_amount+third_approve_amount) from claim_record where month_year='" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                total_approve_amount.ExecuteNonQuery();
                String totalapproveamount = Convert.ToString((total_approve_amount.ExecuteScalar()));
                if (totalapproveamount == "")
                {

                    GrandTotal.Text = "-";

                }
                else
                {
                    string taa = Convert.ToDecimal(totalapproveamount).ToString("C2");
                    GrandTotal.Text = taa;
                }
                con.Close();

                //Display Grand Total Cases Approved
                con.Open();
                SqlCommand total_case_approve = new SqlCommand("select sum(first_approve_quantity+second_approve_quantity + third_approve_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String totalcaseapprove = Convert.ToString((total_case_approve.ExecuteScalar()));
                if (totalcaseapprove == "")
                {
                    TotalCaseApprove.Text = "-";
                }
                else
                {
                    TotalCaseApprove.Text = totalcaseapprove;
                }
                con.Close();

                //Display First Amount Claimed
                con.Open();
                SqlCommand First_Total_Amount = new SqlCommand("select sum(first_total_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String FirstTotalAmount = Convert.ToString((First_Total_Amount.ExecuteScalar()));
                if (FirstTotalAmount == "")
                {
                    first_total.Text = "-";
                }
                else
                {
                    first_total.Text = Convert.ToDecimal(FirstTotalAmount).ToString("C2");
                }
                con.Close();

                //Display First Total Case 
                con.Open();
                SqlCommand First_Total_Case = new SqlCommand("select sum(first_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String FirstTotalCase = Convert.ToString((First_Total_Case.ExecuteScalar()));
                if (FirstTotalCase == "")
                {
                    first_case.Text = "-";
                }
                else
                {
                    first_case.Text = Convert.ToDecimal(FirstTotalCase).ToString("");
                }
                con.Close();

                //Display First Approve Amount 
                con.Open();
                SqlCommand First_Approve_Amount = new SqlCommand("select sum(first_approve_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String FirstApproveAmount = Convert.ToString((First_Approve_Amount.ExecuteScalar()));
                if (FirstApproveAmount == "")
                {
                    first_approve_amount.Text = "-";
                }
                else
                {
                    first_approve_amount.Text = Convert.ToDecimal(FirstApproveAmount).ToString("C2");
                }

                con.Close();

                //Display First Approve Case 
                con.Open();
                SqlCommand First_Approve_Case = new SqlCommand("select sum(first_approve_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String FirstApproveCase = Convert.ToString((First_Approve_Case.ExecuteScalar()));
                if (FirstApproveCase == "")
                {
                    first_approve_case.Text = "-";
                }
                else
                {
                    first_approve_case.Text = Convert.ToDecimal(FirstApproveCase).ToString("");
                }

                con.Close();


                //Display First Reject Amount 
                con.Open();
                SqlCommand First_Reject_Amount = new SqlCommand("select sum(first_reject_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String FirstRejectAmount = Convert.ToString((First_Reject_Amount.ExecuteScalar()));
                if (FirstRejectAmount == "")
                {
                    first_reject_amount.Text = "-";
                }
                else
                {
                    first_reject_amount.Text = Convert.ToDecimal(FirstRejectAmount).ToString("C2");
                }
                con.Close();

                //Display First Reject case 
                con.Open();
                SqlCommand First_Reject_Case = new SqlCommand("select sum(first_reject_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String FirstRejectCase = Convert.ToString((First_Reject_Case.ExecuteScalar()));
                if (FirstRejectCase == "")
                {
                    first_reject_case.Text = "-";
                }
                else
                {
                    first_reject_case.Text = Convert.ToDecimal(FirstRejectCase).ToString("");
                }
                con.Close();

                //Display First Pending Amount 
                con.Open();
                SqlCommand First_Pending_Amount = new SqlCommand("select sum(first_approve_amount+first_reject_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String AppRejectAmount = Convert.ToString(First_Pending_Amount.ExecuteScalar());
                SqlCommand Total_AmountLPKD = new SqlCommand("select sum(total_amount_claim) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String TotalAmountLPKD = Convert.ToString(Total_AmountLPKD.ExecuteScalar());
                if (AppRejectAmount == "")
                {
                    first_pending_amount.Text = "-";
                }

                Decimal AppRejectAmount2 = Convert.ToDecimal(AppRejectAmount); // Amount Approve + Reject for 1st submission
                Decimal Total1stPending = Convert.ToDecimal(TotalAmountLPKD) - AppRejectAmount2; // Amount Pending (Total Amount Claim - (Approve+Reject 1st submit) )
                first_pending_amount.Text = Convert.ToDecimal(Total1stPending).ToString("C2");

                con.Close();

                //Display First Pending case 
                con.Open();
                SqlCommand First_Pending_Case = new SqlCommand("select sum(first_approve_quantity+first_reject_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String FirstPendingCase = Convert.ToString((First_Pending_Case.ExecuteScalar()));
                SqlCommand Total_CaseLPKD = new SqlCommand("select sum(total_claim) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String TotalCaseLPKD = Convert.ToString((Total_CaseLPKD.ExecuteScalar()));
                if (FirstPendingCase == "")
                {
                    first_pending_case.Text = "-";
                }
                int FirstPendingCase2 = Convert.ToInt32(FirstPendingCase);
                int Total1stPendingCase = Convert.ToInt32(TotalCaseLPKD) - FirstPendingCase2;
                first_pending_case.Text = Convert.ToString(Total1stPendingCase);

                con.Close();

                //Display Second Amount Claimed
                con.Open();
                SqlCommand Second_Total_Amount = new SqlCommand("select sum(second_total_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type='LP' OR supplier_type='KD') ", con);
                String SecondTotalAmount = Convert.ToString((Second_Total_Amount.ExecuteScalar()));
                if (SecondTotalAmount == string.Empty || SecondTotalAmount == null || SecondTotalAmount == "")
                {
                    second_total.Text = "-";
                }
                else
                {
                    second_total.Text = Convert.ToDecimal(SecondTotalAmount).ToString("C2");
                }
                con.Close();

                //Display Second Total Case Claimed
                con.Open();
                SqlCommand Second_Total_Case = new SqlCommand("select sum(second_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String SecondTotalCase = Convert.ToString((Second_Total_Case.ExecuteScalar()));
                if (SecondTotalCase == "")
                {
                    second_case.Text = "-";
                }
                else
                {
                    second_case.Text = Convert.ToDecimal(SecondTotalCase).ToString("");
                }
                con.Close();

                //Display Second Approve Amount 
                con.Open();
                SqlCommand Second_Approve_Amount = new SqlCommand("select sum(second_approve_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String SecondApproveAmount = Convert.ToString((Second_Approve_Amount.ExecuteScalar()));
                if (SecondApproveAmount == "")
                {
                    second_approve_amount.Text = "-";
                }
                else
                {
                    second_approve_amount.Text = Convert.ToDecimal(SecondApproveAmount).ToString("c2");
                }
                con.Close();

                //Display Second Approve Case 
                con.Open();
                SqlCommand Second_Approve_Case = new SqlCommand("select sum(second_approve_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String SecondApproveCase = Convert.ToString((Second_Approve_Case.ExecuteScalar()));
                if (SecondApproveCase == "")
                {
                    second_approve_case.Text = "-";
                }
                else
                {
                    second_approve_case.Text = Convert.ToDecimal(SecondApproveCase).ToString();
                }
                con.Close();

                //Display Second Reject Amount 
                con.Open();
                SqlCommand Second_Reject_Amount = new SqlCommand("select sum(second_reject_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String SecondRejectAmount = Convert.ToString((Second_Reject_Amount.ExecuteScalar()));
                if (SecondRejectAmount == "")
                {
                    second_reject_amount.Text = "-";
                }
                else
                {
                    second_reject_amount.Text = Convert.ToDecimal(SecondRejectAmount).ToString("c2");
                }
                con.Close();

                //Display Second Reject Case 
                con.Open();
                SqlCommand Second_Reject_Case = new SqlCommand("select sum(second_reject_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String SecondRejectCase = Convert.ToString((Second_Reject_Case.ExecuteScalar()));
                if (SecondRejectCase == "")
                {
                    second_reject_case.Text = "-";
                }
                else
                {
                    second_reject_case.Text = Convert.ToDecimal(SecondRejectCase).ToString();
                }

                con.Close();

                //Display Second Pending Amount 
                con.Open();
                SqlCommand Second_Pending_Amount = new SqlCommand("select sum(second_approve_amount + second_reject_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String SecondPendingAmount = Convert.ToString((Second_Pending_Amount.ExecuteScalar()));
                if (SecondPendingAmount == "")
                {
                    second_pending_amount.Text = "-";
                }
                Decimal SecondPendingAmount2 = Convert.ToDecimal(SecondPendingAmount);//(Approve+Reject 2nd submit)
                Decimal Total2ndPending = Total1stPending - SecondPendingAmount2; // Amount Pending (Pending Amount 1st - (Approve+Reject 2nd submit)
                second_pending_amount.Text = Convert.ToDecimal(Total2ndPending).ToString("c2");
                con.Close();

                //Display Second Pending case 
                con.Open();
                SqlCommand Second_Pending_Case = new SqlCommand("select sum(second_approve_quantity + second_reject_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String SecondPendingCase = Convert.ToString((Second_Pending_Case.ExecuteScalar()));
                if (SecondPendingCase == "")
                {
                    second_pending_case.Text = "-";
                }
                int SecondPendingCase2 = Convert.ToInt32(SecondPendingCase);//2nd Submission case Approve + Reject
                int Total2ndPendingCase = Convert.ToInt32(Total1stPendingCase) - SecondPendingCase2;// Total Pending Case 1st submission - ( Approve + Reject 2nd submission) )
                second_pending_case.Text = Convert.ToInt32(Total2ndPendingCase).ToString("");
                con.Close();

                //Display Third Amount Claimed
                con.Open();
                SqlCommand Third_Total_Amount = new SqlCommand("select sum(third_total_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String ThirdTotalAmount = Convert.ToString((Third_Total_Amount.ExecuteScalar()));
                if (ThirdTotalAmount == "")
                {
                    third_total.Text = "-";
                }
                else
                {
                    third_total.Text = Convert.ToDecimal(ThirdTotalAmount).ToString("c2");
                }
                con.Close();

                //Display Third Total Case Claimed
                con.Open();
                SqlCommand Third_Total_Case = new SqlCommand("select sum(third_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String ThirdTotalCase = Convert.ToString((Third_Total_Case.ExecuteScalar()));
                if (ThirdTotalCase == "")
                {
                    third_case.Text = "-";
                }
                else
                {
                    third_case.Text = Convert.ToDecimal(ThirdTotalCase).ToString("");
                }
                con.Close();

                //Display Third Approve Amount 
                con.Open();
                SqlCommand Third_Approve_Amount = new SqlCommand("select sum(third_approve_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String ThirdApproveAmount = Convert.ToString((Third_Approve_Amount.ExecuteScalar()));
                if (ThirdApproveAmount == "")
                {
                    third_approve_amount.Text = "-";
                }
                else
                {
                    third_approve_amount.Text = Convert.ToDecimal(ThirdApproveAmount).ToString("c2");
                }
                con.Close();

                //Display Third Approve Case 
                con.Open();
                SqlCommand Third_Approve_Case = new SqlCommand("select sum(third_approve_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String ThirdApproveCase = Convert.ToString((Third_Approve_Case.ExecuteScalar()));
                if (ThirdApproveCase == "")
                {
                    third_approve_case.Text = "-";
                }
                else
                {
                    third_approve_case.Text = Convert.ToDecimal(ThirdApproveCase).ToString("");
                }
                con.Close();

                //Display Third Reject Amount 
                con.Open();
                SqlCommand Third_Reject_Amount = new SqlCommand("select sum(third_reject_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String ThirdRejectAmount = Convert.ToString((Third_Reject_Amount.ExecuteScalar()));
                if (ThirdRejectAmount == "")
                {
                    third_reject_amount.Text = "-";
                }
                else
                {
                    third_reject_amount.Text = Convert.ToDecimal(ThirdRejectAmount).ToString("c2");
                }
                con.Close();

                //Display Third Reject Case 
                con.Open();
                SqlCommand Third_Reject_Case = new SqlCommand("select sum(third_reject_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String ThirdRejectCase = Convert.ToString((Third_Reject_Case.ExecuteScalar()));
                if (ThirdRejectCase == "")
                {
                    third_reject_case.Text = "-";
                }
                else
                {
                    third_reject_case.Text = Convert.ToDecimal(ThirdRejectCase).ToString("");
                }
                con.Close();

                //Display Third Pending Amount 
                con.Open();
                SqlCommand Third_Pending_Amount = new SqlCommand("select sum(third_approve_amount + third_reject_amount) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String ThirdPendingAmount = Convert.ToString((Third_Pending_Amount.ExecuteScalar()));
                if (ThirdPendingAmount != "")
                {
                    third_pending_amount.Text = "-";
                }
                Decimal ThirrdPendingAmount2 = Convert.ToDecimal(ThirdPendingAmount); //3rd submit Approve + Reject Amount
                Decimal Total3rdPending = Convert.ToDecimal(Total2ndPending) - ThirrdPendingAmount2; // 2nd submit Total Pending - (3rd Approve + Reject) )
                third_pending_amount.Text = Convert.ToDecimal(Total3rdPending).ToString("c2");

                con.Close();


                //Display Third Pending case 
                con.Open();
                SqlCommand Third_Pending_Case = new SqlCommand("select sum(third_approve_quantity + third_reject_quantity) from claim_record where month_year = '" + DropDownApplicationMonth.Text + "'  AND (supplier_type = 'LP' OR supplier_type = 'KD') ", con);
                String ThirdPendingCase = Convert.ToString((Third_Pending_Case.ExecuteScalar()));
                if (SecondPendingCase == "")
                {
                    third_pending_case.Text = "-";
                }
                else
                {
                    int ThirdPendingCase2 = Convert.ToInt32(ThirdPendingCase); //3rd Total case (Approve + Reject)
                    int Total3rdPendingCase = Convert.ToInt32(Total2ndPendingCase) - ThirdPendingCase2; //2nd Pending Case - (3rd Total Case Approve + Month) ) 
                    third_pending_case.Text = Convert.ToInt32(Total3rdPendingCase).ToString();
                }
                con.Close();
            }
            catch (Exception)
            {
                GrandTotal.Text = "No record";

                //Set the label to " - " if no value fetch from database
                first_pending_case.Text = "-"; second_total.Text = "-"; second_case.Text = "-"; second_approve_amount.Text = "-"; second_approve_case.Text = "-";
                second_reject_amount.Text = "-"; second_reject_case.Text = "-"; second_pending_amount.Text = "-"; second_pending_case.Text = "-";
                third_total.Text = "-"; third_case.Text = "-"; third_approve_amount.Text = "-"; third_approve_case.Text = "-"; third_reject_amount.Text = "-";
                third_reject_case.Text = "-"; third_pending_amount.Text = "-"; third_pending_case.Text = "-";
            }

        }
    }
}