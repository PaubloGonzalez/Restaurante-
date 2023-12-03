
function validarFormulario(event) {

    // Prevenir el envío automático del formulario
    event.preventDefault();

    if (validarCedula() && validarNombre() && validarCorreo() && validarContrasenna() && validarConfirmarContrasenna()) {
        mensajeExito();

        setTimeout(function () {
            document.getElementById('formRegister').submit();
        }, 3000); 

        return true;
    } else {
        return false;
    }

    /*return validarCedula() && validarNombre() && validarCorreo() && validarContrasenna();*/

}

function validarCedula() {
    var tipoCedula = parseInt(document.getElementById('TipoCedula').value);
    var identificacion = document.getElementById('Identificacion').value;

    if (isNaN(identificacion)) {
        Swal.fire({
            icon: "error",
            text: "Por favor, ingrese solo números en el campo de identificación.",
            background: "#242424",
            width: "30%",
            confirmButtonColor: '#24a0ed',
        });
        return false;
    }

    if (tipoCedula === 0) {
        if (identificacion.length !== 9) {
            Swal.fire({
                icon: "error",
                text: "La cédula nacional debe tener 9 dígitos.",
                background: "#242424",
                width: "30%",
                confirmButtonColor: '#24a0ed',
            });
            return false;
        }
    }
    else if (tipoCedula === 1) {
        if (identificacion.length !== 12) {
            Swal.fire({
                icon: "error",
                text: "La cédula de residente debe tener 12 digitos.",
                background: "#242424",
                width: "30%",
                confirmButtonColor: '#24a0ed',
            });
            return false;
        }
    }

    return true;
}

function validarNombre() {
    var nombre = document.getElementById('Nombre').value;

    if (nombre.trim() === "") {
        Swal.fire({
            icon: "error",
            text: "Por favor, ingrese su nombre.",
            background: "#242424",
            width: "30%",
            confirmButtonColor: '#24a0ed',
        });
        return false;
    }

    return true;
}


function validarCorreo()
{
    var correoElectronico = document.getElementById("CorreoElectronico").value;
    var correoRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!correoRegex.test(correoElectronico)) {
        Swal.fire({
            icon: "error",
            text: "Por favor, ingrese un correo electrónico válido.",
            background: "#242424",
            width: "30%",
            confirmButtonColor: '#24a0ed',
        });
        return false;
    }

    return true;
}

function validarContrasenna() {
    var contrasenna = document.getElementById('Contrasenna').value;
    if (contrasenna.length < 6) {
        Swal.fire({
            icon: "error",
            text: "La contraseña debe tener al menos 6 caracteres.",
            background: "#242424",
            width: "30%",
            confirmButtonColor: '#24a0ed',
        });
        return false;
    }
    return true;
}

function mensajeExito() {
    Swal.fire({
        icon: "success",
        text: "¡Usuario agregado correctamente!",
        background: "#242424",
        width: "30%",
        confirmButtonColor: '#24a0ed',
    }).then(function () {
        // Después de cerrar la alerta, enviar el formulario
        document.getElementById('formRegister').submit();
    });
}

function validarConfirmarContrasenna() {
    var contrasenna = document.getElementById("Contrasenna").value;
    var confirmarContrasenna = document.getElementById("confirmPassword").value;

    if (confirmarContrasenna.trim() === "" || contrasenna !== confirmarContrasenna) {
        Swal.fire({
            icon: "error",
            text: "Las contraseñas no coincide",
            background: "#242424",
            width: "30%",
            confirmButtonColor: '#24a0ed',
        });
        return false;
    }
    return true;
}

//function mensajeExito() {
//    Swal.fire({
//        icon: "success",
//        text: "¡Usuario agregado correctamente!",
//        background: "#242424",
//        width: "30%",
//        confirmButtonColor: '#24a0ed',
//    });
//}

