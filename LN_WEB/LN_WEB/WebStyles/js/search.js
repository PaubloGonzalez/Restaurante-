document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput');

    searchInput.addEventListener('input', function () {
        const searchText = searchInput.value.toLowerCase();
        const rows = document.querySelectorAll('.user-row');

        rows.forEach(row => {
            const rowData = row.textContent.toLowerCase();

            if (rowData.includes(searchText)) {
                row.style.display = '';  // Mostrar la fila si coincide con el texto de búsqueda
            } else {
                row.style.display = 'none';  // Ocultar la fila si no coincide con el texto de búsqueda
            }
        });
    });
});
