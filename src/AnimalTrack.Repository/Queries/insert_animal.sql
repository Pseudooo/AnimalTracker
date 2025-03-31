insert into animals (Name) 
    values (@Name)
    returning Id, Name, CreatedAt;