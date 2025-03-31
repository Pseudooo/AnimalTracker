create table if not exists AnimalNotes
(
    Id integer generated always as identity
        constraint AnimalNotes_pk
            primary key,
    AnimalId integer not null 
        references animals (Id),
    Note text not null,
    CreatedAt timestamp with time zone default now() not null
);

