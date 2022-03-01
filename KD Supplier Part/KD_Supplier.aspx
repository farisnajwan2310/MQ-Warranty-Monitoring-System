<%@ Page Title="KD Supplier Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KD_Supplier.aspx.cs" Inherits="MqWebApp2.KD_Supplier_Part.KD_Supplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <section id="breadcrumbs" class="breadcrumbs lineKD">
      <div class="container">
        <ol>
        <li><a href="/Homepage.aspx">Home</a></li>
        <li>KD Supplier Menu</li>
        </ol>
      </div>
    </section><!-- End Breadcrumbs -->

    <div class="container">

        <div class="yeardropdown">
            <asp:Label runat="server" AssociatedControlID="DropDownApplicationMonth"><b>Pick Application Month:</b></asp:Label><br />
            <asp:DropDownList ID="DropDownApplicationMonth" runat="server" CssClass="yeardropbtn" DataSourceID="SqlDataSourceDropDownMonth" DataTextField="month_year" DataValueField="month_year" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownYear_SelectedIndexChanged">
                <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceDropDownMonth" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT [month_year] FROM [month_detail] WHERE ([supplier_type] = @supplier_type) ORDER BY [claim_dateline] DESC">
                <SelectParameters>
                    <asp:Parameter DefaultValue="KD" Name="supplier_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

        <div class="section-title">
              <h2>KD Supplier Claim </h2>
        </div><!--End of title-->


        <section id="counts" class="counts">
        <div class="container">

                <div class="row">

                  <div class="col-lg-4  text-center">
                    <h3><asp:Label ID="Submission_Date" runat="server" Text=""></asp:Label></h3>
                    <p>Submission Date</p>
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
         </section><!-- End KD Date Function Section -->

        <!-- ======= Menu Card KD Section ======= -->
        <section id="services" class="services">
          <div class="container">
            <div class="row">
              <div class="col-lg-6 mt-4">
                <div class="icon-box">
                  <div class="icon"><i class="bi bi-calendar2-check" style="color: #598eff;"></i></div>
                  <h4 class="title"><a href="KD_Edit_Month.aspx">Edit Month Detail</a></h4>
                  <p class="description">Change information on Collection date, Dateline, PDCA date</p>
                </div>
              </div>
              <div class="col-lg-6 mt-4">
                <div class="icon-box">
                  <div class="icon"><i class="bi bi-folder-plus" style="color: #21ed40;"></i></div>
                  <h4 class="title"><a href="KD_Add_Claim.aspx">Add New Record</a></h4>
                  <p class="description">Create new record of claim in the month. Supplier information, status claim and detail will be needed to fill in too.</p>
                </div>
              </div>
              <div class="col-lg-6 mt-4">
                <div class="icon-box">
                  <div class="icon"><i class="bi bi-cash-stack" style="color: #FEE839;"></i></div>
                  <h4 class="title"><a href="KD_Summary">Total Amount & Case in the Month</a></h4>
                  <p class="description">Showing a total amount of claim and total cases from overseas supplier in selected month</p>
                </div>
              </div>
              <div class="col-lg-6 mt-4">
                <div class="icon-box">
                  <div class="icon"><i class="bi bi-pencil-square" style="color: #fca74c;"></i></div>
                  <h4 class="title"><a href="KD_Edit_Claim">Edit KD Supplier Claim Record</a></h4>
                  <p class="description">Edit claim listed in the month for KD Supplier.</p>
                </div>
              </div>
            
          </div></div>
        </section><!-- End KD Menu Section -->


        <div class="section-title">
              <h4>Summary of KD Claim in <asp:Label ID="MonthDisplay" runat="server" Text=""></asp:Label></h4>
        </div>

        <div class="row total_pending">

            <div class="col-3 total_pending"> 
                  <h5>Total Pending = <asp:Label ID="TotalPendingLabel" runat="server" Text=""></asp:Label></h5>
               </div>
               <div class="col-3">
                  <h5> Total Incomplete = <asp:Label ID="TotalIncompleteLabel" runat="server" Text=""></asp:Label></h5>
               </div>
               <div class="col-3">
                  <h5> Total Completed= <asp:Label ID="TotalCompleteLabel" runat="server" Text=""></asp:Label> </h5>
               </div>
               <div class="col-3"> 
                  <h5>Total Supplier Claim = <asp:Label ID="TotalAllLPLabel" runat="server" Text=""></asp:Label></h5>
               </div>

        </div>

        <div class="table-group">

            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" HorizontalAlign="Center">
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
                <PagerStyle BackColor="#484848" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>

        </div>


    </div>
</asp:Content>
