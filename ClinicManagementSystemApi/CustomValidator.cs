using ClinicManagementSystem.Core.DTOs;

namespace ClinicManagementSystemApi
{
    public static class CustomValidator
    {
        public static bool IsValidPaging(PaginRequest paginRequest)
        {
            if(paginRequest == null || paginRequest.Skip<0 || paginRequest.Take<=0) 
                return false;

            return true;
        }
    }
}
