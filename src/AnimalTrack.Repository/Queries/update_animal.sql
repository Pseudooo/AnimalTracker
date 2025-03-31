update animals
    set
        Name = @name
        where Id = @Id
    returning Id, Name, CreatedAt
