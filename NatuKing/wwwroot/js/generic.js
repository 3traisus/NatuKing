function mostrarModal(titulo = "'Estás a punto de Borrar la informacion'", texto = "¿Estás Seguro?") {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, Eliminar'
    })
}