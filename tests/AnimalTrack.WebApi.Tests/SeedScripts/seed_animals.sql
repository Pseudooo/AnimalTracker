insert into animals (name, createdat)
    values
        ('Alice', NOW()),
        ('Bob', NOW()),
        ('John', NOW()),
        ('Alan', NOW());

insert into AnimalNotes (AnimalId, Note, CreatedAt)
    values
        (1, 'This is a note', NOW()),
        (1, 'This is my second note', NOW()),
        (4, 'I will delete this note', NOW());