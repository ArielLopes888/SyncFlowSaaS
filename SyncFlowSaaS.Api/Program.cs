using Scheduling.Infrastructure;
using Shared.Infrastructure;
using Shared.Infrastructure.Tenancy;
using Scheduling.Application.Services;
using Scheduling.Infrastructure.Handlers;
using Scheduling.Infrastructure.Services;
using Shared.Infrastructure.Events;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddSchedulingInfrastructure(builder.Configuration);

// Application services
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<ILoyaltyService, LoyaltyService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Domain event handlers
builder.Services.AddScoped<IHandle<AppointmentCreated>, UpdateAnalyticsHandler>();
builder.Services.AddScoped<IHandle<AppointmentCreated>, LoyaltyHandler>();

// Web
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Multi-tenant middleware
app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
