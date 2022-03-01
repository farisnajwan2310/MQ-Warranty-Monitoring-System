<%@ Page Title="Edit Supplier Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit_SupplierDetails.aspx.cs" Inherits="MqWebApp2.Edit_SupplierDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ======= Breadcrumbs ======= -->
    <section id="breadcrumbs" class="breadcrumbs ">
      <div class="container">

        <ol>
          <li><a href="Homepage.aspx">Home</a></li>
          <li><a href="/SupplierInfo.aspx">Supplier Name & Add Year Menu</a></li>
          <li>Edit Supplier Details</li>
        </ol>

      </div>
    </section><!-- End Breadcrumbs -->

    <div class="container">

        <div class="section-title">
              <h2>Edit Supplier Details </h2>
        </div><!--End of title-->

        <!--Search Supplier by Code-->
        <asp:Label ID="SearchCodeLabel" runat="server" CssClass="offset-1" Text="Search Supplier Code:"></asp:Label><br />
        <asp:TextBox ID="SearchSuppCode" CssClass="offset-1" runat="server"></asp:TextBox>
        <asp:Button ID="SearchCodeBtn" runat="server" Text="Search" OnClick="SearchCodeBtn_Click"/>
        <asp:Button ID="RefreshBtn" runat="server" Text="Refresh Grid" OnClick="RefreshBtn_Click"  />

        <asp:GridView ID="GridViewSearchResult" Visible="false" runat="server" OnRowDataBound="GridViewSearchResult_RowDataBound" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center">
            
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="SelectSearchTable" OnClick="SelectSearchTable_Click" runat="server" CausesValidation="false" CommandName="" Text="Select"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="Red" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>

        <section class="form">
        <div class="form-list section-bg">
                  <form action="forms/edit_month.html" method="post" role="form" class="formSetting">
                      <div class="row">
                        
                        <!-- Supplier Code -->
                        <div class="col-lg-4 ">
                            <asp:Label runat="server" AssociatedControlID="supplier_code"><b>Supplier Code* :</b></asp:Label><br />
                            <asp:TextBox runat="server"  Enabled="true" ReadOnly="true" ID="supplier_code" CssClass="form-control" placeholder="Write supplier code here..."></asp:TextBox>
                        </div>
                        <!-- Supplier Name-->
                        <div class="col-lg-8 form-group">
                         <asp:Label runat="server" AssociatedControlID="supplier_name"><b>Supplier Name*:</b></asp:Label><br />
                         <asp:TextBox runat="server"  Enabled="true" ID="supplier_name" CssClass="form-control" placeholder="Write supplier name here..."></asp:TextBox>
                        </div>
                        <!-- Supplier Type-->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" AssociateControlID="supplier_type"><b>Supplier Type</b></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="supplier_type" CssClass="dropdownform">
                                    <asp:ListItem Text="LP"/>
                                    <asp:ListItem Text="KD"/>
                                    </asp:DropDownList>
                        </div>
                         <!-- Supplier Address-->
                        <div class="col-lg-8 form-group">
                         <asp:Label runat="server" AssociatedControlID="supplier_address"><b>Supplier Address*:</b></asp:Label><br />
                         <asp:TextBox runat="server"  Enabled="true" ID="supplier_address" TextMode="MultiLine" CssClass="form-control" placeholder="Write supplier address here..."></asp:TextBox>
                        </div>
                        <!-- Supplier Telephone Contact-->
                        <div class="col-lg-6 form-group">
                         <asp:Label runat="server" AssociatedControlID="supplier_telephone"><b>Supplier Telephone Number:</b></asp:Label><br />
                         <asp:TextBox runat="server"  Enabled="true" ID="supplier_telephone" CssClass="form-control" placeholder="Write supplier telephone here..."></asp:TextBox>
                        </div>
                          <!-- Supplier Fax Contact-->
                        <div class="col-lg-6 form-group">
                         <asp:Label runat="server" AssociatedControlID="supplier_fax"><b>Supplier Fax No.:</b></asp:Label><br />
                         <asp:TextBox runat="server"  Enabled="true" ID="supplier_fax" CssClass="form-control" placeholder="Write supplier Fax No. here..."></asp:TextBox>
                        </div>


                        </div>
                        <p>[Column marked(*) is mandatory to fill in]</p>
                      

                      <div class="col-md-12 form-group"><p> </p></div><div class="col-md-12 form-group"><p> </p></div><!--New line skip-->
                     <asp:Button Text="Delete" ID="BtnDelete" OnClick="BtnDelete_Click" onclientclick="return confirm('Are you sure to delete this data?')" CssClass="formSetting2 offset-2" Width="170px" runat="server" />
                      <asp:Button Text="Save" ID="BtnSave" OnClick="BtnSave_Click" CssClass="formSetting2 offset-2" Width="170px" runat="server" />
                      </form>
                </div>
        </section>

        <div class="table-group">
        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" HorizontalAlign="Center" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField>
                        <ItemTemplate >
                             <%#Container.DataItemIndex + 1%>
                        </ItemTemplate></asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="SelectButton" runat="server" CausesValidation="false" OnClick="SelectButton_Click" CommandName="" Text="Select"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Red" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Underline="True" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:Label ID="PageNumber" runat="server" Text=""></asp:Label>
        </div>
        



     <div>

     </div>

    </div>


</asp:Content>
