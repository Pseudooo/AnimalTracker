create table if not exists AnimalTasks
(
    Id integer generated always as identity
        constraint AnimalTasks_pk
            primary key,
    AnimalId integer not null
        references Animals (Id),
    Name text not null,
    CreatedAt timestamp with time zone not null
        default (now() at time zone 'utc')
)