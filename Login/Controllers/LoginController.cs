using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Entrar()
        {
            return View();
        }
       [HttpPost]
       public async Task<IActionResult> Entrar(string usuario, string senha)
        {
            if(usuario =="adm" && senha == "123")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usuario));
                claims.Add(new Claim(ClaimTypes.Sid, "10"));
                claims.Add(new Claim(ClaimTypes.Role, "AcessarTela"));
                // Role- quais tipos de login podem usar tais telas - 
                //ID - id do user



                var userIdentity = new ClaimsIdentity(claims, "Acesso");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                // comunicar com servidor // 
                await HttpContext.SignInAsync("Autenticacao", principal, new AuthenticationProperties
                //signin-login , await -aguarde criar terminar de criar os cookies
                {
                    ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(7)),
                    IsPersistent = true
                    // 7 horas sem precisar logar de novo

                }); ;

                //Just redirect to our index after logging in. 
                return Redirect("/");
            }
            else
            {
                TempData["erro"] = "Usuario e/ou senha inválidos";
                return View();
                //não logado
            }
           
            //TempData - informar que deu erro no login
            //Where (a => a.usuario == usuario && a.senha ==senha)  "a" se torna objeto dinâmico
            // claim ( ...) - aguard\ informação -string. Exemplo: "bem-vindo fernando, ..... nome da pessoa logada
            // 
        }
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync("Autenticacao");
            ViewData["ReturnUrl"] = "/";
            return Redirect("/Login/Entrar");
        }
    }

}
