<%@ Page Title="Add Supplier Record" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddSupplierName.aspx.cs" Inherits="MqWebApp2.AddSupplierName" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ======= Breadcrumbs ======= -->
    <section id="breadcrumbs" class="breadcrumbs ">
      <div class="container">

        <ol>
          <li><a href="Homepage.aspx">Home</a></li>
          <li><a href="/SupplierInfo.aspx">Supplier Name & Add Year Menu</a></li>
          <li>Add New Supplier Name</li>
        </ol>

      </div>
    </section><!-- End Breadcrumbs -->

    <section id="form" class="form ">


          <div class="container">

            <div class="section-title">
              <h2>Add New Supplier Name Record</h2>
              <p>Please insert the information without typing error or capslock error. All data should be match with the existing data.</p>
            </div>

                <div class="form-list section-bg">
                  <form action="forms/edit_month.html" method="post" role="form" class="formSetting">
                      <div class="row">
                        
                        <!-- Supplier Code -->
                        <div class="col-lg-4 ">
                            <asp:Label runat="server" AssociatedControlID="supplier_code"><b>Supplier Code* :</b></asp:Label><br />
                            <asp:TextBox runat="server"  Enabled="true" ID="supplier_code" required="required" CssClass="form-control" placeholder="Write supplier code here..."></asp:TextBox>
                        </div>
                        <!-- Supplier Name-->
                        <div class="col-lg-8 form-group">
                         <asp:Label runat="server" AssociatedControlID="supplier_name"><b>Supplier Name*:</b></asp:Label><br />
                         <asp:TextBox runat="server"  Enabled="true" ID="supplier_name" required="required" CssClass="form-control" placeholder="Write supplier name here..."></asp:TextBox>
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
                         <asp:TextBox runat="server"  Enabled="true" ID="supplier_address" TextMode="MultiLine" required="required" CssClass="form-control" placeholder="Write supplier address here..."></asp:TextBox>
                        </div>
                        <!-- Supplier Telephone Contact-->
                        <div class="col-lg-6 form-group">
                         <asp:Label runat="server" AssociatedControlID="supplier_telephone"><b>Office Telephone Number:</b></asp:Label><br />
                         <asp:TextBox runat="server"  Enabled="true" ID="supplier_telephone" CssClass="form-control" placeholder="Write supplier telephone here..."></asp:TextBox>
                        </div>
                          <!-- Supplier Fax Contact-->
                        <div class="col-lg-6 form-group">
                         <asp:Label runat="server" AssociatedControlID="supplier_fax"><b>Office Fax No.:</b></asp:Label><br />
                         <asp:TextBox runat="server"  Enabled="true" ID="supplier_fax" CssClass="form-control" placeholder="Write supplier Fax No. here..."></asp:TextBox>
                        </div>


                        </div>
                        <p>[Column marked(*) is mandatory to fill in]</p>
                      
                      

                      <div class="col-md-12 form-group"><p> </p></div><div class="col-md-12 form-group"><p> </p></div><!--New line skip-->
                     <asp:Button Text="Save" ID="btnsave" OnClick="AddSupplierDetails" CssClass="formSetting2 offset-5" Width="170px" runat="server" />
                      </form>
                </div>

                <!-- End Form -->
        

          

          </div>
        </section>
        <div class="container">
             <div id="btnaddsupplierIndex" class="btnaddsupplierIndex">
                <a class="btnaddsupplierIndex-btn" href="Edit_SupplierDetails.aspx">Edit Supplier Name & Detail</a> <br />
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT supplier_code, supplier_name, supplier_type, supplier_address, supplier_tel, supplier_fax FROM supplier_details ORDER BY supplier_type DESC, supplier_code"></asp:SqlDataSource>
            <asp:GridView ID="GridViewSupplierDetail" runat="server" OnRowDataBound="GridViewSupplierDetail_RowDataBound" AutoGenerateColumns="False" CellPadding="3" DataSourceID="SqlDataSource1" HorizontalAlign="Center" GridLines="Vertical" Width="1133px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" AllowPaging="True">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate >
                             <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:BoundField DataField="supplier_code" HeaderText="supplier_code" SortExpression="supplier_code" >
                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="supplier_name" HeaderText="supplier_name" SortExpression="supplier_name" >
                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="supplier_type" HeaderText="supplier_type" SortExpression="supplier_type" >
                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="supplier_address" HeaderText="supplier_address" SortExpression="supplier_address" >
                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="supplier_tel" HeaderText="supplier_tel" SortExpression="supplier_tel" >
                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="supplier_fax" HeaderText="supplier_fax" SortExpression="supplier_fax" >
                    <ItemStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#FF3300" ForeColor="Black" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" BackColor="Red" Font-Bold="True" />
                <PagerStyle BackColor="#FF2B2B" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" Font-Size="12pt" Font-Strikeout="False" Font-Underline="True" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
        
  
<!--  End of Form-->

</asp:Content>
