namespace PCI.Shared.Common.Constants
{
    public class ErrorCodes
    {
        public const string UserNotFound = "UserNotFound";
        public const string InvalidCredentials = "InvalidCredentials";
        public const string SessionNotFound = "SessionNotFound";
        public const string OrganisationCreationError = "OrganisationCreationError";
        public const string OrganisationNotFound = "OrganisationNotFound";
        public const string CategoryCreationError = "CategoryCreationError";
        public const string CategoryAlreadyExists = "CategoryAlreadyExists";
        public const string CategoryImageUploadError = "CategoryImageUploadError";
        public const string CategoryRetrievalError = "CategoryRetrievalError";
        public const string CategoryNotFound = "CategoryNotFound";
        public const string ProductAlreadyExists = "ProductAlreadyExists";
        public const string ProductCreationError = "ProductCreationError";
        public const string ProductNotFound = "ProductNotFound";
    }

    public class Messages
    {
        public const string InvalidCredentials = "Invalid email or password.";
        public const string SessionNotFound = "Session not found or inactive.";
        public const string OrganisationNotFound = "Organisation not found.";
        public const string CategoryAlreadyExists = "A category with the same name already exists for this user.";

        public const string RoleAssigned = "Role assigned to user successfully.";
        public const string OrganisationCreated = "Organisation created successfully.";
        public const string OrganisationUpdated = "Organisation updated successfully.";
        public const string OrganisationRetrieved = "Organisation retrieved successfully.";

        public const string UserLoggedIn = "User logged in successfully.";
        public const string UserLoginDetailsUpdated = "User login details updated successfully.";

        public const string CategoryCreated = "Category created successfully.";
        public const string CategoryUpdated = "Category updated successfully.";
        public const string CategoryDeleted = "Category deleted successfully.";
        public const string CategoryCreationFailed = "Category creation failed. Please try again.";
        public const string CategoryNotFound = "Category not found.";
    }
}