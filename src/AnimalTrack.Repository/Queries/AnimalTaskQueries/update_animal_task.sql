update AnimalTasks
    set
        Name = @Name
    where Id = @TaskId
    returning Id, AnimalId, Name, CreatedAt;