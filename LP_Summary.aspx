<%@ Page Title="(LP) Summary Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LP_Summary.aspx.cs" Inherits="MqWebApp2.LP_Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ======= Breadcrumbs ======= -->
    <section id="breadcrumbs" class="breadcrumbs lineLP">
      <div class="container">

        <ol>
          <li><a href="Homepage.aspx">Home</a></li>
          <li><a href="LP_Supplier.aspx">LP Supplier Menu</a></li>
          <li>Summary of Total Amount & Case</li>
        </ol>
      </div>
    </section><!-- End Breadcrumbs -->

    <div class="container">


        <div class="yeardropdown">
            
            <asp:Label runat="server" AssociatedControlID="YearDropDown"><b>Pick Application Month:</b></asp:Label><br />
            <asp:DropDownList ID="YearDropDown" CssClass="yeardropbtn" AutoPostBack="True" AppendDataBoundItems="True" runat="server" DataSourceID="SqlDataSource1" DataTextField="month_year" DataValueField="month_year" OnSelectedIndexChanged="YearDropDown_SelectedIndexChanged">
                <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT month_year FROM month_detail WHERE (supplier_type = @supplier_type) ORDER BY collection_date DESC">
                <SelectParameters>
                    <asp:Parameter DefaultValue="LP" Name="supplier_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

        
        
        
         <div class="section-title">
             <h2>Summary of Total Amount & Case in <asp:Label ID="MonthPageTitle"  runat="server" Text=""></asp:Label></h2>
         </div>
      
        
        <!-- ====================Summary Card==============================-->
        <section id="percentclaim" class="percentclaim">
                <div class="col-lg-12 mt-4 ">
                    <div class="col-lg-12 cardpercentclaim-body2">
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
                        <h3><u>Summary LP Claim in <asp:Label ID="SummaryLabel" runat="server" Text=""></asp:Label></u></h3>
                        <div class="col-sm-3 percent-digit">
                            <h4><asp:Label ID="LblPercentPending" runat="server" Text="-"></asp:Label></h4>
                            <p>Pending</p>
                        </div>
                        <div class="col-sm-3 percent-digit">
                            <h4><asp:Label ID="LblPercentOthers" runat="server" Text="-%"></asp:Label></h4>
                            <p>Others</p>
                        </div>
                        <div class="col-sm-3 percent-digit">
                            <h4><asp:Label ID="LblPercentIncomplete" runat="server" Text="-"></asp:Label></h4>
                            <p>Incomplete</p>
                        </div>
                        <div class="col-sm-3 percent-digit">
                            <h4><asp:Label ID="LblPercentCompleted" runat="server" Text="-"></asp:Label> </h4>
                            <p>Completed</p>                            
                        </div>
                    </div>
                    
                    <div class="col-md-12 mt-4"> <!--==Lower Part==-->
                        <div class="col-lg-3 statuspercent text-center left-line-blind">
                           <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="LPAmountAll" runat="server" Text="-"></asp:Label></h4>
                               <p>Amount</p>
                           </div>
                           <div class="col-lg-12 status-digit">
                               <h4><asp:Label ID="LPQuantityAll" runat="server" Text="-"></asp:Label></h4>
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

            <!--==Pending Supplier Graph==-->
             <div class="col-lg-6 mt-4 ">
                <div class="col-lg-12 cardpercentclaim-body2">
                    <div class="col-lg-8">
                        <h3>Pending Supplier</h3>
                    </div>

                    <div class="col-lg-4 full-graph">
                        <a href="#" data-toggle="modal" data-target="#modalPending">Full View</a>
                    </div>
                    

                    <!--Modal: Full graph supplier Pending-->
                    <div class="modal fade" id="modalPending" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                      aria-hidden="true">
                      <div class="modal-dialog modal-lg" role="document">

                        <!--Content-->
                        <div class="modal-graph">

                          <!--Body-->
                          <div class="modal-body mb-0 p-0">

                            <div class=" embed-responsive embed-responsive-16by9 z-depth-1-half">
                                <div class="modal-graph">
                                <asp:Chart ID="Chart4" runat="server" Palette="None" PaletteCustomColors="Red" Height="600px" Width="850px">
                                    <Series>
                                        <asp:Series Name="Series1" ChartType="Bar"  ></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <axisy>
                                                <MajorGrid Enabled ="False" />
                                            </axisy>
                                            <axisx>
                                                <MajorGrid Enabled="false"/>
                                            </axisx>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                                </div>
                              
                            </div>

                          </div>

                        </div>
                        <!--/Modal Content-->

                      </div>
                    </div>
                    <!--Modal: Pending Supp-->
                            
                    
                    <asp:Chart ID="Chart2" runat="server" Palette="None" PaletteCustomColors="Red" Width="450px">
                    <Series>
                        <asp:Series Name="Series2a" ChartType="Bar"  IsValueShownAsLabel="True" ></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                                <axisy>
                                    <MajorGrid Enabled ="False" />
                                </axisy>
                                <axisx>
                                    <MajorGrid Enabled="false"/>
                                </axisx>
                        </asp:ChartArea>
                    </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
            <!--==Incomplete Supplier Graph==-->
            <div class="col-lg-6 mt-4 ">
                <div class="col-lg-12 cardpercentclaim-body2">
                    <h3>Incomplete Supplier</h3>

                    <asp:Chart ID="Chart3" runat="server" Width="450px">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Bar"  IsValueShownAsLabel="True" XValueMember="supplier_name" YValueMembers="total_incomplete"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <axisy>
                                    <MajorGrid Enabled ="False" />
                                </axisy>
                                <axisx>
                                    <MajorGrid Enabled="false"/>
                                </axisx>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    
                </div>
            </div>

            <!--==1st,2nd,3rd Submission Total==-->
              <div class="col-lg-4 mt-4"> 
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
                
                <div class="col-lg-4 mt-4 ">
                    <div class="iconbox">
                        <h4 class="title">2nd Submission Total</h4>
                        <p class="description col-12">Total amount : <asp:Label ID="TotalAmountSecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Total claim: <asp:Label ID="TotalQuantitySecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Approved amount : <asp:Label ID="TotalApprovedSecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Approved claim: <asp:Label ID="TotalApprovedQuantitySecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Rejected amount : <asp:Label ID="TotalRejectedSecond" runat="server" Text=""></asp:Label></p>
                        <p class="description col-12">Rejected claim: <asp:Label ID="TotalRejectedQuantitySecond" runat="server" Text=""></asp:Label> </p>
                    </div>
                </div>
                <div class="col-lg-4 mt-4 ">
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


       

        

        <asp:SqlDataSource ID="SqlDataSourcePending" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="select top 5 supplier_name,total_amount_claim from claim_record where month_year = 'Oct-2020' and status_claim='pending' and supplier_type='lp' order by total_amount_claim desc"></asp:SqlDataSource>

    </div>
    

</asp:Content>
