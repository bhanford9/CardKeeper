using CardManager.Models.Grading;
using CardManager.Models.StorageSpecification;

namespace CardManager.Models.Cards;

public class Card(Guid id, string name, IStorageSpecification storageSpecification, ICardGrade grade)
{
    public Guid Id { get; set; } = id;

    public string Name { get; set; } = name;

    public IStorageSpecification StorageSpecification { get; set; } = storageSpecification;

    public ICardGrade Grade { get; set; } = grade;
}
