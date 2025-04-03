insert into animals (name, createdat)
    values
        ('Alice', NOW()),
        ('Bob', NOW()),
        ('John', NOW()),
        ('Alan', NOW()),
        ('James', NOW());

insert into AnimalNotes (AnimalId, Note, CreatedAt)
    values
        (1, 'This is a note', NOW()),
        (1, 'This is my second note', NOW()),
        (4, 'I will delete this note', NOW());

insert into AnimalTasks (AnimalId, Name, CreatedAt)
    values
        (5, 'Feed me', NOW()),
        (5, 'Wash me', NOW());