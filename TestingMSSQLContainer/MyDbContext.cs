using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;

namespace TestingMSSQLContainer
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
