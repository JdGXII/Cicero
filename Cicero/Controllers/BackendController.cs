﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cicero.DataAccess;
using Cicero.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cicero.Controllers
{
    public class BackendController : Controller
    {
        private string usuario = "cicero";
        private string password = "cicero";
        private bool login = false;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string password)
        {
            if(this.usuario == "cicero" && this.password == "cicero")
            {
                this.login = true;
                return RedirectToAction("ListaExpedientes");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    

        private List<ModeloExpediente> getExpedientes()
        {
            List<ModeloExpediente> expedientes = new List<ModeloExpediente>();
            DBConnection testconn = new DBConnection();
            SqlDataReader dataReader = testconn.ReadFromTest("SELECT codigo, email_demandante, nombre_demandado, direccion_demandado, comentario_adicional_reclamo, solicitud_reclamo, foto_dni_url, foto_reclamo_url, video_reclamo_url FROM Expedientes");
            while (dataReader.Read())
            {
                
                string codigo = dataReader.GetString(0);
                string email = dataReader.GetString(1);
                string nombre = dataReader.GetString(2);
                string direccion = dataReader.GetString(3);
                string comentario = dataReader.GetString(4);
                string solicitud = dataReader.GetString(5);
                string foto_dni = dataReader.GetString(6);
                string foto_reclamo = dataReader.GetString(7);
                string video = dataReader.GetString(8);

                ModeloExpediente expediente = new ModeloExpediente(codigo, foto_dni, email, nombre, direccion, solicitud, comentario, video, foto_reclamo);
                expedientes.Add(expediente);
            }
            testconn.CloseDataReader();
            testconn.CloseConnection();
            return expedientes;
            

        }

        private ModeloExpediente getExpediente(string codigo_expediente)
        {
            ModeloExpediente expediente = new ModeloExpediente();
            DBConnection testconn = new DBConnection();
            SqlDataReader dataReader = testconn.ReadFromTest($"SELECT codigo, email_demandante, nombre_demandado, direccion_demandado, comentario_adicional_reclamo, solicitud_reclamo, foto_dni_url, foto_reclamo_url, video_reclamo_url, apoderado_dni_url, respuesta FROM Expedientes WHERE codigo = '{codigo_expediente}'");
            while (dataReader.Read())
            {
                
                
                string codigo = dataReader.GetString(0);
                string email = dataReader.GetString(1);
                string nombre = dataReader.GetString(2);
                string direccion = dataReader.GetString(3);
                string comentario = dataReader.GetString(4);
                string solicitud = dataReader.GetString(5);
                string foto_dni = dataReader.GetString(6);
                string foto_reclamo = dataReader.GetString(7);
                string video = dataReader.GetString(8);
                string dni_respuesta = "No hay respuesta aun";
                string respuesta = "No hay respuesta aun";
                if (!dataReader.IsDBNull(9))
                {
                    dni_respuesta = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    respuesta = dataReader.GetString(10);
                }

                expediente = new ModeloExpediente(codigo, foto_dni, email, nombre, direccion, solicitud, comentario, video, foto_reclamo, dni_respuesta, respuesta);
                
            }
            testconn.CloseDataReader();
            testconn.CloseConnection();
            return expediente;


        }

        public IActionResult ListaExpedientes()
        {
            var modelo = getExpedientes();
            return View(modelo);
            /*if (login)
            {

            }
            else
            {
                return RedirectToAction("Index");
            }*/
        }

        public IActionResult VerExpediente(string id)
        {
            var modelo = getExpediente(id);
            return View(modelo);
            /*if (login)
            {

            }
            else
            {
                return RedirectToAction("Index");
            }*/
        }
    }
    
}