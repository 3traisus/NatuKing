using NatuKing.Clases;
using Microsoft.AspNetCore.Mvc;
using NatuKing.Models;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace NatuKing.Controllers
{
    public class AdminsController : Controller
    {
        public IActionResult AdminIndex(User obj)
        {
            ViewBag.User = obj.Name;
            return View();
        }

        public IActionResult Mercancia()
        {
            ViewBag.Error = false;
            List<Medicamento> meds = new List<Medicamento>();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                meds = (from med in db.Mercancia
                        where med.Habilitado==1
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
            ViewBag.Medicamento = meds;
            return View();
        }

        [HttpPost]
        public IActionResult Mercancia(String bus)
        {
            try
            {
                List<Medicamento> meds = new List<Medicamento>();
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    if (bus != null)
                    {
                        if (Regex.IsMatch(bus, "([A-Z]|[a-z])[0-9]*([A-Z]|[a-z])[0-9]*([A-Z]|[a-z])"))
                        {
                            meds = (from med in db.Mercancia
                                    where (med.Codbar.Equals(bus) || med.Nombrem.Contains(bus)) && med.Habilitado == 1
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
                        else
                        {
                            meds = (from med in db.Mercancia
                                    where med.Clvmer == int.Parse(bus) && med.Habilitado == 1
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
                        Console.WriteLine("Estoy aqui");
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
                    if (meds[0] == null)
                        ViewBag.Error = true;
                    else
                    {
                        ViewBag.Medicamento = meds;
                        ViewBag.Error = false;
                    }
                    ViewBag.Bus = bus;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            string error;
            Console.WriteLine(id);
            try
            {
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    Mercancium oPagina = db.Mercancia
                        .Where(p => p.Clvmer == id)
                        .First();
                    oPagina.Habilitado = 0;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return RedirectToAction("Mercancia");
        }

        public IActionResult AgregarMer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarMer(Medicamento obj)
        {
            try
            {
                Console.WriteLine("Llegamos" + obj.Nombre);
                List<Medicamento> list = new List<Medicamento>();
                int temp;
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    list = (from med in db.Mercancia
                            select new Medicamento
                            {
                                Clv = med.Clvmer
                            }).ToList();
                    temp = list[0].Clv;
                    for (int x = 1; x < list.Count; x++)
                        if (temp < list[x].Clv)
                            temp = list[x].Clv;

                    Mercancium ob = new Mercancium();
                    ob.Clvmer = temp + 1;
                    ob.Nombrem = obj.Nombre;
                    ob.Caducidad = obj.Caducidad;
                    ob.Codbar = obj.CodBar;
                    ob.Cura = obj.Cura;
                    ob.Descripcion = obj.Descripcion;
                    ob.Habilitado = 1;
                    ob.Presentacion= obj.Presentacion;
                    db.Mercancia.Add(ob);
                    db.SaveChanges();
                }
                return RedirectToAction("Exito");
            }
            catch (Exception e)
            {
                Console.WriteLine("ValioVerga");
                return View(obj);
            }
        }

        public IActionResult ModificarMed(int id, String cad)
        {
            List<Medicamento> meds = new List<Medicamento>();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                meds = (from med in db.Mercancia
                        where med.Clvmer == id
                        select new Medicamento
                        {
                            Foto = med.Foto,
                            Clv = med.Clvmer,
                            CodBar = med.Codbar,
                            Nombre = med.Nombrem,
                            Pcompra = (float)med.Precomp,
                            Pventa = (float)med.Prevent,
                            Existencia = med.Existencia,
                            Cura = med.Cura,
                            Descripcion = med.Descripcion,
                            Caducidad = med.Caducidad,
                            Presentacion = med.Presentacion
                        }).ToList();
            }
            ViewBag.Medicamento = meds;
            return View();
        }
        [HttpPost]
        public IActionResult ModificarMed(int id)
        {
            List<Medicamento> meds = new List<Medicamento>();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                meds = (from med in db.Mercancia
                        where med.Clvmer == id
                        select new Medicamento
                        {
                            Foto = med.Foto,
                            Clv = med.Clvmer,
                            CodBar = med.Codbar,
                            Nombre = med.Nombrem,
                            Pcompra = (float)med.Precomp,
                            Pventa = (float)med.Prevent,
                            Existencia = med.Existencia,
                            Cura = med.Cura,
                            Descripcion = med.Descripcion,
                            Caducidad = med.Caducidad,
                            Presentacion = med.Presentacion
                        }).ToList();
            }
            ViewBag.Medicamento = meds;
            return View();
        }

        [HttpPost]
        public IActionResult ModificarObjeto(Medicamento obj)
        {
            try
            {
                Console.WriteLine("Hola Modificar");
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    Mercancium ob = db.Mercancia
                        .Where(p => p.Clvmer == obj.Clv)
                        .First();

                    ob.Foto = obj.Foto;
                    ob.Nombrem = obj.Nombre;
                    ob.Existencia = obj.Existencia;
                    ob.Cura = obj.Cura;
                    if (obj.Descripcion != null)
                        ob.Descripcion = obj.Descripcion;
                    ob.Caducidad = obj.Caducidad;
                    ob.Precomp = obj.Pcompra;
                    ob.Prevent = obj.Pventa;
                    ob.Codbar = obj.CodBar;
                    ob.Presentacion= obj.Presentacion;
                    db.SaveChanges();
                    return RedirectToAction("Exito");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Vergaaaaaaa");
                return RedirectToAction("ModificarMed", new { id = 1, cad = "" });
            }
        }

        public IActionResult Exito()
        {
            return View();
        }

        public IActionResult Empleados()
        {
            ViewBag.Error = false;
            List<Empleadocls> list = new List<Empleadocls>(); 
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                list = (from med in db.Empleados
                        where med.Habilitado==1
                        select new Empleadocls
                        {
                            Nombre = med.Nombree,
                            Puesto = med.Puesto,
                            Horario = med.Horario,
                            Sueldo = (float)med.Sueldo,
                            Id= med.Id,
                        }).ToList();
            }
            ViewBag.Empleado = list;
            return View();
        }
        [HttpPost]
        public IActionResult Empleados(String bus)
        {
            try
            {
                List<Empleadocls> meds = new List<Empleadocls>();
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    if (bus != null)
                    {
                        if (Regex.IsMatch(bus,@"^\d+$"))
                        {
                            meds = (from med in db.Empleados
                                    where med.Habilitado == 1 && med.Id == int.Parse(bus)
                                    select new Empleadocls
                                    {
                                        Nombre = med.Nombree,
                                        Puesto = med.Puesto,
                                        Horario = med.Horario,
                                        Sueldo = (float)med.Sueldo,
                                        Id = med.Id,
                                    }).ToList();
                        }
                        else
                        {
                            meds = (from med in db.Empleados
                                    where med.Habilitado == 1 &&
                                        (med.Nombree.Contains(bus) || med.Puesto.Contains(bus) || med.Correoe.Contains(bus))
                                    select new Empleadocls
                                    {
                                        Nombre = med.Nombree,
                                        Puesto = med.Puesto,
                                        Horario = med.Horario,
                                        Sueldo = (float)med.Sueldo,
                                        Id = med.Id,
                                    }).ToList();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Estoy aqui");
                        meds = (from med in db.Empleados
                                where med.Habilitado == 1
                                select new Empleadocls
                                {
                                    Nombre = med.Nombree,
                                    Puesto = med.Puesto,
                                    Horario = med.Horario,
                                    Sueldo = (float)med.Sueldo,
                                    Id = med.Id,
                                }).ToList();
                    }

                    ViewBag.Empleado = meds;
                    ViewBag.Error = false;
                    ViewBag.Bus = bus;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                return View();
            }
        }

        public IActionResult EliminarEmp(int id)
        {
            string error;
            Console.WriteLine(id);
            try
            {
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    Empleado oPagina = db.Empleados
                        .Where(p => p.Id == id)
                        .First();
                    oPagina.Habilitado = 0;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return RedirectToAction("Empleados");
        }

        public IActionResult AgregarEmp()
        {
            ViewBag.Horario = false;
            ViewBag.Puesto = false;
            return View();
        }
        [HttpPost]
        public IActionResult AgregarEmp(Empleadocls obj)
        {
            try
            {
                Console.WriteLine("Llegamos" + obj.Nombre);
                List<Empleadocls> list = new List<Empleadocls>();
                int temp;
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    list = (from med in db.Empleados
                            select new Empleadocls
                            {
                                Id= med.Id,
                            }).ToList();
                    temp = list[0].Id;
                    for (int x = 1; x < list.Count; x++)
                        if (temp < list[x].Id)
                            temp = list[x].Id;

                    Empleado ob = new Empleado();
                    ob.Nombree = obj.Nombre;
                    if(obj.Puesto!=null)
                    {
                        ob.Puesto = obj.Puesto;
                        ViewBag.Puesto = false;
                    }
                    else
                        ViewBag.Puesto = true;
                    ob.Telefonoe = obj.Telefono;
                    ob.Sueldo= obj.Sueldo;
                    if(obj.Horario!=null)
                    {
                        ob.Horario = obj.Horario;
                        ViewBag.Horario = false;
                    }
                    else
                        ViewBag.Horario = true;
                    ob.Correoe = obj.Correo;
                    ob.Ppass = obj.Password;
                    ob.Habilitado = 1;
                    ob.Id= temp+1;
                    db.Empleados.Add(ob);
                    db.SaveChanges();
                }
                return RedirectToAction("Exito");
            }
            catch (Exception e)
            {
                Console.WriteLine("ValioVerga");
                return View(obj);
            }
        }

        public IActionResult ModificarEmp(Empleadocls obj)
        {
            ViewBag.Empleado = obj;
            return View();
        }
        [HttpPost]
        public IActionResult ModificarEmp(int id)
        {
            Empleadocls obj = new Empleadocls();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                Empleado oPagina = db.Empleados
                        .Where(p => p.Id == id)
                        .First();
                obj.Puesto = oPagina.Puesto;
                obj.Id= oPagina.Id;
                obj.Nombre = oPagina.Nombree;
                obj.Horario = oPagina.Horario;
                obj.Telefono = oPagina.Telefonoe;
                obj.Sueldo = (float)oPagina.Sueldo;
                obj.Correo = oPagina.Correoe;
                obj.Password = oPagina.Ppass;
                ViewBag.Empleado = obj;
            }
            return View();
        }

        [HttpPost]
        public IActionResult ModificarObj(Empleadocls obj)
        {

            try
            {
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    Empleado oPagina = db.Empleados
                            .Where(p => p.Id == obj.Id)
                            .First();
                    oPagina.Nombree = obj.Nombre;
                    oPagina.Puesto = obj.Puesto;
                    oPagina.Telefonoe = obj.Telefono;
                    oPagina.Sueldo = obj.Sueldo;
                    oPagina.Horario = obj.Horario;
                    oPagina.Correoe = obj.Correo;
                    oPagina.Ppass = obj.Password;
                    db.SaveChanges();
                    return RedirectToAction("Exito");
                }

            }
            catch(Exception ex)
            {
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    return RedirectToAction("ModificarEmp", obj);
                }
            }
        }

        public IActionResult Proveedores()
        {
            ViewBag.Bus = "";
            List<Provedorcls> list = new List<Provedorcls>();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                list = (from med in db.Proveedors
                        where med.Habilitado == 1
                        select new Provedorcls
                        {
                            Nombre = med.Nombrep,
                            Ciudad = med.Ciudad,
                            Telefono = med.Telefonop,
                            Correo = med.Correop,
                            Clv = med.Clvpro
                        }).ToList();
            }
            ViewBag.Proveedor = list;
            return View();
        }
        [HttpPost]
        public IActionResult Proveedores(String bus)
        {
            try
            {
                List<Provedorcls> meds = new List<Provedorcls>();
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    if (bus != null)
                    {
                        if (Regex.IsMatch(bus, @"^\d+$"))
                        {
                            meds = (from med in db.Proveedors
                                    where med.Habilitado == 1 && (med.Clvpro == long.Parse(bus) ||
                                        med.Telefonop.Contains(bus) || med.Telefonop.Equals(bus))
                                    select new Provedorcls
                                    {
                                        Nombre = med.Nombrep,
                                        Ciudad = med.Ciudad,
                                        Telefono = med.Telefonop,
                                        Correo = med.Correop,
                                        Clv = med.Clvpro,
                                    }).ToList();
                        }
                        else
                        {
                            meds = (from med in db.Proveedors
                                    where med.Habilitado == 1 &&
                                        (med.Nombrep.Contains(bus)||med.Correop.Contains(bus))
                                    select new Provedorcls
                                    {
                                        Nombre = med.Nombrep,
                                        Ciudad = med.Ciudad,
                                        Telefono = med.Telefonop,
                                        Correo = med.Correop,
                                        Clv = med.Clvpro,
                                    }).ToList();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Estoy aqui");
                        meds = (from med in db.Proveedors
                                where med.Habilitado == 1
                                select new Provedorcls
                                {
                                    Nombre = med.Nombrep,
                                    Ciudad = med.Ciudad,
                                    Telefono = med.Telefonop,
                                    Correo = med.Correop,
                                    Clv = med.Clvpro,
                                }).ToList();
                    }

                    ViewBag.Proveedor = meds;
                    ViewBag.Bus = bus;
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarPro(int id)
        {
            string error;
            Console.WriteLine(id);
            try
            {
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    Proveedor oPagina = db.Proveedors
                        .Where(p => p.Clvpro == id)
                        .First();
                    oPagina.Habilitado = 0;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return RedirectToAction("Proveedores");
        }

        public IActionResult AgregarProv()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarProv(Provedorcls obj)
        {
            try
            {
                Console.WriteLine("Llegamos" + obj.Nombre);
                List<Provedorcls> list = new List<Provedorcls>();
                int temp;
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    list = (from med in db.Proveedors
                            select new Provedorcls
                            {
                                Clv = med.Clvpro,
                            }).ToList();
                    temp = list[0].Clv;
                    for (int x = 1; x < list.Count; x++)
                        if (temp < list[x].Clv)
                            temp = list[x].Clv;

                    Proveedor ob = new Proveedor();
                    ob.Clvpro = temp + 1;
                    ob.Nombrep = obj.Nombre;
                    ob.Ciudad= obj.Ciudad;
                    ob.Cp= obj.Cp;
                    ob.Correop = obj.Correo;
                    ob.Telefonop = obj.Telefono;
                    ob.Habilitado = 1;
                    db.Proveedors.Add(ob);
                    db.SaveChanges();
                }
                return RedirectToAction("Exito");
            }
            catch (Exception e)
            {
                return View(obj);
            }
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModificarPro(int id)
        {
            Provedorcls obj = new Provedorcls();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                Proveedor oPagina = db.Proveedors
                        .Where(p => p.Clvpro == id)
                        .First();
                obj.Nombre = oPagina.Nombrep;
                obj.Ciudad= oPagina.Ciudad;
                obj.Cp= oPagina.Cp;
                obj.Correo = oPagina.Correop;
                obj.Telefono = oPagina.Telefonop;
                obj.Clv = oPagina.Clvpro;
                ViewBag.Proveedor = obj;
            }
            return View();
        }

        public IActionResult ModificarPro(Empleadocls obj)
        {
            ViewBag.Empleado = obj;
            return View();
        }

        [HttpPost]
        public IActionResult ModificarObjPro(Provedorcls obj)
        {
            try
            {
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    Proveedor oPagina = db.Proveedors
                            .Where(p => p.Clvpro == obj.Clv)
                            .First();
                    oPagina.Nombrep= obj.Nombre;
                    oPagina.Ciudad= obj.Ciudad;
                    oPagina.Cp= obj.Cp;
                    oPagina.Correop = obj.Correo;
                    oPagina.Telefonop = obj.Telefono;
                    db.SaveChanges();
                    return RedirectToAction("Exito");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("ModificarPro", obj);
            }
        }

        public IActionResult Comprar()
        {
            List<Comprascls> meds = new List<Comprascls>();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                meds = (from mer in db.Mercancia
                        join rel1 in db.Commers on mer.Clvmer equals rel1.Clvmer
                        join comp in db.Compras on rel1.Clvcom equals comp.Clvcom
                        join rel2 in db.Compros on comp.Clvcom equals rel2.Clvcom
                        join rel3 in db.Empcoms on comp.Clvcom equals rel3.Clvcom
                        join prov in db.Proveedors on rel2.Clvpro equals prov.Clvpro
                        join emp in db.Empleados on rel3.Id equals emp.Id
                        select new Comprascls
                        {
                            TotalV = (float)comp.Totalc,
                            Cantidad = (int)rel1.Cantidadcm,
                            CostoU = (float)comp.Totalc / (int)rel1.Cantidadcm,
                            Fecha = comp.Fechac,
                            Codbar = mer.Codbar,
                            idEmp = emp.Id,
                            idProv = prov.Clvpro

                        }).ToList();
            }
            ViewBag.Compra = meds;
            ViewBag.Sect = 0;
            return View();
        }

        [HttpPost]
        public IActionResult Comprar(String bus,int sector,String bus2)
        {
            try
            {
                ViewBag.Sect = 0;
                List<Comprascls> meds = new List<Comprascls>();
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    if (bus != null)
                    {
                            if (Regex.IsMatch(bus, @"^\d+$"))
                            {
                                meds = (from mer in db.Mercancia
                                        join rel1 in db.Commers on mer.Clvmer equals rel1.Clvmer
                                        join comp in db.Compras on rel1.Clvcom equals comp.Clvcom
                                        join rel2 in db.Compros on comp.Clvcom equals rel2.Clvcom
                                        join rel3 in db.Empcoms on comp.Clvcom equals rel3.Clvcom
                                        join prov in db.Proveedors on rel2.Clvpro equals prov.Clvpro
                                        join emp in db.Empleados on rel3.Id equals emp.Id
                                        where emp.Id == long.Parse(bus) || prov.Clvpro == long.Parse(bus) || mer.Codbar.Contains(bus)
                                        select new Comprascls
                                        {
                                            TotalV = (float)comp.Totalc,
                                            Cantidad = (int)rel1.Cantidadcm,
                                            CostoU = (float)comp.Totalc / (int)rel1.Cantidadcm,
                                            Fecha = comp.Fechac,
                                            Codbar = mer.Codbar,
                                            idEmp = emp.Id,
                                            idProv = prov.Clvpro

                                        }).ToList();
                            }
                            else
                            {
                                meds = (from mer in db.Mercancia
                                        join rel1 in db.Commers on mer.Clvmer equals rel1.Clvmer
                                        join comp in db.Compras on rel1.Clvcom equals comp.Clvcom
                                        join rel2 in db.Compros on comp.Clvcom equals rel2.Clvcom
                                        join rel3 in db.Empcoms on comp.Clvcom equals rel3.Clvcom
                                        join prov in db.Proveedors on rel2.Clvpro equals prov.Clvpro
                                        join emp in db.Empleados on rel3.Id equals emp.Id
                                        where comp.Fechac.Contains(bus)
                                        select new Comprascls
                                        {
                                            TotalV = (float)comp.Totalc,
                                            Cantidad = (int)rel1.Cantidadcm,
                                            CostoU = (float)comp.Totalc / (int)rel1.Cantidadcm,
                                            Fecha = comp.Fechac,
                                            Codbar = mer.Codbar,
                                            idEmp = emp.Id,
                                            idProv = prov.Clvpro

                                        }).ToList();
                            }
                        
                    }
                    else
                    {
                        meds = (from mer in db.Mercancia
                                join rel1 in db.Commers on mer.Clvmer equals rel1.Clvmer
                                join comp in db.Compras on rel1.Clvcom equals comp.Clvcom
                                join rel2 in db.Compros on comp.Clvcom equals rel2.Clvcom
                                join rel3 in db.Empcoms on comp.Clvcom equals rel3.Clvcom
                                join prov in db.Proveedors on rel2.Clvpro equals prov.Clvpro
                                join emp in db.Empleados on rel3.Id equals emp.Id
                                select new Comprascls
                                {
                                    TotalV = (float)comp.Totalc,
                                    Cantidad = (int)rel1.Cantidadcm,
                                    CostoU = (float)comp.Totalc / (int)rel1.Cantidadcm,
                                    Fecha = comp.Fechac,
                                    Codbar = mer.Codbar,
                                    idEmp = emp.Id,
                                    idProv = prov.Clvpro

                                }).ToList();
                    }
                    if (bus2 != null)
                        if (sector == 1)
                        {
                            Medicamento mer = new Medicamento();
                            Mercancium oPagina = db.Mercancia
                                    .Where(p => p.Codbar == bus2)
                                    .First();

                            mer.Nombre = oPagina.Nombrem;
                            mer.Presentacion = oPagina.Presentacion;
                            mer.Pcompra = (float)oPagina.Precomp;
                            mer.Pventa = (float)oPagina.Prevent;
                            ViewBag.Mer = mer;
                        }
                        else if (sector == 2)
                        {
                            Empleadocls emp = new Empleadocls();
                            Empleado oPagina = db.Empleados
                                    .Where(p => p.Id == int.Parse(bus2))
                                    .First();
                            emp.Nombre = oPagina.Nombree;
                            emp.Correo = oPagina.Correoe;
                            emp.Telefono = oPagina.Telefonoe;
                            ViewBag.Emp = emp;
                        }
                        else if (sector == 3)
                        {
                            Provedorcls prov = new Provedorcls();
                            Proveedor oPagina = db.Proveedors
                                    .Where(p => p.Clvpro == int.Parse(bus2))
                                    .First();
                            prov.Nombre = oPagina.Nombrep;
                            prov.Correo = oPagina.Correop;
                            prov.Telefono = oPagina.Telefonop;
                            ViewBag.Prov = prov;
                        }

                }
                ViewBag.Compra = meds;
                ViewBag.Bus = bus;
                ViewBag.Bus2 = bus2;
                ViewBag.Sect = sector;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult NuevaVenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NuevaVenta(Medicamento obj)
        {
            try
            {
                List<Medicamento> list = new List<Medicamento>();
                int temp;
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    list = (from med in db.Mercancia
                            select new Medicamento
                            {
                                Clv = med.Clvmer
                            }).ToList();
                    temp = list[0].Clv;
                    for (int x = 1; x < list.Count; x++)
                        if (temp < list[x].Clv)
                            temp = list[x].Clv;

                    Mercancium ob = new Mercancium();
                    ob.Clvmer = temp + 1;
                    ob.Nombrem = obj.Nombre;
                    ob.Caducidad = obj.Caducidad;
                    ob.Codbar = obj.CodBar;
                    ob.Cura = obj.Cura;
                    ob.Descripcion = obj.Descripcion;
                    ob.Habilitado = 1;
                    ob.Presentacion = obj.Presentacion;
                    db.Mercancia.Add(ob);
                    db.SaveChanges();
                }
                return RedirectToAction("ViejaVenta", obj);
            }
            catch (Exception e)
            {
                Console.WriteLine("ValioVerga");
                return View(obj);
            }
        }

        public IActionResult ViejaVenta(Medicamento obj)
        {
            ViewBag.Codbar = obj.CodBar;
            ViewBag.Bus = "";
            ViewBag.Bus2 = "";
            List<Provedorcls> list = new List<Provedorcls>();
            using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
            {
                list = (from med in db.Proveedors
                        where med.Habilitado == 1
                        select new Provedorcls
                        {
                            Nombre = med.Nombrep,
                            Telefono = med.Telefonop,
                            Correo = med.Correop,
                            Clv = med.Clvpro
                        }).ToList();
            }
            ViewBag.Proveedor = list;
            return View();
        }

        [HttpPost]
            public IActionResult ViejaVenta(String bus,int ban,String bus2)
        {
            Console.WriteLine(bus);
            ViewBag.Bus = bus;
            ViewBag.Bus2 = bus2;
            ViewBag.Fecha = this.Fecha();
            List<Provedorcls> list = new List<Provedorcls>();
            List<Medicamento> mer = new List<Medicamento>();

            if (bus2 != null)
            {
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    list = (from med in db.Proveedors
                            where med.Habilitado == 1 && (med.Nombrep.Contains(bus2) || 
                                med.Correop.Contains(bus2)||med.Telefonop.Contains(bus2))
                                select new Provedorcls
                                {
                                    Nombre = med.Nombrep,
                                    Telefono = med.Telefonop,
                                    Correo = med.Correop,
                                    Clv = med.Clvpro
                                }).ToList();
                }
            }
            else
            {
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    list = (from med in db.Proveedors
                            where med.Habilitado == 1
                            select new Provedorcls
                            {
                                Nombre = med.Nombrep,
                                Telefono = med.Telefonop,
                                Correo = med.Correop,
                                Clv = med.Clvpro
                            }).ToList();
                }
            }
            if(ban==1)
            {
                if (bus != null)
                {
                    if (Regex.IsMatch(bus, @"^\d+$"))
                    {
                        using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                        {
                            mer = (from med in db.Mercancia
                                    where med.Habilitado == 1 && med.Codbar.Equals(bus)
                                    select new Medicamento
                                    {
                                        Clv = med.Clvmer
                                    }).ToList();
                        }
                    }

                }
            }

            ViewBag.Proveedor = list;
            if(mer!=null)
                ViewBag.Med = 1;
            else
                ViewBag.Med = 0;
            return View();
        }

        public String Fecha()
        {
            return "";
        }
        [HttpPost]
        public IActionResult ViejaVentaFinalizar(compraRealizarcls obj)
        {
            try
            {
                int indcompDb = 0;
                List<CompraDbcls> compraDb = new List<CompraDbcls>();
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    compraDb = (from med in db.Compras
                                select new CompraDbcls
                                {
                                    Clv = med.Clvcom
                                }).ToList();
                    for (int x = 0; x < compraDb.Count; x++)
                    {
                        if (indcompDb < compraDb[x].Clv)
                            indcompDb = compraDb[x].Clv;
                    }

                    Compra ob = new Compra();
                    ob.Clvcom = indcompDb + 1;
                    ob.Fechac = obj.caducidad;
                    ob.Totalc = obj.total;
                    db.Compras.Add(ob);
                    db.SaveChanges();

                    Mercancium oMerca = db.Mercancia
                                .Where(p => p.Codbar.Equals(obj.codbar))
                                .First();

                    Compro ocp = new Compro();
                    ocp.Clvpro = obj.clvPro;
                    ocp.Clvcom = indcompDb + 1;
                    db.Compros.Add(ocp);

                    Commer ocm = new Commer();
                    ocm.Cantidadcm = obj.cantidad;
                    ocm.Clvcom = indcompDb + 1;
                    ocm.Clvmer = oMerca.Clvmer;
                    db.Commers.Add(ocm);

                    Empcom oec = new Empcom();
                    oec.Clvcom = indcompDb + 1;
                    oec.Id = obj.idEmp;
                    db.Empcoms.Add(oec);

                    oMerca.Existencia = oMerca.Existencia + obj.cantidad;
                    oMerca.Precomp = obj.total / obj.cantidad;
                    if (obj.pVenta != 0)
                        oMerca.Prevent = obj.pVenta;
                    db.SaveChanges();
                }
                return RedirectToAction("Exito");
            }catch
            {
                return View(obj);
            }
                
        }



    }
}
