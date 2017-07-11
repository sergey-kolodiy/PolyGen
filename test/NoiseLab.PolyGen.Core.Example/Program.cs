using System;
using NoiseLab.PolyGen.Core.Builders;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Example
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var schema = BuildDatabaseSchema();
                var code = schema.GenerateCode();
                Console.WriteLine(code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static Schema BuildDatabaseSchema()
        {
            var schema = SchemaBuilder
                .Create()
                    .Table("user", "Person")
                        .Column("SSN").String().MaxLength(9).PrimaryKey()
                        .Column("FirstName").String().PrimaryKey().MaxLength(100)
                        .Column("LastName").String().PrimaryKey().MaxLength(100)
                        .Column("Nickname").String().MaxLength(100).Nullable()
                        .Column("BirthDate").Date()
                        .Column("Age").Int32().Computed()
                        .Column("RowVersion").RowVersion()
                    .Table("blogging", "Blog")
                        .Column("Id").Int32().PrimaryKey().Identity()
                        .Column("AuthorSSN").String().MaxLength(9)
                        .Column("AuthorFirstName").String().MaxLength(100)
                        .Column("AuthorLastName").String().MaxLength(100)
                        .Column("Title").String().MaxLength(200)
                        .Column("Description").String().MaxLength(500)
                        .Column("URL").String().MaxLength(1000)
                        .Column("Founded").Date()
                    .Table("blogging", "Post")
                        .Column("Id").Int32().PrimaryKey().Identity()
                        .Column("BlogId").Int32()
                        .Column("Title").String().MaxLength(200)
                        .Column("Summary").String().MaxLength(1000)
                        .Column("Content").String()
                        .Column("EditorSSN").String().MaxLength(9).Nullable()
                        .Column("EditorFirstName").String().MaxLength(100).Nullable()
                        .Column("EditorLastName").String().MaxLength(100).Nullable()
                        .Column("Rating").Byte()
                    .Table("blogging", "Tag")
                        .Column("Id").Int32().PrimaryKey().Identity()
                        .Column("Name").String().MaxLength(200)
                        .Column("Description").String().MaxLength(500)
                    .Table("blogging", "PostTag")
                        .Column("Id").Int32().PrimaryKey().Identity()
                        .Column("PostId").Int32()
                        .Column("TagId").Int32()
                    .Table("blogging", "Comment")
                        .Column("Id").Int32().PrimaryKey().Identity()
                        .Column("PostId").Int32()
                        .Column("AuthorSSN").String().MaxLength(9)
                        .Column("AuthorFirstName").String().MaxLength(100)
                        .Column("AuthorLastName").String().MaxLength(100)
                        .Column("Content").String()
                        .Column("DateTime").String()
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

            return schema;
        }
    }
}
