using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions
{
    public static class ApiModelToPersistenceEntityExtensions
    {
        public static SAPRESSC.Middleware.Media.Persistence.Entities.Image ToPersistenceEntity(this Api.Common.Models.ImageDisplayModel target)
        {
            return new SAPRESSC.Middleware.Media.Persistence.Entities.Image
            {
                Id = target.Id,
                CreateAt = target.CreateAt,
                ContentType = target.ContentType,
                Length = target.Length,
                Width = target.Width,
                Height = target.Height,
                About = target.About
            };
        }

        public static SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity ToPersistenceEntity(this Api.Common.Models.PersonIdentityDisplayModel target)
        {
            return new SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity
            {
                Id = target.Id,
                Name = target.Name,
                LastName = target.LastName,
                Alias = target.Alias,
                BornDate = target.BornDate,
                CivilStatus = target.CivilStatus,
                Sex = target.Sex,
                About = target.About,
                ImageId = target.Image?.Id ?? null,
                Image = target.Image?.ToPersistenceEntity()
            };
        }

        public static Cloud.Persistence.Entities.Employ ToPersistenceEntity(this Api.Common.Models.EmployDisplayModel target)
        {
            return new Cloud.Persistence.Entities.Employ
            {
                Id = target.Id,
                IdentityId = target.Identity.Id,
                Identity = target.Identity.ToPersistenceEntity(),
                AdministrationRoleBridges = target.AdministrativeRoles.Select(n => new Cloud.Persistence.Entities.EmployAdministrationRoleBridge
                {
                    EmployId = target.Id,
                    AdministrativeRole = n
                }).ToList()
            };
        }

        public static Cloud.Persistence.Entities.Show ToPersistenceEntity(this Api.Common.Models.ShowDisplayModel target)
        {
            return new Cloud.Persistence.Entities.Show
            {
                Id = target.Id,
                Name = target.Name,
                About = target.About,
                LogoId = target.LogoImage.Id,
                Logo = target.LogoImage.ToPersistenceEntity(),
                PosterId = target.PosterImage.Id,
                Poster = target.PosterImage.ToPersistenceEntity()
            };
        }

        public static Cloud.Persistence.Entities.Presentation ToPersistenceEntity(this Api.Common.Models.PresentationDisplayModel target)
        {
            return new Cloud.Persistence.Entities.Presentation
            {
                Id = target.Id,
                At = target.At,
                About = target.About,
                ShowId = target.Show.Id,
                Show = target.Show.ToPersistenceEntity(),
                ProductionTeamId = target.ProductionTeam.Id,
                ProductionTeam = target.ProductionTeam.ToPersistenceEntity()
            };
        }

        public static Cloud.Persistence.Entities.ProductionTeam ToPersistenceEntity(this Api.Common.Models.ProductionTeamDisplayModel target)
        {
            return new Cloud.Persistence.Entities.ProductionTeam
            {
                Id = target.Id,
                Name = target.Name,
                Description = target.Description,
                EmployBridges = target.Members.Select(m => new Cloud.Persistence.Entities.ProductionTeamEmployBridge
                {
                    ProductionTeamId = target.Id,
                    EmployId = m.Employ.Id,
                    Employ = m.Employ.ToPersistenceEntity(),
                    ProductionRole = m.ProductionRole
                }).ToList()
            };
        }

        public static Internet.Persistence.Entities.Tag ToPersistenceEntity(this Api.Common.Models.TagDisplayModel target)
        {
            return new Internet.Persistence.Entities.Tag
            {
                Id = target.Id,
                Name = target.Name
            };
        }

        public static Internet.Persistence.Entities.Category ToPersistenceEntity(this Api.Common.Models.CategoryDisplayModel target)
        {
            var result = new Internet.Persistence.Entities.Category
            {
                Id = target.Id,
                Name = target.Name
            };

            if (target.Parent != null)
            {
                result.Parent = target.Parent.ToPersistenceEntity();
            }

            return result;
        }

        public static Internet.Persistence.Entities.Post ToPersistenceEntity(this Api.Common.Models.PostDisplayModel target)
        {
            var result = new Internet.Persistence.Entities.Post
            {
                Id = target.Id,
                Title = target.Title,
                Summary = target.Summary,
                Content = target.Content,
                PostedAt = target.PostedAt,
                AuthorId = target.Author.Id,
                Author = target.Author.ToPersistenceEntity(),
                Category = target.Category.ToPersistenceEntity(),
                TagBridges = target.Tags.Select(m => new Internet.Persistence.Entities.PostTagBridge
                {
                    PostId = target.Id,
                    TagId = m.Id,
                    Tag = m.ToPersistenceEntity()
                }).ToList()
            };

            if (target.Poster != null)
            {
                result.Poster = target.Poster.ToPersistenceEntity();
                result.PosterId = target.Poster.Id;
            }

            return result;
        }
    }
}
