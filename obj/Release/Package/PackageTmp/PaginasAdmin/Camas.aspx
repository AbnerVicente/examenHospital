<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Camas.aspx.cs" Inherits="ExamenFinalHospital.PaginasAdmin.Camas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center text-primary">Ingresar Cama</h1>
    <div class="container text-center">
        <div class="row">
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Codigo:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:TextBox ID="TextBoxCodigo" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="9999454578" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Nombre Cama:</div>
            <div class="col-sm-12 col-md-4  text-start mt-2">
                <asp:TextBox ID="TextBoxNombre" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="Cama 1" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Descripcion:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:TextBox ID="TextBoxDescripcion" CssClass="form-control text-primary fs-5 " Width="95%" TextMode="MultiLine" placeholder="Imperial" runat="server"></asp:TextBox>
            </div>
            <asp:ScriptManager runat="server" ID="ScriptManager2"></asp:ScriptManager>
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Estado:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownListEstado" CssClass="form-select rounded-2 text-primary fs-5 border-primary" Width="95%" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Habitacion:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownListHabitacion" CssClass="form-select rounded-2 text-primary fs-5 border-primary" Width="95%" runat="server" AutoPostBack="True"></asp:DropDownList>
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
    <div class="d-flex align-items-center">
        <div class="col-sm-12 mt-4 overflow-auto">
            <asp:GridView ID="GridViewListado" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewListado_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Codigo"></asp:BoundField>
                    <asp:BoundField DataField="Nombre" HeaderText="Medico"></asp:BoundField>
                    <asp:BoundField DataField="Descripcion" HeaderText="Direccion"></asp:BoundField>
                    <asp:BoundField DataField="Estado" HeaderText="Telefono"></asp:BoundField>
                    <asp:BoundField DataField="CodHabitacion" HeaderText="CodHabitacion"></asp:BoundField>
                    <asp:BoundField DataField="Habitacion" HeaderText="Habitacion"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-primary"
                                CommandName="Seleccionar" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClientClick="return confirm('Esta seguro de eliminar al Medico')"
                                CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
