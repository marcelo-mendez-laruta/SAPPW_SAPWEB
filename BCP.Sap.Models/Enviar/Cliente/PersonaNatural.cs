using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Cliente
{
    public class EnvioCreacionCliente:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object cliente { get; set; }
        public object data { get; set; }
    }
    public class EnvioCreacionINFOCLIENTE : EnvioOperacion
    {
        public string usuario { get; set; }
        public EnvioDocumentoPN cliente { get; set; }
        public RegistroINFOCLIENTE data { get; set; }
    }
    public class EnvioModificacionDatos : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object cliente { get; set; }
        public object data { get; set; }
    }
    public class EnvioModificarIDC : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object data { get; set; }
    }
    public class EnvioModificarNombre : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object cliente { get; set; }
        public object data { get; set; }
    }
    public class EnvioModificarTipoCliente : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public object data { get; set; }
    }
    public class ModificacionData
    {
        public string residente { get; set; }//S50334 Residente(S,N)
        public string nombreComercial { get; set; }
        public string domicilio { get; set; }
        public string localidad { get; set; }
        public string ciiu { get; set; }
        public string telefono { get; set; }
        public string tipoCliente { get; set; }
        public string funcionarioNegocios { get; set; }
        public string tipoBanca { get; set; }
        public string registroMercantil { get; set; }
        public string magnitudEmpresa { get; set; }
        public string nit { get; set; }
        public string estadoNIT { get; set; }
        public string codigoSBS { get; set; }
        public string oficina { get; set; }
        public string correspondencia { get; set; }
    }
    public class ModificacionDataPJ: ModificacionData
    {      
        public string oficina { get; set; }
        public string finSocial { get; set; }
        public string fechaConstitucion { get; set; }
        public string categoria { get; set; }
        public string naturalezaJuridica { get; set; }
        public string tipoEmpresa { get; set; }
        public string sucursal { get; set; }
    }
    public class ModificacionDataPN: ModificacionData
    {
        public string fechaNacimiento { get; set; }
        public string indicadorVivienda { get; set; }
        public string gradoInstruccion { get; set; }
        public string sexo { get; set; }
        public string situacionLaboral { get; set; }
        public string estadoCivil { get; set; }
        public string profesion { get; set; }
        public string nacionalidad { get; set; }
        public string segmento { get; set; }
        public string tipoCuenta { get; set; }
    }
    public class ModificarNombrePersonaNatural
    {
        public string nombreNuevo { get; set; }
        public string paternoNuevo { get; set; }
        public string maternoNuevo { get; set; }
    }
    public class ModificarNombrePersonaJuridica
    {
        public string razonSocialNuevo { get; set; }
    }
    public class ModificarTCPersonaNatural : EnvioDocumentoPN
    {
        public string tipoCliente { get; set; }
    }
    public class ModificarTCPersonaJuridica : EnvioDocumentoPJ
    {
        public string tipoCliente { get; set; }
    }
    public class ModificarIDCData
    {
        public string idcNumeroActual { get; set; }
        public string idcTipoActual { get; set; }
        public string idcExtensionActual { get; set; }        
        public string idcNumeroNuevo { get; set; }
        public string idcTipoNuevo { get; set; }
        public string idcExtensionNuevo { get; set; }
        
    }
    public class ModificarIDCDataPN:ModificarIDCData
    {
        public readonly string idcComplementoActual = "00";
        public readonly string idcComplementoNuevo = "00";
    }
    public class CreacionCliente
    {
        public string residente { get; set; }
        public string direccion { get; set; }
        public string localidad { get; set; }
        public string ciiu { get; set; }
        public string telefono { get; set; }
        public string tipoCliente { get; set; }
        public string funcionarioNegocios { get; set; }
        public string tipoBanca { get; set; }
        public string registroMercantil { get; set; }
        public string magnitudEmpresa { get; set; }
        public string codigoSBS { get; set; }
        public string negocioPropio { get; set; }
        public string nombreComercial { get; set; }
        public string oficina { get; set; }
    }
    public class RegistroINFOCLIENTE:CreacionClientePN
    {
        public string ciiu2 { get; set; }
        public string strCalle { get; set; }
        public string domicilio { get; set; }
        public string strNumeroDomicilio { get; set; }
        public string strManzana { get; set; }
        public string strLote { get; set; }
        public string strDepartamento { get; set; }
        public string strDepPiso { get; set; }
        public string strUrbanizacionTipo { get; set; }
        public string strUrbanizacion { get; set; }
        public string strSectorTipo { get; set; }
        public string strSector { get; set; }
    }
    public class CreacionClientePN:CreacionCliente
    {
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombres { get; set; }
        public string gradoInstruccion { get; set; }
        public string sexo { get; set; }
        public string situacionLaboral { get; set; }
        public string estadoCivil { get; set; }
        public string profesion { get; set; }
        public string fechaNacimiento { get; set; }
        public string tipoVivienda { get; set; }
        public string nacionalidad { get; set; }
        public string segmento { get; set; }
        public string tipoCuenta { get; set; }
        public string nit { get; set; }
        public string estadoNIT { get; set; }
        public string nombreEmpresa { get; set; }
        public string ruc { get; set; }
        public string cic { get; set; }
        public string mail { get; set; }
        public string celular { get; set; }
    }
    public class CreacionClientePJ : CreacionCliente
    {
        public string finSocial { get; set; }
        public string fechaConstitucion { get; set; }
        public string categoria { get; set; }
        public string naturalezaJuridica { get; set; }
        public string razonSocial { get; set; }
        public string tipoEmpresa { get; set; }
        public string sucursal { get; set; }
        public bool registroCompleto { get; set; }
    }
}
