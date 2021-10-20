using System.ComponentModel.DataAnnotations;

namespace Framework.Exception.DataAccessConfig
{
    public enum CudResultStatus
    {
        [Display(Name = "your request is successful")]
        Success = 0,
        [Display(Name = "duplicate error")]
        Duplicate = 1,
        [Display(Name = "UnAuthorize")]
        ValidationError = 2,
        [Display(Name = "General error")]
        GeneralException = 3,
        [Display(Name = "Foreign key error")]
        ForiegnKeyException = 4,
        [Display(Name = "record not found")]
        NotFound = 5
    }

    public enum ResultStatus
    {
        [Display(Name = "Successfull")]
        Success = 0,
        [Display(Name ="NotFound")]
        NotFound = 1,
        [Display(Name ="List Is Empty")]
        ListEmpty = 2,
        [Display(Name ="UnAuthorized")]
        UnAuthorized = 3,
        [Display(Name = "General error")]
        GeneralException = 4,
    }

    public enum SortOrder
    {
        Unsorted = 0,
        Ascending = 1,
        Descending = 2,
    }
    public enum DisplayJustNameProperty
    {
        Name = 0
    }
}