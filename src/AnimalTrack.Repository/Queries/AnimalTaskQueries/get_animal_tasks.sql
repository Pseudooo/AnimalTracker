select
    Id,
    AnimalId,
    Name,
    CreatedAt
    from AnimalTasks
    where AnimalId = @AnimalId