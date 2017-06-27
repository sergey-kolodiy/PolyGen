# PolyGen

PolyGen is a code generator that produces ORM layer, REST API and a user interface for your database.

## Getting Started

1. Define your database structure using Fluent API:

```csharp
var databaseSchema = SchemaBuilder.Create()
    .Table("user", "User")
        .Column("SSN").String().MaxLength(9).PrimaryKey()
        .Column("FirstName").String().MaxLength(100).PrimaryKey()
        .Column("LastName").String().MaxLength(100).PrimaryKey()
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
        .Column("RowVersion").RowVersion()
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
        .Column("RowVersion").RowVersion()
    .Table("blogging", "Tag")
        .Column("Id").Int32().PrimaryKey().Identity()
        .Column("Name").String().MaxLength(200)
        .Column("Description").String().MaxLength(500)
        .Column("RowVersion").RowVersion()
    .Table("blogging", "PostTag")
        .Column("Id").Int32().PrimaryKey().Identity()
        .Column("PostId").Int32()
        .Column("TagId").Int32()
        .Column("RowVersion").RowVersion()
    .Table("blogging", "Comment")
        .Column("Id").Int32().PrimaryKey().Identity()
        .Column("PostId").Int32()
        .Column("AuthorSSN").String().MaxLength(9)
        .Column("AuthorFirstName").String().MaxLength(100)
        .Column("AuthorLastName").String().MaxLength(100)
        .Column("Content").String()
        .Column("DateTime").String()
        .Column("RowVersion").RowVersion()
    .Relationship("Author_Blogs")
        .From("blogging", "Blog")
        .To("user", "Person")
            .Reference("AuthorSSN", "SSN")
            .Reference("AuthorFirstName", "FirstName")
            .Reference("AuthorLastName", "LastName")
            .OnDeleteCascade()
    .Relationship("Blog_Posts")
        .From("blogging", "Post")
        .To("blogging", "Blog")
            .Reference("BlogId", "Id")
    .Relationship("Post_Comments")
        .From("blogging", "Comment")
        .To("blogging", "Post")
            .Reference("PostId", "Id")
    .Relationship("Comment_Author")
        .From("blogging", "Comment")
        .To("user", "Person")
            .Reference("AuthorSSN", "SSN")
            .Reference("AuthorFirstName", "FirstName")
            .Reference("AuthorLastName", "LastName")
    .Relationship("Post_Editor")
        .From("blogging", "Post")
        .To("user", "Person")
            .Reference("EditorSSN", "SSN")
            .Reference("EditorFirstName", "FirstName")
            .Reference("EditorLastName", "LastName")
            .OnDeleteSetNull()
    .Relationship("Post_PostTags")
        .From("blogging", "PostTag")
        .To("blogging", "Post")
            .Reference("PostId", "Id")
    .Relationship("Tag_PostTags")
        .From("blogging", "PostTag")
        .To("blogging", "Tag")
            .Reference("TagId", "Id")
    .Build();
```

2. Generate code for the database schema:

```csharp
var compilationUnitSyntax = databaseSchema.GenerateCode();
```

## Built With

* [.NET Compiler Platform ("Roslyn")](https://github.com/dotnet/roslyn)

## Contributing

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/dr-noise/PolyGen/tags).

## Authors

* **Sergey Kolodiy**

See also the list of [contributors](https://github.com/dr-noise/PolyGen/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments
