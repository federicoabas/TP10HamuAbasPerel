namespace TP10.Models;
using Microsoft.Data.SqlClient;
public class Juego
{
    public string username;
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
        if (listPreguntas == null || listPreguntas.Count == 0)
            return null;

        contadorNroPreguntaActual++;
        if (contadorNroPreguntaActual < 0 || contadorNroPreguntaActual >= listPreguntas.Count)
            return null;

        preguntaActual = listPreguntas[contadorNroPreguntaActual];
        return preguntaActual;
    }
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
        contadorNroPreguntaActual++;
        if (listPreguntas != null && contadorNroPreguntaActual < listPreguntas.Count)
            preguntaActual = listPreguntas[contadorNroPreguntaActual];
        else
            preguntaActual = null;
        return correcta;
        
    }

}