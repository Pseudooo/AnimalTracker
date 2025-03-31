create table if not exists Animals
(
    Id        integer generated always as identity
        constraint Animals_pk
            primary key,
    Name      text                                   not null,
    CreatedAt timestamp with time zone default now() not null
);

