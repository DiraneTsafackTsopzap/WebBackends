using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Repository.FahrerRepository
{
    public interface IFahrerRepository
    {
        void Save(FahrerViewModel neuFahrer);
        Fahrer Update( Guid id, FahrerViewModel existingFahrer);

        List<Fahrer> GetAll(int from = 0, int max = 1000);

        Fahrer ById(Guid id);

        void Delete(Guid id);
    }
}
