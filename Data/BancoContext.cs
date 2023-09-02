﻿using Microsoft.EntityFrameworkCore;
using SiteMVC.Controllers;
using SiteMVC.Models;

namespace SiteMVC.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) 
        { 
        }

        public DbSet<ContatoModel> Contatos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}
