using NatuKing.Clases;
using Microsoft.AspNetCore.Mvc;
using NatuKing.Models;
using System.Drawing;

namespace NatuKing.Controllers
{
    public class MostradorController : Controller
    {
        public IActionResult MostIndexPrincipal(User obj)
        {
            List<Medicamento> meds = new List<Medicamento>();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                meds = (from med in db.Mercancia
                        select new Medicamento
                        {
                            Clv = med.Clvmer,
                            CodBar = med.Codbar,
                            Nombre = med.Nombrem,
                            Existencia = med.Existencia,
                            //Pcompra = (float)med.Precomp,
                            Pventa = (float)med.Prevent,
                            Caducidad = med.Caducidad,
                            Cura = med.Cura,
                            Descripcion = med.Descripcion,
                            Foto = med.Foto
                        }).ToList();
            }
            ViewBag.Medicamento = meds;
            ViewBag.Valor = 0;
            ViewBag.Caso = 0;
            return View();
        }

        [HttpPost]
        public IActionResult MostIndexPrincipal(String bus,int caso,String valor)
        {
            try
            {
                List<Medicamento> meds = new List<Medicamento>();
                switch(caso)
                {
                    case 0:
                        if (bus != null)
                        {
                            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                            {
                                meds = (from med in db.Mercancia
                                        where med.Codbar.Contains(bus) || med.Nombrem.Contains(bus) || med.Cura.Contains(bus)
                                        select new Medicamento
                                        {
                                            Clv = med.Clvmer,
                                            CodBar = med.Codbar,
                                            Nombre = med.Nombrem,
                                            Existencia = med.Existencia,
                                            //Pcompra = (float)med.Precomp,
                                            Pventa = (float)med.Prevent,
                                            Caducidad = med.Caducidad,
                                            Cura = med.Cura,
                                            Descripcion = med.Descripcion,
                                            Foto = med.Foto
                                        }).ToList();
                            }
                        }
                        else
                        {
                            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                            {
                                meds = (from med in db.Mercancia
                                        select new Medicamento
                                        {
                                            Clv = med.Clvmer,
                                            CodBar = med.Codbar,
                                            Nombre = med.Nombrem,
                                            Existencia = med.Existencia,
                                            //Pcompra = (float)med.Precomp,
                                            Pventa = (float)med.Prevent,
                                            Caducidad = med.Caducidad,
                                            Cura = med.Cura,
                                            Descripcion = med.Descripcion,
                                            Foto = med.Foto
                                        }).ToList();
                            }
                        }
                        break;
                    case 1:
                        if (bus != null)
                        {
                            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                            {
                                meds = (from med in db.Mercancia
                                        where (med.Codbar.Contains(bus) || med.Nombrem.Contains(bus) || med.Cura.Contains(bus)) && 
                                        med.Prevent<=float.Parse(valor)
                                        select new Medicamento
                                        {
                                            Clv = med.Clvmer,
                                            CodBar = med.Codbar,
                                            Nombre = med.Nombrem,
                                            Existencia = med.Existencia,
                                            //Pcompra = (float)med.Precomp,
                                            Pventa = (float)med.Prevent,
                                            Caducidad = med.Caducidad,
                                            Cura = med.Cura,
                                            Descripcion = med.Descripcion,
                                            Foto = med.Foto
                                        }).ToList();
                            }
                        }
                        else
                        {
                            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                            {
                                meds = (from med in db.Mercancia
                                        where med.Prevent <= float.Parse(valor)
                                        select new Medicamento
                                        {
                                            Clv = med.Clvmer,
                                            CodBar = med.Codbar,
                                            Nombre = med.Nombrem,
                                            Existencia = med.Existencia,
                                            //Pcompra = (float)med.Precomp,
                                            Pventa = (float)med.Prevent,
                                            Caducidad = med.Caducidad,
                                            Cura = med.Cura,
                                            Descripcion = med.Descripcion,
                                            Foto = med.Foto
                                        }).ToList();
                            }
                        }
                        break;
                }
                ViewBag.Bus = bus;
                ViewBag.Valor = int.Parse(valor);
                ViewBag.Caso = caso;
                ViewBag.Medicamento = meds;
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}
