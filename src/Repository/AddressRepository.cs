using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public class AddressRepository
    {
        //create field to access Address table:
        protected DbSet<Address> _address;

        //create field to access the database:

        protected DatabaseContext _databaseContext;

        // DI the DatabaseContext (Database) in the constructer by passing it as parametr value
        public AddressRepository(DatabaseContext databaseContext)
        {
            // Assign the value of the data base in the feild _databaseContext:
            _databaseContext = databaseContext;

            // initialize the Address table in database:
            _address = databaseContext.Set<Address>();
        }

        // methods

        
        // create a new Address:

        public async Task<Address> CreateOneAsync(Address newAddress)
        {
            await _address.AddAsync(newAddress);
            await _databaseContext.SaveChangesAsync();

            return newAddress;
        }

        // get by Address by ID

        public async Task<Address?> GetAddressByIdAsync(Guid id)
        {
            return await _address.FindAsync(id);
        }

        //delete Address:

        public async Task<bool> DeleteOneAsync(Address address)
        {
            _address.Remove(address);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        //Update Address:

        public async Task<bool> UpdateOneAsync(Address updatedAddress)
        {
            _address.Update(updatedAddress);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
