using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Application.Specifications;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Vendor;

namespace PCI.Application.Services.Implementations;

public class VendorService(IUnitOfWork unitOfWork) : IVendorService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<bool>> CreateVendor(
        string userId,
        int organisationId,
        CreateVendorDto createVendorDto)
    {
        var existingVendor = await _unitOfWork.Repository<Vendor>()
            .GetFirstOrDefaultAsync(x => x.VendorCode == createVendorDto.VendorCode && x.OrganisationId == organisationId);

        if (existingVendor != null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorAlreadyExists, "Vendor with this code already exists"));
        }

        // Check if email already exists for this organization
        if (!string.IsNullOrEmpty(createVendorDto.Email))
        {
            var existingEmail = await _unitOfWork.Repository<BusinessContact>()
                .GetFirstOrDefaultAsync(x => x.Email == createVendorDto.Email &&
                                            x.EntityType == "Vendor" &&
                                            x.OrganisationId == organisationId);

            if (existingEmail != null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorEmailExists, "Vendor with this email already exists"));
            }
        }

        try
        {
            var vendor = new Vendor
            {
                VendorCode = createVendorDto.VendorCode,
                VendorName = createVendorDto.VendorName,
                CompanyName = createVendorDto.CompanyName,
                WebsiteUrl = createVendorDto.WebsiteUrl,
                VendorType = createVendorDto.VendorType,
                Category = createVendorDto.Category,
                Status = createVendorDto.Status,
                Industry = createVendorDto.Industry,
                ParentVendorId = createVendorDto.ParentVendorId,
                IsManufacturer = createVendorDto.IsManufacturer,
                IsDropshipVendor = createVendorDto.IsDropshipVendor,
                CurrencyId = createVendorDto.CurrencyId,
                PortalAccessEmail = createVendorDto.PortalAccessEmail,
                HasPortalAccess = createVendorDto.HasPortalAccess,
                PreferredCommunicationMethod = createVendorDto.PreferredCommunicationMethod,
                RequiresPOApproval = createVendorDto.RequiresPOApproval,
                Notes = createVendorDto.Notes,
                StatusChangedDate = DateTime.UtcNow,
                OrganisationId = organisationId,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            _unitOfWork.Repository<Vendor>().Add(vendor);
            await _unitOfWork.SaveChangesAsync();

            // Create BusinessContact if contact info provided
            if (!string.IsNullOrEmpty(createVendorDto.ContactPerson) ||
                !string.IsNullOrEmpty(createVendorDto.Email) ||
                !string.IsNullOrEmpty(createVendorDto.PhoneNumber) ||
                !string.IsNullOrEmpty(createVendorDto.MobileNumber))
            {
                var businessContact = new BusinessContact
                {
                    EntityType = "Vendor",
                    EntityId = vendor.Id,
                    ContactType = ContactType.Primary,
                    ContactPersonName = createVendorDto.ContactPerson ?? "N/A",
                    Email = createVendorDto.Email,
                    PhoneNumber = createVendorDto.PhoneNumber,
                    MobileNumber = createVendorDto.MobileNumber,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessContact>().Add(businessContact);
            }

            // Create BusinessAddress if address info provided
            if (!string.IsNullOrEmpty(createVendorDto.Address) ||
                !string.IsNullOrEmpty(createVendorDto.City) ||
                !string.IsNullOrEmpty(createVendorDto.State) ||
                !string.IsNullOrEmpty(createVendorDto.Country))
            {
                var businessAddress = new BusinessAddress
                {
                    EntityType = "Vendor",
                    EntityId = vendor.Id,
                    AddressType = AddressType.Office,
                    AddressLine1 = createVendorDto.Address ?? "N/A",
                    City = createVendorDto.City,
                    State = createVendorDto.State,
                    PostalCode = createVendorDto.PostalCode,
                    Country = createVendorDto.Country,
                    IsDefault = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessAddress>().Add(businessAddress);
            }

            // Create VendorFinancial if financial info provided
            if (createVendorDto.CreditLimit > 0 || createVendorDto.PaymentTermDays > 0 || createVendorDto.MinimumOrderValue > 0)
            {
                var vendorFinancial = new VendorFinancial
                {
                    VendorId = vendor.Id,
                    PaymentTermDays = createVendorDto.PaymentTermDays,
                    MinimumOrderValue = createVendorDto.MinimumOrderValue,
                    PreferredPaymentMethod = createVendorDto.PreferredPaymentMethod.ToString(),
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<VendorFinancial>().Add(vendorFinancial);
            }

            // Create BusinessTaxInfo if tax info provided
            var taxInfos = new List<BusinessTaxInfo>();
            if (!string.IsNullOrEmpty(createVendorDto.TaxIdentificationNumber))
            {
                taxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Vendor",
                    EntityId = vendor.Id,
                    TaxType = TaxType.TaxIdentificationNumber,
                    TaxNumber = createVendorDto.TaxIdentificationNumber,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(createVendorDto.GSTNumber))
            {
                taxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Vendor",
                    EntityId = vendor.Id,
                    TaxType = TaxType.GST,
                    TaxNumber = createVendorDto.GSTNumber,
                    IsPrimary = string.IsNullOrEmpty(createVendorDto.TaxIdentificationNumber),
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(createVendorDto.PANNumber))
            {
                taxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Vendor",
                    EntityId = vendor.Id,
                    TaxType = TaxType.PAN,
                    TaxNumber = createVendorDto.PANNumber,
                    IsPrimary = string.IsNullOrEmpty(createVendorDto.TaxIdentificationNumber) && string.IsNullOrEmpty(createVendorDto.GSTNumber),
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            foreach (var taxInfo in taxInfos)
            {
                _unitOfWork.Repository<BusinessTaxInfo>().Add(taxInfo);
            }

            // Create BusinessBankInfo if bank info provided
            if (!string.IsNullOrEmpty(createVendorDto.BankAccountNumber) ||
                !string.IsNullOrEmpty(createVendorDto.BankName))
            {
                var businessBankInfo = new BusinessBankInfo
                {
                    EntityType = "Vendor",
                    EntityId = vendor.Id,
                    BankAccountNumber = createVendorDto.BankAccountNumber ?? "N/A",
                    BankName = createVendorDto.BankName ?? "N/A",
                    BankBranch = createVendorDto.BankBranch,
                    IFSCCode = createVendorDto.IFSCCode,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessBankInfo>().Add(businessBankInfo);
            }

            // Create VendorPerformance if performance-related fields provided
            if (createVendorDto.IsPreferredVendor)
            {
                var vendorPerformance = new VendorPerformance
                {
                    VendorId = vendor.Id,
                    IsPreferredVendor = createVendorDto.IsPreferredVendor,
                    ReviewPeriodStart = DateTime.UtcNow,
                    ReviewPeriodEnd = DateTime.UtcNow.AddMonths(12),
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<VendorPerformance>().Add(vendorPerformance);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorCreationError, ex.Message, ex.ToString()));
        }
    }

    public async Task<ServiceResult<bool>> UpdateVendor(
        string userId,
        int organisationId,
        UpdateVendorDto updateVendorDto)
    {
        var existingVendor = await _unitOfWork.Repository<Vendor>()
            .GetFirstOrDefaultAsync(x => x.Id == updateVendorDto.Id && x.OrganisationId == organisationId);

        if (existingVendor == null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
        }

        // Check if vendor code already exists for another vendor
        var duplicateCode = await _unitOfWork.Repository<Vendor>()
            .GetFirstOrDefaultAsync(x => x.VendorCode == updateVendorDto.VendorCode &&
                                        x.Id != updateVendorDto.Id &&
                                        x.OrganisationId == organisationId);

        if (duplicateCode != null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorAlreadyExists, "Vendor with this code already exists"));
        }

        // Check if email already exists for another vendor
        if (!string.IsNullOrEmpty(updateVendorDto.Email))
        {
            var duplicateEmail = await _unitOfWork.Repository<BusinessContact>()
                .GetFirstOrDefaultAsync(x => x.Email == updateVendorDto.Email &&
                                            x.EntityType == "Vendor" &&
                                            x.EntityId != updateVendorDto.Id &&
                                            x.OrganisationId == organisationId);

            if (duplicateEmail != null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorEmailExists, "Vendor with this email already exists"));
            }
        }

        try
        {
            // Track status changes
            var statusChanged = existingVendor.Status != updateVendorDto.Status;

            // Update core vendor fields
            existingVendor.VendorCode = updateVendorDto.VendorCode;
            existingVendor.VendorName = updateVendorDto.VendorName;
            existingVendor.CompanyName = updateVendorDto.CompanyName;
            existingVendor.WebsiteUrl = updateVendorDto.WebsiteUrl;
            existingVendor.VendorType = updateVendorDto.VendorType;
            existingVendor.Category = updateVendorDto.Category;
            existingVendor.Status = updateVendorDto.Status;
            existingVendor.Industry = updateVendorDto.Industry;
            existingVendor.ParentVendorId = updateVendorDto.ParentVendorId;
            existingVendor.IsManufacturer = updateVendorDto.IsManufacturer;
            existingVendor.IsDropshipVendor = updateVendorDto.IsDropshipVendor;
            existingVendor.CurrencyId = updateVendorDto.CurrencyId;
            existingVendor.PortalAccessEmail = updateVendorDto.PortalAccessEmail;
            existingVendor.HasPortalAccess = updateVendorDto.HasPortalAccess;
            existingVendor.PreferredCommunicationMethod = updateVendorDto.PreferredCommunicationMethod;
            existingVendor.RequiresPOApproval = updateVendorDto.RequiresPOApproval;
            existingVendor.Notes = updateVendorDto.Notes;
            existingVendor.ModifiedBy = userId;
            existingVendor.ModifiedOn = DateTime.UtcNow;

            if (statusChanged)
            {
                existingVendor.StatusChangedDate = DateTime.UtcNow;
            }

            _unitOfWork.Repository<Vendor>().Update(existingVendor);

            // Update or create BusinessContact
            var existingContact = await _unitOfWork.Repository<BusinessContact>()
                .GetFirstOrDefaultAsync(x => x.EntityType == "Vendor" &&
                                           x.EntityId == updateVendorDto.Id &&
                                           x.IsPrimary == true);

            if (existingContact != null)
            {
                existingContact.ContactPersonName = updateVendorDto.ContactPerson ?? "N/A";
                existingContact.Email = updateVendorDto.Email;
                existingContact.PhoneNumber = updateVendorDto.PhoneNumber;
                existingContact.MobileNumber = updateVendorDto.MobileNumber;
                existingContact.ModifiedBy = userId;
                existingContact.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<BusinessContact>().Update(existingContact);
            }
            else if (!string.IsNullOrEmpty(updateVendorDto.ContactPerson) ||
                     !string.IsNullOrEmpty(updateVendorDto.Email) ||
                     !string.IsNullOrEmpty(updateVendorDto.PhoneNumber) ||
                     !string.IsNullOrEmpty(updateVendorDto.MobileNumber))
            {
                var newContact = new BusinessContact
                {
                    EntityType = "Vendor",
                    EntityId = updateVendorDto.Id,
                    ContactType = ContactType.Primary,
                    ContactPersonName = updateVendorDto.ContactPerson ?? "N/A",
                    Email = updateVendorDto.Email,
                    PhoneNumber = updateVendorDto.PhoneNumber,
                    MobileNumber = updateVendorDto.MobileNumber,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessContact>().Add(newContact);
            }

            // Update or create BusinessAddress
            var existingAddress = await _unitOfWork.Repository<BusinessAddress>()
                .GetFirstOrDefaultAsync(x => x.EntityType == "Vendor" &&
                                           x.EntityId == updateVendorDto.Id &&
                                           x.AddressType == AddressType.Office);

            if (existingAddress != null)
            {
                existingAddress.AddressLine1 = updateVendorDto.Address ?? "N/A";
                existingAddress.City = updateVendorDto.City;
                existingAddress.State = updateVendorDto.State;
                existingAddress.PostalCode = updateVendorDto.PostalCode;
                existingAddress.Country = updateVendorDto.Country;
                existingAddress.ModifiedBy = userId;
                existingAddress.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<BusinessAddress>().Update(existingAddress);
            }
            else if (!string.IsNullOrEmpty(updateVendorDto.Address) ||
                     !string.IsNullOrEmpty(updateVendorDto.City) ||
                     !string.IsNullOrEmpty(updateVendorDto.State) ||
                     !string.IsNullOrEmpty(updateVendorDto.Country))
            {
                var newAddress = new BusinessAddress
                {
                    EntityType = "Vendor",
                    EntityId = updateVendorDto.Id,
                    AddressType = AddressType.Office,
                    AddressLine1 = updateVendorDto.Address ?? "N/A",
                    City = updateVendorDto.City,
                    State = updateVendorDto.State,
                    PostalCode = updateVendorDto.PostalCode,
                    Country = updateVendorDto.Country,
                    IsDefault = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessAddress>().Add(newAddress);
            }

            // Update or create VendorFinancial
            var existingFinancial = await _unitOfWork.Repository<VendorFinancial>()
                .GetFirstOrDefaultAsync(x => x.VendorId == updateVendorDto.Id);

            if (existingFinancial != null)
            {
                existingFinancial.PaymentTermDays = updateVendorDto.PaymentTermDays;
                existingFinancial.MinimumOrderValue = updateVendorDto.MinimumOrderValue;
                existingFinancial.PreferredPaymentMethod = updateVendorDto.PreferredPaymentMethod.ToString();
                existingFinancial.ModifiedBy = userId;
                existingFinancial.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<VendorFinancial>().Update(existingFinancial);
            }
            else if (updateVendorDto.PaymentTermDays > 0 || updateVendorDto.MinimumOrderValue > 0)
            {
                var newFinancial = new VendorFinancial
                {
                    VendorId = updateVendorDto.Id,
                    PaymentTermDays = updateVendorDto.PaymentTermDays,
                    MinimumOrderValue = updateVendorDto.MinimumOrderValue,
                    PreferredPaymentMethod = updateVendorDto.PreferredPaymentMethod.ToString(),
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<VendorFinancial>().Add(newFinancial);
            }

            // Update BusinessTaxInfo
            var existingTaxInfos = await _unitOfWork.Repository<BusinessTaxInfo>()
                .GetFilteredAsync(x => x.EntityType == "Vendor" && x.EntityId == updateVendorDto.Id);

            // Remove existing tax info
            foreach (var taxInfo in existingTaxInfos)
            {
                _unitOfWork.Repository<BusinessTaxInfo>().Remove(taxInfo);
            }

            // Add new tax info
            var newTaxInfos = new List<BusinessTaxInfo>();
            if (!string.IsNullOrEmpty(updateVendorDto.TaxIdentificationNumber))
            {
                newTaxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Vendor",
                    EntityId = updateVendorDto.Id,
                    TaxType = TaxType.TaxIdentificationNumber,
                    TaxNumber = updateVendorDto.TaxIdentificationNumber,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(updateVendorDto.GSTNumber))
            {
                newTaxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Vendor",
                    EntityId = updateVendorDto.Id,
                    TaxType = TaxType.GST,
                    TaxNumber = updateVendorDto.GSTNumber,
                    IsPrimary = string.IsNullOrEmpty(updateVendorDto.TaxIdentificationNumber),
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(updateVendorDto.PANNumber))
            {
                newTaxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Vendor",
                    EntityId = updateVendorDto.Id,
                    TaxType = TaxType.PAN,
                    TaxNumber = updateVendorDto.PANNumber,
                    IsPrimary = string.IsNullOrEmpty(updateVendorDto.TaxIdentificationNumber) && string.IsNullOrEmpty(updateVendorDto.GSTNumber),
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            foreach (var taxInfo in newTaxInfos)
            {
                _unitOfWork.Repository<BusinessTaxInfo>().Add(taxInfo);
            }

            // Update or create BusinessBankInfo
            var existingBankInfo = await _unitOfWork.Repository<BusinessBankInfo>()
                .GetFirstOrDefaultAsync(x => x.EntityType == "Vendor" &&
                                           x.EntityId == updateVendorDto.Id &&
                                           x.IsPrimary == true);

            if (existingBankInfo != null)
            {
                existingBankInfo.BankAccountNumber = updateVendorDto.BankAccountNumber ?? "N/A";
                existingBankInfo.BankName = updateVendorDto.BankName ?? "N/A";
                existingBankInfo.BankBranch = updateVendorDto.BankBranch;
                existingBankInfo.IFSCCode = updateVendorDto.IFSCCode;
                existingBankInfo.ModifiedBy = userId;
                existingBankInfo.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<BusinessBankInfo>().Update(existingBankInfo);
            }
            else if (!string.IsNullOrEmpty(updateVendorDto.BankAccountNumber) ||
                     !string.IsNullOrEmpty(updateVendorDto.BankName))
            {
                var newBankInfo = new BusinessBankInfo
                {
                    EntityType = "Vendor",
                    EntityId = updateVendorDto.Id,
                    BankAccountNumber = updateVendorDto.BankAccountNumber ?? "N/A",
                    BankName = updateVendorDto.BankName ?? "N/A",
                    BankBranch = updateVendorDto.BankBranch,
                    IFSCCode = updateVendorDto.IFSCCode,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessBankInfo>().Add(newBankInfo);
            }

            // Update or create VendorPerformance
            var existingPerformance = await _unitOfWork.Repository<VendorPerformance>()
                .GetFirstOrDefaultAsync(x => x.VendorId == updateVendorDto.Id);

            if (existingPerformance != null)
            {
                existingPerformance.IsPreferredVendor = updateVendorDto.IsPreferredVendor;
                existingPerformance.IsBlacklisted = updateVendorDto.IsBlacklisted;
                existingPerformance.BlacklistReason = updateVendorDto.BlacklistReason;
                existingPerformance.PerformanceRating = updateVendorDto.PerformanceRating;
                existingPerformance.OnTimeDeliveryPercentage = updateVendorDto.OnTimeDeliveryPercentage;
                existingPerformance.QualityRating = updateVendorDto.QualityRating;
                existingPerformance.ModifiedBy = userId;
                existingPerformance.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<VendorPerformance>().Update(existingPerformance);
            }
            else if (updateVendorDto.IsPreferredVendor || updateVendorDto.IsBlacklisted ||
                     updateVendorDto.PerformanceRating > 0 || updateVendorDto.OnTimeDeliveryPercentage > 0 ||
                     updateVendorDto.QualityRating > 0)
            {
                var newPerformance = new VendorPerformance
                {
                    VendorId = updateVendorDto.Id,
                    IsPreferredVendor = updateVendorDto.IsPreferredVendor,
                    IsBlacklisted = updateVendorDto.IsBlacklisted,
                    BlacklistReason = updateVendorDto.BlacklistReason,
                    PerformanceRating = updateVendorDto.PerformanceRating,
                    OnTimeDeliveryPercentage = updateVendorDto.OnTimeDeliveryPercentage,
                    QualityRating = updateVendorDto.QualityRating,
                    ReviewPeriodStart = DateTime.UtcNow,
                    ReviewPeriodEnd = DateTime.UtcNow.AddMonths(12),
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<VendorPerformance>().Add(newPerformance);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<VendorDto>> GetVendorById(int id)
    {
        try
        {
            var vendor = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(
                    x => x.Id == id,
                    includeroperties: "Currency,ParentVendor,BusinessContacts,BusinessAddresses,BusinessTaxInfos,BusinessBankInfos,VendorFinancial,VendorPerformances");

            if (vendor == null)
            {
                return ServiceResult<VendorDto>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            var vendorDto = vendor.Adapt<VendorDto>();
            vendorDto.CurrencyName = vendor.Currency?.Name;
            vendorDto.CurrencySymbol = vendor.Currency?.Symbol;
            vendorDto.ParentVendorName = vendor.ParentVendor?.VendorName;

            // Get primary contact info
            var primaryContact = vendor.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive);
            if (primaryContact != null)
            {
                vendorDto.ContactPerson = primaryContact.ContactPersonName;
                vendorDto.Email = primaryContact.Email;
                vendorDto.PhoneNumber = primaryContact.PhoneNumber;
                vendorDto.MobileNumber = primaryContact.MobileNumber;
            }

            // Get address info
            var officeAddress = vendor.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Office && x.IsActive);
            if (officeAddress != null)
            {
                vendorDto.Address = officeAddress.AddressLine1;
                vendorDto.City = officeAddress.City;
                vendorDto.State = officeAddress.State;
                vendorDto.PostalCode = officeAddress.PostalCode;
                vendorDto.Country = officeAddress.Country;
            }

            // Get financial info
            if (vendor.VendorFinancial != null)
            {
                vendorDto.PaymentTermDays = vendor.VendorFinancial.PaymentTermDays;
                vendorDto.MinimumOrderValue = vendor.VendorFinancial.MinimumOrderValue;
                if (Enum.TryParse<VendorPaymentMethod>(vendor.VendorFinancial.PreferredPaymentMethod, out var paymentMethod))
                {
                    vendorDto.PreferredPaymentMethod = paymentMethod;
                }
            }

            // Get tax info
            var taxNumber = vendor.BusinessTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.TaxIdentificationNumber && x.IsActive);
            if (taxNumber != null)
            {
                vendorDto.TaxIdentificationNumber = taxNumber.TaxNumber;
            }

            var gstNumber = vendor.BusinessTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.GST && x.IsActive);
            if (gstNumber != null)
            {
                vendorDto.GSTNumber = gstNumber.TaxNumber;
            }

            var panNumber = vendor.BusinessTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.PAN && x.IsActive);
            if (panNumber != null)
            {
                vendorDto.PANNumber = panNumber.TaxNumber;
            }

            // Get bank info
            var primaryBankInfo = vendor.BusinessBankInfos?.FirstOrDefault(x => x.IsPrimary && x.IsActive);
            if (primaryBankInfo != null)
            {
                vendorDto.BankAccountNumber = primaryBankInfo.BankAccountNumber;
                vendorDto.BankName = primaryBankInfo.BankName;
                vendorDto.BankBranch = primaryBankInfo.BankBranch;
                vendorDto.IFSCCode = primaryBankInfo.IFSCCode;
            }

            // Get performance info
            var latestPerformance = vendor.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
            if (latestPerformance != null)
            {
                vendorDto.IsPreferredVendor = latestPerformance.IsPreferredVendor;
                vendorDto.IsBlacklisted = latestPerformance.IsBlacklisted;
                vendorDto.BlacklistReason = latestPerformance.BlacklistReason;
                vendorDto.PerformanceRating = latestPerformance.PerformanceRating;
                vendorDto.OnTimeDeliveryPercentage = latestPerformance.OnTimeDeliveryPercentage;
                vendorDto.QualityRating = latestPerformance.QualityRating;
            }

            return ServiceResult<VendorDto>.Success(vendorDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<VendorDto>
                .Error(new Problem(ErrorCodes.VendorRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<List<VendorListItemDto>>> GetAllVendors(int organisationId, int pageIndex, int pageSize)
    {
        try
        {
            var vendors = await _unitOfWork.Repository<Vendor>()
                .GetPaginatedAsync(
                    pageIndex,
                    pageSize,
                    filter: x => x.OrganisationId == organisationId,
                    includeroperties: "Currency,BusinessContacts,BusinessAddresses,VendorFinancial,VendorPerformances");

            var vendorDtos = vendors.Select(v => new VendorListItemDto
            {
                Id = v.Id,
                VendorCode = v.VendorCode,
                VendorName = v.VendorName,
                CompanyName = v.CompanyName,
                ContactPerson = v.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.ContactPersonName,
                PhoneNumber = v.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.PhoneNumber,
                Email = v.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.Email,
                City = v.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Office && x.IsActive)?.City,
                State = v.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Office && x.IsActive)?.State,
                Country = v.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Office && x.IsActive)?.Country,
                VendorType = v.VendorType,
                Category = v.Category,
                Status = v.Status,
                Industry = v.Industry,
                CreditLimit = v.VendorFinancial?.CurrentBalance ?? 0,
                CurrentBalance = v.VendorFinancial?.CurrentBalance ?? 0,
                OutstandingAmount = v.VendorFinancial?.OutstandingAmount ?? 0,
                IsPreferredVendor = v.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.IsPreferredVendor ?? false,
                IsBlacklisted = v.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.IsBlacklisted ?? false,
                PerformanceRating = v.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.PerformanceRating ?? 0,
                OnTimeDeliveryPercentage = v.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.OnTimeDeliveryPercentage ?? 0,
                LastOrderDate = v.VendorFinancial?.LastPurchaseDate,
                LastPaymentDate = v.VendorFinancial?.LastPaymentDate,
                CurrencyName = v.Currency?.Name,
                CreatedOn = v.CreatedOn
            }).ToList();

            return ServiceResult<List<VendorListItemDto>>.Success(vendorDtos);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<VendorListItemDto>>
                .Error(new Problem(ErrorCodes.VendorRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<List<VendorListItemDto>>> GetFilteredVendors(int organisationId, VendorFilterDto filter)
    {
        try
        {
            var specification = new VendorSpecification(organisationId, filter);
            var vendors = await _unitOfWork.Repository<Vendor>().GetAsync(specification);

            var vendorDtos = vendors.Select(v => new VendorListItemDto
            {
                Id = v.Id,
                VendorCode = v.VendorCode,
                VendorName = v.VendorName,
                CompanyName = v.CompanyName,
                ContactPerson = v.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.ContactPersonName,
                PhoneNumber = v.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.PhoneNumber,
                Email = v.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.Email,
                City = v.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Office && x.IsActive)?.City,
                State = v.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Office && x.IsActive)?.State,
                Country = v.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Office && x.IsActive)?.Country,
                VendorType = v.VendorType,
                Category = v.Category,
                Status = v.Status,
                Industry = v.Industry,
                CreditLimit = v.VendorFinancial?.CurrentBalance ?? 0,
                CurrentBalance = v.VendorFinancial?.CurrentBalance ?? 0,
                OutstandingAmount = v.VendorFinancial?.OutstandingAmount ?? 0,
                IsPreferredVendor = v.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.IsPreferredVendor ?? false,
                IsBlacklisted = v.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.IsBlacklisted ?? false,
                PerformanceRating = v.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.PerformanceRating ?? 0,
                OnTimeDeliveryPercentage = v.VendorPerformances?.OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.OnTimeDeliveryPercentage ?? 0,
                LastOrderDate = v.VendorFinancial?.LastPurchaseDate,
                LastPaymentDate = v.VendorFinancial?.LastPaymentDate,
                CurrencyName = v.Currency?.Name,
                CreatedOn = v.CreatedOn
            }).ToList();

            return ServiceResult<List<VendorListItemDto>>.Success(vendorDtos);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<VendorListItemDto>>
                .Error(new Problem(ErrorCodes.VendorRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> DeleteVendor(int id, string userId)
    {
        try
        {
            var vendor = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (vendor == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            // Check if vendor has any related transactions
            var hasPurchaseOrders = await _unitOfWork.Repository<PurchaseOrder>()
                .AnyAsync(x => x.VendorId == id);

            if (hasPurchaseOrders)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorHasTransactions, "Cannot delete vendor with existing transactions"));
            }

            _unitOfWork.Repository<Vendor>().Remove(vendor);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorDeletionError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> ToggleVendorStatus(int id, string userId)
    {
        try
        {
            var vendor = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (vendor == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            vendor.Status = vendor.Status == VendorStatus.Active ? VendorStatus.Inactive : VendorStatus.Active;
            vendor.StatusChangedDate = DateTime.UtcNow;
            vendor.ModifiedBy = userId;
            vendor.ModifiedOn = DateTime.UtcNow;

            _unitOfWork.Repository<Vendor>().Update(vendor);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> UpdateVendorPerformance(int id, decimal performanceRating, int onTimeDeliveryPercentage, int qualityRating, string userId)
    {
        try
        {
            var vendor = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (vendor == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            // Update or create VendorPerformance
            var existingPerformance = await _unitOfWork.Repository<VendorPerformance>()
                .GetFirstOrDefaultAsync(x => x.VendorId == id);

            if (existingPerformance != null)
            {
                existingPerformance.PerformanceRating = performanceRating;
                existingPerformance.OnTimeDeliveryPercentage = onTimeDeliveryPercentage;
                existingPerformance.QualityRating = qualityRating;
                existingPerformance.LastPerformanceReview = DateTime.UtcNow;
                existingPerformance.ModifiedBy = userId;
                existingPerformance.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<VendorPerformance>().Update(existingPerformance);
            }
            else
            {
                var newPerformance = new VendorPerformance
                {
                    VendorId = id,
                    PerformanceRating = performanceRating,
                    OnTimeDeliveryPercentage = onTimeDeliveryPercentage,
                    QualityRating = qualityRating,
                    LastPerformanceReview = DateTime.UtcNow,
                    ReviewPeriodStart = DateTime.UtcNow,
                    ReviewPeriodEnd = DateTime.UtcNow.AddMonths(12),
                    ReviewedBy = userId,
                    OrganisationId = vendor.OrganisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<VendorPerformance>().Add(newPerformance);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> BlacklistVendor(int id, string reason, string userId)
    {
        try
        {
            var vendor = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (vendor == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            // Update vendor status
            vendor.Status = VendorStatus.Blacklisted;
            vendor.StatusChangedDate = DateTime.UtcNow;
            vendor.ModifiedBy = userId;
            vendor.ModifiedOn = DateTime.UtcNow;
            _unitOfWork.Repository<Vendor>().Update(vendor);

            // Update or create VendorPerformance for blacklist tracking
            var existingPerformance = await _unitOfWork.Repository<VendorPerformance>()
                .GetFirstOrDefaultAsync(x => x.VendorId == id);

            if (existingPerformance != null)
            {
                existingPerformance.IsBlacklisted = true;
                existingPerformance.BlacklistReason = reason;
                existingPerformance.BlacklistDate = DateTime.UtcNow;
                existingPerformance.BlacklistedBy = userId;
                existingPerformance.ModifiedBy = userId;
                existingPerformance.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<VendorPerformance>().Update(existingPerformance);
            }
            else
            {
                var newPerformance = new VendorPerformance
                {
                    VendorId = id,
                    IsBlacklisted = true,
                    BlacklistReason = reason,
                    BlacklistDate = DateTime.UtcNow,
                    BlacklistedBy = userId,
                    ReviewPeriodStart = DateTime.UtcNow,
                    ReviewPeriodEnd = DateTime.UtcNow.AddMonths(12),
                    OrganisationId = vendor.OrganisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<VendorPerformance>().Add(newPerformance);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> RemoveVendorFromBlacklist(int id, string userId)
    {
        try
        {
            var vendor = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (vendor == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            // Update vendor status
            vendor.Status = VendorStatus.Active;
            vendor.StatusChangedDate = DateTime.UtcNow;
            vendor.ModifiedBy = userId;
            vendor.ModifiedOn = DateTime.UtcNow;
            _unitOfWork.Repository<Vendor>().Update(vendor);

            // Update VendorPerformance to remove blacklist
            var existingPerformance = await _unitOfWork.Repository<VendorPerformance>()
                .GetFirstOrDefaultAsync(x => x.VendorId == id);

            if (existingPerformance != null)
            {
                existingPerformance.IsBlacklisted = false;
                existingPerformance.BlacklistReason = null;
                existingPerformance.BlacklistDate = null;
                existingPerformance.BlacklistedBy = null;
                existingPerformance.ModifiedBy = userId;
                existingPerformance.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<VendorPerformance>().Update(existingPerformance);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> MarkAsPreferredVendor(int id, string userId)
    {
        try
        {
            var vendor = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (vendor == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            // Update or create VendorPerformance for preferred vendor tracking
            var existingPerformance = await _unitOfWork.Repository<VendorPerformance>()
                .GetFirstOrDefaultAsync(x => x.VendorId == id);

            if (existingPerformance != null)
            {
                existingPerformance.IsPreferredVendor = true;
                existingPerformance.ModifiedBy = userId;
                existingPerformance.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<VendorPerformance>().Update(existingPerformance);
            }
            else
            {
                var newPerformance = new VendorPerformance
                {
                    VendorId = id,
                    IsPreferredVendor = true,
                    ReviewPeriodStart = DateTime.UtcNow,
                    ReviewPeriodEnd = DateTime.UtcNow.AddMonths(12),
                    OrganisationId = vendor.OrganisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<VendorPerformance>().Add(newPerformance);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> RemovePreferredVendorStatus(int id, string userId)
    {
        try
        {
            var vendor = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (vendor == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            // Update VendorPerformance to remove preferred vendor status
            var existingPerformance = await _unitOfWork.Repository<VendorPerformance>()
                .GetFirstOrDefaultAsync(x => x.VendorId == id);

            if (existingPerformance != null)
            {
                existingPerformance.IsPreferredVendor = false;
                existingPerformance.ModifiedBy = userId;
                existingPerformance.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<VendorPerformance>().Update(existingPerformance);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.VendorUpdateError, ex.Message));
        }
    }
}