insert into animals (name, createdat)
values
    ('Alice', NOW()),
    ('Bob', NOW()),
    ('John', NOW()),
    ('Sam', NOW());

insert into AnimalTasks (AnimalId, Name, CreatedAt)
values
    (1, 'Feed me', NOW()),
    (1, 'Wash me', NOW()),
    (4, 'Original Task', NOW());