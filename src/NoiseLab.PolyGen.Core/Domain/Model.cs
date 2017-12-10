using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Formatting;

namespace NoiseLab.PolyGen.Core.Domain
{
    public sealed class Model
    {
        public string GenerateCodeAsString()
        {
            var compilationUnit = GenerateCompilationUnitSyntax();

            using (var workspace = new AdhocWorkspace())
            {
                var formattedCode = Formatter.Format(compilationUnit, workspace).ToString();
                return formattedCode;
            }
        }

        public CodeGenerationArtifact GenerateCode()
        {
            var compilationUnit = GenerateCompilationUnitSyntax();
            var syntaxTree = CSharpSyntaxTree.Create(compilationUnit);

            MetadataReference[] references =
            {
                MetadataReference.CreateFromFile(CreateMetadataReference("netstandard.library.2.0.0", @"build\netstandard2.0\ref\netstandard.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("netstandard.library.2.0.0", @"build\netstandard2.0\ref\System.Runtime.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("system.componentmodel.annotations.4.4.0", @"lib\netcore50\System.ComponentModel.Annotations.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.entityframeworkcore.2.0.0", @"lib\netstandard2.0\Microsoft.EntityFrameworkCore.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.entityframeworkcore.relational.2.0.0", @"lib\netstandard2.0\Microsoft.EntityFrameworkCore.Relational.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.entityframeworkcore.sqlserver.2.0.0", @"lib\netstandard2.0\Microsoft.EntityFrameworkCore.SqlServer.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.hosting.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Hosting.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.hosting.abstractions.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Hosting.Abstractions.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.http.abstractions.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Http.Abstractions.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.mvc.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Mvc.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.mvc.abstractions.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Mvc.Abstractions.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.mvc.core.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Mvc.Core.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.mvc.viewfeatures.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Mvc.ViewFeatures.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.server.iisintegration.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Server.IISIntegration.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.aspnetcore.server.kestrel.2.0.0", @"lib\netstandard2.0\Microsoft.AspNetCore.Server.Kestrel.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.extensions.configuration.2.0.0", @"lib\netstandard2.0\Microsoft.Extensions.Configuration.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.extensions.configuration.binder.2.0.0", @"lib\netstandard2.0\Microsoft.Extensions.Configuration.Binder.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.extensions.configuration.abstractions.2.0.0", @"lib\netstandard2.0\Microsoft.Extensions.Configuration.Abstractions.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.extensions.configuration.environmentvariables.2.0.0", @"lib\netstandard2.0\Microsoft.Extensions.Configuration.EnvironmentVariables.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.extensions.configuration.fileextensions.2.0.0", @"lib\netstandard2.0\Microsoft.Extensions.Configuration.FileExtensions.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.extensions.configuration.json.2.0.0", @"lib\netstandard2.0\Microsoft.Extensions.Configuration.Json.dll")),
                MetadataReference.CreateFromFile(CreateMetadataReference("microsoft.extensions.dependencyinjection.abstractions.2.0.0", @"lib\netstandard2.0\Microsoft.Extensions.DependencyInjection.Abstractions.dll"))
            };

            var compilation = CSharpCompilation.Create(
                "NoiseLab.PolyGen.Generated",
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));

            EmitResult result;
            byte[] peBytes = null;
            byte[] pdbBytes = null;
            byte[] xmlBytes = null;
            using (var peStream = new MemoryStream())
            {
                using (var pdbStream = new MemoryStream())
                {
                    using (var xmlStream = new MemoryStream())
                    {
                        result = compilation.Emit(peStream, pdbStream, xmlStream);

                        if (result.Success)
                        {
                            peStream.Seek(0, SeekOrigin.Begin);
                            peBytes = peStream.ToArray();

                            pdbStream.Seek(0, SeekOrigin.Begin);
                            pdbBytes = pdbStream.ToArray();

                            xmlStream.Seek(0, SeekOrigin.Begin);
                            xmlBytes = xmlStream.ToArray();
                        }
                    }
                }
            }
            return new CodeGenerationArtifact(result, peBytes, pdbBytes, xmlBytes);
        }

        private static string CreateMetadataReference(string nugetPackageName, string path)
        {
            // TODO: Is it possible to avoid disk I/O for code generation?
            var resource = (byte[])Resources.ResourceManager.GetObject(nugetPackageName.Replace('.', '_'));
            using (var memoryStream = new MemoryStream(resource))
            {
                using (var archive = new ZipArchive(memoryStream))
                {
                    var tempPath = Path.Combine(
                        Path.GetTempPath(),
                        $@"NoiseLab.PolyGen\MetadataReferences\{nugetPackageName}");
                    archive.ExtractToDirectory(tempPath, true);
                    return Path.Combine(tempPath, path);
                }
            }
        }

        private CompilationUnitSyntax GenerateCompilationUnitSyntax()
        {
            var classes = new List<MemberDeclarationSyntax>();
            classes.AddRange(Entities.Select(t => t.GenerateOrmModel()));
            classes.AddRange(Entities.Select(t => t.GeneratePostValidationModel()));
            classes.AddRange(Entities.Select(t => t.GeneratePutValidationModel()));
            classes.Add(GenerateDbContext());
            classes.Add(GenerateStartup());
            classes.Add(GenerateProgram());
            classes.AddRange(Entities.Select(entity => entity.GenerateWebApiController()));
            classes.AddRange(GenerateInfrastructureClasses());

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
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Mvc")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Mvc.Filters")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Builder")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Hosting")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.Extensions.Configuration")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.Extensions.DependencyInjection"))
                )
                .AddMembers(classes.ToArray());

            return SyntaxFactory.CompilationUnit().AddMembers(@namespace);
        }

        internal Model(IReadOnlyList<Entity> entities, IEnumerable<Relationship> relationships)
        {
            Entities = entities;
            Relationships = relationships;
        }

        internal IReadOnlyList<Entity> Entities { get; }

        internal IEnumerable<Relationship> Relationships { get; }

        private static IEnumerable<ClassDeclarationSyntax> GenerateInfrastructureClasses()
        {
            var classes = new List<ClassDeclarationSyntax>
            {
                GenerateModelStateValidationAttribute(),
                GenerateNullValidationAttribute()
            };

            return classes;
        }

        private static ClassDeclarationSyntax GenerateModelStateValidationAttribute()
        {
            var members = new List<MemberDeclarationSyntax>();

            var configureServicesMethod = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    "OnActionExecuting")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.OverrideKeyword))
                .AddParameterListParameters(
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("context"))
                        .WithType(SyntaxFactory.ParseTypeName("ActionExecutingContext")))
                .AddBodyStatements(
                    SyntaxFactory.ParseStatement(
                        "if (!context.ModelState.IsValid) { context.Result = new BadRequestObjectResult(context.ModelState); }"),
                    SyntaxFactory.ParseStatement("base.OnActionExecuting(context);"));
            members.Add(configureServicesMethod);

            var @class = SyntaxFactory
                .ClassDeclaration("ModelStateValidationAttribute")
                .AddBaseListTypes(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName("ActionFilterAttribute")))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(members.ToArray());

            return @class;
        }

        private static ClassDeclarationSyntax GenerateNullValidationAttribute()
        {
            var members = new List<MemberDeclarationSyntax>();

            var configureServicesMethod = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    "OnActionExecuting")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.OverrideKeyword))
                .AddParameterListParameters(
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("context"))
                        .WithType(SyntaxFactory.ParseTypeName("ActionExecutingContext")))
                .AddBodyStatements(
                    SyntaxFactory.ParseStatement("var nullArguments = context.ActionArguments.Where(a => a.Value == null).ToList();"),
                    SyntaxFactory.ParseStatement("if (nullArguments.Any()) { context.Result = new BadRequestObjectResult(new { arguments = nullArguments.Select(a => $\"The {a.Key} argument is required.\") }); }"),
                    SyntaxFactory.ParseStatement("base.OnActionExecuting(context);"));
            members.Add(configureServicesMethod);

            var @class = SyntaxFactory
                .ClassDeclaration("NullValidationAttribute")
                .AddBaseListTypes(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName("ActionFilterAttribute")))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(members.ToArray());

            return @class;
        }

        private MemberDeclarationSyntax GenerateDbContext()
        {
            var members = new List<MemberDeclarationSyntax>();

            var properties = Entities
                .Select(entity => entity.GenerateDbSetProperty())
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
            foreach (var entity in Entities)
            {
                onModelCreatingMethodBody.Add(entity.GeneratePrimaryKeyDefinition());
                onModelCreatingMethodBody.AddRange(entity.GenerateRelationshipDefinitions());
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
