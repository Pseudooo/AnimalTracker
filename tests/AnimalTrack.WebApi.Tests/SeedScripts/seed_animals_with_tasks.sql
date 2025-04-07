insert into animals (name, createdat)
values
    ('Alice', NOW()),
    ('Bob', NOW()),
    ('John', NOW()),
    ('Sam', NOW());

insert into AnimalTasks (AnimalId, Name, CreatedAt, Frequency, ScheduledFor)
values
    (1, 'Feed me', NOW(), 'OneOff', '2025-08-27'),
    (1, 'Wash me', NOW(), 'OneOff', '2025-08-27'),
    (4, 'Original Task', NOW(), 'OneOff', '2025-08-27'),
    (4, 'Original Task', NOW(), 'OneOff', '2025-08-27');