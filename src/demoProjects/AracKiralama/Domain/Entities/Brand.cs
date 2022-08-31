using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Brand : Entity
{
    public Brand()
    {
    }

    public Brand(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
}