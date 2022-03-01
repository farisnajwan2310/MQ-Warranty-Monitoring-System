<%@ Page Title="(KD) Edit Claim" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KD_Edit_Claim.aspx.cs" Inherits="MqWebApp2.KD_Supplier_Part.KD_Edit_Claim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section id="breadcrumbs" class="breadcrumbs lineKD">
      <div class="container">

        <ol>
        <li><a href="/Homepage.aspx">Home</a></li>
        <li><a href="KD_Supplier.aspx">KD Supplier Menu</a></li>
        <li>Edit Claim Record</li>
        </ol>

      </div>
    </section><!-- End Breadcrumbs -->


    <div class="container">

        <div class="yeardropdown">
            <asp:Label runat="server"><b>Pick Application Month:</b></asp:Label><br />
            <asp:DropDownList ID="DropDownApplicationMonth" CssClass="yeardropbtn" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DropDownApplicationMonth_SelectedIndexChanged" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="month_year" DataValueField="month_year">
                <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
            </asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT [month_year] FROM [month_detail] WHERE ([supplier_type] = @supplier_type) ORDER BY [claim_dateline] DESC">
                <SelectParameters>
                    <asp:Parameter DefaultValue="KD" Name="supplier_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <!--=====Link to export data from the system =====-->
        <i class="bi bi-file-earmark-ruled" style="color: #63e07a;"></i><a class="btnaddsupplier-btn" href="KD_ExportData.aspx">  Generate Data</a>


        <div class="section-title">
              <h2>Edit KD Supplier Claim</h2>
        </div><!--End of title-->
    
        
        <asp:Label ID="SearchCodeLabel" runat="server" CssClass="offset-1" Visible="false" Text="Search Supplier Code:"></asp:Label><br />
        <asp:TextBox ID="SearchSuppCode" Visible="false" CssClass="offset-1" runat="server"></asp:TextBox>
        <asp:Button ID="SearchCodeBtn" runat="server" Text="Search" Visible="false" OnClick="SearchCodeBtn_Click" />
        <asp:Button ID="RefreshBtn" runat="server" Text="Refresh Grid" Visible="false" OnClick="DropDownApplicationMonth_SelectedIndexChanged"  />

    <div class="table-group">
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" GridLines="None" HorizontalAlign="Center" PageSize="8" AllowCustomPaging="False" AllowPaging="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="SelectBtn" runat="server" CausesValidation="false" CommandName="" OnClick="SelectBtn_Click" Text="Select" />
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
            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
        </asp:GridView>

        <asp:Label ID="PageNumber" runat="server" Text=""></asp:Label>
    </div>

        <section id="form" class="form">
            <section id="counts" class="counts">
                <div class="container mt-6">
                    <div class="row counters">
                        <div class="col-lg-12 col-6 text-center">
                            <asp:Label ID="TotalCollected" runat="server" Text="-Please Select Record-"></asp:Label>
                            <p>Total Amount(RM) Colllected</p>
                        </div>
                    </div>
                </div>
            </section>
        

        <div class="form-list ">
            <ul>
                <li data-aos="fade-up">
                    <div class="center">
                        <p><i>Please fill in the form</i></p>
                        <div class="row">
                            <div class="col-lg-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="UpdateMonthTextBox"><b>Month/Year :</b></asp:Label><br />
                                <asp:TextBox ID="UpdateMonthTextBox" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="UpdateSupplierCode"><b>Supplier Code:</b></asp:Label> <br />
                                <asp:TextBox ID="UpdateSupplierCode" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                            <div class="col-lg-12 form-group">
                                <asp:Label runat="server" AssociatedControlID="UpdateSupplierName"><b>Supplier Name:</b></asp:Label><br />
                                <asp:TextBox ID="UpdateSupplierName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 form-group">
                                <asp:Label runat="server" AssociateControlID="status_claim"><b>Status Claim</b></asp:Label><br />
                                <asp:DropDownList runat="server" ID="status_claim" CssClass="dropdownform" ValidateRequestMode="Disabled">
                                    <asp:ListItem Text="Pending"/>
                                    <asp:ListItem Text="Incomplete"/>
                                    <asp:ListItem Text="Completed"/>
                                    <asp:ListItem Text="Others"/>
                                </asp:DropDownList>
                            </div>
                             <div class="col-lg-4 form-group">
                                 <asp:Label runat="server" AssociatedControlID="date_claim"><b>Claim Date:</b></asp:Label><br />
                                 <asp:TextBox runat="server"  Enabled="true" ID="date_claim" TextMode="Date" CssClass="form-control" placeholder="Write Date Claim Submitted here..."></asp:TextBox>                
                             </div>
                            <div class="col-lg-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="claim_remarks"><b>Claim Remarks:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="claim_remarks" CssClass="form-control" placeholder="Write Claim Remarks here..."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="total_claim"><b>Total Claim:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="total_claim" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Total Claim here..."></asp:TextBox>                
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="total_amount"><b>Total Amount:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="total_amount" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Total Claim(RM) here..."></asp:TextBox>
                            </div>

                            <!-- ======== New line for First Submission case ========-->
                            <a data-toggle="collapse" data-target="#first" class="collapsed"><h5>First Submission (Click here to edit)
                            </h5><i class="bx bx-chevron-down icon-show"></i><i class="bx bx-chevron-up icon-close"></i></a>
                            <div id="first" class="collapse" data-parent=".addclaim-form">
                            <div class="row">

                            
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="approved_amount_1st"><b>Approve Amount (RM):</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="approved_amount_1st" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Approved Amount here..."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="approved_1st"><b> Quantity Approved:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="approved_1st" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Approved Quantity here..."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="rejected_amount_1st"><b> Rejected Amount (RM):</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="rejected_amount_1st"  Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Rejected Amount here..."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="rejected_1st"><b> Quantity Rejected:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="rejected_1st" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Rejected Quantity here..."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="amount_1st"><b>Total Amount:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="amount_1st" Text="0" TextMode="Number" CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder="Write Total Amount here..."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="quantity_1st"><b>Total Quantity:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="quantity_1st" TextMode="Number" Text="0" CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder="Write Total Quantity here..."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="pending_amount_1st"><b> Pending Amount(RM):</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="pending_amount_1st" Text="0" CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder="Write Pending Amount here..."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <asp:Label runat="server" AssociatedControlID="pending_1st"><b> Quantity Pending:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="pending_1st" TextMode="Number" Text="0" CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder="Write Quantity Pending here..."></asp:TextBox>
                            </div>
                            <div class="col-lg-3 form-group">
                                <asp:Label runat="server" AssociatedControlID="date_1st"><b> 1st Submission Date:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="date_1st" TextMode="Date"  CssClass="form-control" placeholder="Pick date..."></asp:TextBox>
                             </div>
                             <div class="col-lg-9 form-group">
                                <asp:Label runat="server" AssociatedControlID="remarks_1st"><b>  1st Submission Remarks:</b></asp:Label><br />
                                <asp:TextBox runat="server"  Enabled="true" ID="remarks_1st" CssClass="form-control" placeholder="Write any remarks here.."></asp:TextBox>
                             </div>
                          </div><!--End of 1st submit row-->
                
                            </div><!--End of 1st submit collapse-->

                            <!-- ========New line for Second Submission case========-->
                        
                            <a data-toggle="collapse" data-target="#second" class="collapsed"><h5>Second Submission (Click here to edit)
                            </h5><i class="bx bx-chevron-down icon-show"></i><i class="bx bx-chevron-up icon-close"></i></a>
                          <div id="second" class="collapse" data-parent=".addclaim-form">
                          <div class="row">
                          
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="amount_approved_2nd"><b>  Approved Amount (RM):</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="amount_approved_2nd"  Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Total Quantity here.."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="approved_2nd"><b>  Quantity Approved:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="approved_2nd" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Total Quantity here.."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="amount_rejected_2nd"><b>  Rejected Amount (RM):</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="amount_rejected_2nd" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Rejected amount here.."></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="rejected_2nd"><b>  Quantity Rejected:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="rejected_2nd" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder="Write Rejected amount here.."></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="amount_2nd"><b>  Total Amount:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="amount_2nd"  Text="0"  CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder="Write Amount here.."></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="quantity_2nd"><b>  Total Quantity:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="quantity_2nd"  Text="0" CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder="Write Total Quantity here.."></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="pending_amount_2nd"><b> Pending Amount(RM):</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="pending_amount_2nd" Text="0" CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder="Write Pending amount here.."></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="pending_2nd"><b> Quantity Pending:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="pending_2nd" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" ReadOnly="True" placeholder="Write Pending Quantity here.."></asp:TextBox>
                            </div>
                            <div class="col-lg-3 form-group">
                            <p> </p>
                              <asp:Label runat="server" AssociatedControlID="date_2nd"><b>  2nd Submission Date:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="date_2nd" TextMode="Date" CssClass="form-control" placeholder="Write Date 2nd Claim here.." ></asp:TextBox>
                           </div>
                           <div class="col-lg-9 form-group">
                            <p> </p>
                              <asp:Label runat="server" AssociatedControlID="remarks_2nd"><b>  2nd Submission Remarks:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="remarks_2nd" CssClass="form-control" placeholder="Write Remarks here.."></asp:TextBox>
                           </div>
                          </div><!--End collapse 2nd submit-->
                        </div><!--End 2nd submit row-->

                            <!-- ========New line for Third Submission case========-->
                            <a data-toggle="collapse" data-target="#third" class="collapsed"><h5>Third Submission (Click here to edit)
                            </h5><i class="bx bx-chevron-down icon-show"></i><i class="bx bx-chevron-up icon-close"></i></a>
                          <div id="third" class="collapse" data-parent=".addclaim-form">
                          <div class="row">
                          
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="amount_approved_3rd"><b>Approved Amount (RM):</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="amount_approved_3rd" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder=""></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="approved_3rd"><b>Quantity Approved:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="approved_3rd" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder=""></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="amount_rejected_3rd"><b> Rejected Amount (RM):</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="amount_rejected_3rd" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder=""></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="rejected_3rd"><b> Quantity Rejected:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="rejected_3rd" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" placeholder=""></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="amount_3rd"><b>Total Amount:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="amount_3rd" Text="0" CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder=""></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="quantity_3rd"><b>Total Quantity:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="quantity_3rd" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" ReadOnly="True" placeholder=""></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="pending_amount_3rd"><b>Pending Amount(RM):</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="pending_amount_3rd" Text="0" CssClass="form-control" ReadOnly="True" ClientIDMode="Static" placeholder=""></asp:TextBox>
                            </div>
                          <div class="col-md-6 form-group">
                              <asp:Label runat="server" AssociatedControlID="pending_3rd"><b>Quantity Pending:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="pending_3rd" TextMode="Number" Text="0" CssClass="form-control" ClientIDMode="Static" ReadOnly="True" placeholder=""></asp:TextBox>
                            </div>
                          <div class="col-lg-3 form-group">
                            <p> </p>
                              <asp:Label runat="server" AssociatedControlID="date_3rd"><b> 3rd Submission Date:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="date_3rd" TextMode="Date" CssClass="form-control" placeholder="" ></asp:TextBox>
                           </div>
                           <div class="col-lg-9 form-group">
                            <p> </p>
                              <asp:Label runat="server" AssociatedControlID="remarks_3rd"><b> 3rd Submission Remarks:</b></asp:Label><br />
                              <asp:TextBox runat="server"  Enabled="true" ID="remarks_3rd" Text="-" CssClass="form-control" placeholder=""></asp:TextBox>
                           </div>
                  

                            </div><!-- End of row from 3rd submission -->
                
                          </div><!--End of collapse 3rd submission-->
                          <asp:TextBox runat="server"  Enabled="true" ID="supplier_type1"  CssClass="form-control" Text="KD" Visible="False"></asp:TextBox>
                          <asp:Button Text="Delete" ID="btndelete"  CssClass="formSetting2 offset-2 mt-4" OnClick="btndelete_Click" OnClientClick="return confirm('Are you sure to delete this data?')"  Width="170px" runat="server" />
                          <asp:Button Text="Update" ID="btnupdate"  CssClass="formSetting2 offset-3 mt-4" OnClick="btnupdate_Click" Width="170px" runat="server" />
                            
                        </div>
                    </div>
                </li>
            </ul>
        </div>
      </section>
    
    


    </div> <!--container closing-->


    <script language="javascript" type="text/javascript">
        //Function to show the real-time calculation at the approve + reject
        function GetTotal() {
            //TotalAmount+PendingFirst
            var TotalAmount = parseFloat($('#total_amount').val());
            var amountapp1 = parseFloat($('#approved_amount_1st').val());
            var amountrej1 = parseFloat($('#rejected_amount_1st').val());
            var TotalAmount1 = amountapp1 + amountrej1;
            var amountpend1 = TotalAmount - (amountapp1 + amountrej1);
            $('#amount_1st').val(TotalAmount1);
            $('#pending_amount_1st').val(amountpend1);

            //TotalQuantity+PendingFirst
            var TotalQuantity = parseInt($('#total_claim').val());
            var quantapp1 = parseInt($('#approved_1st').val());
            var quantrej1 = parseInt($('#rejected_1st').val());
            var TotalQuantity1 = quantapp1 + quantrej1;
            var quantpend1 = TotalQuantity - TotalQuantity1;
            $('#quantity_1st').val(TotalQuantity1);
            $('#pending_1st').val(quantpend1);

            //TotalAmount+PendingSecond
            var amountapp2 = parseFloat($('#amount_approved_2nd').val());
            var amountrej2 = parseFloat($('#amount_rejected_2nd').val());
            var TotalAmount2 = amountapp2 + amountrej2;
            var amountpend2 = amountpend1 - TotalAmount2;
            $('#amount_2nd').val(TotalAmount2);
            $('#pending_amount_2nd').val(amountpend2);
            
            //TotalQuantity+PendingSecond
            var quantapp2 = parseInt($('#approved_2nd').val());
            var quantrej2 = parseInt($('#rejected_2nd').val());
            var TotalQuantity2 = quantapp2 + quantrej2;
            var quantpend2 = quantpend1 - TotalQuantity2;
            $('#quantity_2nd').val(TotalQuantity2);
            $('#pending_2nd').val(quantpend2);

            //TotalAmount+PendingThird
            var amountapp3 = parseFloat($('#amount_approved_3rd').val());
            var amountrej3 = parseFloat($('#amount_rejected_3rd').val());
            var TotalAmount3 = amountapp3 + amountrej3;
            var amountpend3 = amountpend2 - TotalAmount3;
            $('#amount_3rd').val(TotalAmount3);
            $('#pending_amount_3rd').val(amountpend3);

            //TotalQuantity+PendingThird
            var quantapp3 = parseInt($('#approved_3rd').val());
            var quantrej3 = parseInt($('#rejected_3rd').val());
            var TotalQuantity3 = quantapp3 + quantrej3;
            var quantpend3 = quantpend2 - TotalQuantity3;
            $('#quantity_3rd').val(TotalQuantity3);
            $('#pending_3rd').val(quantpend3);

        }
    </script>
</asp:Content>
