using Mazad.SharedKernel.Domain;

namespace Mazad.Domain.Entities.Catalog;

/// <summary>
/// Hierarchical category for products, vehicles and auction lots
/// (e.g. Cars, Real Estate, Electronics, Furniture, Equipment).
/// </summary>
public class Category : AggregateRoot<Guid>, ISoftDeletable
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }

    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
