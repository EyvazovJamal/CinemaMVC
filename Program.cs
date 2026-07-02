using Cinema.Api;
using Cinema.Common;
using Cinema.Services.Hall;
using Cinema.Services.Movie;
using Cinema.Services.Schedule;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var movieApiBaseUrl = builder.Configuration["MovieApiSettings:BaseUrl"]
                      ?? "http://localhost:5281";

builder.Services.Configure<CinemaSettings>(builder.Configuration.GetSection(CinemaSettings.SectionName));
builder.Services.AddSingleton<ICinemaTime, CinemaTimeService>();

void ConfigureMovieApiClient(IHttpClientBuilder client) =>
    client.ConfigureHttpClient(c => c.BaseAddress = new Uri(movieApiBaseUrl));

ConfigureMovieApiClient(builder.Services.AddRefitClient<IMovieApi>());
ConfigureMovieApiClient(builder.Services.AddRefitClient<IHallApi>());
ConfigureMovieApiClient(builder.Services.AddRefitClient<IScheduleApi>());

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IHallService, HallService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );



app.Run();
