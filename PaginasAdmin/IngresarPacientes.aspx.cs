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
    public partial class IngresarPacientes : System.Web.UI.Page
    {
        IngresoPacienteTableAdapter IngresosPac= new IngresoPacienteTableAdapter();
        CamasTableAdapter Camas= new CamasTableAdapter();
        PacientesTableAdapter Pacientes= new PacientesTableAdapter();
        MedicosTableAdapter Medicos = new MedicosTableAdapter();
        public void Lista()
        {
            GridViewListado.DataSource = IngresosPac.GetDataListadoPacientes();
            GridViewListado.DataBind();

        }
        public void limpiar()
        {
            TextBoxCodigo.Text=IngresosPac.Codigo().ToString();
            TextBoxFecha.Text= DateTime.Now.ToString("yyyy-MM-dd");
            DropDownListCama.DataSource = Camas.GetDataListadoCamasActivas();
            DropDownListCama.DataValueField = "Codigo";
            DropDownListCama.DataTextField = "NombreCompleto";
            DropDownListCama.DataBind();
            DropDownListCama.SelectedIndex = 0;
            DropDownListMedicos.SelectedIndex = 0;
            DropDownListPacientes.SelectedIndex = 0;
            Lista();
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Lista();
                DropDownListCama.Items.Clear();
                DropDownListCama.DataSource=Camas.GetDataListadoCamasActivas();
                DropDownListCama.DataValueField = "Codigo";
                DropDownListCama.DataTextField = "NombreCompleto";
                DropDownListCama.DataBind();
                DropDownListCama.Items.Insert(0, new ListItem("[Seleccione una cama]"));
                DropDownListMedicos.Items.Clear();
                DataTable TablaMedicos = Medicos.GetDataListadoMedico();
                foreach (DataRow row in TablaMedicos.Rows)
                {
                    string nombreCompleto = row["Medico"].ToString() + " " + row["Especialidad"].ToString();
                    DropDownListMedicos.Items.Add(new ListItem(nombreCompleto, row["Codigo"].ToString()));
                }
                DropDownListMedicos.Items.Insert(0, new ListItem("[Seleccione un medico]"));
                DropDownListPacientes.Items.Clear();
                DropDownListPacientes.DataSource = Pacientes.GetDataListadoPacientes();
                DropDownListPacientes.DataValueField = "Codigo";
                DropDownListPacientes.DataTextField = "Paciente";
                DropDownListPacientes.DataBind();
                DropDownListPacientes.Items.Insert(0, new ListItem("[Seleccione un Paciente]"));
                TextBoxCodigo.Text = IngresosPac.Codigo().ToString();
                TextBoxFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            TextBoxCodigo.Enabled = false;
            TextBoxFecha.Enabled = false;
            ButtonEditar.Enabled = false;
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if (DropDownListCama.SelectedIndex > 0)
            {
                if (DropDownListPacientes.SelectedIndex > 0)
                {
                    if (DropDownListMedicos.SelectedIndex > 0)
                    {
                        DataTable auxpac = IngresosPac.GetDataBuscarPorPacMedico(Convert.ToInt32(DropDownListPacientes.SelectedValue),Convert.ToInt32(DropDownListMedicos.SelectedValue));
                        bool estado = true;
                        if(auxpac.Rows.Count== 0)
                        {
                            estado= false;
                            IngresosPac.GuardarPaciente(Convert.ToInt32(TextBoxCodigo.Text),Convert.ToDateTime(TextBoxFecha.Text),Convert.ToInt32(DropDownListCama.SelectedValue),Convert.ToInt32(DropDownListPacientes.SelectedValue),Convert.ToInt32(DropDownListMedicos.SelectedValue));
                            Camas.ActualizarEstado(estado, Convert.ToInt32(DropDownListCama.SelectedValue));
                            limpiar();
                            Response.Write("<script>alert('Paciente ingresado correctamente')</script>");
                        }
                        else
                            Response.Write("<script>alert('No se puede asignar el mismo doctor al paciente')</script>");
                    }
                    else
                        Response.Write("<script>alert('Seleccione un Medico')</script>");
                }
                else
                    Response.Write("<script>alert('Seleccione un Paciente')</script>");
            }
            else
                Response.Write("<script>alert('Seleccione una Cama')</script>");
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
                    DateTime fechaConvertida = Convert.ToDateTime(GridViewListado.Rows[indice].Cells[1].Text);
                    TextBoxFecha.Text = fechaConvertida.ToString("yyyy-MM-dd");
                    DropDownListCama.Items.Clear();
                    DropDownListCama.DataSource = Camas.GetDataListadoCamasActivas();
                    DropDownListCama.DataBind();
                    DropDownListCama.Items.Add(new ListItem(GridViewListado.Rows[indice].Cells[8].Text, GridViewListado.Rows[indice].Cells[3].Text));
                    DropDownListCama.SelectedValue = GridViewListado.Rows[indice].Cells[3].Text;
                    DropDownListPacientes.SelectedValue = GridViewListado.Rows[indice].Cells[4].Text;
                    DropDownListMedicos.SelectedValue = GridViewListado.Rows[indice].Cells[5].Text;
                    break;
                case "Alta":
                    try
                    {
                        bool estado = true;
                        DateTime fechaHoy=Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        Camas.ActualizarEstado(estado, Convert.ToInt32(DropDownListCama.SelectedValue));
                        IngresosPac.DarDeAlta(fechaHoy,Convert.ToInt32(GridViewListado.Rows[indice].Cells[0].Text));
                        limpiar();
                    }
                    catch
                    {
                        Response.Write("<script>alert('No se puede dar de alta un paciente')</script>");
                    }
                    break;
            }
        }
    }
}