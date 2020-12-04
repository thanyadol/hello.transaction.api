using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//model
using hello.transaction.core.Models;
//EF
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace hello.transaction.core.Repositories
{
    /*
     * 
     *
     */

    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> ListAsync();


        Task<Attachment> GetAsync(string id);
        Task<Attachment> CreateAsync(Attachment attachment);
        Task<Attachment> UpdateAsync(Attachment attachment);

        //Task<Attachment> DeleteAsync(string id);
    }

    public class AttachmentRepository : IAttachmentRepository
    {

        private readonly NorthwindContext _context;
        public AttachmentRepository(NorthwindContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        }

        public async Task<Attachment> CreateAsync(Attachment attachment)
        {

            var entity = await _context.Attachments.AddAsync(attachment);
            _context.SaveChanges();

            return entity.Entity;
        }

        public async Task<Attachment> DeleteAsync(string id)
        {
            var entity = await _context.Attachments.FindAsync(id);
            _context.Attachments.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<IEnumerable<Attachment>> ListAsync()
        {

            var entities = await _context.Attachments.ToListAsync();
            return entities;
        }

        public async Task<Attachment> UpdateAsync(Attachment attachment)
        {

            var entity = await _context.Attachments.FindAsync(attachment.Id);
            _context.Attachments.Update(attachment);

            _context.SaveChanges();
            return entity;
        }

        public async Task<Attachment> GetAsync(string id)
        {
            var entity = await _context.Attachments.FindAsync(id);
            return entity;
        }


    }
}