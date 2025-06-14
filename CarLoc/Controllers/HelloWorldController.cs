using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace CarLoc.Controllers;

public class HelloWorldController: Controller
{
    public IActionResult Index()
    {
        return View();
    }
    /*
    public string Welcome()
    {
        return "Isso é uma ação do método Welcome";
    }
    */
    public IActionResult Welcome(string nome, int vezes=1)
    {
        // Preparar dicionario de dados
        ViewData["mensagem"] = "Olá "+nome;
        ViewData["vezes"] = vezes;
        
        return View();
    }

    public IActionResult Soma(int alg1, int alg2)
    {
        // Calcular
        int res = alg1 + alg2;
        // Preparar dicionario de dados
        ViewData["alg1"] = alg1;
        ViewData["alg2"] = alg2;
        ViewData["result"] = res;
        
        return View();
    }
}