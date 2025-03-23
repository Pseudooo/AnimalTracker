select
        Id,
        Name,
        CreatedAt
    from animals
        order by Id
        limit @Take
        offset @Skip;