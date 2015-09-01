namespace CMS.Models
{
    public abstract class TypeBase : ModelBase
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }
}