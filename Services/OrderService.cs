using LibreriaAdmin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Services
{
    public class OrderService
    {
        private readonly LibreriaRepository _dbRepository;

        public OrderService()
        {
            _dbRepository = new LibreriaRepository();
        }
    }
}
