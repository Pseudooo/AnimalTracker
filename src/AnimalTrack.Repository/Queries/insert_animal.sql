insert into animals (Name) 
    values (@Name)
    returning Id, CreatedAt;