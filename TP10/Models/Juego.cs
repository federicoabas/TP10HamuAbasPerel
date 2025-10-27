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
        contadorNroPreguntaActual++;
        foreach(Preguntas pre in listPreguntas){Console.WriteLine(pre.Enunciado);}
        return listPreguntas[contadorNroPreguntaActual];
    }
    public void obtenerProximasRespuestas( int idPregunta)
    {
        listRespuestas = BD.ObtenerRespuestas(idPregunta);
    }
    public bool verificarRespuesta(int idRespuesta)
    {
        bool correcta=false;
        Respuestas rtaCorrecta=null;
        foreach(Respuestas respuesta in BD.ObtenerRespuestas(preguntaActual.IdPregunta)){
            if(respuesta.Correcta){rtaCorrecta=respuesta;}
        }
        if(rtaCorrecta.IdRespuesta==idRespuesta)
        {
            puntuajeActual+=100;
            cantidadPreguntasCorrectas++;
        }
        contadorNroPreguntaActual++;
        preguntaActual = listPreguntas[contadorNroPreguntaActual];
        return correcta;
        
    }

}