insert into AnimalNotes (AnimalId, Note)
    values (@AnimalId, @Note)
    returning Id, AnimalId, Note, CreatedAt;