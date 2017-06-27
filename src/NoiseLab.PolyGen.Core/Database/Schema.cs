using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NoiseLab.PolyGen.Core.Database
{
    public sealed class Schema
    {
        public CompilationUnitSyntax GenerateCode()
        {
            var classes = new List<MemberDeclarationSyntax>();
            classes.AddRange(Tables.Select(t => t.GenerateOrmModel()));
            classes.AddRange(Tables.Select(t => t.GeneratePostValidationModel()));
            classes.AddRange(Tables.Select(t => t.GeneratePutValidationModel()));
            classes.Add(GenerateDbContext());
            classes.Add(GenerateStartup());
            classes.Add(GenerateProgram());
            classes.AddRange(Tables.Select(table => table.GenerateWebApiController()));

            MemberDeclarationSyntax @namespace = SyntaxFactory
                .NamespaceDeclaration(SyntaxFactory.ParseName("NoiseLab.PolyGen.Generated"))
                .AddUsings(
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.IO")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.ComponentModel.DataAnnotations")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.ComponentModel.DataAnnotations.Schema")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.EntityFrameworkCore")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.EntityFrameworkCore.Metadata")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Mvc")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Builder")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Hosting")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.Extensions.Configuration")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.Extensions.DependencyInjection"))
                )
                .AddMembers(classes.ToArray());

            return SyntaxFactory.CompilationUnit().AddMembers(@namespace);
        }

        internal Schema(IReadOnlyList<Table> tables, IEnumerable<Relationship> relationships)
        {
            Tables = tables;
            Relationships = relationships;
        }

        internal IReadOnlyList<Table> Tables { get; }

        internal IEnumerable<Relationship> Relationships { get; }

        private MemberDeclarationSyntax GenerateDbContext()
        {
            var members = new List<MemberDeclarationSyntax>();

            var properties = Tables
                .Select(table => table.GenerateDbSetProperty())
                .Cast<MemberDeclarationSyntax>()
                .ToList();
            members.AddRange(properties);

            var constructor = SyntaxFactory
                .ConstructorDeclaration("DatabaseContext")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("options"))
                        .WithType(SyntaxFactory.ParseTypeName("DbContextOptions")))
                .WithInitializer(SyntaxFactory.ConstructorInitializer(SyntaxKind.BaseConstructorInitializer,
                    SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(new[]
                    {
                        SyntaxFactory.Argument(SyntaxFactory.ParseExpression("options"))
                    }))))
                .AddBodyStatements();
            members.Add(constructor);

            var onModelCreatingMethodBody = new List<StatementSyntax>();
            foreach (var table in Tables)
            {
                onModelCreatingMethodBody.Add(table.GeneratePrimaryKeyDefinition());
                onModelCreatingMethodBody.AddRange(table.GenerateRelationshipDefinitions());
            }
            var method = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)), "OnModelCreating")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword))
                .AddParameterListParameters(
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("modelBuilder"))
                        .WithType(SyntaxFactory.ParseTypeName("ModelBuilder")))
                .AddBodyStatements(SyntaxFactory.Block(onModelCreatingMethodBody));
            members.Add(method);

            var @class = SyntaxFactory
                .ClassDeclaration("DatabaseContext")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName("DbContext")))
                .AddMembers(members.ToArray());

            return @class;
        }

        private static MemberDeclarationSyntax GenerateStartup()
        {
            var members = new List<MemberDeclarationSyntax>();

            var constructor = SyntaxFactory
                .ConstructorDeclaration("Startup")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("env"))
                        .WithType(SyntaxFactory.ParseTypeName("IHostingEnvironment")))
                .AddBodyStatements(
                    SyntaxFactory.ParseStatement("var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile(\"appsettings.json\", false, true).AddJsonFile($\"appsettings.{ env.EnvironmentName}.json\", true).AddEnvironmentVariables();"),
                    SyntaxFactory.ParseStatement("_configuration = builder.Build();"));
            members.Add(constructor);

            var configurationField = SyntaxFactory.FieldDeclaration(
                    SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.ParseTypeName("IConfigurationRoot"),
                        SyntaxFactory.SeparatedList(
                            new[] {SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier("_configuration"))})))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));
            members.Add(configurationField);

            var configureServicesMethod = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)), "ConfigureServices")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("services"))
                        .WithType(SyntaxFactory.ParseTypeName("IServiceCollection")))
                .AddBodyStatements(
                    SyntaxFactory.ParseStatement("services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(_configuration.GetValue<string>(\"ConnectionString\")));"),
                    SyntaxFactory.ParseStatement("services.AddMvc(options => { options.Filters.Add(typeof(ModelStateValidationAttribute)); options.Filters.Add(typeof(NullValidationAttribute)); });"));
            members.Add(configureServicesMethod);

            var configureMethod = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)), "Configure")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("context"))
                        .WithType(SyntaxFactory.ParseTypeName("DatabaseContext")),
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("app"))
                        .WithType(SyntaxFactory.ParseTypeName("IApplicationBuilder")))
                .AddBodyStatements(
                    SyntaxFactory.ParseStatement("context.Database.EnsureCreated();"),
                    SyntaxFactory.ParseStatement("app.UseMvc();"));
            members.Add(configureMethod);

            var @class = SyntaxFactory
                .ClassDeclaration("Startup")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(members.ToArray());

            return @class;
        }

        private static MemberDeclarationSyntax GenerateProgram()
        {
            var members = new List<MemberDeclarationSyntax>();

            var mainMethod = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)), "Main")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword))
                .AddBodyStatements(
                    SyntaxFactory.ParseStatement("var host = new WebHostBuilder().UseKestrel().UseContentRoot(Directory.GetCurrentDirectory()).UseIISIntegration().UseStartup<Startup>().Build();"),
                    SyntaxFactory.ParseStatement("host.Run();"));
            members.Add(mainMethod);

            var @class = SyntaxFactory
                .ClassDeclaration("Program")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword))
                .AddMembers(members.ToArray());

            return @class;
        }
    }
}
