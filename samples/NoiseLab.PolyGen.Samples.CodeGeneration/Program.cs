using System;
using System.IO;
using NoiseLab.PolyGen.Core.Domain;
using NoiseLab.PolyGen.Core.FluentConfiguration;

namespace NoiseLab.PolyGen.Samples.CodeGeneration
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var model = BuildModel();
                var codeGenerationResult = model.GenerateCode();
                WriteCodeGenerationArtifacts(codeGenerationResult, @"c:\Users\Sergey\Desktop\");

                var code = model.GenerateCodeAsString();
                Console.WriteLine(code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static Model BuildModel()
        {
            var model = ModelBuilder.Create()
                .Entity("user", "Person")
                    .PrimaryKeyProperty("SSN").String().MaxLength(9)
                    .PrimaryKeyProperty("FirstName").String().MaxLength(100)
                    .PrimaryKeyProperty("LastName").String().MaxLength(100)
                    .Property("Nickname").String().MaxLength(100).Nullable()
                    .Property("BirthDate").Date()
                    .Property("Age").Int32().Computed()
                    .Property("RowVersion").RowVersion()
                .Entity("blogging", "Blog")
                    .PrimaryKeyProperty("Id").Int32().Identity()
                    .Property("AuthorSSN").String().MaxLength(9)
                    .Property("AuthorFirstName").String().MaxLength(100)
                    .Property("AuthorLastName").String().MaxLength(100)
                    .Property("Title").String().MaxLength(200)
                    .Property("Description").String().MaxLength(500)
                    .Property("URL").String().MaxLength(1000)
                    .Property("Founded").Date()
                .Entity("blogging", "Post")
                    .PrimaryKeyProperty("Id").Int32().Identity()
                    .Property("BlogId").Int32()
                    .Property("Title").String().MaxLength(200)
                    .Property("Summary").String().MaxLength(1000)
                    .Property("Content").String()
                    .Property("EditorSSN").String().MaxLength(9).Nullable()
                    .Property("EditorFirstName").String().MaxLength(100).Nullable()
                    .Property("EditorLastName").String().MaxLength(100).Nullable()
                    .Property("Rating").Byte()
                .Entity("blogging", "Tag")
                    .PrimaryKeyProperty("Id").Int32().Identity()
                    .Property("Name").String().MaxLength(200)
                    .Property("Description").String().MaxLength(500)
                .Entity("blogging", "PostTag")
                    .PrimaryKeyProperty("Id").Int32().Identity()
                    .Property("PostId").Int32()
                    .Property("TagId").Int32()
                .Entity("blogging", "Comment")
                    .PrimaryKeyProperty("Id").Int32().Identity()
                    .Property("PostId").Int32()
                    .Property("AuthorSSN").String().MaxLength(9)
                    .Property("AuthorFirstName").String().MaxLength(100)
                    .Property("AuthorLastName").String().MaxLength(100)
                    .Property("Content").String()
                    .Property("DateTime").String()
                .Relationship("FK_Author_Blogs")
                    .From("blogging", "Blog")
                    .To("user", "Person")
                        .Reference("AuthorSSN", "SSN")
                        .Reference("AuthorFirstName", "FirstName")
                        .Reference("AuthorLastName", "LastName")
                        .OnDeleteCascade()
                .Relationship("FK_Blog_Posts")
                    .From("blogging", "Post")
                    .To("blogging", "Blog")
                        .Reference("BlogId", "Id")
                .Relationship("FK_Post_Comments")
                    .From("blogging", "Comment")
                    .To("blogging", "Post")
                        .Reference("PostId", "Id")
                .Relationship("FK_Comment_Author")
                    .From("blogging", "Comment")
                    .To("user", "Person")
                        .Reference("AuthorSSN", "SSN")
                        .Reference("AuthorFirstName", "FirstName")
                        .Reference("AuthorLastName", "LastName")
                .Relationship("FK_Post_Editor")
                    .From("blogging", "Post")
                    .To("user", "Person")
                        .Reference("EditorSSN", "SSN")
                        .Reference("EditorFirstName", "FirstName")
                        .Reference("EditorLastName", "LastName")
                        .OnDeleteSetNull()
                .Relationship("FK_Post_PostTags")
                    .From("blogging", "PostTag")
                    .To("blogging", "Post")
                        .Reference("PostId", "Id")
                .Relationship("FK_Tag_PostTags")
                    .From("blogging", "PostTag")
                    .To("blogging", "Tag")
                        .Reference("TagId", "Id")
            // TODO: Implement configuring indexes.		
            .Build();

            return model;
        }

        private static void WriteCodeGenerationArtifacts(CodeGenerationArtifact codeGenerationArtifact, string basePath)
        {
            if (codeGenerationArtifact.EmitResult.Success)
            {
                using (var file = new FileStream(
                    $@"{basePath}NoiseLab.PolyGen.Generated.dll",
                    FileMode.Create, FileAccess.Write))
                {
                    file.Write(codeGenerationArtifact.PeBytes, 0, codeGenerationArtifact.PeBytes.Length);
                }

                using (var file = new FileStream(
                    $@"{basePath}NoiseLab.PolyGen.Generated.pdb",
                    FileMode.Create, FileAccess.Write))
                {
                    file.Write(codeGenerationArtifact.PdbBytes, 0, codeGenerationArtifact.PdbBytes.Length);
                }

                using (var file = new FileStream(
                    $@"{basePath}NoiseLab.PolyGen.Generated.xml",
                    FileMode.Create, FileAccess.Write))
                {
                    file.Write(codeGenerationArtifact.XmlBytes, 0, codeGenerationArtifact.XmlBytes.Length);
                }
            }
        }
    }
}
