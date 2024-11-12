<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="IngresarPacientes.aspx.cs" Inherits="ExamenFinalHospital.PaginasAdmin.IngresarPacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center text-primary">Pacientes</h1>
    <div class="container text-center">
        <div class="row">
            <div class="col-sm-12 col-md-4 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Codigo:</div>
            <div class="col-sm-12 col-md-4  text-start mt-2">
                <asp:TextBox ID="TextBoxCodigo" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="1" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-4 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Fecha:</div>
            <div class="col-sm-12 col-md-4  text-start mt-2">
                <asp:TextBox ID="TextBoxFecha" CssClass="form-control text-primary fs-5 " Width="95%" TextMode="Date" placeholder="12/12/2000" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-4 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Cama:</div>
            <div class="col-sm-12 col-md-4  text-start mt-2">
                <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownListCama" CssClass="form-select rounded-2 text-primary fs-5 border-primary" Width="95%" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-4 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Paciente:</div>
            <div class="col-sm-12 col-md-4  text-start mt-2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownListPacientes" CssClass="form-select rounded-2 text-primary fs-5 border-primary" Width="95%" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-4 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Medico:</div>
            <div class="col-sm-12 col-md-4  text-start mt-2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownListMedicos" CssClass="form-select rounded-2 text-primary fs-5 border-primary" Width="95%" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row text-center">
            <div class="col-sm-12 col-md-6 text-center">
                <asp:Button ID="ButtonGuardar" CssClass="btn btn-success btn-lg fs-5 mt-3" Width="70%" runat="server" Text="Guardar" OnClick="ButtonGuardar_Click"/>
            </div>
            <div class="col-sm-12 col-md-6 text-center">
                <asp:Button ID="ButtonEditar" CssClass="btn btn-warning btn-lg fs-5 mt-3" Width="70%" runat="server" Text="Editar"/>
            </div>
        </div>
        <div class="d-flex align-items-center">
            <div class="col-sm-10 mt-4 overflow-auto">
                <asp:GridView ID="GridViewListado" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewListado_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo"></asp:BoundField>
                        <asp:BoundField DataField="Ingreso" HeaderText="Ingreso" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="Egreso" HeaderText="Egreso" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="CodCama" HeaderText="CodCama"></asp:BoundField>
                        <asp:BoundField DataField="CodPaciente" HeaderText="CodPaciente"></asp:BoundField>
                        <asp:BoundField DataField="CodMedico" HeaderText="CodMedico"></asp:BoundField>
                        <asp:BoundField DataField="Medico" HeaderText="Medico"></asp:BoundField>
                        <asp:BoundField DataField="Paciente" HeaderText="Paciente"></asp:BoundField>
                        <asp:BoundField DataField="Cama" HeaderText="Cama"></asp:BoundField>
                        <asp:BoundField DataField="EstadoCama" HeaderText="Estado Cama"></asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-primary"
                                    CommandName="Seleccionar" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDarDeAlta" runat="server" Text="Dar de alta" CssClass="btn btn-danger" OnClientClick="return confirm('¿Esta seguro de dar de alta al paciente?')"
                                    CommandName="Alta" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
