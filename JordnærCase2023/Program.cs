using JordnærCase2023.Interfaces;
using JordnærCase2023.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IShiftService, ShiftService>();
builder.Services.AddTransient<IShiftTypeService, ShiftTypeService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IMemberService, MemberService>();
builder.Services.AddTransient<IUserLoginService, UserLoginService>();
builder.Services.AddTransient<IShiftTypeMemberService, ShiftTypeMemberService>();

// For user login
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
