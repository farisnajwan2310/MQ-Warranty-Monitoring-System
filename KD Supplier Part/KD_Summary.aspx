<%@ Page Title="(KD) Summary Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KD_Summary.aspx.cs" Inherits="MqWebApp2.KD_Supplier_Part.KD_Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

        <section id="breadcrumbs" class="breadcrumbs lineKD">
          <div class="container">

            <ol>
            <li><a href="/Homepage.aspx">Home</a></li>
            <li><a href="KD_Supplier.aspx">KD Supplier Menu</a></li>
            <li>Summary of Total Amount & Case</li>
            </ol>

          </div>
        </section><!-- End Breadcrumbs -->


    <div class="container">

        <div class="yeardropdown">
            <asp:Label runat="server" AssociatedControlID="YearDropDown"><b>Pick Application month:</b></asp:Label><br />
            <asp:DropDownList ID="YearDropDown" runat="server" CssClass="yeardropbtn" OnSelectedIndexChanged="YearDropDown_SelectedIndexChanged" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="month_year" DataValueField="month_year" AutoPostBack="True">
                <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
            </asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT [month_year] FROM [month_detail] WHERE ([supplier_type] = @supplier_type)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="KD" Name="supplier_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div>


        <!-- ====================Summary Card==============================-->
        <section id="percentclaim" class="percentclaim">

                <div class="col-lg-12 mt-4 ">
                <div class="col-lg-12 cardpercentclaim-body2">
                    <div class="col-xl-12">
                    <div class="col-lg-6 mt-4 text-center ">
                        <asp:Chart ID="Chart1" runat="server" BackColor="Transparent" EnableTheming="True" Width="350px" ImageType="Png">
                            <Series>
                                <asp:Series Name="Series1" ChartType="Doughnut" XValueMember="status_claim" YValueMembers="Status1" IsValueShownAsLabel="True" Legend="Legend1"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" AlignmentOrientation="Horizontal"></asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend DockedToChartArea="ChartArea1" Alignment="Center" Docking="Bottom" IsDockedInsideChartArea="False" Name="Legend1">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Name="Title1">
                                </asp:Title>
                            </Titles>
                        </asp:Chart>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="select  status_claim,count(status_claim) as Status1 from claim_record where supplier_type='LP' AND month_year='Nov-2020' group by status_claim"></asp:SqlDataSource>
                        
                    </div>
                    <div class="col-lg-6 mt-4">
                        <h3>Summary KD Claim in <asp:Label ID="SummaryLabel" runat="server" Text=""></asp:Label></h3>
                        <div class="col-sm-4 percent-digit">
                            <h4><asp:Label ID="LblPercentPending" runat="server" Text="-"></asp:Label></h4>
                            <p>Pending</p>
                        </div>
                        <div class="col-sm-4 percent-digit">
                            <h4><asp:Label ID="LblPercentIncomplete" runat="server" Text="-"></asp:Label></h4>
                            <p>Incomplete</p>
                        </div>
                        <div class="col-sm-4 percent-digit">
                            <h4><asp:Label ID="LblPercentCompleted" runat="server" Text="-"></asp:Label> </h4>
                            <p>Completed</p>                            
                        </div>
                    </div>
                   </div>
                   <div class="col-lg-12 mt-4"> <!--==Lower Part==-->
                        <div class="col-lg-3 statuspercent text-center left-line-blind">
                           <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="KDAmountAll" runat="server" Text="-"></asp:Label></h4>
                               <p>Amount</p>
                           </div>
                           <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="KDQuantityAll" runat="server" Text="-"></asp:Label></h4>
                               <p>Case</p>
                           </div>
                           <div class="col-lg-12 summary-label">
                               <h3>Total</h3>
                           </div>
                        </div>
                        <div class="col-lg-3 statuspercent text-center left-line">
                            <div class="col-sm-12 status-digit">
                               <h4><asp:Label ID="Total_Amount" runat="server" Text="-"></asp:Label></h4>
                               <p>Amount</p>
                           </div>
                           <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="Total_Quantity_Approve" runat="server" Text="-"></asp:Label></h4>
                               <p>Case</p>
                           </div>
                           <div class="col-lg-12 summary-label">
                               <h3>Approved</h3>
                           </div>
                        </div>
                        <div class="col-lg-3 statuspercent text-center left-line">
                            <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="Total_Amount_Reject" runat="server" Text="-"></asp:Label></h4>
                               <p>Amount</p>
                           </div>
                           <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="Total_Quantity_Reject" runat="server" Text="-"></asp:Label></h4>
                               <p>Case</p>
                           </div>
                           <div class="col-lg-12 summary-label">
                               <h3>Rejected</h3>
                           </div>
                        </div>
                       <div class="col-lg-3 statuspercent text-center left-line">
                            <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="Total_Amount_Pending" runat="server" Text="-"></asp:Label></h4>
                               <p>Amount</p>
                           </div>
                           <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="Total_Quantity_Pending" runat="server" Text="-"></asp:Label></h4>
                               <p>Case</p>
                           </div>
                           <div class="col-lg-12 summary-label">
                               <h3>Pending</h3>
                           </div>
                        </div>
                       
                    </div>
                </div>
                </div>
                
                <div class="col-md-4 mt-4">
                    <div class="iconbox">
                      <h4 class="title">1st Submission Total</h4>
                         <div class="row">
                          <p class="description col-12">Total amount  : <asp:Label ID="TotalAmountFirst" runat="server" Text=""></asp:Label></p>
                          <p class="description col-12">Total claim: <asp:Label ID="TotalQuantityFirst" runat="server" Text=""></asp:Label></p>
                          <p class="description col-12">Approved amount : <asp:Label ID="TotalApprovedFirst" runat="server" Text=""></asp:Label></p>
                          <p class="description col-12">Approved claim: <asp:Label ID="TotalApprovedQuantityFirst" runat="server" Text=""></asp:Label></p>
                          <p class="description col-12">Rejected amount : <asp:Label ID="TotalRejectedFirst" runat="server" Text=""></asp:Label></p>
                          <p class="description col-12">Rejected claim: <asp:Label ID="TotalRejectedQuantityFirst" runat="server" Text=""></asp:Label> </p>
                          </div>
                    </div>
                </div>

                <div class="col-md-4 mt-4">
                    <div class="iconbox">
                      <h4 class="title">2nd Submission Total</h4>
                      <div class="row">
                        <p class="description col-12">Total amount : <asp:Label ID="TotalAmountSecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Total claim: <asp:Label ID="TotalQuantitySecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Approved amount : <asp:Label ID="TotalApprovedSecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Approved claim: <asp:Label ID="TotalApprovedQuantitySecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Rejected amount : <asp:Label ID="TotalRejectedSecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Rejected claim: <asp:Label ID="TotalRejectedQuantitySecond" runat="server" Text=""></asp:Label> </p>
                      </div>
                    </div>
                </div>

                <div class="col-md-4 mt-4">
                    <div class="iconbox">
                      <h4 class="title">3rd Submission Total</h4>
                      <div class="row">
                        <p class="description col-12">Total amount : <asp:Label ID="TotalAmountThird" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Total claim: <asp:Label ID="TotalQuantityThird" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Approved amount : <asp:Label ID="TotalApprovedThird" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Approved claim: <asp:Label ID="TotalApprovedQuantityThird" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Rejected amount : <asp:Label ID="TotalRejectThird" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Rejected claim: <asp:Label ID="TotalRejectQuantityThird" runat="server" Text=""></asp:Label> </p>
                      </div>
                    </div>
                </div>

        </section>


    </div>
</asp:Content>
