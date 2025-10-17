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
        ViewBag.preguntaActual = juego.obtenerProximaPregunta();
        ViewBag.respuestas=juego.obtenerProximasRespuestas(ViewBag.preguntaActual.IdPregunta);
        if(ViewBag.preguntaActual== null)
        {
            ViewBag.puntajeActual = juego.puntuajeActual;
            return View("Fin");
        }
        else
        {
        ViewBag.respuestaActual = juego.obtenerProximasRespuestas(ViewBag.preguntaActual.idPregunta); 

        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return View("Juego");
        } 
    }
    [HttpPost]
    public IActionResult verificarRespuesta(int idPregunta, int idRespuesta)
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        ViewBag.verificarRespuesta = juego.verificarRespuesta(idRespuesta);
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return View("Juego");
    }
}
