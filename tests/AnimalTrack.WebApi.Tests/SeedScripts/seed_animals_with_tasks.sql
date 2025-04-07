insert into animals (name, createdat)
values
    ('Alice', NOW()),
    ('Bob', NOW()),
    ('John', NOW()),
    ('Sam', NOW());

insert into AnimalTasks (AnimalId, Name, CreatedAt, Frequency)
values
    (1, 'Feed me', NOW(), 'OneOff'),
    (1, 'Wash me', NOW(), 'OneOff'),
    (4, 'Original Task', NOW(), 'OneOff'),
    (4, 'Original Task', NOW(), 'OneOff');