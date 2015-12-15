using System;

namespace Alamut.Data.Entity
{
    public interface IDateEntity
    {
        DateTime CreateDate { get; set; } 
    }


    /// <summary>
    /// just for use in mongo query builder
    /// mondo builder can't get serilization information of an interface
    /// </summary>
    public class DateEntity : IDateEntity
    {
        public DateTime CreateDate { get; set; }
    }
}