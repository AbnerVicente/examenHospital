using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExamenFinalHospital.BD.DataSetHospitalTableAdapters;
namespace ExamenFinalProgra4.PaginasAdmin
{
    public partial class Pacientes : System.Web.UI.Page
    {
        PacientesTableAdapter Paciente = new PacientesTableAdapter();
        DepartamentoTableAdapter Departamentos = new DepartamentoTableAdapter();
        MunicipioTableAdapter Municipios = new MunicipioTableAdapter();
        public void Lista()
        {
            GridViewListado.DataSource = Paciente.GetDataListadoPacientes();
            GridViewListado.DataBind();

        }
        public void limpiar()
        {
            TextBoxCodigo.Text = "";
            TextBoxCodigo.Text = Paciente.Codigo().ToString();
            TextBoxNombre.Text = "";
            TextBoxApellido.Text = "";
            TextBoxFechaNac.Text = "";
            TextBoxDireccion.Text = "";
            TextBoxTelefono.Text = "";
            DropDownListDepartamento.SelectedIndex = 0;
            DropDownListMunicipio.Items.Clear();
            DropDownListMunicipio.Items.Insert(0, new ListItem("[Seleccione primero un Departamento]"));
            Lista();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Lista();
                DropDownListDepartamento.DataSource = Departamentos.GetDataListadoDepartamentos();
                DropDownListDepartamento.DataValueField = "Codigo";
                DropDownListDepartamento.DataTextField = "Departamento";
                DropDownListDepartamento.DataBind();
                DropDownListDepartamento.Items.Insert(0, new ListItem("[Seleccione un Departamento]"));
                if (DropDownListDepartamento.SelectedIndex == 0)
                {
                    DropDownListMunicipio.Items.Clear();
                    DropDownListMunicipio.Items.Insert(0, new ListItem("[Seleccione primero un Departamento]"));
                }
                TextBoxCodigo.Text = Paciente.Codigo().ToString();
            }
            ButtonEditar.Enabled = false;
            TextBoxCodigo.Enabled = false;
        }

        protected void DropDownListDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListDepartamento.SelectedIndex > 0)
                {
                    DropDownListMunicipio.DataSource = Municipios.GetDataBuscarPorDepartamento(Convert.ToInt32(DropDownListDepartamento.SelectedValue));
                    DropDownListMunicipio.DataValueField = "Codigo";
                    DropDownListMunicipio.DataTextField = "Municipio";
                    DropDownListMunicipio.DataBind();
                }
                else
                {
                    DropDownListMunicipio.Items.Clear();
                    DropDownListMunicipio.Items.Insert(0, new ListItem("[Seleccione primero un Departamento]"));
                }

            }
            catch
            {
            }
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if (TextBoxNombre.Text != "")
            {
                if (TextBoxApellido.Text != "")
                {
                    if (TextBoxFechaNac.Text != null)
                    {
                        if (TextBoxDireccion.Text != "")
                        {
                            if (TextBoxTelefono.Text != "")
                            {
                                if (DropDownListDepartamento.SelectedIndex > 0)
                                {
                                    Paciente.GuardarPaciente(Convert.ToInt32(TextBoxCodigo.Text), TextBoxNombre.Text, TextBoxApellido.Text, TextBoxDireccion.Text, TextBoxTelefono.Text, Convert.ToDateTime(TextBoxFechaNac.Text), Convert.ToInt32(DropDownListMunicipio.Text));
                                    limpiar();
                                    Response.Write("<script>alert('Paciente Ingresado correctamente')</script>");
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
                        Response.Write("<script>alert('Ingrese una fecha Valida')</script>");
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
                    string fecha = GridViewListado.Rows[indice].Cells[4].Text;
                    DateTime fechaConvertida;
                    if (DateTime.TryParse(fecha, out fechaConvertida))
                    {
                        // Asignar la fecha en el formato que espera el TextBox (yyyy-MM-dd)
                        TextBoxFechaNac.Text = fechaConvertida.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        // Manejar el caso en que la fecha no se pueda convertir
                        TextBoxFechaNac.Text = "";
                        Response.Write("<script>alert('Formato de fecha inválido.')</script>");
                    }
                    TextBoxDireccion.Text = GridViewListado.Rows[indice].Cells[2].Text;
                    TextBoxTelefono.Text = GridViewListado.Rows[indice].Cells[3].Text;
                    string codMuni = GridViewListado.Rows[indice].Cells[5].Text;
                    DataTable Muni = Municipios.GetDataBuscarPorCodigo(Convert.ToInt32(codMuni));
                    DropDownListDepartamento.SelectedIndex = Convert.ToInt32(Muni.Rows[0][2]);
                    DropDownListDepartamento_SelectedIndexChanged(sender, e);
                    DropDownListMunicipio.SelectedValue = Muni.Rows[0][0].ToString();
                    break;
                case "Eliminar":
                    try
                    {
                        Paciente.EliminarPaciente(Convert.ToInt32(GridViewListado.Rows[indice].Cells[0].Text));
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
                    if (TextBoxFechaNac.Text != null)
                    {
                        if (TextBoxDireccion.Text != "")
                        {
                            if (TextBoxTelefono.Text != "")
                            {
                                if (DropDownListDepartamento.SelectedIndex > 0)
                                {
                                    Paciente.ActualizarPaciente(TextBoxNombre.Text, TextBoxApellido.Text, TextBoxDireccion.Text, TextBoxTelefono.Text, Convert.ToDateTime(TextBoxFechaNac.Text), Convert.ToInt32(DropDownListMunicipio.Text), Convert.ToInt32(TextBoxCodigo.Text));
                                    limpiar();
                                    Response.Write("<script>alert('Paciente Actualizado correctamente')</script>");
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
                        Response.Write("<script>alert('Ingrese una fecha Valida')</script>");
                }
                else
                    Response.Write("<script>alert('Ingrese un apellido')</script>");
            }
            else
                Response.Write("<script>alert('Ingrese un nombre')</script>");
        }
    }
}
