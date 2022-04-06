﻿using Login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Controllers {

public class ClientesController : Controller
    {
    [Authorize(AuthenticationSchemes = "Autenticacao")]
    //precisa de 
    public IActionResult Index()
        {
            Random rand = new Random();
            List<ClientesModel> model = new List<ClientesModel>();
            for (int i = 0; i < rand.Next(20,99); i++)
                // variar entre 20 a 99 vezes, criar uma lista de clientes model, fazer lista dela na variavel modelo, criar objetos e ir adicionado na lista. Debug, fazer simulação.
            {
                ClientesModel cliente = new ClientesModel();
                cliente.CPF = rand.Next(111, 999) + "." + rand.Next(111, 999) + "." + rand.Next(111, 999) + "-" + rand.Next(11, 99);
                cliente.Nome = "Nome do Cliente " + i;
                cliente.Telefone = "(16)" + rand.Next(1000, 9999) + "-" + rand.Next(1000, 9999);
                cliente.Id = rand.Next(1000, 9999);
                model.Add(cliente);

            }
            return View(model);
        }
    }
}
