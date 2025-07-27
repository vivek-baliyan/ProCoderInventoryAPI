using Mapster;
using Microsoft.EntityFrameworkCore;
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
            var existingEmail = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Email == createVendorDto.Email && x.OrganisationId == organisationId);

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
                ContactPerson = createVendorDto.ContactPerson,
                PhoneNumber = createVendorDto.PhoneNumber,
                MobileNumber = createVendorDto.MobileNumber,
                Email = createVendorDto.Email,
                Address = createVendorDto.Address,
                City = createVendorDto.City,
                State = createVendorDto.State,
                PostalCode = createVendorDto.PostalCode,
                Country = createVendorDto.Country,
                WebsiteUrl = createVendorDto.WebsiteUrl,
                VendorType = createVendorDto.VendorType,
                Category = createVendorDto.Category,
                Status = createVendorDto.Status,
                Industry = createVendorDto.Industry,
                ParentVendorId = createVendorDto.ParentVendorId,
                IsManufacturer = createVendorDto.IsManufacturer,
                IsDropshipVendor = createVendorDto.IsDropshipVendor,
                CreditLimit = createVendorDto.CreditLimit,
                PaymentTermDays = createVendorDto.PaymentTermDays,
                PreferredPaymentMethod = createVendorDto.PreferredPaymentMethod,
                TaxIdentificationNumber = createVendorDto.TaxIdentificationNumber,
                GSTNumber = createVendorDto.GSTNumber,
                PANNumber = createVendorDto.PANNumber,
                BankAccountNumber = createVendorDto.BankAccountNumber,
                BankName = createVendorDto.BankName,
                BankBranch = createVendorDto.BankBranch,
                IFSCCode = createVendorDto.IFSCCode,
                CurrencyId = createVendorDto.CurrencyId,
                PortalAccessEmail = createVendorDto.PortalAccessEmail,
                HasPortalAccess = createVendorDto.HasPortalAccess,
                PreferredCommunicationMethod = createVendorDto.PreferredCommunicationMethod,
                RequiresPOApproval = createVendorDto.RequiresPOApproval,
                MinimumOrderValue = createVendorDto.MinimumOrderValue,
                Notes = createVendorDto.Notes,
                IsPreferredVendor = createVendorDto.IsPreferredVendor,
                StatusChangedDate = DateTime.UtcNow,
                OrganisationId = organisationId,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            _unitOfWork.Repository<Vendor>().Add(vendor);
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
            var duplicateEmail = await _unitOfWork.Repository<Vendor>()
                .GetFirstOrDefaultAsync(x => x.Email == updateVendorDto.Email && 
                                            x.Id != updateVendorDto.Id && 
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

            existingVendor.VendorCode = updateVendorDto.VendorCode;
            existingVendor.VendorName = updateVendorDto.VendorName;
            existingVendor.CompanyName = updateVendorDto.CompanyName;
            existingVendor.ContactPerson = updateVendorDto.ContactPerson;
            existingVendor.PhoneNumber = updateVendorDto.PhoneNumber;
            existingVendor.MobileNumber = updateVendorDto.MobileNumber;
            existingVendor.Email = updateVendorDto.Email;
            existingVendor.Address = updateVendorDto.Address;
            existingVendor.City = updateVendorDto.City;
            existingVendor.State = updateVendorDto.State;
            existingVendor.PostalCode = updateVendorDto.PostalCode;
            existingVendor.Country = updateVendorDto.Country;
            existingVendor.WebsiteUrl = updateVendorDto.WebsiteUrl;
            existingVendor.VendorType = updateVendorDto.VendorType;
            existingVendor.Category = updateVendorDto.Category;
            existingVendor.Status = updateVendorDto.Status;
            existingVendor.Industry = updateVendorDto.Industry;
            existingVendor.ParentVendorId = updateVendorDto.ParentVendorId;
            existingVendor.IsManufacturer = updateVendorDto.IsManufacturer;
            existingVendor.IsDropshipVendor = updateVendorDto.IsDropshipVendor;
            existingVendor.CreditLimit = updateVendorDto.CreditLimit;
            existingVendor.PaymentTermDays = updateVendorDto.PaymentTermDays;
            existingVendor.PreferredPaymentMethod = updateVendorDto.PreferredPaymentMethod;
            existingVendor.TaxIdentificationNumber = updateVendorDto.TaxIdentificationNumber;
            existingVendor.GSTNumber = updateVendorDto.GSTNumber;
            existingVendor.PANNumber = updateVendorDto.PANNumber;
            existingVendor.BankAccountNumber = updateVendorDto.BankAccountNumber;
            existingVendor.BankName = updateVendorDto.BankName;
            existingVendor.BankBranch = updateVendorDto.BankBranch;
            existingVendor.IFSCCode = updateVendorDto.IFSCCode;
            existingVendor.CurrencyId = updateVendorDto.CurrencyId;
            existingVendor.PortalAccessEmail = updateVendorDto.PortalAccessEmail;
            existingVendor.HasPortalAccess = updateVendorDto.HasPortalAccess;
            existingVendor.PreferredCommunicationMethod = updateVendorDto.PreferredCommunicationMethod;
            existingVendor.RequiresPOApproval = updateVendorDto.RequiresPOApproval;
            existingVendor.MinimumOrderValue = updateVendorDto.MinimumOrderValue;
            existingVendor.Notes = updateVendorDto.Notes;
            existingVendor.IsPreferredVendor = updateVendorDto.IsPreferredVendor;
            existingVendor.IsBlacklisted = updateVendorDto.IsBlacklisted;
            existingVendor.BlacklistReason = updateVendorDto.BlacklistReason;
            existingVendor.PerformanceRating = updateVendorDto.PerformanceRating;
            existingVendor.OnTimeDeliveryPercentage = updateVendorDto.OnTimeDeliveryPercentage;
            existingVendor.QualityRating = updateVendorDto.QualityRating;
            existingVendor.ModifiedBy = userId;
            existingVendor.ModifiedOn = DateTime.UtcNow;

            if (statusChanged)
            {
                existingVendor.StatusChangedDate = DateTime.UtcNow;
            }

            _unitOfWork.Repository<Vendor>().Update(existingVendor);
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
                    includeroperties: "Currency,ParentVendor");

            if (vendor == null)
            {
                return ServiceResult<VendorDto>
                    .Error(new Problem(ErrorCodes.VendorNotFound, "Vendor not found"));
            }

            var vendorDto = vendor.Adapt<VendorDto>();
            vendorDto.CurrencyName = vendor.Currency?.Name;
            vendorDto.CurrencySymbol = vendor.Currency?.Symbol;
            vendorDto.ParentVendorName = vendor.ParentVendor?.VendorName;

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
                    includeroperties: "Currency");

            var vendorDtos = vendors.Select(v => new VendorListItemDto
            {
                Id = v.Id,
                VendorCode = v.VendorCode,
                VendorName = v.VendorName,
                CompanyName = v.CompanyName,
                ContactPerson = v.ContactPerson,
                PhoneNumber = v.PhoneNumber,
                Email = v.Email,
                City = v.City,
                State = v.State,
                Country = v.Country,
                VendorType = v.VendorType,
                Category = v.Category,
                Status = v.Status,
                Industry = v.Industry,
                CreditLimit = v.CreditLimit,
                CurrentBalance = v.CurrentBalance,
                OutstandingAmount = v.OutstandingAmount,
                IsPreferredVendor = v.IsPreferredVendor,
                IsBlacklisted = v.IsBlacklisted,
                PerformanceRating = v.PerformanceRating,
                OnTimeDeliveryPercentage = v.OnTimeDeliveryPercentage,
                LastOrderDate = v.LastOrderDate,
                LastPaymentDate = v.LastPaymentDate,
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
                ContactPerson = v.ContactPerson,
                PhoneNumber = v.PhoneNumber,
                Email = v.Email,
                City = v.City,
                State = v.State,
                Country = v.Country,
                VendorType = v.VendorType,
                Category = v.Category,
                Status = v.Status,
                Industry = v.Industry,
                CreditLimit = v.CreditLimit,
                CurrentBalance = v.CurrentBalance,
                OutstandingAmount = v.OutstandingAmount,
                IsPreferredVendor = v.IsPreferredVendor,
                IsBlacklisted = v.IsBlacklisted,
                PerformanceRating = v.PerformanceRating,
                OnTimeDeliveryPercentage = v.OnTimeDeliveryPercentage,
                LastOrderDate = v.LastOrderDate,
                LastPaymentDate = v.LastPaymentDate,
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

            vendor.PerformanceRating = performanceRating;
            vendor.OnTimeDeliveryPercentage = onTimeDeliveryPercentage;
            vendor.QualityRating = qualityRating;
            vendor.LastPerformanceReview = DateTime.UtcNow;
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

            vendor.IsBlacklisted = true;
            vendor.BlacklistReason = reason;
            vendor.Status = VendorStatus.Blacklisted;
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

            vendor.IsBlacklisted = false;
            vendor.BlacklistReason = null;
            vendor.Status = VendorStatus.Active;
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

            vendor.IsPreferredVendor = true;
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

            vendor.IsPreferredVendor = false;
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
}