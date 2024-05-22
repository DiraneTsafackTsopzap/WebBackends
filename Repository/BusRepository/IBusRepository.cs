using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;

namespace webapp.Repository.BusRepository
{
    public interface IBusRepository
    {
        void Save(Bus neuFahrer);

        List<Bus> GetAll(int from = 0, int max = 1000);

        void Delete(Guid id);

        Bus ById(Guid id);
    }
}
