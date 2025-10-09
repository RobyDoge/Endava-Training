using System.ComponentModel.DataAnnotations;

namespace Basics.Base;

public abstract class Person
{
    public Guid Id { get; } = Guid.NewGuid();
    public required string Name { get; set; }


}
