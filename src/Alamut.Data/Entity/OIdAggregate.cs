using System.ComponentModel.DataAnnotations;
using DF.Core.Models;
using MongoDB.Bson;

namespace Alamut.Data.Entity
{
    public class OIdAggregate : IAggregate<string>
    {
        public OIdAggregate()
        {
            this.Id = ObjectId.GenerateNewId().ToString();
        }

        [MaxLength(24)]
        public string Id { get; set; }
    }
}
