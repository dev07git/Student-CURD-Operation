using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityInCore3.Contracts.Utility
{
    public class Constants
    {
        public const string Anonymous = "Anonymous";

        public static string SomeThingWentWrong = "Something went wrong, Please contact system Administrator";
        public const string RecordCreatedSuccessfully = "{0} created successfully";
        public const string RecordCreatedUpdatedSuccessfully = "{0} created and updated successfully";


        public const string InvalidRequestObject = "Invalid Request Object";
        public const string InvalidField = "Invalid {0}";

        public const string ItemRequired = "{0} is Required";
        public const string RequiredFieldMissing = "Required field are missing";
        public const string Validation = "Unable to proceed !! '{0}' already exists.";

        // Success Message

        public const string RegisteredSuccessfully = "User has been registered successfully";
        public const string CreatedSuccessfully = "{0} created successfully.";
        public const string UpdatedSuccessfully = "{0} updated successfully.";
        public const string RemovedSuccessfully = "{0} removed successfully.";
        public const string FailedToUpdate = "Failed to updated {0}. "+ ContactSysAdmin;
        public const string FailedToRemove = "Failed to remove {0}. " + ContactSysAdmin;

        public const string ContactSysAdmin = "Please contact system administrator";
        public const string RecordNotFound = "{0} not found .";



        // labels
        public static string lblStudentName = "Student Name";
        public static string lblStudent = "Student";
        public static string lblStudentId = "Student Id";
        public static string lblSubject = "Subject";




    }
}
