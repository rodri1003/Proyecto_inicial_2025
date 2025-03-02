using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proyecto_CRUD.DB;
using Proyecto_CRUD.Models;

namespace Proyecto_CRUD.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly AppDbContext _dbConn;
        public AlumnosController(AppDbContext appDb)
        {
            _dbConn = appDb;
        }
        public IActionResult Index()
        {
            List<Alumnos> alumnos = _dbConn.Alumnos.ToList();
            return View(alumnos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Alumnos alumno = new();
            return View(alumno);

        }

        [HttpPost]
        public IActionResult Create(Alumnos model)
        {
            ModelState.Remove("NombreCompleto");

            if (ModelState.IsValid)
            {
                model.NombreCompleto = $"{model.Nombres} {model.Apellidos}";

                _dbConn.Alumnos.Add(model);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
