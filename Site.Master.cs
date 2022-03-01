using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MqWebApp2
{
    public partial class SiteMaster : MasterPage
    {
        //Below is Connection file, get the connection string from web.config file
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MQWebAppConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            CheckUserType();
            currentPage();
        }

        protected void SignOutBtn_Click(object sender, EventArgs e)
        {
            //To sign out from the system
            Session.Contents.RemoveAll();
            Response.Redirect("/Login/LoginPage.aspx");
        }

        

        protected void CheckUserType()
        {
            //Determine the user type to display some features or not
            try
            {
                //if user not set, user is not allowed to access page.
                if (Session["UserType"] == null)
                {
                    //Uncomment below for login session purpose
                    //Response.Redirect("/Login/LoginPage.aspx");
                }
                else
                {
                    UserFullNameSession.Text = Convert.ToString(Session["UserFullName"]);

                    //Check user type to recognize admin or not
                    if (Session["UserType"].ToString() == "System Admin")
                    {
                        admintools.Visible = true;


                    }
                    else if (Session["UserType"].ToString() == "HOS")
                    {
                        admintools.Visible = true;

                    }
                    else if(Session["UserType"].ToString() == "HOD")
                    {
                        admintools.Visible = true;

                    }
                    else //If user not admin the section admin tools must be hidden
                    {
                        admintools.Visible = false;
                    }

                    //Check the current browse selected menu


                }
            }
            catch (Exception)
            {
                Response.Redirect("/Login/LoginPage.aspx");
            }
            
           
            
        }

        protected void currentPage()
        {
            //This whole method just to highlight icon the current navigation on the left bar navigation. If the current user on the page, icon will hightlighted on the related page
            string absolutepath = HttpContext.Current.Request.Url.AbsolutePath;

            if (absolutepath == "/UserMgmnt")
            {
                UserMgmntIcon.Attributes.Add("class", "nav-link-color");
            }
            else if (absolutepath == "/SupplierInfo" || absolutepath == "/AddSupplierName" || absolutepath == "/Edit_SupplierDetails" || absolutepath == "/PICSupplier")
            {
                SupplierInfoDiv.Attributes.Add("class", "nav-link-color");
            }
            else if (absolutepath == "/About")
            {
                WarrantyIcon.Attributes.Add("class", "nav-link-color");
            }
            else if (absolutepath == "/Add_Month_Record")
            {
                ApplicationMonth.Attributes.Add("class", "nav-link-color");
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
            }
            else if (absolutepath == "/LP_Supplier")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                LPSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div1.Attributes.Add("class", "nav-link-color");//Icon LP coloured in red
                LPsublist1.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                LPOverviewLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.
                
            }
            else if (absolutepath == "/Edit_Month_LP")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                LPSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div1.Attributes.Add("class", "nav-link-color");//Icon LP coloured in red
                LPsublist2.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                LPEditLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/Add_Claim_Record_LP" || absolutepath == "/ImportExcel_LP")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                LPSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div1.Attributes.Add("class", "nav-link-color");//Icon LP coloured in red
                LPsublist3.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                LPAddRecordLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/EditRecordLP" || absolutepath == "/LP_ExportData")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                LPSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div1.Attributes.Add("class", "nav-link-color");//Icon LP coloured in red
                LPsublist4.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                LPEditRecordLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/LP_Summary")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                LPSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div1.Attributes.Add("class", "nav-link-color");//Icon LP coloured in red
                LPsublist5.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                LPSummaryLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/KD%20Supplier%20Part/KD_Supplier")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                KDSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div2.Attributes.Add("class", "nav-link-color");//Icon KD coloured in red
                KDsublist1.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                KDOverviewLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/KD%20Supplier%20Part/KD_Edit_Month")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                KDSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div2.Attributes.Add("class", "nav-link-color");//Icon KD coloured in red
                KDsublist2.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                KDEditMonthLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/KD%20Supplier%20Part/KD_Add_Claim" || absolutepath == "/KD%20Supplier%20Part/KD_ImportExcel")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                KDSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div2.Attributes.Add("class", "nav-link-color");//Icon KD coloured in red
                KDsublist3.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                KDAddRecordLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/KD%20Supplier%20Part/KD_Edit_Claim" || absolutepath == "/KD%20Supplier%20Part/KD_ExportData")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                KDSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div2.Attributes.Add("class", "nav-link-color");//Icon KD coloured in red
                KDsublist4.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                KDEditClaimLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/KD%20Supplier%20Part/KD_Summary")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                KDSupp.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div2.Attributes.Add("class", "nav-link-color");//Icon KD coloured in red
                KDsublist5.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                KDSummaryLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/PUD%20Claim/PUD_Claim")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                PUDClaim.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div3.Attributes.Add("class", "nav-link-color");//Icon PUD coloured in red
                PUDsublist1.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                PUDOverviewLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/PUD%20Claim/PUD_Edit_Month")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                PUDClaim.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div3.Attributes.Add("class", "nav-link-color");//Icon PUD coloured in red
                PUDsublist2.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                PUDEditMonthLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/PUD%20Claim/PUD_Add_Claim" || absolutepath == "/PUD%20Claim/PUD_ImportExcel")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                PUDClaim.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div3.Attributes.Add("class", "nav-link-color");//Icon PUD coloured in red
                PUDsublist3.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                PUDAddRecordLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/PUD%20Claim/PUD_Edit_Claim" || absolutepath == "/PUD%20Claim/PUD_ExportData")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                PUDClaim.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div3.Attributes.Add("class", "nav-link-color");//Icon PUD coloured in red
                PUDsublist4.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                PUDEditRecordLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/PUD%20Claim/PUD_Summary")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                PUDClaim.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div3.Attributes.Add("class", "nav-link-color");//Icon PUD coloured in red
                PUDsublist5.Attributes.Add("class", "nav-link-color");//Bullet sublist coloured in red
                PUDSummaryLink.Attributes.Add("class", "nav-link-sublist-selected");//Link/word italic and bold.

            }
            else if (absolutepath == "/All%20Claim/All_Claim")
            {
                Warranty.CssClass = "collapse nav-box-collapse in";//collapse will show if the menu selected
                Div4.Attributes.Add("class", "nav-link-color");//Icon All Claim coloured in red

            }


        }

        
    }
}