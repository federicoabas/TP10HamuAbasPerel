using Microsoft.Data.SqlClient;
public class Preguntas
{
    private string username;
    private int puntuajeActual;
    private int cantidadPreguntasCorrectas;
    private int contadorNroPreguntaActual;
    private Preguntas preguntaActual;
    private List<Preguntas> listPreguntas;
    List<Respuesta> listRespuestas;

    private void inicializarJuego()
    {
        username = null;
        puntuajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = 0;
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
    public List<int> obtenerProximaPregunta()
    {
        return listPreguntas[contadorNroPreguntaActual +1];
    }
    public void obtenerProximasRespuestas( int idPregunta)
    {
        listRespuestas = BD.ObtenerRespuestas(idPregunta);
    }
    public bool verificarRespuesta(int idRespuesta)
    {
        bool correcta=false;
        if(BD.ObtenerRespuestas(idPregunta)==idRespuesta)
        {
            puntuajeActual+=100;
            cantidadPreguntasCorrectas++;
        }
        contadorNroPreguntaActual++;
        preguntaActual = listPreguntas(contadorNroPreguntaActual);
        return correcta;
    }

}