using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesApp.Model;

namespace NotesApp.NotesAppContext
{
    public class NoteAppDbContext : DbContext
    {

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data source=notes.db");

    }
}
