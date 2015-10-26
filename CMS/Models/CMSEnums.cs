using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public enum PropertyTypeEnum
    {
        Draft = 0,
        Prospect = 1,
        Active = 2,
        Closed = 3

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