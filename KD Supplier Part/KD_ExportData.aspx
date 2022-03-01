<%@ Page Title="(KD) Export Data" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KD_ExportData.aspx.cs" Inherits="MqWebApp2.KD_Supplier_Part.KD_ExportData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section id="breadcrumbs" class="breadcrumbs lineKD">
      <div class="container">

        <ol>
        <li><a href="/Homepage.aspx">Home</a></li>
        <li><a href="KD_Supplier.aspx">KD Supplier Menu</a></li>
        <li><a href="KD_Edit_Claim.aspx">Edit Claim Record</a></li>
        <li>Export Data</li>
        </ol>

      </div>
    </section><!-- End Breadcrumbs -->


    <div class="container">

        <div class="yeardropdown">
             <asp:DropDownList ID="DropDownList1" CssClass="yeardropbtn" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" DataSourceID="SqlDataSource1" DataTextField="month_year" AppendDataBoundItems="True" DataValueField="month_year" AutoPostBack="True">
                 <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
             </asp:DropDownList>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT [month_year] FROM [month_detail] WHERE ([supplier_type] = @supplier_type) ORDER BY [collection_date] DESC">
                 <SelectParameters>
                     <asp:Parameter DefaultValue="KD" Name="supplier_type" Type="String" />
                 </SelectParameters>
             </asp:SqlDataSource>
         </div>

        <asp:Button ID="BtnExportData" runat="server" Text="Export Data" OnClick="BtnExportData_Click" />


        <br />
        <p>Data Preview</p>
        <div class="table-group">
            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server"></asp:GridView>
        </div>
    </div>


</asp:Content>
