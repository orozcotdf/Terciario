using ColegioTerciario.DAL.Models;
using ColegioTerciario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColegioTerciario.Controllers
{
    public class Select2PagedResult
    {
        public int Total { get; set; }
        public List<Select2Result> Results { get; set; }
    }
    public class Select2Result
    {
        public string id { get; set; }
        public string text { get; set; }
    } 
    public class DatosController : Controller
    {
        [HttpGet]
        public ActionResult GetPersonas(string searchTerm, int pageSize, int pageNum)
        {
            //Get the paged results and the total count of the results for this query. 
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            List<Persona> personas = (from e in db.Personas
                                    where (
                                    e.PERSONA_DOCUMENTO_NUMERO.ToLower().Contains(searchTerm.ToLower()) ||
                                    e.PERSONA_NOMBRE.ToLower().Contains(searchTerm.ToLower()) ||
                                    e.PERSONA_APELLIDO.ToLower().Contains(searchTerm.ToLower()))
                                    select e)
                .OrderBy(p => p.PERSONA_APELLIDO)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();

            int count = (from e in db.Personas
                         where (
                         e.PERSONA_DOCUMENTO_NUMERO.ToLower().Contains(searchTerm.ToLower()) ||
                         e.PERSONA_NOMBRE.ToLower().Contains(searchTerm.ToLower()) ||
                         e.PERSONA_APELLIDO.ToLower().Contains(searchTerm.ToLower()))
                         select e).Count();

            //Translate the attendees into a format the select2 dropdown expects
            Select2PagedResult pagedAttendees = PersonasToSelect2Format(personas, count);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpGet]
        public ActionResult GetPersona(int id)
        {
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            Persona persona = db.Personas.Find(id);
            return new JsonpResult
            {
                Data = new Select2Result { id = persona.ID.ToString(), text = persona.PERSONA_NOMBRE_COMPLETO },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpGet]
        public ActionResult GetPaises(string searchTerm, int pageSize, int pageNum)
        {
            //Get the paged results and the total count of the results for this query. 
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            List<Pais> paises = db.Paises.Where(
                p => p.PAIS_NAME.Contains(searchTerm))
                .OrderBy(p => p.PAIS_NAME)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();

            int count = db.Paises.Where(
                p => p.PAIS_NAME.Contains(searchTerm))
                .Count();

            //Translate the attendees into a format the select2 dropdown expects
            Select2PagedResult pagedAttendees = AttendeesToSelect2Format(paises, count);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult GetPais(int id)
        {
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            Pais pais = db.Paises.Find(id);
            return new JsonpResult
            {
                Data = new Select2Result { id = pais.ID.ToString(), text = pais.PAIS_NAME },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult GetProvincia(int id)
        {
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            Provincia provincia = db.Provincias.Find(id);
            return new JsonpResult
            {
                Data = new Select2Result { id = provincia.ID.ToString(), text = provincia.PROVINCIA_NAME },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult GetCiudad(int id)
        {
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            Ciudad ciudad = db.Ciudades.Find(id);
            return new JsonpResult
            {
                Data = new Select2Result { id = ciudad.ID.ToString(), text = ciudad.CIUDAD_NAME },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult GetBarrio(int id)
        {
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            Barrio barrio = db.Barrios.Find(id);
            return new JsonpResult
            {
                Data = new Select2Result { id = barrio.ID.ToString(), text = barrio.BARRIO_NOMBRE },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult GetProvincias(string searchTerm, int pageSize, int pageNum)
        {
            //Get the paged results and the total count of the results for this query. 
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            List<Provincia> provincias = db.Provincias.Where(
                p => p.PROVINCIA_NAME.Contains(searchTerm))
                .OrderBy(p => p.PROVINCIA_NAME)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();

            int count = db.Paises.Where(
                p => p.PAIS_NAME.Contains(searchTerm))
                .Count();

            //Translate the attendees into a format the select2 dropdown expects
            Select2PagedResult pagedAttendees = ProvinciasToSelect2Format(provincias, count);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult GetCiudades(string searchTerm, int pageSize, int pageNum)
        {
            //Get the paged results and the total count of the results for this query. 
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            List<Ciudad> ciudades = db.Ciudades.Where(
                p => p.CIUDAD_NAME.Contains(searchTerm))
                .OrderBy(p => p.CIUDAD_NAME)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();

            int count = db.Paises.Where(
                p => p.PAIS_NAME.Contains(searchTerm))
                .Count();

            //Translate the attendees into a format the select2 dropdown expects
            Select2PagedResult pagedAttendees = CiudadesToSelect2Format(ciudades, count);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult GetBarrios(string searchTerm, int pageSize, int pageNum)
        {
            //Get the paged results and the total count of the results for this query. 
            ColegioTerciarioContext db = new ColegioTerciarioContext();
            List<Barrio> barrios = db.Barrios.Where(
                p => p.BARRIO_NOMBRE.Contains(searchTerm))
                .OrderBy(p => p.BARRIO_NOMBRE)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();

            int count = db.Paises.Where(
                p => p.PAIS_NAME.Contains(searchTerm))
                .Count();

            //Translate the attendees into a format the select2 dropdown expects
            Select2PagedResult pagedAttendees = BarriosToSelect2Format(barrios, count);

            //Return the data as a jsonp result
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        private Select2PagedResult AttendeesToSelect2Format(List<Pais> attendees, int totalAttendees)
        {
            Select2PagedResult jsonAttendees = new Select2PagedResult();
            jsonAttendees.Results = new List<Select2Result>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            foreach (Pais a in attendees)
            {
                jsonAttendees.Results.Add(new Select2Result { id = a.ID.ToString(), text = a.PAIS_NAME });
            }
            //Set the total count of the results from the query.
            jsonAttendees.Total = totalAttendees;

            return jsonAttendees;
        }

        private Select2PagedResult PersonasToSelect2Format(List<Persona> personas, int totalAttendees)
        {
            Select2PagedResult jsonAttendees = new Select2PagedResult();
            jsonAttendees.Results = new List<Select2Result>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            foreach (Persona a in personas)
            {
                jsonAttendees.Results.Add(new Select2Result { id = a.ID.ToString(), text = a.PERSONA_NOMBRE_COMPLETO });
            }
            //Set the total count of the results from the query.
            jsonAttendees.Total = totalAttendees;

            return jsonAttendees;
        }

        private Select2PagedResult ProvinciasToSelect2Format(List<Provincia> attendees, int totalAttendees)
        {
            Select2PagedResult jsonAttendees = new Select2PagedResult();
            jsonAttendees.Results = new List<Select2Result>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            foreach (Provincia a in attendees)
            {
                jsonAttendees.Results.Add(new Select2Result { id = a.ID.ToString(), text = a.PROVINCIA_NAME });
            }
            //Set the total count of the results from the query.
            jsonAttendees.Total = totalAttendees;

            return jsonAttendees;
        }

        private Select2PagedResult CiudadesToSelect2Format(List<Ciudad> attendees, int totalAttendees)
        {
            Select2PagedResult jsonAttendees = new Select2PagedResult();
            jsonAttendees.Results = new List<Select2Result>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            foreach (Ciudad a in attendees)
            {
                jsonAttendees.Results.Add(new Select2Result { id = a.ID.ToString(), text = a.CIUDAD_NAME });
            }
            //Set the total count of the results from the query.
            jsonAttendees.Total = totalAttendees;

            return jsonAttendees;
        }

        private Select2PagedResult BarriosToSelect2Format(List<Barrio> attendees, int totalAttendees)
        {
            Select2PagedResult jsonAttendees = new Select2PagedResult();
            jsonAttendees.Results = new List<Select2Result>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            foreach (Barrio a in attendees)
            {
                jsonAttendees.Results.Add(new Select2Result { id = a.ID.ToString(), text = a.BARRIO_NOMBRE });
            }
            //Set the total count of the results from the query.
            jsonAttendees.Total = totalAttendees;

            return jsonAttendees;
        }
    }





    //I found this somewhere online...can't remember the source.  Returns a jsonp instead of a standard json result.
    public class JsonpResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;
            string jsoncallback = (context.RouteData.Values["callback"] as string) ?? request["callback"];
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                if (string.IsNullOrEmpty(base.ContentType))
                {
                    base.ContentType = "application/x-javascript";
                }
                response.Write(string.Format("{0}(", jsoncallback));
            }
            base.ExecuteResult(context);
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                response.Write(")");
            }
        }
    }
}