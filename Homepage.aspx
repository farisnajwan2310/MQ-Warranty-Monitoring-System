<%@ Page Title="Warranty Claim Monitoring" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="MqWebApp2.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <section id="breadcrumbs" class="breadcrumbs">
        <div class="container">
        <ol>
        <li><a href="#">Supplier Claim</a></li>
        <li></li>
        </ol>
        </div>
    </section><!-- End Breadcrumbs -->
    
    

        <h2></h2>
        
    <section id="supplier_menu" class="supplier_menu">
    
      <div class="container">

        

        <div class="row">
          <div class="col-md-6 d-flex align-items-stretch">
            <div class="card" style='background-image: url("/Content/images/LP.jpg");'>
              <div class="card-body">
                <h5 class="card-title"><a href="LP_Supplier.aspx">LP Supplier Claim</a></h5>
                <p class="card-text">List of claim record from LP Supplier from January to December. </p>
                

                <!--Choose month LP Supplier-->
                
                <!--End of LP Suppier-->
              </div>
            </div>
          </div>
          <div class="col-md-6 d-flex align-items-stretch mt-4 mt-md-0">
            <div class="card" style='background-image: url("/Content/images/kd_supplier.jpg");'>
              <div class="card-body">
                <h5 class="card-title"><a href="KD Supplier Part/KD_Supplier.aspx">KD Supplier Claim</a></h5>
                <p class="card-text">List of claim record from KD Supplier from January to December. </p>
              </div>
            </div>
          </div>
          <div class="col-md-6 d-flex align-items-stretch mt-4">
            <div class="card" style='background-image: url("/Content/images/front2.jpg");'>
              <div class="card-body">
                <h5 class="card-title"><a href="PUD Claim/PUD_Claim.aspx">PUD Claim</a></h5>
                <p class="card-text">List of another claim besides Local (LP) and Genpo (KD) Supplier</p>
              </div>
            </div>
          </div>
          <div class="col-md-6 d-flex align-items-stretch mt-4">
            <div class="card" style='background-image: url("/Content/images/our-values-3.jpg");'>
              <div class="card-body">
                <h5 class="card-title"><a href="All Claim/All_Claim.aspx">All Total Claim</a></h5>
                <p class="card-text">Summary of All Total Claim (Amount&Case)</p>
            </div>
          </div>
        </div>

      </div>
    </div>
    </section><!-- End Our Values Section -->


</asp:Content>
