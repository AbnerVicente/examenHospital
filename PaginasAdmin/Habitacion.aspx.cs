using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExamenFinalHospital.BD.DataSetHospitalTableAdapters;

namespace ExamenFinalHospital.PaginasAdmin
{
    public partial class Habitacion : System.Web.UI.Page
    {
        HabitacionTableAdapter habitacion = new HabitacionTableAdapter();
        public void Lista()
        {
            GridViewListado.DataSource = habitacion.GetDataListadoHabitaciones();
            GridViewListado.DataBind();

        }
        public void limpiar()
        {
            TextBoxCodigo.Text = "";
            TextBoxCodigo.Text = habitacion.Codigo().ToString();
            TextBoxHabitacion.Text = "";
            Lista();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Lista();
                TextBoxCodigo.Text = habitacion.Codigo().ToString();
            }
            ButtonEditar.Enabled = false;
            TextBoxCodigo.Enabled = false;
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if (TextBoxHabitacion.Text != "")
            {
                DataTable tabla = habitacion.GetDataBuscarPorHabitacion(TextBoxHabitacion.Text);
                if (tabla.Rows.Count == 0)
                {
                    habitacion.GuardarHabitacion(Convert.ToInt32(TextBoxCodigo.Text),TextBoxHabitacion.Text);
                    limpiar();
                    Response.Write("<script>alert('Habitacion ingresada correctamente')</script>");
                }
                else
                    Response.Write("<script>alert('La habitacion ya ha sido ingresado')</script>");
            }
            else
                Response.Write("<script>alert('Ingrese una habitacion')</script>");
        }

        protected void GridViewListado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indice = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Seleccionar":
                    ButtonGuardar.Enabled = false;
                    ButtonEditar.Enabled = true;
                    TextBoxCodigo.Text = GridViewListado.Rows[indice].Cells[0].Text;
                    TextBoxHabitacion.Text = GridViewListado.Rows[indice].Cells[1].Text;
                    break;
                case "Eliminar":
                    try
                    {
                        habitacion.EliminarHabitacion(Convert.ToInt32(GridViewListado.Rows[indice].Cells[0].Text));
                        limpiar();
                    }
                    catch
                    {
                        Response.Write("<script>alert('No se puede eliminar la habitacion')</script>");
                    }

                    break;
            }
        }

        protected void ButtonEditar_Click(object sender, EventArgs e)
        {
            if (TextBoxHabitacion.Text != "")
            {
                DataTable auxHab = habitacion.GetDataBuscarPorCodigo(Convert.ToInt32(TextBoxCodigo.Text));
                string nombreHab = auxHab.Rows[0][1].ToString();
                if (TextBoxHabitacion.Text == nombreHab)
                {
                    habitacion.ActualizarHabitacion(TextBoxHabitacion.Text, Convert.ToInt32(TextBoxCodigo.Text));
                    limpiar();
                    Response.Write("<script>alert('Habitación actualizada correctamente')</script>");
                    ButtonGuardar.Enabled = true;
                    ButtonEditar.Enabled = false;
                }
                else
                {
                    DataTable tabla = habitacion.GetDataBuscarPorHabitacion(TextBoxHabitacion.Text);
                    if (tabla.Rows.Count == 0)
                    {
                        habitacion.ActualizarHabitacion(TextBoxHabitacion.Text, Convert.ToInt32(TextBoxCodigo.Text));
                        limpiar();
                        Response.Write("<script>alert('Habitación actualizada correctamente')</script>");
                        ButtonGuardar.Enabled = true;
                        ButtonEditar.Enabled = false;
                    }
                    else
                    {
                        Response.Write("<script>alert('El nombre de la habitación ya fue ingresado')</script>");
                        ButtonGuardar.Enabled = false;
                        ButtonEditar.Enabled = true;
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Ingrese un nombre de habitación')</script>");
            }
        }
    }
}