using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Entrevista_Técnica_IISI
{
    public partial class Viaje : System.Web.UI.Page
    {
        //Se crea la instancia del webservice 
        ServiceReferenceViajes.viajeSoapClient wsViajes = new ServiceReferenceViajes.viajeSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Se consume el webservice para alimentar los dropdownlists ddlOrigen y ddlDireccion
                foreach (DataRow r in wsViajes.Estaciones().Rows)
                {
                    if (ddlOrigen.Items.Count != wsViajes.Estaciones().Rows.Count)
                    {
                        ddlOrigen.Items.Add(new ListItem(r["nombre"].ToString(), r["codigo"].ToString()));
                        ddlDireccion.Items.Add(new ListItem(r["nombre"].ToString(), r["codigo"].ToString()));
                    }
                }

                //se consume el webservice para alimentar el dropdownlist TipoUsusario
                foreach (DataRow r in wsViajes.TipoUsario().Rows)
                {
                    if (ddlTipoUsuario.Items.Count != wsViajes.TipoUsario().Rows.Count)
                    {
                        ddlTipoUsuario.Items.Add(new ListItem(r["tipo"].ToString(), r["codigo"].ToString()));
                    }
                }

                //Se cargan las tarifas de los viajes de acuerdo al horario
                DataTable tTarifasViajes = wsViajes.TarifasViajes(Convert.ToInt32(ddlOrigen.SelectedValue), Convert.ToInt32(ddlDireccion.SelectedValue));
                foreach (DataRow r in tTarifasViajes.Rows)
                {
                    if (r["tipo"].ToString() == ddlTipoUsuario.SelectedItem.Text)
                        lbCosto.Text = String.Format("{0:c}", Convert.ToDecimal(r["valor"].ToString()));
                        lblCosto.Text = "Costo " + ddlTipoUsuario.SelectedItem.Text;
                }

                //se asignan los valores previamente seleccionados de los dropdownlists
                DataTable tUltimaConfig = wsViajes.ConsultarUltimaConfig();
                if (tUltimaConfig.Rows.Count > 0)
                {
                    ddlOrigen.SelectedValue = tUltimaConfig.Rows[0]["Origen"].ToString();
                    ddlDireccion.SelectedValue = tUltimaConfig.Rows[0]["Destino"].ToString();
                    ddlTipoUsuario.SelectedValue = tUltimaConfig.Rows[0]["TipoUsuario"].ToString();                    
                }
                ActualizarInformacion();
            }
        }

        protected void ActualizarInformacion()
        {
            // se consume el webservice para cargar la ultima configuracion de los dropdownlists
            wsViajes.GuardarUltimaConfig(Convert.ToInt32(ddlOrigen.SelectedValue), Convert.ToInt32(ddlDireccion.SelectedValue), Convert.ToInt32(ddlTipoUsuario.SelectedValue));                        
            DataTable tProximosTrenes = wsViajes.ProximosTrenes(Convert.ToInt32(ddlOrigen.SelectedValue.ToString()), Convert.ToInt32(ddlDireccion.SelectedValue.ToString()));
            DataTable tTarifasViajes = wsViajes.TarifasViajes(Convert.ToInt32(ddlOrigen.SelectedValue), Convert.ToInt32(ddlDireccion.SelectedValue.ToString()));
            gvTarifasViajes.DataSource = tTarifasViajes;
            gvTarifasViajes.DataBind();
            //Se cargan las tarifas de los viajes de acuerdo al horario
            if (tTarifasViajes.Rows.Count > 0)
            {
                foreach (DataRow r in tTarifasViajes.Rows)
                {
                    if (r["tipo"].ToString() == ddlTipoUsuario.SelectedItem.Text)
                        lbCosto.Text = String.Format("{0:c}", Convert.ToDecimal(r["valor"].ToString()));                    
                }
            }
            else
            {
                lbCosto.Text = "No hay costo definido.";
                gvTarifasViajes.DataBind();
            }

            if (tProximosTrenes.Rows.Count > 0)
            {
                lbProximo.Text = tProximosTrenes.Rows[0]["diferencia"].ToString() + " Minutos (" + tProximosTrenes.Rows[0]["hora"].ToString() + ")";
                gvProximosTrenes.DataSource = tProximosTrenes;
                gvProximosTrenes.DataBind();
            }
            else
            {
                gvProximosTrenes.DataBind();
                lbProximo.Text = "No hay más trenes";
            }
            lblCosto.Text = "Costo " + ddlTipoUsuario.SelectedItem.Text;
            lblTiempoViaje.Text = wsViajes.DuracionViaje(Convert.ToInt32(ddlOrigen.SelectedValue), Convert.ToInt32(ddlDireccion.SelectedValue)) + " Minutos";
        }

        protected void ddlOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarInformacion();
        }

        protected void ddlDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarInformacion();
        }

        protected void lbProximo_Click(object sender, EventArgs e)
        {
            PanelPrincipal.Visible = false;
            PanelCostoHora.Visible = false;
            PanelDatos.Visible = false;
            PanelProximosTrenes.Visible = true;
        }

        protected void lbRegresarProximosTrenes_Click(object sender, EventArgs e)
        {
            PanelPrincipal.Visible = true;
            PanelCostoHora.Visible = false;
            PanelDatos.Visible = false;
            PanelProximosTrenes.Visible = false;
        }

        protected void lbRegresarCostos_Click(object sender, EventArgs e)
        {
            PanelPrincipal.Visible = true;
            PanelCostoHora.Visible = false;
            PanelDatos.Visible = false;
            PanelProximosTrenes.Visible = false;
        }

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblCosto.Text = "Costo " + ddlTipoUsuario.SelectedItem.Text;
            ActualizarInformacion();
        }

        protected void lbCosto_Click(object sender, EventArgs e)
        {
            PanelPrincipal.Visible = false;
            PanelCostoHora.Visible = true;
            PanelDatos.Visible = false;
            PanelProximosTrenes.Visible = false;
        }

        protected void btnDatos_Click(object sender, EventArgs e)
        {
            PanelPrincipal.Visible = false;
            PanelCostoHora.Visible = false;
            PanelDatos.Visible = true;
            PanelProximosTrenes.Visible = false;
        }

        protected void lbRegresarTipoUsuario_Click(object sender, EventArgs e)
        {
            PanelPrincipal.Visible = true;
            PanelCostoHora.Visible = false;
            PanelDatos.Visible = false;
            PanelProximosTrenes.Visible = false;
        }

        protected void tRefresh_Tick(object sender, EventArgs e)
        {
            ActualizarInformacion();
        }
    }
}