using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoiseLab.PolyGen.Core.Extensions;

namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class PrimaryKey
    {
        internal PrimaryKey(IReadOnlyList<Property> properties)
        {
            Properties = properties;
        }

        internal IReadOnlyList<Property> Properties { get; }

        internal string GeneratePrimaryKeyDefinitionForModelBuilder(string objectName)
        {
            /*
             * o.@P1, o.@P2, o.@P3
             * 
             * Context:
             * modelBuilder.Entity<user_Person_OrmModel>().HasKey(o => new { o.@P1, o.@P2, o.@P3 });
             *                                                               ^^^^^^^^^^^^^^^^^^^
             */
            return string.Join(", ", Properties.Select(key => $"{objectName}.{key.Name.GenerateVerbatimIdentifierString()}"));
        }

        internal ParameterSyntax[] GenerateParameterList()
        {
            /*
             * System.String @P1, System.String @P2, System.String @P3
             * 
             * Context:
             * public IActionResult GetById (System.String @P1, ...) { ... }
             *                               ^^^^^^^^^^^^^^^^^^^^^^
             */
            return Properties.Select(c => SyntaxFactory
                .Parameter(c.Name.GenerateVerbatimIdentifier())
                .WithType(c.GeneratePropertyTypeSyntax())).ToArray();
        }

        internal string GenerateParameterListString()
        {
            /*
             * @P1, @P2, @P3
             * 
             * Context:
             * var entity = _context.DBSet.Find(@P1, @P2, @P3)
             *                                  ^^^^^^^^^^^^^
             */
            return string.Join(", ", Properties.Select(c => $"{c.Name.GenerateVerbatimIdentifierString()}"));
        }

        internal string GenerateRouteTemplate()
        {
            /*
             * {P1}/{P2}/{P3}
             * 
             * Context:
             * [HttpGet("{P1}/{P2}/{P3}")]
             *           ^^^^^^^^^^^^^^
             */
            return string.Join("/", Properties.Select(c => $"{{{c.Name}}}"));
        }

        internal string GenerateEntityUri()
        {
            /*
             * {ormModel.@P1}/{ormModel.@P2}/{ormModel.@P3}
             * 
             * Context:
             * return Created($"api/namespace_Entity/{ormModel.@P1}/{ormModel.@P2}/...", entity);
             *                                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
             */
            return string.Join("/", Properties.Select(c => $"{{ormModel.{c.Name.GenerateVerbatimIdentifierString()}}}"));
        }

        internal string GenerateRouteIdEqualsToEntityId()
        {
            /*
             * @P1 == entity.@P1 && @P2 == entity.@P2 && @P3 == entity.@P3
             * 
             * Context:
             * if (!(@P1 == entity.@P1 && @P2 == entity.@P2 && ...)) { return BadRequest(); }
             *       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
             */
            return string.Join(" && ",
                Properties.Select(c =>
                $"{c.Name.GenerateVerbatimIdentifierString()} == entity.{c.Name.GenerateVerbatimIdentifierString()}"));
        }
    }
}
