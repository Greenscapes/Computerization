namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyTaskDetail
    {
        public int Id { get; set; }

        public int PropertyTaskId { get; set; }

        public int PropertyTaskHeaderId { get; set; }

        public string Value { get; set; }

        public virtual PropertyTaskHeader PropertyTaskHeader { get; set; }

        public virtual PropertyTask PropertyTask { get; set; }
    }
}
