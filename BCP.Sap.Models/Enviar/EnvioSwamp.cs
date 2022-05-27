using BCP.Sap.Models.Enviar.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioRegistroSwampCuenta : EnvioOperacion
    {
        public EnvioDocumentoPN cliente { get; set; }
        public ParametrosHost usuario { get; set; }
        public EnvioRegistroSwampCuentaData data { get; set; }
    }
    public class EnvioRegistroSwampCuentaProducto : EnvioOperacion
    {
        public string usuario { get; set; }
        public EnvioRegistroSwampCuentaProductoData data { get; set; }
    }
    public class EnvioRegistroSwampCuentaFirmante : EnvioOperacion
    {
        public string usuario { get; set; }
        public EnvioRegistroSwampFirmanteData data { get; set; }
        public EnvioDocumentoPN cliente { get; set; }
    }
    public class EnvioRegistroSwampCuentaData
    {
        public string ses_guid { get; set; }
        public int cta_id { get; set; }
        public bool cta_apertura { get; set; }
        public bool cta_original { get; set; }
        public int cta_countfirmantes { get; set; }
        public int cta_countproductos { get; set; }
        public int cta_numfirmantes { get; set; }
        public string cta_apmaterno { get; set; }
        public string cta_appaterno { get; set; }
        public string cta_clientedelbanco { get; set; }
        public string cta_codciiu { get; set; }
        public string cta_codigosectorista { get; set; }
        public string cta_codtipobanca { get; set; }
        public string cta_codtipotarjetacredimas { get; set; }
        public string cta_ctaaplazoinfo { get; set; }
        public string cta_ctaexcinfo { get; set; }
        public string cta_direccion { get; set; }
        public string cta_gremio { get; set; }
        public string cta_localidaddescripcion { get; set; }
        public string cta_monto { get; set; }
        public string cta_nombres_razsocial { get; set; }
        public string cta_nomcomerc_nomcuenta { get; set; }
        public string cta_numerocredimas { get; set; }
        public string cta_situaciontarjetadescripcion { get; set; }
        public string cta_tarjetabancaexclusiva { get; set; }
        public string cta_telefono { get; set; }
        public string cta_tipocuenta { get; set; }
        public string cta_tipooperacioncredimas { get; set; }
        public string cta_codSucursalAgencia { get; set; }
    }

    public class EnvioRegistroSwampCuentaProductoData
    {
        public string ses_guid { get; set; }
        public int cta_id { get; set; }
        public int pro_id { get; set; }
        public bool pro_nueva { get; set; }
        public string pro_clave { get; set; }
        public string pro_codmoneda { get; set; }
        public string pro_codtipoproducto { get; set; }
        public string pro_monto { get; set; }
        public string pro_numerocuenta { get; set; }
        public string pro_subcodtipoproducto { get; set; }
        public string pro_tipodpf { get; set; }
        public string pro_tipoplazo { get; set; }
    }
    public class EnvioRegistroSwampFirmanteData
    {
        public string ses_guid { get; set; }
        public int cta_id { get; set; }
        public int fir_id { get; set; }
        public bool fir_actualizadatos { get; set; }
        public bool fir_clientenuevo { get; set; }
        public string fir_apmaterno { get; set; }
        public string fir_appaterno { get; set; }
        public string fir_estadocivil { get; set; }
        public string fir_fechanac { get; set; }
        public string fir_nombres_razsocial { get; set; }
        public string fir_numerocredimas { get; set; }
    }
}
