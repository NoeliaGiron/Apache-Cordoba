// Si quiere una introducción sobre la plantilla En blanco, vea la siguiente documentación:
// http://go.microsoft.com/fwlink/?LinkID=397704
// Para depurar código al cargar la página en cordova-simulate o en dispositivos o emuladores Android: inicie la aplicación, establezca puntos de interrupción 
// y ejecute "window.location.reload()" en la Consola de JavaScript.(function () {
(function () {
    "use strict";

    document.addEventListener('deviceready', onDeviceReady.bind(this), false);

    function onDeviceReady() {
        // Controlar la pausa de Cordova y reanudar eventos
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);

        // Inicializar eventos
        document.getElementById("btnBuscar").addEventListener('click', BuscarUsuario, false);
        document.getElementById("btnGuardar").addEventListener('click', RegistrarUsuario, false);
    }

    // FUNCIONES PERSONALIZADAS
    function BuscarUsuario() {
        var pFullname = document.getElementById("txtNombre").value;
        if (pFullname === "") {
            $("#divResultado").html("Ingrese el nombre de usuario!");
        } else {
            var usuarios = JSON.parse(localStorage.getItem("usuarios")) || [];
            var cadena = "<table data-role='table' class='ui-responsive' width='100%'>";
            cadena += "<thead><tr><th>Usuario</th><th>Nombre</th><th>Fecha Nac.</th></tr></thead><tbody>";
            var encontrado = false;

            for (var i = 0; i < usuarios.length; i++) {
                if (usuarios[i].pFullname.toLowerCase() === pFullname.toLowerCase()) {
                    cadena += "<tr><td>" + usuarios[i].pUsername + "</td>";
                    cadena += "<td>" + usuarios[i].pFullname + "</td>";
                    cadena += "<td>" + usuarios[i].pFechaNac + "</td></tr>";
                    encontrado = true;
                }
            }

            cadena += "</tbody></table>";
            if (encontrado) {
                $("#divResultado").html(cadena);
            } else {
                $("#divResultado").html("No se encontraron resultados!");
            }
        }
    }

    function RegistrarUsuario() {
        var pUsername = document.getElementById("txtUsername").value;
        var pPassword = document.getElementById("txtPassword").value;
        var pFullname = document.getElementById("txtFullname").value;
        var pCelular = document.getElementById("txtCelular").value;
        var pEmail = document.getElementById("txtEmail").value;
        var pFechaNac = document.getElementById("txtFechaNac").value;

        if (pUsername === "" || pPassword === "" || pFullname === "" || pEmail === "") {
            alert("Por favor, complete todos los campos requeridos.");
            return false;
        }

        if (confirm("¿Estás seguro?")) {
            var usuarios = JSON.parse(localStorage.getItem("usuarios")) || [];
            var nuevoUsuario = {
                pUsername: pUsername,
                pPassword: pPassword,
                pFullname: pFullname,
                pCelular: pCelular,
                pEmail: pEmail,
                pFechaNac: pFechaNac
            };
            usuarios.push(nuevoUsuario);
            localStorage.setItem("usuarios", JSON.stringify(usuarios));

            // Limpiar los campos después de un registro exitoso
            document.getElementById("txtUsername").value = "";
            document.getElementById("txtPassword").value = "";
            document.getElementById("txtFullname").value = "";
            document.getElementById("txtCelular").value = "";
            document.getElementById("txtEmail").value = "";
            document.getElementById("txtFechaNac").value = "";
            $("#divRegistro").html("Registro creado satisfactoriamente!");
        }
    }

    function onPause() {
        // TODO: esta aplicación se ha suspendido. Guarde el estado de la aplicación aquí.
    }

    function onResume() {
        // TODO: esta aplicación se ha reactivado. Restaure el estado de la aplicación aquí.
    }
})();

