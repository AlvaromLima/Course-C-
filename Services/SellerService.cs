using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {

        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Sincrona
        //public List<Seller> FindAll()
        //{
        //    return _context.Seller.ToList();
        // }
        //Assincrona
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //Sincrona
        //public void Insert(Seller obj)
        //{
        //    _context.Add(obj);
        //    _context.SaveChanges();
        //}
        // Assincrona
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //Sincrona
        //public Seller FindById(int id)
        //{
        //    return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        //}
        // Assincrona
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        //Sincrona
        //public void Remove(int id)
        //{
        //    try
        //    {
        //       var obj = _context.Seller.Find(id);
        //       _context.Seller.Remove(obj);
        //       _context.SaveChanges();
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        throw new IntegrityException("Can´t delete seller because he/she has sales");
        //     }
        //}
        // Assincrona
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Can´t delete seller because he/she has sales");
            }
        }

        //Sincrona
        //public void Update(Seller obj)
        //{
        //    if ( !_context.Seller.Any(x => x.Id == obj.Id))
        //    {
        //        throw new NotFoundException("Id not found");
        //    }
        //    try
        //    {
        //        _context.Update(obj);
        //        _context.SaveChanges();
        //    } catch (DbUpdateConcurrencyException e)
        //    {
        //        throw new DbConcurrencyException(e.Message);
        //    }
        //}
        //Assincrona
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if ( !hasAny )
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
