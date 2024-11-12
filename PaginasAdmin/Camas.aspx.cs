using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using ExamenFinalHospital.BD.DataSetHospitalTableAdapters;
using ExamenFinalProgra4.BD.DataSetHospitalTableAdapters;

namespace ExamenFinalHospital.PaginasAdmin
{
    public partial class Camas : System.Web.UI.Page
    {
        CamasTableAdapter camas= new CamasTableAdapter();
        HabitacionTableAdapter habitacion= new HabitacionTableAdapter();
        DataTable estado = new DataTable();
        public void Lista()
        {
            GridViewListado.DataSource = camas.GetDataListadoCamas();
            GridViewListado.DataBind();

        }
        public void limpiar()
        {
            TextBoxCodigo.Text = "";
            TextBoxCodigo.Text = camas.Codigo().ToString();
            TextBoxNombre.Text = "";
            TextBoxDescripcion.Text = "";
            DropDownListEstado.SelectedIndex = 0;
            DropDownListHabitacion.SelectedIndex = 0;
            Lista();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Lista();
                estado.Columns.Add("Bit");
                estado.Columns.Add("Estado");
                estado.Rows.Add("False", "Inactivo");
                estado.Rows.Add("True", "Activo");
                DropDownListEstado.DataSource = estado;
                DropDownListEstado.DataValueField = "Bit";
                DropDownListEstado.DataTextField = "Estado";
                DropDownListEstado.DataBind();
                DropDownListEstado.Items.Insert(0, new ListItem("[Seleccione un Estado]"));
                DropDownListHabitacion.DataSource = habitacion.GetDataListadoHabitaciones();
                DropDownListHabitacion.DataValueField = "Codigo";
                DropDownListHabitacion.DataTextField = "Habitacion";
                DropDownListHabitacion.DataBind();
                DropDownListHabitacion.Items.Insert(0, new ListItem("[Seleccione una habitacion]"));
                TextBoxCodigo.Text = camas.Codigo().ToString();
            }
            ButtonEditar.Enabled = false;
            TextBoxCodigo.Enabled = false;
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if(TextBoxNombre.Text!="")
            {
                if(TextBoxDescripcion.Text!="")
                {
                    if(DropDownListEstado.SelectedIndex>0)
                    {
                        if(DropDownListHabitacion.SelectedIndex>0)
                        {
                            DataTable tabla = camas.GetDataBuscarNombreEnHabitacion(Convert.ToInt32(DropDownListHabitacion.SelectedValue), TextBoxNombre.Text);
                            if(tabla.Rows.Count==0)
                            {
                                camas.GuardarCama(Convert.ToInt32(TextBoxCodigo.Text), TextBoxNombre.Text, TextBoxDescripcion.Text, Convert.ToBoolean(DropDownListEstado.SelectedValue), Convert.ToInt32(DropDownListHabitacion.SelectedValue));
                                limpiar();
                                Response.Write("<script>alert('Cama ingresada correctamente')</script>");
                            }
                            else
                                Response.Write("<script>alert('La cama ya ha sido asiganada a la habitacion seleccionada')</script>");
                        }
                        else
                            Response.Write("<script>alert('Seleccione una habitacion')</script>");
                    }
                    else
                        Response.Write("<script>alert('Seleccione un estado')</script>");
                }
                else
                    Response.Write("<script>alert('Ingrese una descripcion')</script>");
            }
            else
                Response.Write("<script>alert('Ingrese un nombre')</script>");
        }

        protected void GridViewListado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indice = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Seleccionar":
                    TextBoxCodigo.Enabled = false;
                    ButtonGuardar.Enabled = false;
                    ButtonEditar.Enabled = true;
                    TextBoxCodigo.Text = GridViewListado.Rows[indice].Cells[0].Text;
                    TextBoxNombre.Text = GridViewListado.Rows[indice].Cells[1].Text;
                    TextBoxDescripcion.Text = GridViewListado.Rows[indice].Cells[2].Text;
                    DropDownListEstado.Text = GridViewListado.Rows[indice].Cells[3].Text;
                    DropDownListHabitacion.SelectedIndex = Convert.ToInt32(GridViewListado.Rows[indice].Cells[4].Text);
                    break;
                case "Eliminar":
                    try
                    {
                        camas.BorrarCama(Convert.ToInt32(GridViewListado.Rows[indice].Cells[0].Text));
                        limpiar();
                    }
                    catch
                    {
                        Response.Write("<script>alert('No se puede eliminar el ciclo')</script>");
                    }
                    break;
            }
        }

        protected void ButtonEditar_Click(object sender, EventArgs e)
        {
            if (TextBoxNombre.Text != "")
            {
                if (TextBoxDescripcion.Text != "")
                {
                    if (DropDownListEstado.SelectedIndex > 0)
                    {
                        if (DropDownListHabitacion.SelectedIndex > 0)
                        {
                            DataTable tabla =camas.GetDataVerificarDuplicados(Convert.ToInt32(TextBoxCodigo.Text),TextBoxNombre.Text,Convert.ToInt32(DropDownListHabitacion.SelectedValue));
                            if (tabla.Rows.Count == 0)
                            {
                                camas.ActualizarCama( TextBoxNombre.Text, TextBoxDescripcion.Text, Convert.ToBoolean(DropDownListEstado.SelectedValue), Convert.ToInt32(DropDownListHabitacion.SelectedValue),Convert.ToInt32(TextBoxCodigo.Text));
                                limpiar();
                                Response.Write("<script>alert('Cama actualizada correctamente')</script>");
                            }
                            else
                                Response.Write("<script>alert('La cama ya ha sido asiganada a la habitacion seleccionada')</script>");
                        }
                        else
                            Response.Write("<script>alert('Seleccione una habitacion')</script>");
                    }
                    else
                        Response.Write("<script>alert('Seleccione un estado')</script>");
                }
                else
                    Response.Write("<script>alert('Ingrese una descripcion')</script>");
            }
            else
                Response.Write("<script>alert('Ingrese un nombre')</script>");
        }
    }
}