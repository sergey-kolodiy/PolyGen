# PolyGen

PolyGen is a code generator that produces database schema, ORM layer, REST API and a (coming soon — stay tuned!) single-page web UI for your business model.

[//]: # "[![Build Status](https://travis-ci.org/sergey-kolodiy/PolyGen.svg?branch=master)](https://travis-ci.org/sergey-kolodiy/PolyGen)"
[![Join the chat at https://gitter.im/PolyGen/Lobby](https://badges.gitter.im/PolyGen/Lobby.svg)](https://gitter.im/PolyGen/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Getting Started

1. Define your business model using Fluent Configuration API:

```csharp
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
    .Build();
```

2. Generate code for the business model:

```csharp
string code = model.GenerateCode();
```

3. PolyGen will generate the following classes for you:

 - ORM model (Entity Framerork Core is used), including `DbContext`;
 - ASP.NET Core `Startup` and `Program` classes;
 - Web API controller for each business model entity, including `Get`, `GetById`, `Post`, `Put` and `Delete` methods;
 - Validation models for `Post` and `Put` controller methods (required to prevent under- and overposting);

For a business model defined in step 1, the following code will be generated:

```csharp
namespace NoiseLab.PolyGen.Generated
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [Table("Person", Schema = "user")]
    public class user_Person_OrmModel
    {
        [Column("SSN", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(9)]
        [Required]
        public System.String @SSN { get; set; }

        [Column("FirstName", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        [Required]
        public System.String @FirstName { get; set; }

        [Column("LastName", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        [Required]
        public System.String @LastName { get; set; }

        [Column("Nickname", Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        public System.String @Nickname { get; set; }

        [Column("BirthDate", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.DateTime @BirthDate { get; set; }

        [Column("Age", Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        public System.Int32 @Age { get; set; }

        [Column("RowVersion", Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [Timestamp]
        public System.Byte[] @RowVersion { get; set; }

        public List<blogging_Blog_OrmModel> @blogging_Blog_FK_Author_Blogs { get; set; }
        public List<blogging_Comment_OrmModel> @blogging_Comment_FK_Comment_Author { get; set; }
        public List<blogging_Post_OrmModel> @blogging_Post_FK_Post_Editor { get; set; }

        public void Update(user_Person_PutValidationModel model)
        {
            @Nickname = model.@Nickname;
            @BirthDate = model.@BirthDate.Value;
            @RowVersion = model.@RowVersion;
        }
    }

    [Table("Blog", Schema = "blogging")]
    public class blogging_Blog_OrmModel
    {
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public System.Int32 @Id { get; set; }

        [Column("AuthorSSN", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(9)]
        [Required]
        public System.String @AuthorSSN { get; set; }

        [Column("AuthorFirstName", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        [Required]
        public System.String @AuthorFirstName { get; set; }

        [Column("AuthorLastName", Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        [Required]
        public System.String @AuthorLastName { get; set; }

        [Column("Title", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(200)]
        [Required]
        public System.String @Title { get; set; }

        [Column("Description", Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(500)]
        [Required]
        public System.String @Description { get; set; }

        [Column("URL", Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(1000)]
        [Required]
        public System.String @URL { get; set; }

        [Column("Founded", Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.DateTime @Founded { get; set; }

        public user_Person_OrmModel @user_Person_FK_Author_Blogs { get; set; }
        public List<blogging_Post_OrmModel> @blogging_Post_FK_Blog_Posts { get; set; }

        public void Update(blogging_Blog_PutValidationModel model)
        {
            @AuthorSSN = model.@AuthorSSN;
            @AuthorFirstName = model.@AuthorFirstName;
            @AuthorLastName = model.@AuthorLastName;
            @Title = model.@Title;
            @Description = model.@Description;
            @URL = model.@URL;
            @Founded = model.@Founded.Value;
        }
    }

    [Table("Post", Schema = "blogging")]
    public class blogging_Post_OrmModel
    {
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public System.Int32 @Id { get; set; }

        [Column("BlogId", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.Int32 @BlogId { get; set; }

        [Column("Title", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(200)]
        [Required]
        public System.String @Title { get; set; }

        [Column("Summary", Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(1000)]
        [Required]
        public System.String @Summary { get; set; }

        [Column("Content", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.String @Content { get; set; }

        [Column("EditorSSN", Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(9)]
        public System.String @EditorSSN { get; set; }

        [Column("EditorFirstName", Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        public System.String @EditorFirstName { get; set; }

        [Column("EditorLastName", Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        public System.String @EditorLastName { get; set; }

        [Column("Rating", Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.Byte @Rating { get; set; }

        public blogging_Blog_OrmModel @blogging_Blog_FK_Blog_Posts { get; set; }
        public user_Person_OrmModel @user_Person_FK_Post_Editor { get; set; }
        public List<blogging_Comment_OrmModel> @blogging_Comment_FK_Post_Comments { get; set; }
        public List<blogging_PostTag_OrmModel> @blogging_PostTag_FK_Post_PostTags { get; set; }

        public void Update(blogging_Post_PutValidationModel model)
        {
            @BlogId = model.@BlogId.Value;
            @Title = model.@Title;
            @Summary = model.@Summary;
            @Content = model.@Content;
            @EditorSSN = model.@EditorSSN;
            @EditorFirstName = model.@EditorFirstName;
            @EditorLastName = model.@EditorLastName;
            @Rating = model.@Rating.Value;
        }
    }

    [Table("Tag", Schema = "blogging")]
    public class blogging_Tag_OrmModel
    {
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public System.Int32 @Id { get; set; }

        [Column("Name", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(200)]
        [Required]
        public System.String @Name { get; set; }

        [Column("Description", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(500)]
        [Required]
        public System.String @Description { get; set; }

        public List<blogging_PostTag_OrmModel> @blogging_PostTag_FK_Tag_PostTags { get; set; }

        public void Update(blogging_Tag_PutValidationModel model)
        {
            @Name = model.@Name;
            @Description = model.@Description;
        }
    }

    [Table("PostTag", Schema = "blogging")]
    public class blogging_PostTag_OrmModel
    {
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public System.Int32 @Id { get; set; }

        [Column("PostId", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.Int32 @PostId { get; set; }

        [Column("TagId", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.Int32 @TagId { get; set; }

        public blogging_Post_OrmModel @blogging_Post_FK_Post_PostTags { get; set; }
        public blogging_Tag_OrmModel @blogging_Tag_FK_Tag_PostTags { get; set; }

        public void Update(blogging_PostTag_PutValidationModel model)
        {
            @PostId = model.@PostId.Value;
            @TagId = model.@TagId.Value;
        }
    }

    [Table("Comment", Schema = "blogging")]
    public class blogging_Comment_OrmModel
    {
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public System.Int32 @Id { get; set; }

        [Column("PostId", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.Int32 @PostId { get; set; }

        [Column("AuthorSSN", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(9)]
        [Required]
        public System.String @AuthorSSN { get; set; }

        [Column("AuthorFirstName", Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        [Required]
        public System.String @AuthorFirstName { get; set; }

        [Column("AuthorLastName", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        [Required]
        public System.String @AuthorLastName { get; set; }

        [Column("Content", Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.String @Content { get; set; }

        [Column("DateTime", Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public System.String @DateTime { get; set; }

        public blogging_Post_OrmModel @blogging_Post_FK_Post_Comments { get; set; }
        public user_Person_OrmModel @user_Person_FK_Comment_Author { get; set; }

        public void Update(blogging_Comment_PutValidationModel model)
        {
            @PostId = model.@PostId.Value;
            @AuthorSSN = model.@AuthorSSN;
            @AuthorFirstName = model.@AuthorFirstName;
            @AuthorLastName = model.@AuthorLastName;
            @Content = model.@Content;
            @DateTime = model.@DateTime;
        }
    }

    public class user_Person_PostValidationModel
    {
        [Required]
        [MaxLength(9)]
        public System.String @SSN { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @LastName { get; set; }

        [MaxLength(100)]
        public System.String @Nickname { get; set; }

        [Required]
        public System.Nullable<System.DateTime> @BirthDate { get; set; }

        public user_Person_OrmModel ToOrmModel()
        {
            var ormModel = new user_Person_OrmModel();
            ormModel.@SSN = @SSN;
            ormModel.@FirstName = @FirstName;
            ormModel.@LastName = @LastName;
            ormModel.@Nickname = @Nickname;
            ormModel.@BirthDate = @BirthDate.Value;
            return ormModel;
        }
    }

    public class blogging_Blog_PostValidationModel
    {
        [Required]
        [MaxLength(9)]
        public System.String @AuthorSSN { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @AuthorFirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @AuthorLastName { get; set; }

        [Required]
        [MaxLength(200)]
        public System.String @Title { get; set; }

        [Required]
        [MaxLength(500)]
        public System.String @Description { get; set; }

        [Required]
        [MaxLength(1000)]
        public System.String @URL { get; set; }

        [Required]
        public System.Nullable<System.DateTime> @Founded { get; set; }

        public blogging_Blog_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_Blog_OrmModel();
            ormModel.@AuthorSSN = @AuthorSSN;
            ormModel.@AuthorFirstName = @AuthorFirstName;
            ormModel.@AuthorLastName = @AuthorLastName;
            ormModel.@Title = @Title;
            ormModel.@Description = @Description;
            ormModel.@URL = @URL;
            ormModel.@Founded = @Founded.Value;
            return ormModel;
        }
    }

    public class blogging_Post_PostValidationModel
    {
        [Required]
        public System.Nullable<System.Int32> @BlogId { get; set; }

        [Required]
        [MaxLength(200)]
        public System.String @Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public System.String @Summary { get; set; }

        [Required]
        public System.String @Content { get; set; }

        [MaxLength(9)]
        public System.String @EditorSSN { get; set; }

        [MaxLength(100)]
        public System.String @EditorFirstName { get; set; }

        [MaxLength(100)]
        public System.String @EditorLastName { get; set; }

        [Required]
        public System.Nullable<System.Byte> @Rating { get; set; }

        public blogging_Post_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_Post_OrmModel();
            ormModel.@BlogId = @BlogId.Value;
            ormModel.@Title = @Title;
            ormModel.@Summary = @Summary;
            ormModel.@Content = @Content;
            ormModel.@EditorSSN = @EditorSSN;
            ormModel.@EditorFirstName = @EditorFirstName;
            ormModel.@EditorLastName = @EditorLastName;
            ormModel.@Rating = @Rating.Value;
            return ormModel;
        }
    }

    public class blogging_Tag_PostValidationModel
    {
        [Required]
        [MaxLength(200)]
        public System.String @Name { get; set; }

        [Required]
        [MaxLength(500)]
        public System.String @Description { get; set; }

        public blogging_Tag_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_Tag_OrmModel();
            ormModel.@Name = @Name;
            ormModel.@Description = @Description;
            return ormModel;
        }
    }

    public class blogging_PostTag_PostValidationModel
    {
        [Required]
        public System.Nullable<System.Int32> @PostId { get; set; }

        [Required]
        public System.Nullable<System.Int32> @TagId { get; set; }

        public blogging_PostTag_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_PostTag_OrmModel();
            ormModel.@PostId = @PostId.Value;
            ormModel.@TagId = @TagId.Value;
            return ormModel;
        }
    }

    public class blogging_Comment_PostValidationModel
    {
        [Required]
        public System.Nullable<System.Int32> @PostId { get; set; }

        [Required]
        [MaxLength(9)]
        public System.String @AuthorSSN { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @AuthorFirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @AuthorLastName { get; set; }

        [Required]
        public System.String @Content { get; set; }

        [Required]
        public System.String @DateTime { get; set; }

        public blogging_Comment_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_Comment_OrmModel();
            ormModel.@PostId = @PostId.Value;
            ormModel.@AuthorSSN = @AuthorSSN;
            ormModel.@AuthorFirstName = @AuthorFirstName;
            ormModel.@AuthorLastName = @AuthorLastName;
            ormModel.@Content = @Content;
            ormModel.@DateTime = @DateTime;
            return ormModel;
        }
    }

    public class user_Person_PutValidationModel
    {
        [Required]
        [MaxLength(9)]
        public System.String @SSN { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @LastName { get; set; }

        [MaxLength(100)]
        public System.String @Nickname { get; set; }

        [Required]
        public System.Nullable<System.DateTime> @BirthDate { get; set; }

        [Required]
        public System.Byte[] @RowVersion { get; set; }

        public user_Person_OrmModel ToOrmModel()
        {
            var ormModel = new user_Person_OrmModel();
            ormModel.@SSN = @SSN;
            ormModel.@FirstName = @FirstName;
            ormModel.@LastName = @LastName;
            ormModel.@Nickname = @Nickname;
            ormModel.@BirthDate = @BirthDate.Value;
            ormModel.@RowVersion = @RowVersion;
            return ormModel;
        }
    }

    public class blogging_Blog_PutValidationModel
    {
        [Required]
        public System.Nullable<System.Int32> @Id { get; set; }

        [Required]
        [MaxLength(9)]
        public System.String @AuthorSSN { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @AuthorFirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @AuthorLastName { get; set; }

        [Required]
        [MaxLength(200)]
        public System.String @Title { get; set; }

        [Required]
        [MaxLength(500)]
        public System.String @Description { get; set; }

        [Required]
        [MaxLength(1000)]
        public System.String @URL { get; set; }

        [Required]
        public System.Nullable<System.DateTime> @Founded { get; set; }

        public blogging_Blog_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_Blog_OrmModel();
            ormModel.@Id = @Id.Value;
            ormModel.@AuthorSSN = @AuthorSSN;
            ormModel.@AuthorFirstName = @AuthorFirstName;
            ormModel.@AuthorLastName = @AuthorLastName;
            ormModel.@Title = @Title;
            ormModel.@Description = @Description;
            ormModel.@URL = @URL;
            ormModel.@Founded = @Founded.Value;
            return ormModel;
        }
    }

    public class blogging_Post_PutValidationModel
    {
        [Required]
        public System.Nullable<System.Int32> @Id { get; set; }

        [Required]
        public System.Nullable<System.Int32> @BlogId { get; set; }

        [Required]
        [MaxLength(200)]
        public System.String @Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public System.String @Summary { get; set; }

        [Required]
        public System.String @Content { get; set; }

        [MaxLength(9)]
        public System.String @EditorSSN { get; set; }

        [MaxLength(100)]
        public System.String @EditorFirstName { get; set; }

        [MaxLength(100)]
        public System.String @EditorLastName { get; set; }

        [Required]
        public System.Nullable<System.Byte> @Rating { get; set; }

        public blogging_Post_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_Post_OrmModel();
            ormModel.@Id = @Id.Value;
            ormModel.@BlogId = @BlogId.Value;
            ormModel.@Title = @Title;
            ormModel.@Summary = @Summary;
            ormModel.@Content = @Content;
            ormModel.@EditorSSN = @EditorSSN;
            ormModel.@EditorFirstName = @EditorFirstName;
            ormModel.@EditorLastName = @EditorLastName;
            ormModel.@Rating = @Rating.Value;
            return ormModel;
        }
    }

    public class blogging_Tag_PutValidationModel
    {
        [Required]
        public System.Nullable<System.Int32> @Id { get; set; }

        [Required]
        [MaxLength(200)]
        public System.String @Name { get; set; }

        [Required]
        [MaxLength(500)]
        public System.String @Description { get; set; }

        public blogging_Tag_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_Tag_OrmModel();
            ormModel.@Id = @Id.Value;
            ormModel.@Name = @Name;
            ormModel.@Description = @Description;
            return ormModel;
        }
    }

    public class blogging_PostTag_PutValidationModel
    {
        [Required]
        public System.Nullable<System.Int32> @Id { get; set; }

        [Required]
        public System.Nullable<System.Int32> @PostId { get; set; }

        [Required]
        public System.Nullable<System.Int32> @TagId { get; set; }

        public blogging_PostTag_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_PostTag_OrmModel();
            ormModel.@Id = @Id.Value;
            ormModel.@PostId = @PostId.Value;
            ormModel.@TagId = @TagId.Value;
            return ormModel;
        }
    }

    public class blogging_Comment_PutValidationModel
    {
        [Required]
        public System.Nullable<System.Int32> @Id { get; set; }

        [Required]
        public System.Nullable<System.Int32> @PostId { get; set; }

        [Required]
        [MaxLength(9)]
        public System.String @AuthorSSN { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @AuthorFirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public System.String @AuthorLastName { get; set; }

        [Required]
        public System.String @Content { get; set; }

        [Required]
        public System.String @DateTime { get; set; }

        public blogging_Comment_OrmModel ToOrmModel()
        {
            var ormModel = new blogging_Comment_OrmModel();
            ormModel.@Id = @Id.Value;
            ormModel.@PostId = @PostId.Value;
            ormModel.@AuthorSSN = @AuthorSSN;
            ormModel.@AuthorFirstName = @AuthorFirstName;
            ormModel.@AuthorLastName = @AuthorLastName;
            ormModel.@Content = @Content;
            ormModel.@DateTime = @DateTime;
            return ormModel;
        }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<user_Person_OrmModel> user_Person_DBSet { get; set; }
        public DbSet<blogging_Blog_OrmModel> blogging_Blog_DBSet { get; set; }
        public DbSet<blogging_Post_OrmModel> blogging_Post_DBSet { get; set; }
        public DbSet<blogging_Tag_OrmModel> blogging_Tag_DBSet { get; set; }
        public DbSet<blogging_PostTag_OrmModel> blogging_PostTag_DBSet { get; set; }
        public DbSet<blogging_Comment_OrmModel> blogging_Comment_DBSet { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                modelBuilder.Entity<user_Person_OrmModel>().HasKey(o => new {o.@SSN, o.@FirstName, o.@LastName});
                modelBuilder.Entity<user_Person_OrmModel>().HasMany(o => o.@blogging_Blog_FK_Author_Blogs)
                    .WithOne(o => o.@user_Person_FK_Author_Blogs)
                    .HasForeignKey("AuthorSSN", "AuthorFirstName", "AuthorLastName").OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Author_Blogs");
                modelBuilder.Entity<user_Person_OrmModel>().HasMany(o => o.@blogging_Comment_FK_Comment_Author)
                    .WithOne(o => o.@user_Person_FK_Comment_Author)
                    .HasForeignKey("AuthorSSN", "AuthorFirstName", "AuthorLastName").OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Comment_Author");
                modelBuilder.Entity<user_Person_OrmModel>().HasMany(o => o.@blogging_Post_FK_Post_Editor)
                    .WithOne(o => o.@user_Person_FK_Post_Editor)
                    .HasForeignKey("EditorSSN", "EditorFirstName", "EditorLastName").OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Post_Editor");
                modelBuilder.Entity<blogging_Blog_OrmModel>().HasKey(o => new {o.@Id});
                modelBuilder.Entity<blogging_Blog_OrmModel>().HasMany(o => o.@blogging_Post_FK_Blog_Posts)
                    .WithOne(o => o.@blogging_Blog_FK_Blog_Posts).HasForeignKey("BlogId")
                    .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Blog_Posts");
                modelBuilder.Entity<blogging_Post_OrmModel>().HasKey(o => new {o.@Id});
                modelBuilder.Entity<blogging_Post_OrmModel>().HasMany(o => o.@blogging_Comment_FK_Post_Comments)
                    .WithOne(o => o.@blogging_Post_FK_Post_Comments).HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Post_Comments");
                modelBuilder.Entity<blogging_Post_OrmModel>().HasMany(o => o.@blogging_PostTag_FK_Post_PostTags)
                    .WithOne(o => o.@blogging_Post_FK_Post_PostTags).HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Post_PostTags");
                modelBuilder.Entity<blogging_Tag_OrmModel>().HasKey(o => new {o.@Id});
                modelBuilder.Entity<blogging_Tag_OrmModel>().HasMany(o => o.@blogging_PostTag_FK_Tag_PostTags)
                    .WithOne(o => o.@blogging_Tag_FK_Tag_PostTags).HasForeignKey("TagId")
                    .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Tag_PostTags");
                modelBuilder.Entity<blogging_PostTag_OrmModel>().HasKey(o => new {o.@Id});
                modelBuilder.Entity<blogging_Comment_OrmModel>().HasKey(o => new {o.@Id});
            }
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true).AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        private readonly IConfigurationRoot _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(_configuration.GetValue<string>("ConnectionString")));
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelStateValidationAttribute));
                options.Filters.Add(typeof(NullValidationAttribute));
            });
        }

        public void Configure(DatabaseContext context, IApplicationBuilder app)
        {
            context.Database.EnsureCreated();
            app.UseMvc();
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder().UseKestrel().UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration().UseStartup<Startup>().Build();
            host.Run();
        }
    }

    [Route("api/user_Person")]
    public class user_Person_Controller : Controller
    {
        private readonly DatabaseContext _context;

        public user_Person_Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<user_Person_OrmModel>> Get()
        {
            await _context.user_Person_DBSet.LoadAsync();
            return _context.user_Person_DBSet;
        }

        [HttpGet("{SSN}/{FirstName}/{LastName}")]
        public async Task<IActionResult> GetById(System.String @SSN, System.String @FirstName, System.String @LastName)
        {
            var entity = await _context.user_Person_DBSet.FindAsync(@SSN, @FirstName, @LastName);
            return entity == null ? (IActionResult) NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] user_Person_PostValidationModel entity)
        {
            var ormModel = entity.ToOrmModel();
            _context.user_Person_DBSet.Add(ormModel);
            await _context.SaveChangesAsync();
            return Created($"api/user_Person/{ormModel.@SSN}/{ormModel.@FirstName}/{ormModel.@LastName}", ormModel);
        }

        [HttpPut("{SSN}/{FirstName}/{LastName}")]
        public async Task<IActionResult> Put(System.String @SSN, System.String @FirstName, System.String @LastName,
            [FromBody] user_Person_PutValidationModel entity)
        {
            if (!(@SSN == entity.@SSN && @FirstName == entity.@FirstName && @LastName == entity.@LastName))
            {
                return BadRequest(new {error = "Entity identifier does not match route identifier."});
            }
            var existing = await _context.user_Person_DBSet.FindAsync(@SSN, @FirstName, @LastName);
            if (existing == null)
            {
                return NotFound();
            }
            _context.Entry(existing).State = EntityState.Detached;
            var ormModel = entity.ToOrmModel();
            _context.user_Person_DBSet.Attach(ormModel);
            _context.Entry(ormModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientObject = (user_Person_OrmModel) exceptionEntry.Entity;
                var databaseValues = exceptionEntry.GetDatabaseValues();
                if (databaseValues == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The specified entity was deleted.");
                }
                else
                {
                    var databaseObject = (user_Person_OrmModel) databaseValues.ToObject();
                    if (databaseObject.@Nickname != clientObject.@Nickname)
                    {
                        ModelState.AddModelError("Nickname", $"Current value: {databaseObject.Nickname}");
                    }
                    if (databaseObject.@BirthDate != clientObject.@BirthDate)
                    {
                        ModelState.AddModelError("BirthDate", $"Current value: {databaseObject.BirthDate}");
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("{SSN}/{FirstName}/{LastName}")]
        public async Task<IActionResult> Delete(System.String @SSN, System.String @FirstName, System.String @LastName)
        {
            var entity = await _context.user_Person_DBSet.FindAsync(@SSN, @FirstName, @LastName);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    [Route("api/blogging_Blog")]
    public class blogging_Blog_Controller : Controller
    {
        private readonly DatabaseContext _context;

        public blogging_Blog_Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<blogging_Blog_OrmModel>> Get()
        {
            await _context.blogging_Blog_DBSet.LoadAsync();
            return _context.blogging_Blog_DBSet;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(System.Int32 @Id)
        {
            var entity = await _context.blogging_Blog_DBSet.FindAsync(@Id);
            return entity == null ? (IActionResult) NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] blogging_Blog_PostValidationModel entity)
        {
            var ormModel = entity.ToOrmModel();
            _context.blogging_Blog_DBSet.Add(ormModel);
            await _context.SaveChangesAsync();
            return Created($"api/blogging_Blog/{ormModel.@Id}", ormModel);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(System.Int32 @Id, [FromBody] blogging_Blog_PutValidationModel entity)
        {
            if (!(@Id == entity.@Id))
            {
                return BadRequest(new {error = "Entity identifier does not match route identifier."});
            }
            var existing = await _context.blogging_Blog_DBSet.FindAsync(@Id);
            if (existing == null)
            {
                return NotFound();
            }
            _context.Entry(existing).State = EntityState.Detached;
            var ormModel = entity.ToOrmModel();
            _context.blogging_Blog_DBSet.Attach(ormModel);
            _context.Entry(ormModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientObject = (blogging_Blog_OrmModel) exceptionEntry.Entity;
                var databaseValues = exceptionEntry.GetDatabaseValues();
                if (databaseValues == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The specified entity was deleted.");
                }
                else
                {
                    var databaseObject = (blogging_Blog_OrmModel) databaseValues.ToObject();
                    if (databaseObject.@AuthorSSN != clientObject.@AuthorSSN)
                    {
                        ModelState.AddModelError("AuthorSSN", $"Current value: {databaseObject.AuthorSSN}");
                    }
                    if (databaseObject.@AuthorFirstName != clientObject.@AuthorFirstName)
                    {
                        ModelState.AddModelError("AuthorFirstName", $"Current value: {databaseObject.AuthorFirstName}");
                    }
                    if (databaseObject.@AuthorLastName != clientObject.@AuthorLastName)
                    {
                        ModelState.AddModelError("AuthorLastName", $"Current value: {databaseObject.AuthorLastName}");
                    }
                    if (databaseObject.@Title != clientObject.@Title)
                    {
                        ModelState.AddModelError("Title", $"Current value: {databaseObject.Title}");
                    }
                    if (databaseObject.@Description != clientObject.@Description)
                    {
                        ModelState.AddModelError("Description", $"Current value: {databaseObject.Description}");
                    }
                    if (databaseObject.@URL != clientObject.@URL)
                    {
                        ModelState.AddModelError("URL", $"Current value: {databaseObject.URL}");
                    }
                    if (databaseObject.@Founded != clientObject.@Founded)
                    {
                        ModelState.AddModelError("Founded", $"Current value: {databaseObject.Founded}");
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(System.Int32 @Id)
        {
            var entity = await _context.blogging_Blog_DBSet.FindAsync(@Id);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    [Route("api/blogging_Post")]
    public class blogging_Post_Controller : Controller
    {
        private readonly DatabaseContext _context;

        public blogging_Post_Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<blogging_Post_OrmModel>> Get()
        {
            await _context.blogging_Post_DBSet.LoadAsync();
            return _context.blogging_Post_DBSet;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(System.Int32 @Id)
        {
            var entity = await _context.blogging_Post_DBSet.FindAsync(@Id);
            return entity == null ? (IActionResult) NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] blogging_Post_PostValidationModel entity)
        {
            var ormModel = entity.ToOrmModel();
            _context.blogging_Post_DBSet.Add(ormModel);
            await _context.SaveChangesAsync();
            return Created($"api/blogging_Post/{ormModel.@Id}", ormModel);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(System.Int32 @Id, [FromBody] blogging_Post_PutValidationModel entity)
        {
            if (!(@Id == entity.@Id))
            {
                return BadRequest(new {error = "Entity identifier does not match route identifier."});
            }
            var existing = await _context.blogging_Post_DBSet.FindAsync(@Id);
            if (existing == null)
            {
                return NotFound();
            }
            _context.Entry(existing).State = EntityState.Detached;
            var ormModel = entity.ToOrmModel();
            _context.blogging_Post_DBSet.Attach(ormModel);
            _context.Entry(ormModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientObject = (blogging_Post_OrmModel) exceptionEntry.Entity;
                var databaseValues = exceptionEntry.GetDatabaseValues();
                if (databaseValues == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The specified entity was deleted.");
                }
                else
                {
                    var databaseObject = (blogging_Post_OrmModel) databaseValues.ToObject();
                    if (databaseObject.@BlogId != clientObject.@BlogId)
                    {
                        ModelState.AddModelError("BlogId", $"Current value: {databaseObject.BlogId}");
                    }
                    if (databaseObject.@Title != clientObject.@Title)
                    {
                        ModelState.AddModelError("Title", $"Current value: {databaseObject.Title}");
                    }
                    if (databaseObject.@Summary != clientObject.@Summary)
                    {
                        ModelState.AddModelError("Summary", $"Current value: {databaseObject.Summary}");
                    }
                    if (databaseObject.@Content != clientObject.@Content)
                    {
                        ModelState.AddModelError("Content", $"Current value: {databaseObject.Content}");
                    }
                    if (databaseObject.@EditorSSN != clientObject.@EditorSSN)
                    {
                        ModelState.AddModelError("EditorSSN", $"Current value: {databaseObject.EditorSSN}");
                    }
                    if (databaseObject.@EditorFirstName != clientObject.@EditorFirstName)
                    {
                        ModelState.AddModelError("EditorFirstName", $"Current value: {databaseObject.EditorFirstName}");
                    }
                    if (databaseObject.@EditorLastName != clientObject.@EditorLastName)
                    {
                        ModelState.AddModelError("EditorLastName", $"Current value: {databaseObject.EditorLastName}");
                    }
                    if (databaseObject.@Rating != clientObject.@Rating)
                    {
                        ModelState.AddModelError("Rating", $"Current value: {databaseObject.Rating}");
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(System.Int32 @Id)
        {
            var entity = await _context.blogging_Post_DBSet.FindAsync(@Id);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    [Route("api/blogging_Tag")]
    public class blogging_Tag_Controller : Controller
    {
        private readonly DatabaseContext _context;

        public blogging_Tag_Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<blogging_Tag_OrmModel>> Get()
        {
            await _context.blogging_Tag_DBSet.LoadAsync();
            return _context.blogging_Tag_DBSet;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(System.Int32 @Id)
        {
            var entity = await _context.blogging_Tag_DBSet.FindAsync(@Id);
            return entity == null ? (IActionResult) NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] blogging_Tag_PostValidationModel entity)
        {
            var ormModel = entity.ToOrmModel();
            _context.blogging_Tag_DBSet.Add(ormModel);
            await _context.SaveChangesAsync();
            return Created($"api/blogging_Tag/{ormModel.@Id}", ormModel);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(System.Int32 @Id, [FromBody] blogging_Tag_PutValidationModel entity)
        {
            if (!(@Id == entity.@Id))
            {
                return BadRequest(new {error = "Entity identifier does not match route identifier."});
            }
            var existing = await _context.blogging_Tag_DBSet.FindAsync(@Id);
            if (existing == null)
            {
                return NotFound();
            }
            _context.Entry(existing).State = EntityState.Detached;
            var ormModel = entity.ToOrmModel();
            _context.blogging_Tag_DBSet.Attach(ormModel);
            _context.Entry(ormModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientObject = (blogging_Tag_OrmModel) exceptionEntry.Entity;
                var databaseValues = exceptionEntry.GetDatabaseValues();
                if (databaseValues == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The specified entity was deleted.");
                }
                else
                {
                    var databaseObject = (blogging_Tag_OrmModel) databaseValues.ToObject();
                    if (databaseObject.@Name != clientObject.@Name)
                    {
                        ModelState.AddModelError("Name", $"Current value: {databaseObject.Name}");
                    }
                    if (databaseObject.@Description != clientObject.@Description)
                    {
                        ModelState.AddModelError("Description", $"Current value: {databaseObject.Description}");
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(System.Int32 @Id)
        {
            var entity = await _context.blogging_Tag_DBSet.FindAsync(@Id);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    [Route("api/blogging_PostTag")]
    public class blogging_PostTag_Controller : Controller
    {
        private readonly DatabaseContext _context;

        public blogging_PostTag_Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<blogging_PostTag_OrmModel>> Get()
        {
            await _context.blogging_PostTag_DBSet.LoadAsync();
            return _context.blogging_PostTag_DBSet;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(System.Int32 @Id)
        {
            var entity = await _context.blogging_PostTag_DBSet.FindAsync(@Id);
            return entity == null ? (IActionResult) NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] blogging_PostTag_PostValidationModel entity)
        {
            var ormModel = entity.ToOrmModel();
            _context.blogging_PostTag_DBSet.Add(ormModel);
            await _context.SaveChangesAsync();
            return Created($"api/blogging_PostTag/{ormModel.@Id}", ormModel);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(System.Int32 @Id, [FromBody] blogging_PostTag_PutValidationModel entity)
        {
            if (!(@Id == entity.@Id))
            {
                return BadRequest(new {error = "Entity identifier does not match route identifier."});
            }
            var existing = await _context.blogging_PostTag_DBSet.FindAsync(@Id);
            if (existing == null)
            {
                return NotFound();
            }
            _context.Entry(existing).State = EntityState.Detached;
            var ormModel = entity.ToOrmModel();
            _context.blogging_PostTag_DBSet.Attach(ormModel);
            _context.Entry(ormModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientObject = (blogging_PostTag_OrmModel) exceptionEntry.Entity;
                var databaseValues = exceptionEntry.GetDatabaseValues();
                if (databaseValues == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The specified entity was deleted.");
                }
                else
                {
                    var databaseObject = (blogging_PostTag_OrmModel) databaseValues.ToObject();
                    if (databaseObject.@PostId != clientObject.@PostId)
                    {
                        ModelState.AddModelError("PostId", $"Current value: {databaseObject.PostId}");
                    }
                    if (databaseObject.@TagId != clientObject.@TagId)
                    {
                        ModelState.AddModelError("TagId", $"Current value: {databaseObject.TagId}");
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(System.Int32 @Id)
        {
            var entity = await _context.blogging_PostTag_DBSet.FindAsync(@Id);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    [Route("api/blogging_Comment")]
    public class blogging_Comment_Controller : Controller
    {
        private readonly DatabaseContext _context;

        public blogging_Comment_Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<blogging_Comment_OrmModel>> Get()
        {
            await _context.blogging_Comment_DBSet.LoadAsync();
            return _context.blogging_Comment_DBSet;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(System.Int32 @Id)
        {
            var entity = await _context.blogging_Comment_DBSet.FindAsync(@Id);
            return entity == null ? (IActionResult) NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] blogging_Comment_PostValidationModel entity)
        {
            var ormModel = entity.ToOrmModel();
            _context.blogging_Comment_DBSet.Add(ormModel);
            await _context.SaveChangesAsync();
            return Created($"api/blogging_Comment/{ormModel.@Id}", ormModel);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(System.Int32 @Id, [FromBody] blogging_Comment_PutValidationModel entity)
        {
            if (!(@Id == entity.@Id))
            {
                return BadRequest(new {error = "Entity identifier does not match route identifier."});
            }
            var existing = await _context.blogging_Comment_DBSet.FindAsync(@Id);
            if (existing == null)
            {
                return NotFound();
            }
            _context.Entry(existing).State = EntityState.Detached;
            var ormModel = entity.ToOrmModel();
            _context.blogging_Comment_DBSet.Attach(ormModel);
            _context.Entry(ormModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientObject = (blogging_Comment_OrmModel) exceptionEntry.Entity;
                var databaseValues = exceptionEntry.GetDatabaseValues();
                if (databaseValues == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The specified entity was deleted.");
                }
                else
                {
                    var databaseObject = (blogging_Comment_OrmModel) databaseValues.ToObject();
                    if (databaseObject.@PostId != clientObject.@PostId)
                    {
                        ModelState.AddModelError("PostId", $"Current value: {databaseObject.PostId}");
                    }
                    if (databaseObject.@AuthorSSN != clientObject.@AuthorSSN)
                    {
                        ModelState.AddModelError("AuthorSSN", $"Current value: {databaseObject.AuthorSSN}");
                    }
                    if (databaseObject.@AuthorFirstName != clientObject.@AuthorFirstName)
                    {
                        ModelState.AddModelError("AuthorFirstName", $"Current value: {databaseObject.AuthorFirstName}");
                    }
                    if (databaseObject.@AuthorLastName != clientObject.@AuthorLastName)
                    {
                        ModelState.AddModelError("AuthorLastName", $"Current value: {databaseObject.AuthorLastName}");
                    }
                    if (databaseObject.@Content != clientObject.@Content)
                    {
                        ModelState.AddModelError("Content", $"Current value: {databaseObject.Content}");
                    }
                    if (databaseObject.@DateTime != clientObject.@DateTime)
                    {
                        ModelState.AddModelError("DateTime", $"Current value: {databaseObject.DateTime}");
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(System.Int32 @Id)
        {
            var entity = await _context.blogging_Comment_DBSet.FindAsync(@Id);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    public class ModelStateValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            base.OnActionExecuting(context);
        }
    }

    public class NullValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var nullArguments = context.ActionArguments.Where(a => a.Value == null).ToList();
            if (nullArguments.Any())
            {
                context.Result = new BadRequestObjectResult(new
                {
                    arguments = nullArguments.Select(a => $"The {a.Key} argument is required.")
                });
            }
            base.OnActionExecuting(context);
        }
    }
}
```

## Built With

* [.NET Compiler Platform ("Roslyn")](https://github.com/dotnet/roslyn)

## Contributing

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/sergey-kolodiy/PolyGen/tags).

## Authors

* **Sergey Kolodiy**

See also the list of [contributors](https://github.com/sergey-kolodiy/PolyGen/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments
