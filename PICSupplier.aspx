<%@ Page Title="Supplier PIC" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PICSupplier.aspx.cs" Inherits="MqWebApp2.PICSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ======= Breadcrumbs ======= -->
    <section id="breadcrumbs" class="breadcrumbs ">
      <div class="container">

        <ol>
          <li><a href="Homepage.aspx">Home</a></li>
          <li><a href="/SupplierInfo.aspx">Supplier Name & Add Year Menu</a></li>
          <li>Supplier Person In Charge </li>
        </ol>

      </div>
    </section><!-- End Breadcrumbs -->

    <section id="form" class="form">
    <div class="container">

        <div class="section-title">
              <h2>Supplier Person In Charge</h2>
              <p>Please insert the information without typing error or capslock error. All data should be match with the existing data.</p>
            </div>

        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" DataSourceID="SqlDataSource1" DataTextField="supplier_code" DataValueField="supplier_code" AppendDataBoundItems="True">
            <Items>
                    <asp:ListItem Text="Select Supplier Code" Value="" Enabled="True" Hidden="True" />
            </Items>
        </asp:DropDownList>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT [supplier_code] FROM [supplier_details] ORDER BY [supplier_type] DESC, [supplier_code]"></asp:SqlDataSource>

                <div class="form-list section-bg">
                      <div class="row">
                        
                        <!-- Supplier Code -->
                        <div class="col-lg-4 ">
                            <asp:Label runat="server" AssociatedControlID="supplier_code"><b>Supplier Code* :</b></asp:Label><br />
                            <asp:TextBox runat="server" ReadOnly="true" Enabled="true" ID="supplier_code" required="required" CssClass="form-control" placeholder="Write supplier code here..."></asp:TextBox>
                        </div>
                        <!-- Supplier Name-->
                        <div class="col-lg-8 form-group">
                         <asp:Label runat="server" AssociatedControlID="supplier_name"><b>Supplier Name*:</b></asp:Label><br />
                         <asp:TextBox runat="server" ReadOnly="true"  Enabled="true" ID="supplier_name" required="required" CssClass="form-control" placeholder="Write supplier name here..."></asp:TextBox>
                        </div>

                         <br />
                        <h4>Person In Charge 1</h4>

                        <!-- PIC No.1 Name -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b> Name: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic1name"  CssClass="form-control" placeholder="Write pic name here..."></asp:TextBox>
                         </div>

                         <!-- PIC No.1 Phone -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b>Phone Number: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic1tel"  CssClass="form-control" placeholder="Write pic telephone here..."></asp:TextBox>
                        </div>

                         <!-- PIC No.1 Email -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b>Email: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic1email"  CssClass="form-control" placeholder="Write pic email here..."></asp:TextBox>
                        </div>

                        <br />
                        <h4>Person In Charge 2</h4>

                        <!-- PIC No.2 Name -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b> Name: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic2name"  CssClass="form-control" placeholder="Write pic name here..."></asp:TextBox>
                         </div>

                         <!-- PIC No.2 Phone -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b>Phone Number: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic2tel"  CssClass="form-control" placeholder="Write pic telephone here..."></asp:TextBox>
                        </div>

                         <!-- PIC No.2 Email -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b>Email: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic2email"  CssClass="form-control" placeholder="Write pic email here..."></asp:TextBox>
                        </div>


                        <br />
                        <h4>Person In Charge 3</h4>

                        <!-- PIC No.3 Name -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b> Name: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic3name"  CssClass="form-control" placeholder="Write pic name here..."></asp:TextBox>
                         </div>

                         <!-- PIC No.3 Phone -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b>Phone Number: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic3tel"  CssClass="form-control" placeholder="Write pic telephone here..."></asp:TextBox>
                        </div>

                         <!-- PIC No.3 Email -->
                        <div class="col-lg-4 form-group">
                        <asp:Label runat="server" ><b>Email: </b></asp:Label><br />
                        <asp:TextBox runat="server"  Enabled="true" ID="pic3email"  CssClass="form-control" placeholder="Write pic email here..."></asp:TextBox>
                        </div>



                        </div>
                        <p>[Column marked(*) is mandatory to fill in]</p>
                      
                      

                      <div class="col-md-12 form-group"><p> </p></div><div class="col-md-12 form-group"><p> </p></div><!--New line skip-->
                     <asp:Button Text="Save" ID="btnsave" OnClick="btnsave_Click" CssClass="formSetting2 offset-5" Width="170px" runat="server" />
                      </form>
                </div>


    </div>
    </section>


</asp:Content>
