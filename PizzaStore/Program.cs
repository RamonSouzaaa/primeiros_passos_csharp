using Microsoft.OpenApi.Models;
using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Pizza Store API",
        Description = "Making the pizzas you love",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/", () => "Hello World!");
app.MapGet("/products", () => PizzaDB.GetPizzas());
app.MapGet("/products/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapPost("/products", (PizzaR pizza) => PizzaDB.Add(pizza));
app.MapPut("/products", (PizzaR pizza) => PizzaDB.Update(pizza));
app.MapDelete("products/{id}", (int id) => PizzaDB.Delete(id));
app.Run();
