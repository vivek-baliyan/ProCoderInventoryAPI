using PCI.Domain.Models;
using PCI.Shared.Common.Enums;

namespace PCI.Application.Specifications;

public class UnitOfMeasureDropdownSpecification : BaseSpecification<UnitOfMeasure>
{
    public UnitOfMeasureDropdownSpecification(int organisationId, int unitType)
        : base(u => u.IsActive &&
               (u.OrganisationId == organisationId || u.OrganisationId == null))
    {

        if (unitType > 0)
        {
            AddCriteria(p => p.UnitType == (UnitType)unitType);
        }

        ApplyDefaultSorting();
    }

    private void ApplyDefaultSorting()
    {
        ApplyOrderBy(u => u.Name);
    }
}