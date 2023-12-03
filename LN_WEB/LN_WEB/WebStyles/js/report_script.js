// Obtener el contexto del lienzo (canvas) para el gráfico
var ctx = document.getElementById('salesChart').getContext('2d');

// Datos para el gráfico
var data = {
    labels: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo'],
    datasets: [{
        label: 'Ventas',
        data: [20, 30, 60, 80, 100],
        backgroundColor: '#fb7304', // Color de las barras
        borderColor: '#2e2e34', // Color del borde de las barras
        borderWidth: 1
    }]
};

// Opciones de configuración del gráfico
var options = {
    responsive: true,
    maintainAspectRatio: true,
    scales: {
        y: {
            beginAtZero: true
        }
    }
};

// Crear el gráfico de barras
var salesChart = new Chart(ctx, {
    type: 'bar',
    data: data,
    options: options
});