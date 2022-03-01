<%@ Page Title="LP Supplier Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LP_Supplier.aspx.cs" Inherits="MqWebApp2.LP_Supplier" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section id="breadcrumbs" class="breadcrumbs lineLP">
      <div class="container">

        <ol>
        <li><a href="Homepage.aspx">Home</a></li>
        <li>LP Supplier Menu</li>
        </ol>

      </div>
    </section><!-- End Breadcrumbs -->

    <div class="container">

        <div class="yeardropdown">
          <asp:Label runat="server" AssociatedControlID="DropDownList1"><b>Pick Application Month :</b></asp:Label><br />
            <asp:DropDownList ID="DropDownList1" CssClass="yeardropbtn" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server" DataSourceID="SqlDataSource2" DataTextField="month_year" DataValueField="month_year" AutoPostBack="True" AppendDataBoundItems="True">
                <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
            </asp:DropDownList>
        </div>


        <div class="section-title">
              <h2>LP Supplier Claim </h2>
        </div><!--End of title-->

        <!-- ======= Detail LP Claim Section ======= -->
    <section id="counts" class="counts">
      <div class="container">

                <div class="row">

                  <div class="col-lg-4  text-center">
                    <h3><asp:Label ID="Collection_Date" runat="server" Text=""></asp:Label></h3>
                    <p>Collection Date</p>
                  </div>

                  <div class="col-lg-4  text-center">
                    <h3><asp:Label ID="Supplier_Dateline" runat="server" Text=""></asp:Label></h3>
                    <p>Supplier Dateline</p>
                  </div>

                  <div class="col-lg-4 text-center">
                    <h3><asp:Label ID="Pdca_date" runat="server" Text=""></asp:Label></h3>
                    <p>PDCA</p>
                  </div>

        </div>
      </div>
    </section><!-- End LP Detail Section -->
    
    <!-- ======= Menu Card LP Section ======= -->
        <section id="services" class="services">
          <div class="container">
            <div class="row">
              <div class="col-lg-6 mt-4">
                <div class="icon-box">
                  <div class="icon"><i class="bi bi-calendar2-check" style="color: #598eff;"></i></div>
                  <h4 class="title"><a href="Edit_Month_LP.aspx">Edit Month Detail</a></h4>
                  <p class="description">Change information on Collection date, Dateline, PDCA date</p>
                </div>
              </div>
              <div class="col-lg-6 mt-4">
                <div class="icon-box">
                  <div class="icon"><i class="bi bi-folder-plus" style="color: #21ed40;"></i></div>
                  <h4 class="title"><a href="Add_Claim_Record_LP.aspx">Add New Record</a></h4>
                  <p class="description">Create new record of claim in the month. Supplier information, status claim and detail will be needed to fill in too.</p>
                </div>
              </div>
              <div class="col-lg-6 mt-4">
                <div class="icon-box">
                  <div class="icon"><i class="bi bi-cash-stack" style="color: #FEE839;"></i></div>
                  <h4 class="title"><a href="LP_Summary.aspx">Total Amount & Case in the Month</a></h4>
                  <p class="description">Showing a total amount of claim and total cases from all local supplier in selected month</p>
                </div>
              </div>
              <div class="col-lg-6 mt-4">
                <div class="icon-box">
                  <div class="icon"><i class="bi bi-pencil-square" style="color: #fca74c;"></i></div>
                  <h4 class="title"><a href="EditRecordLP.aspx">Edit LP Supplier Claim Record</a></h4>
                  <p class="description">Edit claim listed in the month for LP Supplier.</p>
                </div>
              </div>
            
          </div>
        </section><!-- End LP Menu Section -->

    
        <div class="section-title">
              <h4>Summary of LP Claim in <asp:Label ID="MonthDisplay" runat="server" Text=""></asp:Label></h4>
        </div>

        <!--===Total Pending & complete===-->
          <div class="row total_pending" >
               
              <div class="col-12 text-center"> 
                  <h5>Total Supplier Claim = <asp:Label ID="TotalAllLPLabel" runat="server" Text=""></asp:Label></h5>
               </div>
               <div class="col-3"> 
                  <h5>Total Pending = <asp:Label ID="TotalPendingLabel" runat="server" Text=""></asp:Label></h5>
               </div>
              <div class="col-3">
                  <h5> Total Others= <asp:Label ID="TotalOthersLabel" runat="server" Text=""></asp:Label> </h5>
               </div>
               <div class="col-3">
                  <h5> Total Incomplete = <asp:Label ID="TotalIncompleteLabel" runat="server" Text=""></asp:Label></h5>
               </div>
               <div class="col-3">
                  <h5> Total Completed= <asp:Label ID="TotalCompleteLabel" runat="server" Text=""></asp:Label> </h5>
               </div>
                   
                   
                   <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT month_year FROM month_detail WHERE (supplier_type = 'LP') ORDER BY claim_dateline DESC"></asp:SqlDataSource>
                   
               

          </div>

        <div class="table-group">
            
            <asp:GridView ID="GridView1" runat="server" UseAccessibleHeader="true" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" RowHeaderColumn="2">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate >
                             <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="Red" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
            <asp:Label ID="PageNumber" Visible="false" runat="server" Text=""></asp:Label>
            </div>
    </div>

</asp:Content>
