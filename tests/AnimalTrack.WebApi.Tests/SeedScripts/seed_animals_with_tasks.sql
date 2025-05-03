insert into animals (name, createdat)
values
    ('Alice', NOW()),
    ('Bob', NOW()),
    ('John', NOW()),
    ('Sam', NOW()),
    ('Owen', NOW());

insert into AnimalTasks (AnimalId, Name, CreatedAt, Frequency, ScheduledFor, CompletedAt)
values
    (1, 'Feed me', NOW(), 'OneOff', '2025-08-27', null),
    (1, 'Wash me', NOW(), 'OneOff', '2025-08-27', null),
    (4, 'Original Task', NOW(), 'OneOff', '2025-08-27', null),
    (4, 'Original Task', NOW(), 'OneOff', '2025-08-27', null),
    (5, 'Owens completed task', NOW(), 'OneOff', '2025-08-27', NOW()),
    (5, 'Owens completed task', NOW(), 'OneOff', '2025-08-27', null);