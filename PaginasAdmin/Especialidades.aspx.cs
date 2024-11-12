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
    public partial class Especialidades : System.Web.UI.Page
    {
        EspecialidadTableAdapter Especialidad= new EspecialidadTableAdapter();
        public void Lista()
        {
            GridViewListado.DataSource = Especialidad.GetDataListadoEspecialidades();
            GridViewListado.DataBind();

        }
        public void limpiar()
        {
            TextBoxCodigo.Text = "";
            TextBoxCodigo.Text = Especialidad.Codigo().ToString();
            TextBoxEspecialidad.Text = "";
            Lista();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Lista();
                TextBoxCodigo.Text = Especialidad.Codigo().ToString();
            }
            ButtonEditar.Enabled = false;
            TextBoxCodigo.Enabled = false;
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if(TextBoxEspecialidad.Text!="")
            {
                DataTable tabla = Especialidad.GetDataBuscarPorNombre(TextBoxEspecialidad.Text);
                if(tabla.Rows.Count==0)
                {
                    Especialidad.GuardarEspecialidad(Convert.ToInt32(TextBoxCodigo.Text), TextBoxEspecialidad.Text);
                    limpiar();
                    Response.Write("<script>alert('Especialidad ingresada correctamente')</script>");
                }
                else
                    Response.Write("<script>alert('La especialidad ya ha sido ingresado')</script>");
            }
            else
                Response.Write("<script>alert('Ingrese una especialidad')</script>");
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
                    TextBoxEspecialidad.Text = GridViewListado.Rows[indice].Cells[1].Text;
                    break;
                case "Eliminar":
                    try
                    {
                        Especialidad.EliminarEspecialidad(Convert.ToInt32(GridViewListado.Rows[indice].Cells[0].Text));
                        limpiar();
                    }
                    catch
                    {
                        Response.Write("<script>alert('No se puede eliminar la especialidad')</script>");
                    }

                    break;
            }
        }

        protected void ButtonEditar_Click(object sender, EventArgs e)
        {
            if (TextBoxEspecialidad.Text != "")
            {
                DataTable auxEspe= Especialidad.GetDataBuscarPorCodigo(Convert.ToInt32(TextBoxCodigo.Text));
                string nombreEspe = auxEspe.Rows[0][1].ToString();
                if(TextBoxEspecialidad.Text==nombreEspe)
                {
                    Especialidad.ActualizarEspecialidad(TextBoxEspecialidad.Text,Convert.ToInt32( TextBoxCodigo.Text));
                    limpiar();
                    Response.Write("<script>alert('Curso Actualizado correctamente')</script>");
                    ButtonGuardar.Enabled = true;
                    ButtonEditar.Enabled = false;
                }
                else
                {
                    DataTable tabla = Especialidad.GetDataBuscarPorNombre(TextBoxEspecialidad.Text);
                    if (tabla.Rows.Count == 0)
                    {
                        Especialidad.ActualizarEspecialidad(TextBoxEspecialidad.Text, Convert.ToInt32(TextBoxCodigo.Text));
                        limpiar();
                        Response.Write("<script>alert('Especialidad ingresada correctamente')</script>");
                        ButtonGuardar.Enabled = true;
                        ButtonEditar.Enabled = false;
                    }
                    else
                    {
                        Response.Write("<script>alert('El nombre de la especialidad ya fue ingresado')</script>");
                        ButtonGuardar.Enabled = false;
                        ButtonEditar.Enabled = true;
                    }
                }
                
            }
            else
                Response.Write("<script>alert('Ingrese una especialidad')</script>");
        }
    }
}