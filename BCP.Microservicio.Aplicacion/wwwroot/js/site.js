$(function () {
    $(".dropdown-menu > li > button.trigger").on("click", function (e) {
        var current = $(this).next();
        var grandparent = $(this).parent().parent();
        if ($(this).hasClass('left-caret') || $(this).hasClass('right-caret'))
            $(this).toggleClass('right-caret left-caret');
        grandparent.find('.left-caret').not(this).toggleClass('right-caret left-caret');
        grandparent.find(".sub-menu:visible").not(current).hide();
        current.toggle();
        e.stopPropagation();
    });
    $(".dropdown-menu > li > button:not(.trigger)").on("click", function () {
        var root = $(this).closest('.dropdown');
        root.find('.left-caret').toggleClass('right-caret left-caret');
        root.find('.sub-menu:visible').hide();
    });
    $("input[type=text]").keypress(function (e) {
        $input = $(this);
        setTimeout(function () {
            $input.val($input.val().toUpperCase());
        }, 50);
    });

});
function datosFiscales() {
    var mancomuna = document.getElementById('DF');
    if (!document.getElementById('ckDF').checked) {
        mancomuna.style.display = "none";
    }
    else {
        mancomuna.style.display = "block";
    }
}

function cancelarProceso(url) {
    var confirmar = confirm("¿Esta seguro de cancelar el proceso?");
    if (confirmar == true) {
        location.href = url;
    }
}

function validarIDC(e) {
    $input = $(e);
    var patron = /^[-A-Z0-9]+$/;
    if (patron.test($input.val())) {
        $("#Ftxtidc").val($input.val());
    }
    else {
        if ($input.val() != "") {
            $input.val($("#Ftxtidc").val());
        }
        else {
            $("#Ftxtidc").val("");
        }
    }
}

function salir() {
    //var myWindow;
    //myWindow = window.open('', '_parent', '');
    //myWindow.close(); 
    this.window.close();
}

function formatoInputTarjeta(value, key) {
    var cants = value;
    console.log("key:" + key);
    if (key != 8 && key != 46 && key != 37 && key != 39) {
        cants = cants.replace(/[.*+?@!%<>?":;'`~^${}_=+()|[#^&\]\\]/g, '');
        cants = cants.split("-").join("");
        cants = formatoTarjeta(cants);
    }
    return cants;
}
function formatoTarjeta(value) {
    var length = value.length;
    var newValue = "";
    for (var i = 0; i <= length - 1; i++) {
        newValue = newValue + value.charAt(i);
        if (i == 3 || i == 7 || i == 11) {
            newValue = newValue + "-";
        }
    }
    return newValue;
}
function formatoInputFecha(value) {
    console.log(value);
    value = value.replace(/[.*+?@!%<>?":;'`~^${}_=+()|[#^&\]\\]/g, '');
    value = value.replace("-", '');
    value = value.replace("//", '/');
    value = value.slice(-1) == "/" ? value.substring(0, value.length - 1) : value;
    var rawnumbers = value.replace("/", '');
    var datelength = rawnumbers.length;
    if (datelength !== 0 && datelength == 2 || datelength == 4) {
        value = value + '/';
    }
    return value;

}
function formatoCamposTexto(nombre, caracter) {
    $input = $("#" + nombre + "");
    var date = $input.val();
    var patron = $input.prop('placeholder');
    var nuevo = "";
    var indice = $input.prop("selectionStart");
    if (date.length < patron.length) {
        nuevo = date + patron.substr(date.length);
        $input.val(nuevo);
        document.getElementById(nombre).selectionStart = indice;//date.length;
        document.getElementById(nombre).selectionEnd = indice;//date.length;
    }
    else {
        var a = "";
        var regex = new RegExp(caracter, "g");
        var b = date.replace(regex, "").replace(/ /g, "");
        var contador = 0;
        for (var i = 0; i < b.length; i++) {
            a = b.substr(i, 1);
            if (patron.substr(i + contador, 1) == caracter) {
                contador = contador + 1;
                nuevo = nuevo + caracter;
            }
            nuevo = nuevo + a;
        }
        var c = nuevo.substr(indice - 1, 1);
        if (c == caracter) {
            indice = indice + 1;
            /* c = nuevo.substr(indice - 1, 1);
             if (c !=null && c!="" && c!=" ") {
                 indice = indice + 1;
             */
        }
        if (nuevo.length > patron.length) {
            nuevo = nuevo.substr(0, patron.length);
        }
        else {
            nuevo = nuevo + patron.substr(nuevo.length);
        }
        if (date != nuevo) {
            /*if (indice > patron.length) {
                indice = patron.length;
            }*/
            $input.val(nuevo);
            document.getElementById(nombre).selectionStart = indice;
            document.getElementById(nombre).selectionEnd = indice;
        }
    }
    if (nuevo == patron) {
        document.getElementById(nombre).selectionStart = 0;
        document.getElementById(nombre).selectionEnd = 0;
    }
}
function LimpiarTextoRegion(idDiv) {
    $('#' + idDiv + ' :input').each(function () {
        switch ($(this).get(0).tagName) {
            case 'INPUT':
                $(this).val("");
                break;
        }
    });
}
function AntesAjax() {
    $("#CargandoElementoModal").show();
}
function DespuesAjax() {
    $("#CargandoElementoModal").hide();
}

shortcut.add("Ctrl+S", function () {
    MMMMU();
});

/*$(window).bind('beforeunload', function () {
   
    $.ajax({
        url: "@logoutUrl",
        success: function () {
            console.log("Cache liberada");
        }
    });
});*/
/*DESUUSO*/
/*function obtenerDatosLaborales(idc, tidc) {
    $.ajax({
        type: 'POST',
        url: '@@Url.Action("ConsultarRecPN")',
        dataType: 'json',
        data: { id: idc, tid: tidc },
        async: false,
        beforeSend: AntesAjax(),
        success: function (l) {
            if (l == null) {
                alert("Error de conexion al microservicio");
            }
            else if (l.success) {
                if (l.data.total > 0) {
                    var nit = l.data.rows[0].idcPersona;
                    if (l.data.rows[0].extensionPersona != null && l.data.rows[0].extensionPersona != "") {
                        nit = nit + l.data.rows[0].extensionPersona;
                    }
                    $("#txtEmpTra").val(l.data.rows[0].nombre);
                    $("#txtNitEmpresa").val(nit);
                }
            }
        },
        error: function (ex) {
            alert(JSON.stringify(ex));
        },
        complete: function () { DespuesAjax(); }
    });
}
*/
/*function obtenerCorreoCelular(idc,tidc) {
         $.ajax({
            type: 'POST',
            url: '@Url.Action("ConsultarDireccionPN")',
            dataType: 'json',
            data: { id: idc, tid: tidc},
            async: false,
            beforeSend: AntesAjax(),
            success: function (l) {
                if (!(l.direccionComprimida == null || l.direccionComprimida == "")) {

                    $("#txtEmail").val(l.direccionComprimida);
                    if (!(l.telefono == null || l.telefono == "")) {
                        $("#txtCelular").val(l.telefono);
                    }
                }
            },
            error: function (ex) {
                alert(JSON.stringify(ex));
            },
            complete: function () { DespuesAjax(); }
        });
    }*/
/*function crearDireccionPN(idc,tidc,correo,telefono) {
        $.ajax({
            type: 'POST',
            url: '@Url.Action("CrearDireccionCorreoPN")',
            dataType: 'json',
            data: { id: idc, tid: tidc, correo: correo,telefono: telefono},
            async: false,
            beforeSend: AntesAjax(),
            success: function (l) {
                if (l == null) {
                    alert("Error de conexion al microservicio");
                }
                else {
                    //alert(l.message);
                }
            },
                error: function (ex) {
                    alert(JSON.stringify(ex));
            },
            complete: function () { DespuesAjax(); }
        });
    }
     */
/*function crearRecPN(idc,tidc,nit,empresa) {
    $.ajax({
        type: 'POST',
        url: '@Url.Action("CrearRecPN")',
        dataType: 'json',
        data: { id: idc, tid: tidc, nit: nit, nombreEmpresa: empresa},
        async: false,
        beforeSend: AntesAjax(),
        success: function (l) {
            if (l == null) {
                alert("Error de conexion al microservicio");
            }
            else {
                //alert(l.message);
            }
        },
            error: function (ex) {
                alert(JSON.stringify(ex));
        },
        complete: function () { DespuesAjax(); }
    });
}*/
function AjaxErrorHandler(ex, textStatus, errorThrown) {
    if (ex.status === 0) {
        alert('No es posible conectase al servicio');
    } else if (ex.status == 404) {
        alert('La página solicitada no pudo ser encontrada. [404]');
    } else if (ex.status == 500) {
        alert('Error Interno del Servidor [500].');
    } else if (textStatus === 'parsererror') {
        alert('La solicitud de parseo del JSON fallo.');
    } else if (textStatus === 'timeout') {
        alert('Tiempo de espera excedido.');
    } else if (textStatus === 'abort') {
        alert('Solicitud Ajax cancelada.');
    } else {
        alert('Error Inesperado: ' + ex.responseText);
    }
}

function requestSegurosSat( url, listaRawSeguros, idc, tipo, extension, complemento, cuenta) {
        var listaSeguros = listaRawSeguros.split("-");
        for (var i = 1; i < listaSeguros.length; i++) {
            var cheboxname = listaSeguros[i] + "Checkbox";
            if (document.getElementById(cheboxname).checked) {
                var params = {
                    codigoSeguro: listaSeguros[i],
                    cuenta: cuenta,
                    idc: idc,
                    tipo: tipo,
                    extension: extension,
                    complemento: complemento
                };
                getSeguroPDF(url, params);
            }
        }
}
function requestSeguros(masCuentas, url, listaRawSeguros, idc, tipo, extension, complemento, cuentas) {
    if (masCuentas=='N') {
        var listaSeguros = listaRawSeguros.split("-");
        var listaCuentas = cuentas.split(',');
        var cuenta = "";
        //for (var i = 1; i < listaCuentas.length; i++) {
            cuenta = listaCuentas[0].split("-").join("");//adicionar i
            for (var i = 1; i < listaSeguros.length; i++) {
                var cheboxname = listaSeguros[i] + "Checkbox";
                if (document.getElementById(cheboxname).checked) {
                    var params = {
                        codigoSeguro: listaSeguros[i],
                        cuenta: cuenta,
                        idc: idc,
                        tipo: tipo,
                        extension: extension,
                        complemento: complemento
                    };
                    getSeguroPDF(url, params);
                }
            }
        //}
    }
    document.getElementById("Ingreso5Submit").click();
}
function getSeguroPDF(url, params) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: 'json',
        data: params,
        async: false,
        success: function (dta) {
            console.log(dta);
            if (dta.success != false) {
                if (dta.data.pdf != null) {
                    downloadSeguroPDF(dta.data.pdf);
                } else {
                    alert("Registrado Existosamente");
                }
            }
            else {
                alert("No se pudo conectar con Seguros");
            }
        },
        error: function (ex, textStatus, errorThrown) {
            AjaxErrorHandler(ex, textStatus, errorThrown);
        }
    }); 
}
function downloadSeguroPDF(pdfData) {
    var fileName = 'Certificado.pdf';
    var byteCharacters = atob(pdfData);
    var byteNumbers = new Array(byteCharacters.length);
    for (var i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    var byteArray = new Uint8Array(byteNumbers);
    var blob = new Blob([byteArray], { type: 'application/pdf' });
    window.navigator.msSaveOrOpenBlob(blob, fileName);
}
function showSegurosSAT() {
    if (document.getElementById("ckCC").checked) {
        document.getElementById("ContainerSeguros").style.display = "block";
    }
    else {
        document.getElementById("ContainerSeguros").style.display = "none";
    }
}