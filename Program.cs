var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Api is working fine";
});



// testing purpose
// app.MapGet("/hello", () =>
// {
//     var res = new { Success = true, Message = "Hello World!", Status = 200 };
//     return Results.Ok(res); //200
// });

// app.MapGet("/get", () =>
// {
//     //  এখানে Results.Content()-এর দুটি গুরুত্বপূর্ণ argument আছে:
//     // "<h1>GET request received!</h1>" — এটি হলো response-এর content।
//     // "text/html" — এটি হলো Content-Type, যা বলে দেয় content-টি HTML format-এ আছে।
//     // যখন তুমি ব্রাউজারে /get URL-এ request পাঠাবে, তখন ব্রাউজার HTML-টি render করে দেখাবে।
//     return Results.Content("<h1>GET request received!</h1>", "text/html"); //200
// });

// app.MapPost("/post", () =>
// {
//     var res = new { Success = true, Message = "POST request received!", Status = 200 };
//     return Results.Created("/post", res); //201
// });


// app.MapPut("/put", () =>
// {
//     return Results.NoContent(); //204
// });
// app.MapDelete("/delete", () =>
// {
//     return Results.NoContent(); //204
// });// testing purpose

List<Category> categories = new List<Category>();


app.MapGet("/api/categories", () =>
{
    return Results.Ok(categories);
});


app.MapPost("/api/categories", () =>
{
    var newCategory = new Category
    {
        Name = "Electronics",
        Description = "This is a category description",
        ImageUrl = "https://example.com/category-image.jpg"
    };
    categories.Add(newCategory);

    return Results.Created($"/api/categories/{newCategory.Id}", newCategory);
});





app.Run();


public record Category
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public String? Name { get; set; }

    public String? Description { get; set; } = "This is a category description";

    public String? ImageUrl { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

