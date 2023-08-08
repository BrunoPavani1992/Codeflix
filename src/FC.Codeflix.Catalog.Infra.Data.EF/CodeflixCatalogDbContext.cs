﻿namespace FC.Codeflix.Catalog.Infra.Data.EF;

public class CodeflixCatalogDbContext : DbContext
{
    public DbSet<Category> Categories => Set<Category>();

    public CodeflixCatalogDbContext(
        DbContextOptions<CodeflixCatalogDbContext> options
    ) : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder builder
    )
    {
        builder.ApplyConfiguration(new CategoryConfiguration());
    }
}