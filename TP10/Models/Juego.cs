namespace TP10.Models;
using Microsoft.Data.SqlClient;
public class Juego
{
    private string username;
    public int puntuajeActual;
    private int cantidadPreguntasCorrectas;
    private int contadorNroPreguntaActual;
    private Preguntas preguntaActual;
    private List<Preguntas> listPreguntas;
    List<Respuestas> listRespuestas;

    private void inicializarJuego()
    {
        username = null;
        puntuajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = -1;
        preguntaActual = null;
        listPreguntas = null;
        listRespuestas = null;
    }
    public List<Categorias> obtenerCategorias()
    {
        return BD.ObtenerCategorias();
    }
    public void cargarPartida(string nombreUsuario, int categoria)
    {
        inicializarJuego();
        listPreguntas = BD.ObtenerPreguntas(categoria);
        username = nombreUsuario;
    }
    public Preguntas obtenerProximaPregunta()
    {
        // protección contra null/vacío y fuera de rango
        if (listPreguntas == null || listPreguntas.Count == 0)
            return null;

        contadorNroPreguntaActual++;
        if (contadorNroPreguntaActual < 0 || contadorNroPreguntaActual >= listPreguntas.Count)
            return null;

        preguntaActual = listPreguntas[contadorNroPreguntaActual];
        return preguntaActual;
    }
    // ahora devuelve la lista para poder asignarla al ViewBag
    public List<Respuestas> obtenerProximasRespuestas(int idPregunta)
    {
        listRespuestas = BD.ObtenerRespuestas(idPregunta);
        return listRespuestas;
    }
    public bool verificarRespuesta(int idRespuesta)
    {
        bool correcta=false;
        Respuestas rtaCorrecta=null;
        foreach(Respuestas respuesta in BD.ObtenerRespuestas(preguntaActual.IdPregunta)){
            if(respuesta.Correcta){rtaCorrecta=respuesta;}
        }
        if(rtaCorrecta != null && rtaCorrecta.IdRespuesta==idRespuesta)
        {
            puntuajeActual+=100;
            cantidadPreguntasCorrectas++;
            correcta = true;
        }
        // mover a la siguiente pregunta solo si hay más
        contadorNroPreguntaActual++;
        if (listPreguntas != null && contadorNroPreguntaActual < listPreguntas.Count)
            preguntaActual = listPreguntas[contadorNroPreguntaActual];
        else
            preguntaActual = null;
        return correcta;
        
    }

}