# Customer Entity Redesign

## Overview
The Customer entity has been completely redesigned to eliminate foreign key constraint issues and improve maintainability.

## Key Changes

### 1. New Customer-Specific Entities
- **CustomerContact** - Replaces BusinessContact with EntityType/EntityId pattern
- **CustomerAddress** - Replaces BusinessAddress with EntityType/EntityId pattern  
- **CustomerTaxInfo** - Replaces BusinessTaxInfo with EntityType/EntityId pattern
- **CustomerBankInfo** - Replaces BusinessBankInfo with EntityType/EntityId pattern

### 2. Simplified Cascade Delete Strategy
- **Cascade Delete**: Customer → CustomerContact, CustomerAddress, CustomerTaxInfo, CustomerBankInfo, CustomerFinancial, CustomerDocument, CustomerPriceList
- **Restrict Delete**: Customer → SalesOrder, Invoice (prevents accidental deletion of transaction data)

### 3. Clean Entity Relationships
- Direct foreign key relationships instead of generic EntityType/EntityId pattern
- Clear one-to-many and one-to-one relationships
- Proper cascade delete configuration

## Benefits

### 1. Eliminates Foreign Key Errors
- No more complex manual deletion chains
- EF Core handles cascade deletes automatically
- Cleaner delete operation (10 lines vs 160 lines)

### 2. Better Performance
- Direct foreign key relationships are faster than entity queries
- Proper indexes on all foreign key columns
- Single transaction for delete operations

### 3. Improved Maintainability
- Entity-specific configurations
- Clear relationship definitions
- Type-safe navigation properties

## Migration Steps

### 1. Generate Migration
```bash
dotnet ef migrations add CustomerEntityRedesign
```

### 2. Review Migration
The migration will:
- Create new customer-specific tables
- Migrate data from generic business tables
- Drop old relationships
- Add new foreign key constraints

### 3. Update Database
```bash
dotnet ef database update
```

## Files Changed

### Domain Models
- `Customer.cs` - Redesigned with new relationships
- `CustomerContact.cs` - New entity (replaces BusinessContact)
- `CustomerAddress.cs` - New entity (replaces BusinessAddress)  
- `CustomerTaxInfo.cs` - New entity (replaces BusinessTaxInfo)
- `CustomerBankInfo.cs` - New entity (replaces BusinessBankInfo)

### Configurations
- `CustomerConfiguration.cs` - Updated with proper cascade deletes
- `CustomerContactConfiguration.cs` - New configuration
- `CustomerAddressConfiguration.cs` - New configuration
- `CustomerTaxInfoConfiguration.cs` - New configuration
- `CustomerBankInfoConfiguration.cs` - New configuration
- `SalesOrderConfiguration.cs` - Updated to use Restrict delete
- `InvoiceConfiguration.cs` - Updated to use Restrict delete

### Service Layer
- `CustomerService.cs` - Updated to use new entities and simplified delete logic

## Testing Required

1. **Create Customer** - Verify new entities are created properly
2. **Update Customer** - Verify contact/address updates work
3. **Delete Customer** - Verify cascade delete works without FK errors
4. **Delete with Transactions** - Verify restriction works for customers with sales orders/invoices
5. **Performance Testing** - Verify improved query performance

## Rollback Plan

If issues arise, you can:
1. Revert the migration: `dotnet ef migrations remove`
2. Restore original entity files from git
3. Use raw SQL deletion as temporary workaround

## Next Steps

1. Generate and apply migration
2. Test all CRUD operations
3. Update any external references to BusinessContact/BusinessAddress patterns
4. Update documentation and API contracts if needed