<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Especialidades.aspx.cs" Inherits="ExamenFinalHospital.PaginasAdmin.Especialidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center text-primary">Ingresar Especialidades</h1>
    <div class="container text-center">
        <div class="row">
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Codigo:</div>
            <div class="col-sm-12 col-md-4 text-start mt-2">
                <asp:TextBox ID="TextBoxCodigo" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="9999454578" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-2 text-start text-md-end text-lg-end mt-2 fs-5 text-primary">Especialidad:</div>
            <div class="col-sm-12 col-md-4  text-start mt-2">
                <asp:TextBox ID="TextBoxEspecialidad" CssClass="form-control text-primary fs-5 " Width="95%" placeholder="Urologia" runat="server"></asp:TextBox>
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
    <div class=" d-flex justify-content-center">
        <div class="col-sm-10 mt-4 overflow-auto">
            <asp:GridView ID="GridViewListado" CssClass="table table-striped table-hover" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewListado_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Codigo"></asp:BoundField>
                    <asp:BoundField DataField="Especialidad" HeaderText="Especialidad"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-primary"
                                CommandName="Seleccionar" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClientClick="return confirm('Esta seguro de eliminar la especialidad')"
                                CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
