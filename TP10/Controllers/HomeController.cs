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
        if(Objeto!=null){
            Juego juego = new Juego();
            HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        }
        return View("Index");
    }

    public IActionResult ConfigurarJuego()
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        ViewBag.categorias=juego.obtenerCategorias();
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return View("configurarJuego");
    }

    public IActionresult Comenzar(string username, int categoria)
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        ViewBag.categorias=juego.obtenerCategorias();
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return RedirectToAction("Jugar");
    }
    public IActionResult jugar()
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        ViewBag.preguntaActual = juego.obtenerProximaPregunta();
        if(ViewBag.preguntaActual== null)
        {
            return View("Fin");
        }
        else
        {
        ViewBag.respuestaActual = juego.obtenerProximasRespuestas(ViewBag.preguntaActual.idPregunta); 
        HttpContext.Session.SetString("juego", Objeto.ObjectToString(juego));
        return View("Jugar");
    
        } 
        }
    public IActionResult verificarRespuesta(int idPregunta, int idRespuesta)
    {
        Juego juego = Objeto.StringToObject<Juego>(HttpContext.Session.GetString("juego"));
        ViewBag.verificarRespuesta = juego.verificarRespuesta(idRespuesta);
        return View("Respuesta");
    }
}
