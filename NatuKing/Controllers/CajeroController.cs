using NatuKing.Clases;
using Microsoft.AspNetCore.Mvc;
using NatuKing.Models;
using System.Text.RegularExpressions;
using System;

namespace NatuKing.Controllers
{
    public class CajeroController : Controller
    {
        private static List<Medicamento> mediVenta = new List<Medicamento>();
        public IActionResult CajIndex()
        {
            //ViewBag.Numeros = new List<int>();
            ViewBag.Metvent = mediVenta;
            List<Medicamento> meds = new List<Medicamento>();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                meds = (from med in db.Mercancia
                        where med.Habilitado == 1
                        select new Medicamento
                        {
                            Foto = med.Foto,
                            Clv = med.Clvmer,
                            CodBar = med.Codbar,
                            Nombre = med.Nombrem,
                            Pcompra = (float)med.Precomp,
                            Pventa = (float)med.Prevent,
                            Existencia = med.Existencia
                        }).ToList();
                ViewBag.Bus = "";
                ViewBag.Medicamento = meds;
            }
            return View();
        }

        [HttpPost]
        public IActionResult CajIndex(String bus, int id)
        {
            try
            {
                //ViewBag.Numeros.Add(id);
                List<Medicamento> meds = new List<Medicamento>();
                if (bus != null)
                {
                    using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                    {
                        meds = (from med in db.Mercancia
                                where med.Habilitado == 1 && (med.Codbar.Contains(bus) || med.Nombrem.Contains(bus))
                                select new Medicamento
                                {
                                    Foto = med.Foto,
                                    Clv = med.Clvmer,
                                    CodBar = med.Codbar,
                                    Nombre = med.Nombrem,
                                    Pcompra = (float)med.Precomp,
                                    Pventa = (float)med.Prevent,
                                    Existencia = med.Existencia
                                }).ToList();
                    }
                }
                else
                {
                    using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                    {
                        meds = (from med in db.Mercancia
                                where med.Habilitado == 1
                                select new Medicamento
                                {
                                    Foto = med.Foto,
                                    Clv = med.Clvmer,
                                    CodBar = med.Codbar,
                                    Nombre = med.Nombrem,
                                    Pcompra = (float)med.Precomp,
                                    Pventa = (float)med.Prevent,
                                    Existencia = med.Existencia
                                }).ToList();
                    }
                }
                ViewBag.Medicamento = meds;
                ViewBag.Bus = bus;
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public IActionResult LlenarLista(int id)
        {
            if (id != 0)
            {
                List<Medicamento> medicamentos = new List<Medicamento>();
                using (NATURISTADEFINITIVAContext obd = new NATURISTADEFINITIVAContext())
                {
                    medicamentos = (from med in obd.Mercancia
                                    where med.Clvmer== id
                                    select new Medicamento
                                    {
                                        Nombre = med.Nombrem,
                                        Pventa = (float)med.Prevent,
                                        Clv = med.Clvmer
                                    }).ToList();
                }
                mediVenta.AddRange(medicamentos);
            }
            return RedirectToAction("CajIndex");
        }

        public IActionResult RemoverLista(int id)
        {
            return RedirectToAction("CajIndex");
        }

        /*public IActionResult FinalizarVenta(int id)
        {
            
        }*/
    }
}
