insert into animals (name, createdat)
    values
        ('Alice', NOW()),
        ('Bob', NOW()),
        ('John', NOW());

insert into AnimalNotes (AnimalId, Note, CreatedAt)
    values
        (1, 'This is a note', NOW()),
        (1, 'This is my second note', NOW());