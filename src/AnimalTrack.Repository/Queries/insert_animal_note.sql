insert into AnimalNotes (AnimalId, Note)
    values (@AnimalId, @Note)
    returning Id, CreatedAt;