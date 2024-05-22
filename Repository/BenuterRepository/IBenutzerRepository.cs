using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;

namespace webapp.Repository.BenuterRepository
{
    public interface IBenutzerRepository
    {
        void Save(Benutzer benutzer);
        void Update(Benutzer benutzer);

        List<Benutzer> GetAll(int from = 0, int max = 1000);

        Benutzer ById(Guid id);

        void Delete(Guid id);

        Benutzer FindByEmailAndPassword(string email, string password);
    }
}
