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
    public partial class Medicos : System.Web.UI.Page
    {
        MedicosTableAdapter Medico = new MedicosTableAdapter();
        EspecialidadTableAdapter Especilidad = new EspecialidadTableAdapter();
        public void Lista()
        {
            GridViewListado.DataSource = Medico.GetDataListadoMedico();
            GridViewListado.DataBind();

        }
        public void limpiar()
        {
            TextBoxCodigo.Text = "";
            TextBoxCodigo.Text = Medico.Codigo().ToString();
            TextBoxNombre.Text = "";
            TextBoxApellido.Text = "";
            TextBoxDireccion.Text = "";
            TextBoxTelefono.Text = "";
            DropDownListEspecialidades.SelectedIndex = 0;
            Lista();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Lista();
                DropDownListEspecialidades.DataSource = Especilidad.GetDataListadoEspecialidades();
                DropDownListEspecialidades.DataValueField = "Codigo";
                DropDownListEspecialidades.DataTextField = "Especialidad";
                DropDownListEspecialidades.DataBind();
                DropDownListEspecialidades.Items.Insert(0, new ListItem("[Seleccione una Especialidad]"));
                TextBoxCodigo.Text = Medico.Codigo().ToString();
            }
            ButtonEditar.Enabled = false;
            TextBoxCodigo.Enabled = false;
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if (TextBoxNombre.Text != "")
            {
                if (TextBoxApellido.Text != "")
                {
                        if (TextBoxDireccion.Text != "")
                        {
                            if (TextBoxTelefono.Text != "")
                            {
                                if (DropDownListEspecialidades.SelectedIndex > 0)
                                {
                                    Medico.GuardarMedico(Convert.ToInt32(TextBoxCodigo.Text), TextBoxNombre.Text, TextBoxApellido.Text, TextBoxDireccion.Text,Convert.ToInt32(DropDownListEspecialidades.SelectedValue), TextBoxTelefono.Text);
                                    limpiar();
                                    Response.Write("<script>alert('Medico Ingresado correctamente')</script>");
                                }
                                else
                                    Response.Write("<script>alert('Seleccione un departamento')</script>");

                            }
                            else
                                Response.Write("<script>alert('Ingrese un numero de telefono')</script>");

                        }
                        else
                            Response.Write("<script>alert('Ingrese una direccion')</script>");
                }
                else
                    Response.Write("<script>alert('Ingrese un apellido')</script>");
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
                    ButtonGuardar.Enabled = false;
                    ButtonEditar.Enabled = true;
                    TextBoxCodigo.Text = GridViewListado.Rows[indice].Cells[0].Text;
                    string nombreCompleto = GridViewListado.Rows[indice].Cells[1].Text;
                    string[] auxNombre = nombreCompleto.Split(' ');
                    TextBoxNombre.Text = auxNombre[0];
                    TextBoxApellido.Text = auxNombre[1];
                    TextBoxDireccion.Text = GridViewListado.Rows[indice].Cells[2].Text;
                    TextBoxTelefono.Text = GridViewListado.Rows[indice].Cells[3].Text;
                    DropDownListEspecialidades.SelectedIndex=Convert.ToInt32( GridViewListado.Rows[indice].Cells[4].Text);
                    break;
                case "Eliminar":
                    try
                    {
                        Medico.EliminarMedico(Convert.ToInt32(GridViewListado.Rows[indice].Cells[0].Text));
                        limpiar();
                    }
                    catch
                    {
                        Response.Write("<script>alert('No se puede eliminar al paciente')</script>");
                    }

                    break;
            }
        }

        protected void ButtonEditar_Click(object sender, EventArgs e)
        {
            if (TextBoxNombre.Text != "")
            {
                if (TextBoxApellido.Text != "")
                {
                    if (TextBoxDireccion.Text != "")
                    {
                        if (TextBoxTelefono.Text != "")
                        {
                            if (DropDownListEspecialidades.SelectedIndex > 0)
                            {
                                Medico.ActualizarMedico( TextBoxNombre.Text, TextBoxApellido.Text, TextBoxDireccion.Text, TextBoxTelefono.Text, Convert.ToInt32(DropDownListEspecialidades.SelectedValue), Convert.ToInt32(TextBoxCodigo.Text));
                                limpiar();
                                Response.Write("<script>alert('Medico editado correctamente')</script>");
                            }
                            else
                                Response.Write("<script>alert('Seleccione un departamento')</script>");

                        }
                        else
                            Response.Write("<script>alert('Ingrese un numero de telefono')</script>");

                    }
                    else
                        Response.Write("<script>alert('Ingrese una direccion')</script>");
                }
                else
                    Response.Write("<script>alert('Ingrese un apellido')</script>");
            }
            else
                Response.Write("<script>alert('Ingrese un nombre')</script>");
        }
    }
}