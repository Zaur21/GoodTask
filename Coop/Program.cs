using Coop.Controllers;
using Coop.Data;
using Coop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDBContecxt>(x => x.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
            ));

        var app = builder.Build();


         app.MapGet("/products", async (ApplicationDBContecxt db) => db.Products.ToList());
       
        app.MapGet("/products/{id}", async (int id, ApplicationDBContecxt db ) => 
         await db.Products.FirstOrDefaultAsync(p => p.Id == id) is Product product  
         ?Results.Ok(product)
         :Results.NotFound());

        app.MapPost("/products", async ([FromBody] Product product, ApplicationDBContecxt db) =>
        {
             db.Products.Add(product);
             await db.SaveChangesAsync();
            return Results.Created($"/products/{product.Id}", product);
        });

        app.MapPut("/products", async([FromBody] Product product, ApplicationDBContecxt db)=>
        {
            var productFromDB = await db.Products.FindAsync(new object[] { product.Id });
            if (productFromDB == null) return Results.NotFound();
            productFromDB.Name = product.Name;
            productFromDB.EANCode = product.EANCode;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("products/{id}", async (int id, ApplicationDBContecxt db) =>
        {
            var productFromDB = await db.Products.FindAsync(new object[] { id});
            if (productFromDB == null) return Results.NotFound();
            db.Products.Remove(productFromDB);
            await db.SaveChangesAsync(true);
            return Results.NoContent();
        });


         

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

   
    }

    

}


