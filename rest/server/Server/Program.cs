using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Mapper.CommentDTOs.Requests;
using Server.Mapper.CommentDTOs.Responses;
using Server.Mapper.ContributionDTOs.Requests;
using Server.Mapper.ContributionDTOs.Responses;
using Server.Mapper.ProjectAndUserBindingDTOs.Requests;
using Server.Mapper.ProjectAndUserBindingDTOs.Responses;
using Server.Mapper.ProjectDTOs.Requests;
using Server.Mapper.ProjectDTOs.Responses;
using Server.Mapper.UserDTOs.Requests;
using Server.Mapper.UserDTOs.Responses;
using Server.Mapper.VoteDTOs.Requests;
using Server.Mapper.VoteDTOs.Responses;
using Server.Repositories;
using Server.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("angularApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4201");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });
});

// Setup Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),
        };
    });

// Setup Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireClaim("Role", "Admin"));
});

// Services
builder.Services.AddScoped<ITokenService, TokenService>();

// Repository-Services
builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IProjectAndUserBindingRepository, ProjectAndUserBindingRepository>();
builder.Services.AddSingleton<IContributionRepository, ContributionRepository>();
builder.Services.AddSingleton<IVoteRepository, VoteRepository>();
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();

// Mapper-Services
builder.Services.AddSingleton<ICreateProjectDtoToProjectMapper, CreateProjectDtoToProjectMapper>();
builder.Services.AddSingleton<IProjectToGetProjectDtoMapper, ProjectToGetProjectDtoMapper>();
builder.Services.AddSingleton<IUpdateProjectDtoToProjectMapper, UpdateProjectDtoToProjectMapper>();
builder.Services.AddSingleton<ICreateUserDtoToUserMapper, CreateUserDtoToUserMapper>();
builder.Services.AddSingleton<IUserToGetUserDtoMapper, UserToGetUserDtoMapper>();
builder.Services.AddSingleton<IUpdateUserDtoToUserMapper, UpdateUserDtoToUserMapper>();
builder.Services.AddSingleton<IUserToGetUserWithPasswordDtoMapper, UserToGetUserWithPasswordDtoMapper>();
builder.Services.AddSingleton<IPatchUserDtoToUserMapper, PatchUserDtoToUserMapper>();
builder.Services.AddSingleton<ICreateProjectAndUserBindingDtoToProjectAndUserBindingMapper, CreateProjectAndUserBindingDtoToProjectAndUserBindingMapper>();
builder.Services.AddSingleton<IProjectAndUserBindingToGetProjectAndUserBindingDtoMapper, ProjectAndUserBindingToGetProjectAndUserBindingDtoMapper>();
builder.Services.AddSingleton<IUpdateProjectAndUserBindingDtoToProjectAndUserBindingMapper, UpdateProjectAndUserBindingDtoToProjectAndUserBindingMapper>();
builder.Services.AddSingleton<ICreateContributionDtoToContributionMapper, CreateContributionDtoToContributionMapper>();
builder.Services.AddSingleton<IUpdateContributionDtoToContributionMapper, UpdateContributionDtoToContributionMapper>();
builder.Services.AddSingleton<IPatchContributionDtoToContributionMapper, PatchContributionDtoToContributionMapper>();
builder.Services.AddSingleton<IContributionToGetContributionDtoMapper, ContributionToGetContributionDtoMapper>();
builder.Services.AddSingleton<ICreateVoteDtoToVoteMapper, CreateVoteDtoToVoteMapper>();
builder.Services.AddSingleton<IUpdateVoteDtoToVoteMapper, UpdateVoteDtoToVoteMapper>();
builder.Services.AddSingleton<IVoteToGetVoteDtoMapper, VoteToGetVoteDtoMapper>();
builder.Services.AddSingleton<ICreateCommentDtoToCommentMapper, CreateCommentDtoToCommentMapper>();
builder.Services.AddSingleton<IUpdateCommentDtoToCommentMapper, UpdateCommentDtoToCommentMapper>();
builder.Services.AddSingleton<ICommentToGetCommentDtoMapper, CommentToGetCommentDtoMapper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.UseCors("angularApp");

app.Run();