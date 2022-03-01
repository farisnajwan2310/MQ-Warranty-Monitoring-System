<%@ Page Title="(LP) Edit Month" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit_Month_LP.aspx.cs" Inherits="MqWebApp2.Edit_Month_LP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ======= Breadcrumbs ======= -->
    <section id="breadcrumbs" class="breadcrumbs lineLP">
      <div class="container">
        <ol>
          <li><a href="Homepage.aspx">Home</a></li>
          <li><a href="LP_Supplier.aspx">LP Supplier Menu</a></li>
          <li>Edit month detail</li>
        </ol>
      </div>
    </section><!-- End Breadcrumbs -->

    <div class="container">

        <section id="form" class="form">

        <div class="container">

        <div class="section-title">
          <h2>Monthly Application Date Information</h2>
          <p>Please insert the information without typing error or capslock error. All data should be match with the existing data.</p>
        </div>

        <div class="yeardropdown">
          <asp:Label runat="server" AssociatedControlID="DropDownList1"><b>Pick Application Month:</b></asp:Label><br />
            <asp:DropDownList ID="DropDownList1" CssClass="yeardropbtn" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="month_year" DataValueField="month_year" >
                <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT [month_year] FROM [month_detail] WHERE ([supplier_type] = @supplier_type) ORDER BY [collection_date] DESC">
                <SelectParameters>
                    <asp:Parameter DefaultValue="LP" Name="supplier_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

        <div class="table-group">
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="BtnEditMonth" runat="server" CausesValidation="false" OnClick="BtnEditMonth_Click" CommandName="" Text="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="BtnDeleteMonth" runat="server" OnClick="BtnDeleteMonth_Click" OnClientClick="return confirm('Are you sure to delete this data?')" CausesValidation="false" CommandName="" Text="Delete" />
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


        </div>

        <div class="form-list">
          <ul>
            <li data-aos="fade-up">
            <div class="center ">
                <div class="row">
                <!--Month-->
                <div class="col-lg-12 form-group">
                  <asp:Label runat="server" AssociatedControlID="txtmonthyear"><b>Month/Year :</b></asp:Label><br />
                  <asp:TextBox runat="server"  Enabled="true" ID="txtmonthyear" CssClass="form-control"  placeholder="Month/Year" ReadOnly="True"></asp:TextBox>
                </div>
                <!-- Collection Date-->
                <div class="col-lg-6 form-group">
                  <asp:Label runat="server" AssociatedControlID="collect_date"><b>Collection Date (Click at Calendar) :</b></asp:Label><br />
                  <asp:TextBox runat="server"  Enabled="true"   ID="collect_date" AutoPostBack="true" OnTextChanged="collect_date_TextChanged" TextMode="Date" CssClass="form-control" placeholder="Collection Date"></asp:TextBox>
                </div>
                <!-- Supplier Dateline -->
                <div class="col-lg-6 form-group">
                  <asp:Label runat="server" AssociatedControlID="claim_dateline"><b>Supplier Dateline :</b></asp:Label><br />
                  <asp:TextBox runat="server" Enabled="true" ID="claim_dateline"  TextMode="Date"  CssClass="form-control" placeholder="Supplier Dateline"></asp:TextBox>
                </div>
                <!-- Pdca date-->
                <div class="col-lg-12 mt-3">
                  <asp:Label runat="server" AssociatedControlID="pdca_date"><b>PDCA Date :</b></asp:Label><br />
                  <asp:TextBox runat="server"  Enabled="true" ID="pdca_date"  CssClass="form-control" placeholder="Supplier Dateline"></asp:TextBox>
                </div>
                <!-- Supplier Type-->
                    <asp:TextBox runat="server"  Enabled="true" ID="supplier_type"  CssClass="form-control" Text="LP" Visible="false"></asp:TextBox>

              <div class="col-md-6 form-group"><p> </p></div><div class="col-md-6 form-group"></div><!--New line skip-->
              
              <asp:Button Text="Update Changes" ID="btnUpdate" OnClick="btnUpdate_Click" CssClass="formSetting2 offset-5" Width="170px" runat="server" />
              </div>
            
            </div>
           </li>
          </ul>
        </div><!-- End Form -->
        
      </div>
    </section><!--  End of Form Section-->




    </div>

</asp:Content>
