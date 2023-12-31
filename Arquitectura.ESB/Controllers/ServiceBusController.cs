﻿using EjemploPostAzure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arquitectura.ESB.Controllers
{
    public class ServiceBusController : Controller
    {
        private ServiceQueueBus service;
        public ServiceBusController(ServiceQueueBus service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Index(string mensaje, string accion)
        {
            if (accion.ToLower() == "enviar")
            {
                await service.SendMessageAsync(mensaje);
                ViewData["MENSAJE"] = "Mensaje enviado correctamente";
                return View();
            }
            else if (accion.ToLower() == "recibir")
            {
                List<string> mensajes = await service.ReceiveMessagesAsync();
                ViewData["MENSAJE"] = "Mensajes recibidos " + mensajes.Count();
                return View(mensajes);
            }
            return View();
        }
    }
}
