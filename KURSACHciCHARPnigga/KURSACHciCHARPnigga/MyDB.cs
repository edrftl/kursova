using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace KURSACHciCHARPnigga
{
    class MyDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LONMETH\SQLEXPRESS;Integrated Security=True;
                                            Connect Timeout=30;Initial Catalog=<LabirintRoomSystem>;Encrypt=False;TrustServerCertificate=False;
                                            ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}


