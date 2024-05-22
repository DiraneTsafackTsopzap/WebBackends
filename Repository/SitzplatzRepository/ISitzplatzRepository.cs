using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;

namespace webapp.Repository.SitzplatzRepository
{
    public interface ISitzplatzRepository
    {


        List<Sitzplatz> GetAll(int from = 0, int max = 1000);

        Sitzplatz GetById(Guid id);

        void Add(Sitzplatz siege);

        void Delete(Guid id);
    }
}
