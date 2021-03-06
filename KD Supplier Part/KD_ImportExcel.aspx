<%@ Page Title="(KD) Import Excel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KD_ImportExcel.aspx.cs" Inherits="MqWebApp2.KD_Supplier_Part.KD_ImportExcel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ======= Breadcrumbs ======= -->
      <section id="breadcrumbs" class="breadcrumbs lineKD">
          <div class="container">

            <ol>
              <li><a href="/Homepage.aspx">Home</a></li>
              <li><a href="KD_Supplier.aspx">KD Supplier Menu</a></li>
              <li><a href="KD_Add_Claim.aspx">Add new claim</a></li>
              <li>Import Excel File</li>
            </ol>

          </div>
      </section><!-- End Breadcrumbs -->


    <div class="container">

        <div class="section-title">
          <h2>Import Excel as New Record</h2>
          <p>Add new information by uploading file below. The file should follow the instructions below. </p>
        </div>

         <p>Download  :  
             <asp:Button ID="TemplateFileBtn" OnClick="TemplateFileBtn_Click" runat="server" Text="Template" />
             <asp:Button ID="UserManualBtn" OnClick="UserManualBtn_Click" runat="server" Text="User Manual" />
         </p>

        <div class="card-excelupload">

           <p>Please drag/upload file here.</p>
           <asp:DropDownList ID="DropDownApplicationMonth" runat="server" DataSourceID="SqlDataSource1" DataTextField="month_year" DataValueField="month_year" AppendDataBoundItems="True">
               <Items>
                    <asp:ListItem Text="Choose month" Value="" hidden="true"/>
                </Items>
           </asp:DropDownList>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT [month_year] FROM [month_detail] WHERE ([supplier_type] = @supplier_type) ORDER BY [claim_dateline] DESC">
                <SelectParameters>
                    <asp:Parameter DefaultValue="kd" Name="supplier_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

            <div class="excel-fileupload">
                <asp:FileUpload ID="FileUploadExcel" CssClass="uploadzone"  runat="server" /><br />
            </div>
            <div class="text-center">
                <asp:Button ID="BindGrid" runat="server" CssClass="btn-excelupload" Text="Preview Data" OnClick="BindGrid_Click" />
                <asp:Button ID="AddtoDB" runat="server" CssClass="btn-excelupload"  Text="Add Data" OnClick="AddtoDB_Click" /><br />       
                <asp:Label ID="UploadLabel" runat="server" Text=""></asp:Label>
            </div>

        </div>

        <div>
            <asp:Button ID="ResetRecord"  runat="server" onclientclick="return confirm('Reset data in selected month?')" CssClass="" OnClick="ResetRecord_Click" Text="Reset Data" />
            <asp:DropDownList ID="MonthApplication" runat="server" DataSourceID="SqlDataSource1" DataTextField="month_year" DataValueField="month_year" AppendDataBoundItems="True">
                <Items>
                    <asp:ListItem Text="Select Month" Value="" Enabled="True" Hidden="True" />
                </Items>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MQWebAppConnectionString %>" SelectCommand="SELECT [month_year] FROM [month_detail] WHERE ([supplier_type] = @supplier_type) ORDER BY [collection_date] DESC">
                <SelectParameters>
                    <asp:Parameter DefaultValue="KD" Name="supplier_type" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>


        <asp:GridView ID="GridView1" runat="server" Visible="false"  OnRowDataBound="GridView1_RowDataBound"></asp:GridView>
        
        


        <!-- <h1>Instructions</h1>
       
        <p>1. Make sure the file are in .xls format.</p>
        <img alt="Excel_Instructions Photos 1" src="/Content/images/excelsavefile.PNG" style="width:70%;height: 410px;" /> -->


    </div>


</asp:Content>
