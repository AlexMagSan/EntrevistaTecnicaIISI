<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viaje.aspx.cs" Inherits="Entrevista_Técnica_IISI.Viaje" %>
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>  
    <script src="js/bootstrap.min.js"></script>    
    <link href="css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Timer ID="tRefresh" runat="server" OnTick="tRefresh_Tick">
            </asp:Timer>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <div class ="Container">
            <div class="row">   
                 <div class="col-lg-2">  
        <asp:Panel ID="PanelPrincipal" runat="server">
            <table style="width:initial">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; font-weight: bold; background-color: #6699FF;">
                        <asp:Label ID="Label6" runat="server" Text="Viaje"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrigen" runat="server" Text="Origen"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrigen" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrigen_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDestino" runat="server" Text="Destino"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDireccion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; font-weight: bold; background-color: #6699FF;">
                        <asp:Label ID="Label2" runat="server" Text="Datos"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Próximo"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="lbProximo" runat="server" OnClick="lbProximo_Click"></asp:LinkButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCosto" runat="server" Text="Costo"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="lbCosto" runat="server" OnClick="lbCosto_Click"></asp:LinkButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Tiempo de viaje"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblTiempoViaje" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnDatos" runat="server" OnClick="btnDatos_Click" Text="Datos" Width="100%" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PanelProximosTrenes" runat="server" Visible="False">
            <asp:GridView ID="gvProximosTrenes" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="diferencia" HeaderText="Minutos" >
                    <HeaderStyle BackColor="#6699FF" Font-Bold="True" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="hora" HeaderText="Hora" >
                    <HeaderStyle BackColor="#6699FF" Font-Bold="True" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:LinkButton ID="lbRegresarProximosTrenes" runat="server" OnClick="lbRegresarProximosTrenes_Click">&lt;= Regresar</asp:LinkButton>
        </asp:Panel>
        <asp:Panel ID="PanelCostoHora" runat="server" Visible="False">
            <asp:GridView ID="gvTarifasViajes" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="tipo" HeaderText="Tipo" >
                    <HeaderStyle BackColor="#6699FF" Font-Bold="True" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valor" HeaderText="Costo" DataFormatString="{0:c}" >
                    <HeaderStyle BackColor="#6699FF" Font-Bold="True" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:LinkButton ID="lbRegresarCostos" runat="server" OnClick="lbRegresarCostos_Click">&lt;=  Regresar</asp:LinkButton>
        </asp:Panel>
        <asp:Panel ID="PanelDatos" runat="server" Visible="False">
            <table style="width:initial">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo Usuario"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoUsuario" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="lbRegresarTipoUsuario" runat="server" OnClick="lbRegresarTipoUsuario_Click">&lt;=  Regresar</asp:LinkButton>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
</div>
</div>
</div>
    </form>
</body>
</html>
