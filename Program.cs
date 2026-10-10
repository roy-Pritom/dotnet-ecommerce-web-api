using System.ComponentModel.DataAnnotations;
using Ecommerce_Web_Api.Common.Responses;
using Ecommerce_Web_Api.Services;
using Ecommerce_Web_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg => { }, typeof(Program));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddControllers();


// refactor api behavior for validation errors
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        // var errors = context.ModelState
        //     .Where(e => e.Value != null && e.Value.Errors.Count > 0)
        //     .Select(e => new
        //     {
        //         Field = e.Key,
        //         Errors = e.Value != null ? e.Value.Errors.Select(er => er.ErrorMessage).ToArray() : new string[0]
        //     }).ToArray();

        var errors = context.ModelState
        .Where(e => e.Value != null && e.Value.Errors.Count > 0)
        .SelectMany(e => e.Value?.Errors != null ? e.Value.Errors.Select(er => er.ErrorMessage) : new List<string>()).ToList();

        return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors, StatusCodes.Status400BadRequest, "Validation errors occurred."));
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

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






app.Run();


