﻿using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {

        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Sincrona
        //public List<SalesRecord> FindByDate(DateTime? minDate, DateTime? maxDate)
        //{
        //    var result = from obj in _context.SalesRecord select obj;
        //    if ( minDate.HasValue )
        //    {
        //        result = result.Where(x => x.Date >= minDate.Value);
        //    }
        //    if (maxDate.HasValue)
        //    {
        //        result = result.Where(x => x.Date >= maxDate.Value);
        //    }
        //    return result
        //        .Include(x => x.Seller)
        //        .Include(x => x.Seller.Department)
        //        .OrderByDescending(x=> x.Date)
        //        .ToList();
        //}
        //Assincrona
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date >= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        //Sincrona
        //public List<IGrouping<Department,SalesRecord>> FindByDateGrouping(DateTime? minDate, DateTime? maxDate)
        //{
        //    var result = from obj in _context.SalesRecord select obj;
        //    if ( minDate.HasValue )
        //    {
        //        result = result.Where(x => x.Date >= minDate.Value);
        //    }
        //    if (maxDate.HasValue)
        //    {
        //        result = result.Where(x => x.Date >= maxDate.Value);
        //    }
        //    return result
        //        .Include(x => x.Seller)
        //        .Include(x => x.Seller.Department)
        //        .OrderByDescending(x=> x.Date)
        //        .GroupBy(x => x.Seller.Department)
        //        .ToList();
        //}
        //Assincrona
        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date >= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }

    }
}
