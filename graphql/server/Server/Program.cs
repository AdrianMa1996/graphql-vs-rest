using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Server.Mapper.CommentTypes.InputTypes;
using Server.Mapper.CommentTypes.OutputTypes;
using Server.Mapper.ContributionTypes.InputTypes;
using Server.Mapper.ContributionTypes.OutputTypes;
using Server.Mapper.ProjectAndUserBindingTypes.InputTypes;
using Server.Mapper.ProjectAndUserBindingTypes.OutputTypes;
using Server.Mapper.ProjectTypes.InputTypes;
using Server.Mapper.ProjectTypes.OutputTypes;
using Server.Mapper.UserTypes.InputTypes;
using Server.Mapper.UserTypes.OutputTypes;
using Server.Mapper.VoteTypes.InputTypes;
using Server.Mapper.VoteTypes.OutputTypes;
using Server.Repositories;
using Server.Schema;
using Server.Schema.Mutations;
using Server.Schema.Queries;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer().AddQueryType<Query>()
    .AddTypeExtension<UserQuery>()
    .AddTypeExtension<ProjectQuery>()
    .AddTypeExtension<ContributionQuery>();

builder.Services.AddGraphQLServer().AddMutationType<Mutation>()
    .AddTypeExtension<AccountMutation>()
    .AddTypeExtension<CommentMutation>()
    .AddTypeExtension<ContributionMutation>()
    .AddTypeExtension<ProjectAndUserBindingMutation>()
    .AddTypeExtension<ProjectMutation>()
    .AddTypeExtension<UserMutation>()
    .AddTypeExtension<VoteMutation>();

builder.Services.AddGraphQLServer()
    .AddMaxExecutionDepthRule(10)
    .AddAuthorization()
    .AddFiltering()
    .AddSorting();

builder.Services.AddCors(options =>
{
    options.AddPolicy("angularApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200");
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
builder.Services.AddSingleton<ICreateProjectTypeToProjectMapper, CreateProjectTypeToProjectMapper>();
builder.Services.AddSingleton<IProjectToGetProjectTypeMapper, ProjectToGetProjectTypeMapper>();
builder.Services.AddSingleton<IUpdateProjectTypeToProjectMapper, UpdateProjectTypeToProjectMapper>();
builder.Services.AddSingleton<ICreateUserTypeToUserMapper, CreateUserTypeToUserMapper>();
builder.Services.AddSingleton<IUserToGetUserTypeMapper, UserToGetUserTypeMapper>();
builder.Services.AddSingleton<IUpdateUserTypeToUserMapper, UpdateUserTypeToUserMapper>();
builder.Services.AddSingleton<ICreateProjectAndUserBindingTypeToProjectAndUserBindingMapper, CreateProjectAndUserBindingTypeToProjectAndUserBindingMapper>();
builder.Services.AddSingleton<IProjectAndUserBindingToGetProjectAndUserBindingTypeMapper, ProjectAndUserBindingToGetProjectAndUserBindingTypeMapper>();
builder.Services.AddSingleton<IUpdateProjectAndUserBindingTypeToProjectAndUserBindingMapper, UpdateProjectAndUserBindingTypeToProjectAndUserBindingMapper>();
builder.Services.AddSingleton<ICreateContributionTypeToContributionMapper, CreateContributionTypeToContributionMapper>();
builder.Services.AddSingleton<IUpdateContributionTypeToContributionMapper, UpdateContributionTypeToContributionMapper>();
builder.Services.AddSingleton<IContributionToGetContributionTypeMapper, ContributionToGetContributionTypeMapper>();
builder.Services.AddSingleton<ICreateVoteTypeToVoteMapper, CreateVoteTypeToVoteMapper>();
builder.Services.AddSingleton<IUpdateVoteTypeToVoteMapper, UpdateVoteTypeToVoteMapper>();
builder.Services.AddSingleton<IVoteToGetVoteTypeMapper, VoteToGetVoteTypeMapper>();
builder.Services.AddSingleton<ICreateCommentTypeToCommentMapper, CreateCommentTypeToCommentMapper>();
builder.Services.AddSingleton<IUpdateCommentTypeToCommentMapper, UpdateCommentTypeToCommentMapper>();
builder.Services.AddSingleton<ICommentToGetCommentTypeMapper, CommentToGetCommentTypeMapper>();

var app = builder.Build();

app.UseCors("angularApp");

app.UseAuthentication();

app.MapGraphQL();

app.Run();
