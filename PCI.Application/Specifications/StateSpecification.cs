using PCI.Domain.Models;

namespace PCI.Application.Specifications;

public class StateSpecification : BaseSpecification<State>
{
    public StateSpecification(int? countryId = null)
    {
        if (countryId.HasValue)
        {
            AddCriteria(s => s.CountryId == countryId.Value);
        }

        ApplyIncludes();
        ApplyOrdering();
    }

    private void ApplyIncludes()
    {
        AddInclude(s => s.Country);
    }

    private void ApplyOrdering()
    {
        ApplyOrderBy(s => s.Name);
    }
}