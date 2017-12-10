using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoiseLab.PolyGen.Core.Extensions;

namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class Entity
    {
        internal Entity(string @namespace, string name, IReadOnlyCollection<Property> properties, PrimaryKey primaryKey)
        {
            _name = new EntityName(@namespace, name);
            Properties = properties;
            PrimaryKey = primaryKey;
        }

        private readonly EntityName _name;

        internal string Namespace => _name.Namespace;

        internal string Name => _name.Name;

        internal IReadOnlyCollection<Property> Properties { get; }

        internal PrimaryKey PrimaryKey { get; }

        private readonly List<EntityRelationship> _principalRelationships = new List<EntityRelationship>();
        private readonly List<EntityRelationship> _dependentRelationships = new List<EntityRelationship>();

        internal void AddPrincipalRelationship(EntityRelationship relationship)
        {
            _principalRelationships.Add(relationship);
        }

        internal void AddDependentRelationship(EntityRelationship relationship)
        {
            _dependentRelationships.Add(relationship);
        }

        internal MemberDeclarationSyntax GenerateOrmModel()
        {
            var members = Properties
                .Select(property => property.GeneratePropertyForOrmModel())
                .Cast<MemberDeclarationSyntax>()
                .ToList();

            members.AddRange(_principalRelationships
                .Select(r => r.GenerateReferenceNavigationProperty())
                .ToList());

            members.AddRange(_dependentRelationships
                .Select(r => r.GenerateCollectionNavigationProperty())
                .ToList());

            var updateMethod = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)), "Update")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(SyntaxFactory
                    .Parameter(SyntaxFactory.Identifier("model"))
                    .WithType(SyntaxFactory.ParseTypeName(_name.PutValidationModelClassName)))
                .AddBodyStatements(
                    Properties
                        .Where(c => c.IsPutValidationModelProperty() && !c.PrimaryKey && !c.Identity)
                        .Select(c =>
                            SyntaxFactory.ParseStatement(
                                $"{c.Name.GenerateVerbatimIdentifierString()} = model.{c.Name.GenerateVerbatimIdentifierString()}{c.DataType.GetClrDataType(c.IsValueRequiredOnCreate()).GenerateValueInvocation()};"))
                        .ToArray());
            members.Add(updateMethod);

            var @class = SyntaxFactory
                .ClassDeclaration(_name.OrmModelClassName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(members.ToArray())
                .AddAttributeLists(_name.GenerateEntityAttributeLists());

            return @class;
        }

        internal MemberDeclarationSyntax GeneratePostValidationModel()
        {
            var members = Properties
                .Where(c => c.IsPostValidationModelProperty())
                .Select(property => property.GeneratePropertyForPostValidationModel())
                .Cast<MemberDeclarationSyntax>()
                .ToList();

            var toOrmModelMethod = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.ParseTypeName(_name.OrmModelClassName), "ToOrmModel")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBodyStatements(SyntaxFactory.ParseStatement($"var ormModel = new {_name.OrmModelClassName}();"))
                .AddBodyStatements(
                    Properties
                        .Where(c => c.IsPostValidationModelProperty())
                        .Select(c =>
                            SyntaxFactory.ParseStatement(
                                $"ormModel.{c.Name.GenerateVerbatimIdentifierString()} = {c.Name.GenerateVerbatimIdentifierString()}{c.DataType.GetClrDataType(c.IsValueRequiredOnCreate()).GenerateValueInvocation()};"))
                        .ToArray())
                .AddBodyStatements(SyntaxFactory.ParseStatement("return ormModel;"));
            members.Add(toOrmModelMethod);

            var @class = SyntaxFactory
                .ClassDeclaration(_name.PostValidationModelClassName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(members.ToArray());

            return @class;
        }

        internal MemberDeclarationSyntax GeneratePutValidationModel()
        {
            var members = Properties
                .Where(c => c.IsPutValidationModelProperty())
                .Select(property => property.GeneratePropertyForPutValidationModel())
                .Cast<MemberDeclarationSyntax>()
                .ToList();

            var toOrmModelMethod = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.ParseTypeName(_name.OrmModelClassName), "ToOrmModel")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBodyStatements(SyntaxFactory.ParseStatement($"var ormModel = new {_name.OrmModelClassName}();"))
                .AddBodyStatements(
                    Properties
                        .Where(c => c.IsPutValidationModelProperty())
                        .Select(c =>
                            SyntaxFactory.ParseStatement(
                                $"ormModel.{c.Name.GenerateVerbatimIdentifierString()} = {c.Name.GenerateVerbatimIdentifierString()}{c.DataType.GetClrDataType(c.IsValueRequiredOnUpdate()).GenerateValueInvocation()};"))
                        .ToArray())
                .AddBodyStatements(SyntaxFactory.ParseStatement("return ormModel;"));
            members.Add(toOrmModelMethod);

            var @class = SyntaxFactory
                .ClassDeclaration(_name.PutValidationModelClassName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(members.ToArray());

            return @class;
        }

        internal MemberDeclarationSyntax GenerateReferenceNavigationProperty(string relationshipName)
        {
            var type = SyntaxFactory.ParseName(_name.OrmModelClassName);

            var property = SyntaxFactory
                .PropertyDeclaration(type, _name.Suffix(relationshipName).GenerateVerbatimIdentifier())
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            return property;
        }

        internal MemberDeclarationSyntax GenerateCollectionNavigationProperty(string relationshipName)
        {
            var type = SyntaxFactory.ParseName($"List<{_name.OrmModelClassName}>");

            var property = SyntaxFactory
                .PropertyDeclaration(type, _name.Suffix(relationshipName).GenerateVerbatimIdentifier())
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            return property;
        }

        internal PropertyDeclarationSyntax GenerateDbSetProperty()
        {
            var type = GenerateDbSetPropertyType();
            return SyntaxFactory
                .PropertyDeclaration(type, _name.DbSetPropertyName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

            GenericNameSyntax GenerateDbSetPropertyType()
            {
                return SyntaxFactory.GenericName(
                    SyntaxFactory.Identifier("DbSet"),
                    SyntaxFactory.TypeArgumentList(
                        SyntaxFactory.SeparatedList(new[]
                        {
                        SyntaxFactory.ParseTypeName(_name.OrmModelClassName)
                        })));
            }
        }

        internal StatementSyntax GeneratePrimaryKeyDefinition()
        {
            return SyntaxFactory.ParseStatement($"modelBuilder.Entity<{_name.OrmModelClassName}>().HasKey(o => new {{ {PrimaryKey.GeneratePrimaryKeyDefinitionForModelBuilder("o")} }});");
        }

        internal IEnumerable<StatementSyntax> GenerateRelationshipDefinitions()
        {
            return _dependentRelationships.Select(dependentRelationship => SyntaxFactory.ParseStatement(
                $"modelBuilder.Entity<{_name.OrmModelClassName}>().HasMany(o => o.{dependentRelationship.OtherSideEntity._name.Suffix(dependentRelationship.Name).GenerateVerbatimIdentifierString()}).WithOne(o => o.{_name.Suffix(dependentRelationship.Name).GenerateVerbatimIdentifierString()}).HasForeignKey({dependentRelationship.GenerateForeignKeyPropertyNameEnumeration()}).OnDelete({dependentRelationship.GetDeleteBehavior()}).HasConstraintName(\"{dependentRelationship.Name}\");"));
        }

        internal MemberDeclarationSyntax GenerateWebApiController()
        {
            var members = new List<MemberDeclarationSyntax>();

            var databaseContextField = SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.ParseTypeName("DatabaseContext"),
                        SyntaxFactory.SeparatedList(
                            new[] { SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier("_context")) })))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));
            members.Add(databaseContextField);

            var constructor = SyntaxFactory
                .ConstructorDeclaration(_name.WebApiControllerClassName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier("context"))
                        .WithType(SyntaxFactory.ParseTypeName("DatabaseContext")))
                .AddBodyStatements(
                    SyntaxFactory.ParseStatement("_context = context;"));
            members.Add(constructor);

            var getEntitiesMethod = GenerateGetEntitiesMethod();
            members.Add(getEntitiesMethod);

            var getEntityByIdMethod = GenerateGetEntityByIdMethod();
            members.Add(getEntityByIdMethod);

            var postMethod = GeneratePostEntityMethod();
            members.Add(postMethod);

            var putMethod = GeneratePutEntityMethod();
            members.Add(putMethod);

            var deleteMethod = GenerateDeleteEntityMethod();
            members.Add(deleteMethod);

            var @class = SyntaxFactory
                .ClassDeclaration(_name.WebApiControllerClassName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName("Controller")))
                .AddMembers(members.ToArray())
                .AddAttributeLists(
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Route"))
                                .AddArgumentListArguments(
                                    SyntaxFactory.AttributeArgument(
                                        SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal($"api/{_name.FullName}")))))));

            return @class;

            MemberDeclarationSyntax GenerateGetEntitiesMethod()
            {
                // TODO: Consider using .AsNoTracking() for GET methods for performance improvement.
                return SyntaxFactory
                    .MethodDeclaration(SyntaxFactory.ParseTypeName($"Task<IEnumerable<{_name.OrmModelClassName}>>"), "Get")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                    .AddBodyStatements(
                        SyntaxFactory.ParseStatement($"await _context.{_name.DbSetPropertyName}.LoadAsync();"),
                        SyntaxFactory.ParseStatement($"return _context.{_name.DbSetPropertyName};"))
                    .AddAttributeLists(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName("HttpGet")))));
            }

            MemberDeclarationSyntax GenerateGetEntityByIdMethod()
            {
                // TODO: Consider using .AsNoTracking() for GET methods for performance improvement.
                return SyntaxFactory
                    .MethodDeclaration(SyntaxFactory.ParseTypeName("Task<IActionResult>"), "GetById")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                    .AddParameterListParameters(PrimaryKey.GenerateParameterList())
                    .AddBodyStatements(
                        SyntaxFactory.ParseStatement($"var entity = await _context.{_name.DbSetPropertyName}.FindAsync({PrimaryKey.GenerateParameterListString()});"),
                        SyntaxFactory.ParseStatement("return entity == null ? (IActionResult) NotFound() : Ok(entity);"))
                    .AddAttributeLists(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName("HttpGet"))
                                .AddArgumentListArguments(
                                        SyntaxFactory.AttributeArgument(
                                            SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(PrimaryKey.GenerateRouteTemplate())))))));
            }

            MemberDeclarationSyntax GeneratePostEntityMethod()
            {
                return SyntaxFactory
                    .MethodDeclaration(SyntaxFactory.ParseTypeName("Task<IActionResult>"), "Post")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                    .AddParameterListParameters(
                        SyntaxFactory
                            .Parameter(SyntaxFactory.Identifier("entity"))
                            .WithType(SyntaxFactory.ParseTypeName(_name.PostValidationModelClassName))
                            .AddAttributeLists(
                                SyntaxFactory.AttributeList(
                                    SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("FromBody"))))))
                    .AddBodyStatements(
                        SyntaxFactory.ParseStatement("var ormModel = entity.ToOrmModel();"),
                        SyntaxFactory.ParseStatement($"_context.{_name.DbSetPropertyName}.Add(ormModel);"),
                        SyntaxFactory.ParseStatement("await _context.SaveChangesAsync();"),
                        SyntaxFactory.ParseStatement(
                            $"return Created($\"api/{_name.FullName}/{PrimaryKey.GenerateEntityUri()}\", ormModel);"))
                    .AddAttributeLists(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName("HttpPost")))));
            }

            MemberDeclarationSyntax GeneratePutEntityMethod()
            {
                return SyntaxFactory
                    .MethodDeclaration(SyntaxFactory.ParseTypeName("Task<IActionResult>"), "Put")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                    .AddParameterListParameters(PrimaryKey.GenerateParameterList())
                    .AddParameterListParameters(
                        SyntaxFactory
                            .Parameter(SyntaxFactory.Identifier("entity"))
                            .WithType(SyntaxFactory.ParseTypeName(_name.PutValidationModelClassName))
                            .AddAttributeLists(
                                SyntaxFactory.AttributeList(
                                    SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("FromBody"))))))
                    .AddBodyStatements(
                        SyntaxFactory.ParseStatement($"if (!({PrimaryKey.GenerateRouteIdEqualsToEntityId()})) {{ return BadRequest(new {{ error = \"Entity identifier does not match route identifier.\"}}); }}"),
                        SyntaxFactory.ParseStatement($"var existing = await _context.{_name.DbSetPropertyName}.FindAsync({PrimaryKey.GenerateParameterListString()});"),
                        SyntaxFactory.ParseStatement("if (existing == null) { return NotFound(); }"),
                        SyntaxFactory.ParseStatement("_context.Entry(existing).State = EntityState.Detached;"),
                        SyntaxFactory.ParseStatement("var ormModel = entity.ToOrmModel();"),
                        SyntaxFactory.ParseStatement($"_context.{_name.DbSetPropertyName}.Attach(ormModel);"),
                        SyntaxFactory.ParseStatement("_context.Entry(ormModel).State = EntityState.Modified;"),
                        SyntaxFactory.ParseStatement("try { await _context.SaveChangesAsync(); }" +
                                                     "catch (DbUpdateConcurrencyException ex) {" +
                                                     "var exceptionEntry = ex.Entries.Single();" +
                                                     $"var clientObject = ({_name.OrmModelClassName})exceptionEntry.Entity;" +
                                                     "var databaseValues = exceptionEntry.GetDatabaseValues();" +
                                                     "if (databaseValues == null) { ModelState.AddModelError(string.Empty, \"Unable to save changes. The specified entity was deleted.\"); }" +
                                                     "else {" +
                                                     $"var databaseObject = ({_name.OrmModelClassName})databaseValues.ToObject();" +
                                                     $"{string.Join(string.Empty, Properties.Where(c => c.IsConcurrencyCheckComparisonProperty()).Select(c => $"if (databaseObject.{c.Name.GenerateVerbatimIdentifier()} != clientObject.{c.Name.GenerateVerbatimIdentifier()}) {{ ModelState.AddModelError(\"{c.Name}\", $\"Current value: {{databaseObject.{c.Name}}}\"); }}"))}" +
                                                     "}" +
                                                     // TODO: Return Conflict instead of BadRequest here.
                                                     "return BadRequest(ModelState);" +
                                                     "" +
                                                     "}"),
                        SyntaxFactory.ParseStatement("return Ok();"))
                    .AddAttributeLists(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName("HttpPut"))
                                .AddArgumentListArguments(
                                        SyntaxFactory.AttributeArgument(
                                            SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(PrimaryKey.GenerateRouteTemplate())))))));
            }

            MemberDeclarationSyntax GenerateDeleteEntityMethod()
            {
                return SyntaxFactory
                    .MethodDeclaration(SyntaxFactory.ParseTypeName("Task<IActionResult>"), "Delete")
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                    .AddParameterListParameters(PrimaryKey.GenerateParameterList())
                    .AddBodyStatements(
                        SyntaxFactory.ParseStatement($"var entity = await _context.{_name.DbSetPropertyName}.FindAsync({PrimaryKey.GenerateParameterListString()});"),
                        SyntaxFactory.ParseStatement("if (entity == null) { return NotFound(); }"),
                        SyntaxFactory.ParseStatement("_context.Remove(entity);"),
                        SyntaxFactory.ParseStatement("await _context.SaveChangesAsync();"),
                        SyntaxFactory.ParseStatement("return Ok();"))
                    .AddAttributeLists(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName("HttpDelete"))
                                .AddArgumentListArguments(
                                        SyntaxFactory.AttributeArgument(
                                            SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(PrimaryKey.GenerateRouteTemplate())))))));
            }
        }

        private sealed class EntityName
        {
            internal EntityName(string @namespace, string name)
            {
                Namespace = @namespace;
                Name = name;
            }

            internal string Namespace { get; }

            internal string Name { get; }

            internal string FullName => $"{Namespace}_{Name}";

            internal string OrmModelClassName => $"{FullName}_OrmModel";

            internal string WebApiControllerClassName => $"{FullName}_Controller";

            internal string PostValidationModelClassName => $"{FullName}_PostValidationModel";

            internal string PutValidationModelClassName => $"{FullName}_PutValidationModel";

            internal string DbSetPropertyName => $"{FullName}_DBSet";

            internal string Suffix(string suffix)
            {
                return $"{FullName}_{suffix}";
            }

            internal AttributeListSyntax GenerateEntityAttributeLists()
            {
                return
                    SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName("Table"))
                            .AddArgumentListArguments(
                                SyntaxFactory.AttributeArgument(
                                    SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal(Name))),
                                SyntaxFactory.AttributeArgument(
                                        SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                                            SyntaxFactory.Literal(Namespace)))
                                    .WithNameEquals(SyntaxFactory.NameEquals(SyntaxFactory.IdentifierName("Schema"))))));
            }
        }
    }
}