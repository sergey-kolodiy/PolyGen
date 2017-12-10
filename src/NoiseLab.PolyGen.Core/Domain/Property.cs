using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoiseLab.PolyGen.Core.Extensions;

namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class Property
    {

        internal Property(int ordinal, string name, AbstractDataType dataDataType, int? maxLength, bool nullable, bool primaryKey, bool identity, bool computed)
        {
            Ordinal = ordinal;
            Name = name;
            DataType = dataDataType;
            MaxLength = maxLength;
            Nullable = nullable;
            PrimaryKey = primaryKey;
            Identity = identity;
            Computed = computed;
        }

        internal int Ordinal { get; }
        internal string Name { get; }
        internal AbstractDataType DataType { get; }
        internal int? MaxLength { get; }
        internal bool Nullable { get; }
        internal bool PrimaryKey { get; }
        internal bool Identity { get; }
        internal bool Computed { get; }

        internal PropertyDeclarationSyntax GeneratePropertyForOrmModel()
        {
            /*
             * [Column("Property", Order=0)]                     - always generated
             * [DatabaseGenerated(DatabaseGeneratedOption.None)] - always generated
             * [StringLength(9)]                                 - conditionally generated
             * [Required]                                        - conditionally generated
             * [RowVersion]                                      - conditionally generated
             * public System.String @Property { get; set; }
             */
            var type = GeneratePropertyTypeSyntax();
            var attributes = GenerateAttributeLists();

            var property = SyntaxFactory
                .PropertyDeclaration(type, Name.GenerateVerbatimIdentifier())
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                .AddAttributeLists(attributes);
            return property;

            AttributeListSyntax[] GenerateAttributeLists()
            {
                var attributeLists = new List<AttributeListSyntax>
                {
                    GenerateColumnAttribute(),
                    GenerateDatabaseGeneratedAttribute()
                };

                if (MaxLength.HasValue)
                {
                    attributeLists.Add(GenerateStringLengthAttribute(MaxLength.Value));
                }
                if (!Nullable)
                {
                    attributeLists.Add(GenerateRequiredAttribute());
                }
                if (DataType == AbstractDataType.RowVersion)
                {
                    attributeLists.Add(GenerateTimestampAttribute());
                }

                return attributeLists.ToArray();

                AttributeListSyntax GenerateColumnAttribute()
                {
                    var argumentList = GenerateArgumentList();
                    return SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Column"))
                                .AddArgumentListArguments(argumentList)));

                    AttributeArgumentSyntax[] GenerateArgumentList()
                    {
                        var columnArgumentList = new[]
                        {
                            SyntaxFactory.AttributeArgument(
                                SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                                    SyntaxFactory.Literal(Name))),
                            SyntaxFactory.AttributeArgument(SyntaxFactory.LiteralExpression(
                                    SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(Ordinal)))
                                .WithNameEquals(SyntaxFactory.NameEquals(SyntaxFactory.IdentifierName("Order")))
                        };
                        return columnArgumentList;
                    }
                }

                AttributeListSyntax GenerateDatabaseGeneratedAttribute()
                {
                    return SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("DatabaseGenerated"))
                                .AddArgumentListArguments(
                                    SyntaxFactory.AttributeArgument(
                                        SyntaxFactory.ParseExpression(Identity
                                            ? "DatabaseGeneratedOption.Identity"
                                            : (Computed || DataType == AbstractDataType.RowVersion ? "DatabaseGeneratedOption.Computed" : "DatabaseGeneratedOption.None"))))));
                }

                AttributeListSyntax GenerateStringLengthAttribute(int maxLength)
                {
                    return SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("StringLength"))
                                .AddArgumentListArguments(
                                    SyntaxFactory.AttributeArgument(
                                        SyntaxFactory.ParseExpression(maxLength.ToString())))));
                }
            }
        }

        internal PropertyDeclarationSyntax GeneratePropertyForPostValidationModel()
        {
            /*
             * [Required]     - conditionally generated
             * [MaxLength(9)] - conditionally generated
             * public System.Nullable<System.Int32> @Required { get; set; }
             */
            return GeneratePropertyForValidationModel(IsValueRequiredOnCreate());
        }

        internal PropertyDeclarationSyntax GeneratePropertyForPutValidationModel()
        {
            /*
             * [Required]     - conditionally generated
             * [MaxLength(9)] - conditionally generated
             * public System.Nullable<System.Int32> @Required { get; set; }
             */
            return GeneratePropertyForValidationModel(IsValueRequiredOnUpdate());
        }

        internal PropertyDeclarationSyntax GeneratePropertyForValidationModel(bool required)
        {
            var type = DataType.GetClrDataType(required).GenerateTypeSyntax();
            var attributes = GeneratePropertyAttributeLists();

            var property = SyntaxFactory
                .PropertyDeclaration(type, Name.GenerateVerbatimIdentifier())
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                .AddAttributeLists(attributes);
            return property;

            AttributeListSyntax[] GeneratePropertyAttributeLists()
            {
                var attributeLists = new List<AttributeListSyntax>();
                if (required)
                {
                    attributeLists.Add(GenerateRequiredAttribute());
                }
                if (MaxLength.HasValue && MaxLength.Value != -1)
                {
                    attributeLists.Add(SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("MaxLength"))
                                .AddArgumentListArguments(SyntaxFactory.AttributeArgument(
                                    SyntaxFactory.ParseExpression(MaxLength.Value.ToString()))))));
                }
                return attributeLists.ToArray();
            }
        }

        internal TypeSyntax GeneratePropertyTypeSyntax()
        {
            /*
             * System.Nullable<System.Int32>
             */
            return DataType.GetClrDataType(Nullable).GenerateTypeSyntax();
        }

        internal bool IsPostValidationModelProperty()
        {
            return !Computed && !Identity && DataType != AbstractDataType.RowVersion;
        }

        internal bool IsPutValidationModelProperty()
        {
            return !Computed;
        }

        internal bool IsValueRequiredOnCreate()
        {
            return !(Identity || Nullable || Computed);
        }

        internal bool IsValueRequiredOnUpdate()
        {
            return PrimaryKey || !(Nullable || Computed);
        }

        internal bool IsConcurrencyCheckComparisonProperty()
        {
            return !(PrimaryKey || Computed || Identity || DataType == AbstractDataType.RowVersion);
        }

        private static AttributeListSyntax GenerateRequiredAttribute()
        {
            return SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Required"))));
        }

        private static AttributeListSyntax GenerateTimestampAttribute()
        {
            return SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Timestamp"))));
        }
    }
}