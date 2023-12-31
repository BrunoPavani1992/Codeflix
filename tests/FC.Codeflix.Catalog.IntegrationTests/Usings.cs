global using Xunit;
global using Bogus;
global using FC.Codeflix.Catalog.IntegrationTests.Common;
global using FC.Codeflix.Catalog.Domain.Entity;
global using FC.Codeflix.Catalog.Infra.Data.EF;
global using Microsoft.EntityFrameworkCore;
global using FluentAssertions;
global using FC.Codeflix.Catalog.Application.Exception;
global using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
global using FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.Common;
global using FC.Codeflix.Catalog.Infra.Data.EF.Repositories;
global using FC.Codeflix.Catalog.Infra.Data.EF.UnitOfWork;
global using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
global using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
global using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
global using FC.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;
global using FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
global using FC.Codeflix.Catalog.Application.UseCases.Category.Common;