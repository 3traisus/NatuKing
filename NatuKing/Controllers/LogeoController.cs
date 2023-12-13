using Microsoft.AspNetCore.Mvc;
using NatuKing.Models;
using NatuKing.Clases;


namespace NatuKing.Controllers
{
    public class LogeoController : Controller
    {
        public IActionResult LoginIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginIndex(User obj)
        {
            try
            {
                User user = new User();
                List<User> list = new List<User>();
                using (NATURISTADEFINITIVAContext db = new NATURISTADEFINITIVAContext())
                {
                    list = (from us in db.Empleados
                            where us.Correoe.Equals(obj.Email) && us.Ppass.Equals(obj.Password) && us.Habilitado==1
                            select new User
                            {
                                Name = us.Nombree,
                                Puesto = us.Puesto
                            }).ToList();
                }
                if (list[0].Name != null)
                {
                    user.Name = list[0].Name;
                    user.Puesto = list[0].Puesto;

                    if (list[0].Puesto.Equals("CAJERO"))
                        return RedirectToAction("CajIndex", "Cajero", user);
                    else if (list[0].Puesto.Equals("MOSTRADOR"))
                        return RedirectToAction("MostIndexPrincipal", "Mostrador", user);
                    else if (list[0].Puesto.Equals("ADMINISTRADOR"))
                        return RedirectToAction("AdminIndex", "Admins", user);
                    else
                        return View(obj);
                }
                else
                    return View(obj);
            }
            catch (Exception e)
            {
                return View(obj);
            }
        }
    }
}
