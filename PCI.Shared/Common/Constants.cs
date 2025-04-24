namespace PCI.Shared.Common
{
    public static class ErrorCodes
    {
        public const string UserNotFound = "UserNotFound";
        public const string InvalidCredentials = "InvalidCredentials";
        public const string SessionNotFound = "SessionNotFound";
        public const string UserProfileCreationError = "UserProfileCreationError";
        public const string UserProfileNotFound = "UserProfileNotFound";
        public const string CategoryCreationError = "CategoryCreationError";
        public const string CategoryAlreadyExists = "CategoryAlreadyExists";

    }

    public static class Messages
    {
        public const string InvalidCredentials = "Invalid email or password.";
        public const string SessionNotFound = "Session not found or inactive.";
        public const string UserProfileNotFound = "User profile not found.";
        public const string CategoryAlreadyExists = "A category with the same name already exists for this user.";

        public const string RoleAssigned = "Role assigned to user successfully.";
        public const string UserProfileCreated = "User profile created successfully.";
        public const string UserProfileUpdated = "User profile updated successfully.";
        public const string UserProfileDeleted = "User profile deleted successfully.";
        public const string UserProfileRetrieved = "User profile retrieved successfully.";

        public const string UserLoggedIn = "User logged in successfully.";
        public const string UserLoginDetailsUpdated = "User login details updated successfully.";

        public const string CategoryCreated = "Category created successfully.";
        public const string CategoryUpdated = "Category updated successfully.";
        public const string CategoryDeleted = "Category deleted successfully.";


    }
}