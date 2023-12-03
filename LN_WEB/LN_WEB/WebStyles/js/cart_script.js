document.getElementById('eliminarProductoBtn').addEventListener('click', function () {
    // Lógica para eliminar el producto (puedes hacer una solicitud AJAX aquí)

    // Supongamos que la eliminación fue exitosa
    var eliminacionExitosa = true;

    // Muestra la alerta según el resultado de la eliminación
    if (eliminacionExitosa) {
        mostrarAlerta('Producto eliminado correctamente.', 'success');
    } else {
        mostrarAlerta('Error al eliminar el producto.', 'error');
    }
});

function mostrarAlerta(mensaje, tipo) {
    var alertContainer = document.getElementById('alertContainer');
    var alertDiv = document.createElement('div');
    alertDiv.className = 'alert alert-' + tipo;
    alertDiv.appendChild(document.createTextNode(mensaje));
    alertContainer.appendChild(alertDiv);

    // Desaparece la alerta después de 3 segundos
    setTimeout(function () {
        alertDiv.remove();
    }, 3000);
}
