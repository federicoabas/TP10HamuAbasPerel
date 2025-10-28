using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP10.Models;

namespace TP10.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Juego juego = new Juego();
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return View("Index");
    }

    public IActionResult ConfigurarJuego()
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        ViewBag.categorias=juego.obtenerCategorias();
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return View("configurarJuego");
    }

    public IActionResult Comenzar(string username, int categoria)
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        juego.cargarPartida(username, categoria);
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return RedirectToAction("Jugar");
    }
    public IActionResult jugar()
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        Preguntas preg = juego.obtenerProximaPregunta();
        ViewBag.username= juego.username;
        
        if (preg == null)
        {
            ViewBag.puntajeActual = juego.puntuajeActual;
            HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
            return View("Fin");
        }
            ViewBag.preguntaActual = preg;
            ViewBag.respuestas = juego.obtenerProximasRespuestas(preg.IdPregunta);
            ViewBag.puntajeActual = juego.puntuajeActual;

        ViewBag.preguntaActual = preg;
        ViewBag.respuestas = juego.obtenerProximasRespuestas(preg.IdPregunta);

        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return View("Juego");
    }
        
    [HttpPost]
    public IActionResult verificarRespuesta(int idPregunta, int idRespuesta)
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        bool esCorrecta = juego.verificarRespuesta(idRespuesta);
        ViewBag.esCorrecta = esCorrecta;
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return RedirectToAction ("jugar");
    }
}

