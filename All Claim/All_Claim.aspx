<%@ Page Title="All Claim Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="All_Claim.aspx.cs" Inherits="MqWebApp2.All_Claim.All_Claim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section id="breadcrumbs" class="breadcrumbs">
      <div class="container">

        <ol>
        <li><a href="/Homepage.aspx">Home</a></li>
        <li>All Claim Menu</li>
        </ol>

      </div>
    </section><!-- End Breadcrumbs -->


    <div class="container">
        <div class="yeardropdown">
            <asp:Label runat="server" AssociatedControlID="DropDownApplicationMonth"><b>Pick Application Month:</b></asp:Label><br />
            <asp:DropDownList ID="DropDownApplicationMonth" runat="server" CssClass="yeardropbtn" OnSelectedIndexChanged="DropDownApplicationMonth_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="month_year" DataValueField="month_year">
                <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT DISTINCT month_year, MAX(collection_date) AS Expr1 FROM month_detail GROUP BY month_year ORDER BY Expr1 DESC, month_year"></asp:SqlDataSource>
        </div>

        <div class="section-title">
              <h2>All Claim Summary </h2>
        </div><!--End of title-->

      
        <section id="counts" class="counts">
          <div class="container">

            <div class="row counters">
                <div class="col-lg-12">
                <div class="col-lg-12  text-center">
                    <asp:Label ID="GrandTotal" runat="server" Text=""></asp:Label>
                    <p>Total Amount(RM) Approved</p>
                  </div>
                <div class="col-lg-12  text-center">
                    <asp:Label ID="TotalCaseApprove" runat="server" Text=""></asp:Label>
                    <p>Total Case Approved</p>
                  </div>
               </div>
                </div>
              </div>
            </section>


        <!---===========PIE Chart Area===============-->
            <div class="row">
                <div class="col-lg-6 text-center">
                    <div class="cardpercentclaim-body">
                        <h5>LP Claim Status</h5>
                    <asp:Chart ID="Chart2" runat="server" >
                        <Series>
                            <asp:Series Name="Series1" ChartType="Doughnut" XValueMember="status_claim" YValueMembers="Status1" IsValueShownAsLabel="True" Legend="Legend1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Docking="Bottom" Alignment="Center" Name="Legend1">
                            </asp:Legend>
                        </Legends>
                    </asp:Chart>
                    
                    <asp:SqlDataSource ID="SqlDataSourcePieChartLP" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT status_claim, COUNT(status_claim) AS Status1 FROM claim_record WHERE (supplier_type = 'LP') GROUP BY status_claim"></asp:SqlDataSource>
                </div></div>

                <div class="col-lg-6 text-center">
                    <div class="cardpercentclaim-body">
                        <h5>KD Claim Status</h5>
                    <asp:Chart ID="Chart3" runat="server" >
                        <Series>
                            <asp:Series Name="Series1" ChartType="Doughnut" XValueMember="status_claim" YValueMembers="Status1" IsValueShownAsLabel="True" Legend="Legend1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend1">
                            </asp:Legend>
                        </Legends>
                    </asp:Chart>
                    
                </div></div>

                
                <asp:SqlDataSource ID="SqlDataSourcePieChart1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="select  status_claim,count(status_claim) as Status1 from claim_record where supplier_type='PUD Claim' group by status_claim"></asp:SqlDataSource>
                

           </div>
        <!---===========PIE Chart Area===============-->



        <!-- ======= Services Section ======= -->
        <section id="services" class="services">
            <div class="row">
              <div class="col-lg-4 mt-4">
                <div class="icon-box">
                <h4 class="title"><a href="#"><u>1st Submission</u></a></h4>
                  <p class="alltotalclaim"><i class="bi bi-cash-coin"></i> Total Amount : <asp:Label ID="first_total" runat="server" Text="-"></asp:Label></p>
                  <p class="alltotalclaim">Total Case: <asp:Label ID="first_case" runat="server" Text="-"></asp:Label></p><br>
                  <p class="alltotalclaim"><i class="bi bi-check2-square"></i> Approved Amount : <asp:Label ID="first_approve_amount" runat="server" Text="-"></asp:Label></p>
                  <p class="alltotalclaim"></i> Approved Case: <asp:Label ID="first_approve_case" runat="server" Text="-"></asp:Label></p><br>
                  <p class="alltotalclaim"><i class="bi bi-clipboard-x"></i> Rejected Amount : <asp:Label ID="first_reject_amount" runat="server" Text="-"></asp:Label></p>
                  <p class="alltotalclaim"></i> Rejected Case: <asp:Label ID="first_reject_case" runat="server" Text="-"></asp:Label></p><br>
                  <p class="alltotalclaim"><i class="bi bi-clipboard-minus"></i> Pending Amount : <asp:Label ID="first_pending_amount" runat="server" Text="-"></asp:Label></p>
                  <p class="alltotalclaim"></i> Pending Case: <asp:Label ID="first_pending_case" runat="server" Text="-"></asp:Label></p>
                </div>
              </div>
              <div class="col-lg-4 mt-4">
                <div class="icon-box">
                  <h4 class="title"><a href="#"><u>2nd Submission</u></a></h4>
                  <p class="alltotalclaim"><i class="bi bi-cash-coin"></i> Total Amount : <asp:Label ID="second_total" runat="server" Text=""></asp:Label></p>
                  <p class="alltotalclaim">Total Case: <asp:Label ID="second_case" runat="server" Text=""></asp:Label> </p><br>
                  <p class="alltotalclaim"><i class="bi bi-check2-square"></i> Approved Amount : <asp:Label ID="second_approve_amount" runat="server" Text=""></asp:Label></p>
                  <p class="alltotalclaim"></i> Approved Case: <asp:Label ID="second_approve_case" runat="server" Text="Label"></asp:Label> </p><br>
                  <p class="alltotalclaim"><i class="bi bi-clipboard-x"></i> Rejected Amount : <asp:Label ID="second_reject_amount" runat="server" Text=""></asp:Label></p>
                  <p class="alltotalclaim"></i> Rejected Case:  <asp:Label ID="second_reject_case" runat="server" Text=""></asp:Label></p><br>
                  <p class="alltotalclaim"><i class="bi bi-clipboard-minus"></i> Pending Amount : <asp:Label ID="second_pending_amount" runat="server" Text=""></asp:Label></p>
                  <p class="alltotalclaim"></i> Pending Case: <asp:Label ID="second_pending_case" runat="server" Text=""></asp:Label></p>
                </div>
              </div>
              <div class="col-lg-4 mt-4">
                <div class="icon-box">
                  <div class="icon"></div>
                  <h4 class="title"><a href="#"><u>3rd Submission</u></a></h4>
                  <p class="alltotalclaim"><i class="bi bi-cash-coin"></i> Total Amount : <asp:Label ID="third_total" runat="server" Text=""></asp:Label></p>
                  <p class="alltotalclaim">Total Case:  <asp:Label ID="third_case" runat="server" Text=""></asp:Label></p><br>
                  <p class="alltotalclaim"><i class="bi bi-check2-square"></i> Approved Amount : <asp:Label ID="third_approve_amount" runat="server" Text=""></asp:Label></p>
                  <p class="alltotalclaim"></i> Approved Case: <asp:Label ID="third_approve_case" runat="server" Text=""></asp:Label></p><br>
                  <p class="alltotalclaim"><i class="bi bi-clipboard-x"></i> Rejected Amount : <asp:Label ID="third_reject_amount" runat="server" Text=""></asp:Label></p>
                  <p class="alltotalclaim"></i> Rejected Case: <asp:Label ID="third_reject_case" runat="server" Text=""></asp:Label></p><br>
                  <p class="alltotalclaim"><i class="bi bi-clipboard-minus"></i> Pending Amount : <asp:Label ID="third_pending_amount" runat="server" Text=""></asp:Label></p>
                  <p class="alltotalclaim"></i> Pending Case: <asp:Label ID="third_pending_case" runat="server" Text=""></asp:Label></p>

                </div>
              </div>
            
          </div>
            <br />
            <p>
                 *The pending amount displayed subjected to the submission level. 
            </p>
        </section><!-- End Services Section -->


    </div>
</asp:Content>
