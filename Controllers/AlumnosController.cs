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
        public IActionResult UpSert(int id)
        {
            if (id == 0) 
            { 
                // Registro nuevo
                Alumnos alumno = new();
                return View(alumno);
            }
            else
            {
                // Registro existente
                Alumnos alumno = _dbConn.Alumnos.FirstOrDefault(row => row.AlumnoId == id) ?? new();
                return View(alumno);
            }

        }

        [HttpPost]
        public IActionResult UpSert(Alumnos model)
        {
            ModelState.Remove("NombreCompleto"); // Remover validación si es calculado

            // Asegurar que el campo NombreCompleto tenga un valor antes de guardar
            model.NombreCompleto = $"{model.Nombres} {model.Apellidos}".Trim();

            if (model.AlumnoId == 0) // Insertar nuevo registro
            {
                if (ModelState.IsValid)
                {
                    _dbConn.Alumnos.Add(model);
                    _dbConn.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else // Actualizar registro existente
            {
                if (ModelState.IsValid)
                {
                    model.NombreCompleto = $"{model.Nombres} {model.Apellidos}".Trim(); // Evitar NULL
                    _dbConn.Alumnos.Update(model);
                    _dbConn.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model); // Si algo falla, regresar la vista con el modelo
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var alumno = _dbConn.Alumnos.FirstOrDefault(a => a.AlumnoId == id);
            if (alumno != null)
            {
                alumno.IsActive = false;
                _dbConn.Alumnos.Update(alumno);
                _dbConn.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
