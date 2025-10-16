function ponerFuncionFinal() {
    const contenedor = document.getElementById("respuesta")
    let rtaVerificada=document.getElementById("verificado").value
    let rtaActual=document.getElementById("rta").value
    
    contenedor.innerHTML = `
    if(${rtaVerificada}){<h3>Respuesta correcta!</h3>}
    else{<h3>Respuesta incorrecta!
        La respuesta correcta era: ${rta}</h3>}`
}