﻿using Microsoft.EntityFrameworkCore;
using AdminAPI.Models;

namespace AdminAPI.Data;

public class McbaContext : DbContext
{
    public McbaContext(DbContextOptions<McbaContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<BillPay> BillPay { get; set; }
    public DbSet<Payee> Payee { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
