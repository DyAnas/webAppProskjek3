using Microsoft.EntityFrameworkCore;
using Prosjekt_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prosjekt_3
{
    public class SpørsmålDb
    {
        private readonly SpørsmålContext _context;

        public SpørsmålDb(SpørsmålContext context)
        {
            _context = context;
        }
        public List<spørsmåls> hentalleSpørsmål(int id)
        {
            List<spørsmåls> spørsmål = new List<spørsmåls>();
            var alleSpørmål = _context.spørsmåls.Where(k => k.type.TypeId == id).ToList();
            foreach (DBSpørsmål s in alleSpørmål)
            {
                spørsmål.Add(new spørsmåls
                {
                    Id = s.Id,
                    spørmål = s.spørmål,
                    rating = s.rating,
                    svar = s.svar,


                });
            }

            return spørsmål;
        }
        public List<DBType> hentalleType()
        {
            List<DBType> type = _context.TypeSpørsmåls.ToList();
            return type;

        }

        public DBType hentType(int id)
        {
            var type = _context.TypeSpørsmåls.Include(s => s.spørmål)
               .FirstOrDefault(k => k.TypeId == id);
            if (type != null)
            {
                return type;
            }
            return null;
        }
        public bool lagreSpørsmål(int TypeId, spørsmåls innspørsmål)
        {

            var nySpørsmål = new DBSpørsmål()
            {

                spørmål = innspørsmål.spørmål,
                stemmer = 0,
                rating = 0,
                svar = ""
            };
            DBType funntype = _context.TypeSpørsmåls.Find(TypeId);
            if (funntype == null)
            {
                return false;
            }

            nySpørsmål.type.TypeId = funntype.TypeId;
            try
            {
                _context.spørsmåls.Add(nySpørsmål);
                _context.SaveChanges();
            }
            catch (Exception feil)
            {
                return false;
            }
            return true;

        }
        public List<DBSpørsmål> HentEtSp(int id)
        {
            var sp = _context.spørsmåls.Where(k => k.Id == id).OrderByDescending(k => k.rating).ToList();


            return sp;

        }



        public bool TommelOppSp(int id)
        {
            var sp = _context.spørsmåls.FirstOrDefault(s => s.Id == id);
            if (sp == null)
            {
                return false;
            }
            sp.rating++;
            sp.stemmer++;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception feil)
            {
                return false;
            }
            return true;

        }

        public bool TommelNedsp(int id)
        {
            var sp = _context.spørsmåls.FirstOrDefault(s => s.Id == id);
            if (sp == null)
            {
                return false;
            }
            sp.rating--;
            sp.stemmer++;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception feil)
            {
                return false;
            }
            return true;
        }
        public List<DBSpørsmål> HentTop()
        {
            List<DBSpørsmål> sp = _context.spørsmåls.OrderByDescending(s => s.rating).Take(4).ToList();
            return sp;
        }


        public spørsmåls hentEtSvar(int id)
        {
            DBSpørsmål etSvar = _context.spørsmåls.Include(s => s.type).FirstOrDefault(k => k.Id == id);

            var enSpørsmål = new spørsmåls()
            {
                Id = etSvar.Id,
                svar = etSvar.svar
            };
            return enSpørsmål;
        }

    }
}
