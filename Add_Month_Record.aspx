<%@ Page Title="Add Application Month" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add_Month_Record.aspx.cs" Inherits="MqWebApp2.Add_Month_Record" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <!-- ======= Breadcrumbs ======= -->
    
    <section id="breadcrumbs" class="breadcrumbs ">
      <div class="container">

        <ol>
          <li><a href="Homepage.aspx">Home</a></li>
          <li>Add Month Record for Supplier Type</li>
        </ol>

      </div>
    </section><!-- End Breadcrumbs -->


    <div class="container">

        <section id="form" class="form">

        <div class="container">

        <div class="section-title">
          <h2>Add Application Month/Year Supplier Warranty</h2>
          <p>Please insert the information without typing error or capslock error. All data should be suitable with the column.<br> The data will be added for month-year references for the application month for the supplier warranty record.</p>
        </div>

        <div class="form-list">
          <ul>
            <li data-aos="fade-up">
            <div class="center ">

                <div class="row">
                <!--Month-->
                <div class="col-lg-12 form-group">
                        <asp:Label runat="server" ><b>Month :</b></asp:Label><br /> 
                        <asp:TextBox runat="server" ID="month_year" placeholder="Choose month" CssClass="form-control" Type="Month"></asp:TextBox>
                </div>
                <!-- Collection Date-->
                <div class="col-lg-6 form-group">
                  <asp:Label runat="server" AssociatedControlID="collect_date"><b>Collection Date/Submission Date (Click at Calendar) :</b></asp:Label><br />
                  <asp:TextBox runat="server" ToolTip="Please choose at the calendar. No typing the date"  Enabled="true" TextMode="Date" ID="collect_date" CssClass="form-control" OnTextChanged="collect_date_TextChanged" placeholder="Collection Date" AutoPostBack="True"></asp:TextBox>
                </div>
                <!-- Supplier Dateline -->
                <div class="col-lg-6 form-group">
                  <asp:Label runat="server" AssociatedControlID="claim_dateline"><b>Supplier Dateline :</b></asp:Label><br />
                  <asp:TextBox runat="server"  Enabled="true" ToolTip="Please choose at the calendar. No typing the date" TextMode="Date" ID="claim_dateline" CssClass="form-control" placeholder="Supplier Dateline"></asp:TextBox>
                </div>
                <!-- Pdca date-->
                <div class="col-lg-8 mt-3">
                  <asp:Label runat="server" AssociatedControlID="pdca_date"><b>PDCA Date :</b></asp:Label><br />
                  <asp:TextBox runat="server" Type="Month"  Enabled="true" ID="pdca_date" CssClass="form-control" placeholder="Supplier Dateline"></asp:TextBox>
                </div>
                <!-- Supplier Type-->
                <div class="col-lg-4 mt-3">
                    <asp:Label runat="server" AssociateControlID="supplier_type1"><b>Supplier Type</b></asp:Label><br />
                        <asp:DropDownList runat="server" ID="supplier_type1" OnTextChanged="supplier_type1_TextChanged" AutoPostBack="true" CssClass="dropdownform">
                        <asp:ListItem Text="LP"/>
                        <asp:ListItem Text="KD"/>
                        <asp:ListItem Text="PUD Claim"/>
                        </asp:DropDownList>
                </div>

              <div class="col-md-6 form-group"><p> </p></div><div class="col-md-6 form-group"></div><!--New line skip-->
              
              <asp:Button Text="Save" ID="btnsave" OnClick="AddMonthRecord" CssClass="formSetting2 offset-5" Width="170px" runat="server" />
              </div>
            
            </div>
           </li>
              <asp:Label runat="server" AssociateControlID="DropDownList1"><b>Choose Month</b></asp:Label><br/>
              <asp:DropDownList ID="DropDownList1" runat="server" CssClass="yeardropbtn" OnSelectedIndexChanged="DropDownList1_DataBinding" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="month_year" DataValueField="month_year">
                        <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
              </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT DISTINCT month_year, MAX(collection_date) AS Expr1 FROM month_detail GROUP BY month_year ORDER BY Expr1 DESC, month_year"></asp:SqlDataSource>
          </ul>
        </div>

      <!-- End of Form -->
        

            
        

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT * FROM [month_detail]" OldValuesParameterFormatString="original_{0}">
            </asp:SqlDataSource>
            
            
            
                    <asp:GridView ID="GridView2" runat="server" CellPadding="4" OnRowDataBound="GridView2_RowDataBound" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" >
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Red" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />

            </asp:GridView>
        
        
        </div>
        </section>
        <!--  End of Form-->
        </div>

    
          

</asp:Content>
