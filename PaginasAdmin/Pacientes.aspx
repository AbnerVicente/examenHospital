<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="ExamenFinalProgra4.PaginasAdmin.Pacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center text-primary">Ingresar Pacientes</h1>
    <div class="container text-center">
        <div class="row">
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Codigo:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:TextBox ID="TextBoxCodigo" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="9999454578" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Nombre:</div>
            <div class="col-sm-12 col-md-4  text-start mt-2">
                <asp:TextBox ID="TextBoxNombre" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="Juan" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Apellido:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:TextBox ID="TextBoxApellido" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="Perez" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Fecha_Nac:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:TextBox ID="TextBoxFechaNac" CssClass="form-control text-primary fs-5 " Width="95%" TextMode="Date" placeholder="12/12/2012" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Direccion:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:TextBox ID="TextBoxDireccion" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="0-58 zona 2" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Telefono:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:TextBox ID="TextBoxTelefono" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="47886587" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Departamento:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownListDepartamento" CssClass="form-select rounded-2 text-primary fs-5 border-primary" Width="95%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListDepartamento_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Municipio:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownListMunicipio" CssClass="form-select rounded-2 text-primary fs-5 border-primary" Width="95%" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-6 text-center">
                <asp:Button ID="ButtonGuardar" CssClass="btn btn-success btn-lg fs-5 mt-3" Width="70%" runat="server" Text="Guardar" OnClick="ButtonGuardar_Click" />
            </div>
            <div class="col-sm-12 col-md-6 text-center">
                <asp:Button ID="ButtonEditar" CssClass="btn btn-warning btn-lg fs-5 mt-3" Width="70%" runat="server" Text="Editar" OnClick="ButtonEditar_Click" />
            </div>
        </div>
    </div>
    <div>
        <div class="col-sm-12 mt-4 overflow-auto">
            <asp:GridView ID="GridViewListado" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewListado_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="DPI"></asp:BoundField>
                    <asp:BoundField DataField="Paciente" HeaderText="Paciente"></asp:BoundField>
                    <asp:BoundField DataField="Direccion" HeaderText="Direccion"></asp:BoundField>
                    <asp:BoundField DataField="Telefono" HeaderText="Telefono"></asp:BoundField>
                    <asp:BoundField DataField="FechaNac" HeaderText="FechaNac" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="CodMuni" HeaderText="CodMuni"></asp:BoundField>
                    <asp:BoundField DataField="Municipio" HeaderText="Municipio"></asp:BoundField>
                    <asp:BoundField DataField="Departamento" HeaderText="Departamento"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-primary"
                                CommandName="Seleccionar" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClientClick="return confirm('Esta seguro de eliminar el Paciente')"
                                CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>


</asp:Content>
