insert into AnimalTasks (AnimalId, Name)
    values (@AnimalId, @Name)
    returning Id, AnimalId, Name, CreatedAt;