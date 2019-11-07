using Microsoft.AspNetCore.Mvc;
using Prosjekt_3.Models;
using System.Collections.Generic;

namespace Prosjekt_3.Controllers
{
   
    [Route("api/[controller]")]
    public class SpørsmålController : Controller

    {
        private readonly SpørsmålContext _context;

        public SpørsmålController(SpørsmålContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var db = new SpørsmålDb(_context);
            List<DBType> alletyper = db.hentalleType();

            return Json(alletyper);

        }
        [HttpGet("{id}")]
        public JsonResult GetAllespørsmål(int id)
        {
            var db = new SpørsmålDb(_context);
            var ut = db.hentalleSpørsmål(id);

            return Json(ut);
        }

        [HttpGet("{Id}")]
        public JsonResult GetSvar(int id)
        {
            var db = new SpørsmålDb(_context);
            var ut = db.hentEtSvar(id);
            return Json(ut);
        }
     

        [HttpGet("[action]/{id}")]
        public JsonResult Tommelopp(int id)
        {
            var db = new SpørsmålDb(_context);
            bool result = db.TommelOppSp(id);

            return Json(result);
        }
        [HttpGet("[action]/{id}")]
        public JsonResult Tommelned(int id)
        {
            var db = new SpørsmålDb(_context);
            bool result = db.TommelNedsp(id);

            return Json(result);
        }

        [HttpPost("{id}")]
        public JsonResult Post(int id, [FromBody]spørsmåls innSp)
        {

            if (ModelState.IsValid)
            {
                var db = new SpørsmålDb(_context);
                bool Ok = db.lagreSpørsmål(id, innSp);
                if (Ok)
                {
                    return Json("OK");

                }

            }
            return Json("Kunne ike lagre spørsmål");
        }



    }
}
