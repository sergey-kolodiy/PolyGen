using System;
using NoiseLab.PolyGen.Core.Database;
using NoiseLab.PolyGen.Core.Factories;

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

                //using (var workspace = new AdhocWorkspace())
                //{
                //    //cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
                //    var formattedCode = Formatter.Format(code, workspace).ToString();
                //    Console.WriteLine(formattedCode);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static Schema BuildDatabaseSchema()
        {
            var schema = SchemaFactory
                .Create()
                    .Table("user", "Person")
                        .Column("SSN", AbstractDataType.String).MaxLength(9).PrimaryKey()
                        .Column("FirstName", AbstractDataType.String).PrimaryKey().MaxLength(100)
                        .Column("LastName", AbstractDataType.String).PrimaryKey().MaxLength(100)
                        .Column("Nickname", AbstractDataType.String).MaxLength(100).Nullable()
                        .Column("BirthDate", AbstractDataType.Date)
                        .Column("Age", AbstractDataType.Int32).Computed()
                        .Column("RowVersion", AbstractDataType.RowVersion)
                    .Table("blogging", "Blog")
                        .Column("Id", AbstractDataType.Int32).PrimaryKey().Identity()
                        .Column("AuthorSSN", AbstractDataType.String).MaxLength(9)
                        .Column("AuthorFirstName", AbstractDataType.String).MaxLength(100)
                        .Column("AuthorLastName", AbstractDataType.String).MaxLength(100)
                        .Column("Title", AbstractDataType.String).MaxLength(200)
                        .Column("Description", AbstractDataType.String).MaxLength(500)
                        .Column("URL", AbstractDataType.String).MaxLength(1000)
                        .Column("Founded", AbstractDataType.Date)
                    .Table("blogging", "Post")
                        .Column("Id", AbstractDataType.Int32).PrimaryKey().Identity()
                        .Column("BlogId", AbstractDataType.Int32)
                        .Column("Title", AbstractDataType.String).MaxLength(200)
                        .Column("Summary", AbstractDataType.String).MaxLength(1000)
                        .Column("Content", AbstractDataType.String)
                        .Column("EditorSSN", AbstractDataType.String).MaxLength(9).Nullable()
                        .Column("EditorFirstName", AbstractDataType.String).MaxLength(100).Nullable()
                        .Column("EditorLastName", AbstractDataType.String).MaxLength(100).Nullable()
                        .Column("Rating", AbstractDataType.Byte)
                    .Table("blogging", "Tag")
                        .Column("Id", AbstractDataType.Int32).PrimaryKey().Identity()
                        .Column("Name", AbstractDataType.String).MaxLength(200)
                        .Column("Description", AbstractDataType.String).MaxLength(500)
                    .Table("blogging", "PostTag")
                        .Column("Id", AbstractDataType.Int32).PrimaryKey().Identity()
                        .Column("PostId", AbstractDataType.Int32)
                        .Column("TagId", AbstractDataType.Int32)
                    .Table("blogging", "Comment")
                        .Column("Id", AbstractDataType.Int32).PrimaryKey().Identity()
                        .Column("PostId", AbstractDataType.Int32)
                        .Column("AuthorSSN", AbstractDataType.String).MaxLength(9)
                        .Column("AuthorFirstName", AbstractDataType.String).MaxLength(100)
                        .Column("AuthorLastName", AbstractDataType.String).MaxLength(100)
                        .Column("Content", AbstractDataType.String)
                        .Column("DateTime", AbstractDataType.DateTime)
                    .Relationship("FK_Author_Blogs", "user", "Person", "blogging", "Blog")
                        .Reference("SSN", "AuthorSSN")
                        .Reference("FirstName", "AuthorFirstName")
                        .Reference("LastName", "AuthorLastName")
                        .OnDeleteCascade()
                    .Relationship("FK_Blog_Posts", "blogging", "Blog", "blogging", "Post")
                        .Reference("Id", "BlogId")
                    .Relationship("FK_Post_Comments", "blogging", "Post", "blogging", "Comment")
                        .Reference("Id", "PostId")
                    .Relationship("FK_Comment_Author", "user", "Person", "blogging", "Comment")
                        .Reference("SSN", "AuthorSSN")
                        .Reference("FirstName", "AuthorFirstName")
                        .Reference("LastName", "AuthorLastName")
                    .Relationship("FK_Post_Editor", "user", "Person", "blogging", "Post")
                        .Reference("SSN", "EditorSSN")
                        .Reference("FirstName", "EditorFirstName")
                        .Reference("LastName", "EditorLastName")
                        .OnDeleteSetNull()
                    .Relationship("FK_Post_PostTags", "blogging", "Post", "blogging", "PostTag")
                        .Reference("Id", "PostId")
                    .Relationship("FK_Tag_PostTags", "blogging", "Tag", "blogging", "PostTag")
                        .Reference("Id", "TagId")
                // TODO: Implement configuring indexes.
                .Build();

            return schema;
        }
    }
}
