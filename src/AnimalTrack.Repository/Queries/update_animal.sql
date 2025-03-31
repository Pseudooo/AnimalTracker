update animals
    set
        Name = @Name
        where Id = @Id
    returning Id, Name, CreatedAt
