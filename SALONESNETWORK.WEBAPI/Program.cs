using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.BLL.Services;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.DAL.Interfaces;
using SALONESNETWORK.DAL.Repositories;
using SALONESNETWORK.MODELS.Entities;
using System.Diagnostics.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SalonesDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("conexion"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("conexion"))
)
);

builder.Services.AddScoped<IAsuntoPaisSeccionSubRepository<AsuntoPaisSeccionSub>, AsuntoPaisSeccionSubRepository>();
builder.Services.AddScoped<IAsuntoPaisSeccionSubService, AsuntoPaisSeccionSubService>();

builder.Services.AddScoped<IAsuntoRepository<Asunto>, AsuntoRepository>();
builder.Services.AddScoped<IAsuntoService, AsuntoService>();

builder.Services.AddScoped<IDocumentoMensajeRepository<DocumentoMensaje>, DocumentoMensajeRepository>();
builder.Services.AddScoped<IDocumentoMensajeService, DocumentoMensajeService>();

builder.Services.AddScoped<IDocumentoRepository<Documento>, DocumentoRepository>();
builder.Services.AddScoped<IDocumentoService, DocumentoService>();

builder.Services.AddScoped<IMensajeRepository<Mensaje>, MensajeRepository>();
builder.Services.AddScoped<IMensajeService, MensajeService>();

builder.Services.AddScoped<IPaisRepository<Pais>, PaisRepository>();
builder.Services.AddScoped<IPaisService, PaisService>();

builder.Services.AddScoped<IPerfilRepository<Perfil>, PerfilRepository>();
builder.Services.AddScoped<IPerfilService, PerfilService>();

builder.Services.AddScoped<IPerfilSeccionRepository<PerfilSeccion>, PerfilSeccionRepository>();
builder.Services.AddScoped<IPerfilSeccionService, PerfilSeccionService>();

builder.Services.AddScoped<IRegistroVisitaRepository<RegistroVisita>, RegistroVisitaRepository>();
builder.Services.AddScoped<IRegistroVisitaService, RegistroVisitaService>();

builder.Services.AddScoped<ISeccionRepository<Seccion>, SeccionRepository>();
builder.Services.AddScoped<ISeccionService, SeccionService>();

builder.Services.AddScoped<ISubSeccionRepository<SubSeccion>, SubSeccionRepository>();
builder.Services.AddScoped<ISubSeccionService, SubSeccionService>();

builder.Services.AddScoped<ITipoMensajeRepository<TipoMensaje>, TipoMensajeRepository>();
builder.Services.AddScoped<ITipoMensajeService, TipoMensajeService>();

builder.Services.AddScoped<IUsuarioPerfilRepository<UsuarioPerfil>, UsuarioPerfilRepository>();
builder.Services.AddScoped<IUsuarioPerfilService, UsuarioPerfilService>();

builder.Services.AddScoped<IUbicacionMensajeRepository<UbicacionMensaje>, UbicacionMensajeRepository>();
builder.Services.AddScoped<IUbicacionMensajeService, UbicacionMensajeService>();

builder.Services.AddScoped<IUsuarioRepository<Usuario>, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IUsuarioSeccionRepository<UsuarioSeccion>, UsuarioSeccionRepository>();
builder.Services.AddScoped<IUsuarioSeccionService, UsuarioSeccionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
