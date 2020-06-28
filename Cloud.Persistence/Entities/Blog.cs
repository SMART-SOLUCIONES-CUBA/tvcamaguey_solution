using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class Blog : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Blog()
        {
            Id = System.Guid.NewGuid();
            EmployBridges = new List<EmployBlogBridge>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required, RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$")]
        public string Url { get; set; }

        public List<EmployBlogBridge> EmployBridges { get; set; }
    }
}
