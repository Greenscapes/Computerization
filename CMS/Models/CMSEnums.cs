namespace Greenscapes.Data.Models
{
    public enum PropertyTypeEnum
    {
        Draft = 0,
        Prospect = 1,
        Active = 2,
        UnconvertedProspect = 3,
        ExClient = 4

    }

    public enum CustomerTypeEnum
    {
        Draft = 0,
        HOA = 1,
        Commercial = 2,
        Residential = 3

    }

    public enum StatusEnum
    {
        New = 0,
        InProgress = 1,
        Completed = 2,
        Closed = 3

    }
    public enum ReviewStatusEnum
    {
        New = 0,
        InReview = 1,
        Reviewed = 2,
        Closed = 3

    }
}