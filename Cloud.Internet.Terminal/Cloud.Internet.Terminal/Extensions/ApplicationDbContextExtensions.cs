using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions
{
    public static class ApplicationDbContextExtensions
    {
        public static Internet.Persistence.Entities.Tag AddOrUpdate(this Persistence.EF.Context.ApplicationDbContext target, Internet.Persistence.Entities.Tag localEntity, Internet.Persistence.Entities.Tag externalEntity, List<Internet.Persistence.Entities.Tag> unhandledList = null)
        {
            if (localEntity != null)
            {
                localEntity.Name = externalEntity.Name;

                unhandledList?.Remove(localEntity);
            }
            else
            {
                target.Tags.Add(externalEntity);

                localEntity = externalEntity;
            }

            return localEntity;
        }

        public static Internet.Persistence.Entities.Category AddOrUpdate(this Persistence.EF.Context.ApplicationDbContext target, Internet.Persistence.Entities.Category localEntity, Internet.Persistence.Entities.Category externalEntity, List<Internet.Persistence.Entities.Category> unhandledList = null)
        {
            if (localEntity != null)
            {
                localEntity.Name = externalEntity.Name;

                if (externalEntity.Parent != null)
                {
                    var localParent = target.Categories.FirstOrDefault(e => e.Id == externalEntity.ParentId) ?? target.Categories.Local.FirstOrDefault(e => e.Id == externalEntity.ParentId);

                    target.AddOrUpdate(localParent, externalEntity.Parent, unhandledList);

                    localEntity.ParentId = localParent.Id;
                    localEntity.Parent = localParent;
                }

                unhandledList?.Remove(localEntity);
            }
            else
            {
                target.Categories.Add(externalEntity);

                localEntity = externalEntity;
            }

            return localEntity;
        }

        public static Internet.Persistence.Entities.Post AddOrUpdate(this Persistence.EF.Context.ApplicationDbContext target, Internet.Persistence.Entities.Post localEntity, Internet.Persistence.Entities.Post externalEntity, List<Image> deleteImageEntities = null, List<Image> downloadImageEntities = null, List<Internet.Persistence.Entities.Post> unhandledList = null)
        {
            if (localEntity != null)
            {
                localEntity.Title = externalEntity.Title;
                localEntity.Summary = externalEntity.Summary;
                localEntity.Content = externalEntity.Content;
                localEntity.PostedAt = externalEntity.PostedAt;

                var localCategory = target.Categories.FirstOrDefault(e => e.Id == externalEntity.Category.Id) ?? target.Categories.Local.FirstOrDefault(e => e.Id == externalEntity.Category.Id);

                target.AddOrUpdate(localCategory, externalEntity.Category);

                localEntity.Category = localCategory;

                var localAuthor = target.Employs.FirstOrDefault(e => e.Id == externalEntity.Author.Id) ?? target.Employs.FirstOrDefault(e => e.Id == externalEntity.Author.Id);

                target.AddOrUpdate(localAuthor, externalEntity.Author, deleteImageEntities, downloadImageEntities);

                localEntity.AuthorId = localAuthor.Id;
                localEntity.Author = localAuthor;

                if (localEntity.PosterId != externalEntity.PosterId)
                {
                    if (localEntity.Poster != null) deleteImageEntities?.Add(localEntity.Poster);

                    localEntity.PosterId = externalEntity.PosterId;
                    localEntity.Poster = externalEntity.Poster;

                    if (localEntity.Poster != null) downloadImageEntities?.Add(localEntity.Poster);
                }

                var unhandleBridgeList = localEntity.TagBridges.ToList();

                var externalBridgesQueue = new Queue<Internet.Persistence.Entities.PostTagBridge>(externalEntity.TagBridges);

                while (externalBridgesQueue.Count > 0)
                {
                    var externalBridge = externalBridgesQueue.Dequeue();

                    var localBridge = unhandleBridgeList.FirstOrDefault(e => e.TagId == externalBridge.TagId);

                    if (localBridge != null)
                    {
                        var localTag = target.Tags.FirstOrDefault(e => e.Id == externalBridge.TagId);

                        target.AddOrUpdate(localTag, externalBridge.Tag);

                        unhandleBridgeList.Remove(localBridge);
                    }
                    else
                    {
                        var localTag = target.Tags.FirstOrDefault(e => e.Id == externalBridge.TagId);

                        localTag = target.AddOrUpdate(localTag, externalBridge.Tag);

                        localEntity.TagBridges.Add(new Internet.Persistence.Entities.PostTagBridge
                        {
                            TagId = localTag.Id,
                            PostId = localEntity.Id
                        });
                    }
                }

                foreach (var localBridge in unhandleBridgeList)
                {
                    target.PostTagBridges.Remove(localBridge);
                }

                target.Posts.Update(localEntity);

                unhandledList?.Remove(localEntity);
            }
            else
            {
                var localCategory = target.Categories.FirstOrDefault(e => e.Id == externalEntity.Category.Id) ?? target.Categories.Local.FirstOrDefault(e => e.Id == externalEntity.Category.Id);

                target.AddOrUpdate(localCategory, externalEntity.Category);

                externalEntity.Category = localCategory;

                target.Posts.Add(externalEntity);

                localEntity = externalEntity;

                if (localEntity.Poster != null) downloadImageEntities?.Add(localEntity.Poster);
            }

            return localEntity;
        }

        public static Cloud.Persistence.Entities.Show AddOrUpdate(this Persistence.EF.Context.ApplicationDbContext target, Cloud.Persistence.Entities.Show localEntity, Cloud.Persistence.Entities.Show externalEntity, List<Image> deleteImageEntities = null, List<Image> downloadImageEntities = null, List<Cloud.Persistence.Entities.Show> unhandledList = null)
        {
            if (localEntity != null)
            {
                localEntity.Name = externalEntity.Name;
                localEntity.About = externalEntity.About;

                if (localEntity.LogoId != externalEntity.LogoId)
                {
                    deleteImageEntities?.Add(localEntity.Logo);

                    localEntity.LogoId = externalEntity.LogoId;
                    localEntity.Logo = externalEntity.Logo;

                    downloadImageEntities?.Add(localEntity.Logo);
                }

                if (localEntity.PosterId != externalEntity.PosterId)
                {
                    deleteImageEntities?.Add(localEntity.Poster);

                    localEntity.PosterId = externalEntity.PosterId;
                    localEntity.Poster = externalEntity.Poster;

                    downloadImageEntities?.Add(localEntity.Poster);
                }

                target.Shows.Update(localEntity);

                unhandledList?.Remove(localEntity);
            }
            else
            {
                target.Shows.Add(externalEntity);

                localEntity = externalEntity;

                downloadImageEntities?.Add(localEntity.Logo);
                downloadImageEntities?.Add(localEntity.Poster);
            }

            return localEntity;
        }

        public static Cloud.Persistence.Entities.Employ AddOrUpdate(this Persistence.EF.Context.ApplicationDbContext target, Cloud.Persistence.Entities.Employ localEntity, Cloud.Persistence.Entities.Employ externalEntity, List<Image> deleteImageEntities = null, List<Image> downloadImageEntities = null, List<Cloud.Persistence.Entities.Employ> unhandledList = null)
        {
            if (localEntity != null)
            {
                localEntity.Identity = target.PersonIdentities.Include(e => e.Image).First(e => e.Id == localEntity.IdentityId);

                localEntity.Identity.Name = externalEntity.Identity.Name;
                localEntity.Identity.LastName = externalEntity.Identity.LastName;
                localEntity.Identity.BornDate = externalEntity.Identity.BornDate;
                localEntity.Identity.Alias = externalEntity.Identity.Alias;
                localEntity.Identity.CivilStatus = externalEntity.Identity.CivilStatus;
                localEntity.Identity.Sex = externalEntity.Identity.Sex;
                localEntity.Identity.About = externalEntity.Identity.About;

                if (localEntity.Identity.ImageId != externalEntity.Identity.ImageId)
                {
                    if (localEntity.Identity.Image != null) deleteImageEntities?.Add(localEntity.Identity.Image);

                    localEntity.Identity.ImageId = externalEntity.Identity.ImageId;
                    localEntity.Identity.Image = externalEntity.Identity.Image;

                    if (localEntity.Identity.Image != null) downloadImageEntities?.Add(localEntity.Identity.Image);
                }

                target.Employs.Update(localEntity);

                unhandledList?.Remove(localEntity);
            }
            else
            {
                if (!target.Employs.Local.Any(e => e.Id == externalEntity.Id))
                {
                    target.Employs.Add(externalEntity);

                    if (externalEntity.Identity.Image != null)
                    {
                        downloadImageEntities?.Add(externalEntity.Identity.Image);
                    }
                }

                localEntity = externalEntity;
            }

            return localEntity;
        }

        public static Cloud.Persistence.Entities.ProductionTeam AddOrUpdate(this Persistence.EF.Context.ApplicationDbContext target, Cloud.Persistence.Entities.ProductionTeam localEntity, Cloud.Persistence.Entities.ProductionTeam externalEntity, List<Image> deleteImageEntities = null, List<Image> downloadImageEntities = null, List<Cloud.Persistence.Entities.ProductionTeam> unhandledList = null)
        {
            if (localEntity != null)
            {
                localEntity.Name = externalEntity.Name;
                localEntity.Description = externalEntity.Description;

                var unhandleBridgeList = localEntity.EmployBridges.ToList();

                var externalBridgesQueue = new Queue<Cloud.Persistence.Entities.ProductionTeamEmployBridge>(externalEntity.EmployBridges);

                while (externalBridgesQueue.Count > 0)
                {
                    var externalBridge = externalBridgesQueue.Dequeue();

                    var localBridge = unhandleBridgeList.FirstOrDefault(e => e.EmployId == externalBridge.EmployId);

                    if (localBridge != null)
                    {
                        localBridge.ProductionRole = externalBridge.ProductionRole;

                        var localEmploy = target.Employs.FirstOrDefault(e => e.Id == externalBridge.EmployId);

                        target.AddOrUpdate(localEmploy, externalBridge.Employ, deleteImageEntities, downloadImageEntities);

                        unhandleBridgeList.Remove(localBridge);
                    }
                    else
                    {
                        var localEmploy = target.Employs.FirstOrDefault(e => e.Id == externalBridge.EmployId);

                        localEmploy = target.AddOrUpdate(localEmploy, externalBridge.Employ, deleteImageEntities, downloadImageEntities);

                        localEntity.EmployBridges.Add(new Cloud.Persistence.Entities.ProductionTeamEmployBridge
                        {
                            EmployId = localEmploy.Id,
                            ProductionTeamId = localEntity.Id,
                            ProductionRole = externalBridge.ProductionRole
                        });
                    }
                }

                foreach (var localBridge in unhandleBridgeList)
                {
                    target.ProductionTeamEmployBridges.Remove(localBridge);
                }

                target.ProductionTeams.Update(localEntity);

                unhandledList?.Remove(localEntity);
            }
            else
            {
                foreach (var externalEmployBridge in externalEntity.EmployBridges)
                {
                    var localEmploy = target.Employs.FirstOrDefault(e => e.Id == externalEmployBridge.EmployId);

                    localEmploy = target.AddOrUpdate(localEmploy, externalEmployBridge.Employ, deleteImageEntities, downloadImageEntities);
                }

                if (!target.ProductionTeams.Local.Any(e => e.Id == externalEntity.Id))
                {
                    target.ProductionTeams.Add(externalEntity);
                }

                localEntity = externalEntity;
            }

            return localEntity;
        }

        public static Cloud.Persistence.Entities.Presentation AddOrUpdate(this Persistence.EF.Context.ApplicationDbContext target, Cloud.Persistence.Entities.Presentation localEntity, Cloud.Persistence.Entities.Presentation externalEntity, List<Image> deleteImageEntities = null, List<Image> downloadImageEntities = null, List<Cloud.Persistence.Entities.Presentation> unhandledList = null)
        {
            if (localEntity != null)
            {
                localEntity.At = externalEntity.At;

                var localShow = target.Shows.FirstOrDefault(e => e.Id == externalEntity.ShowId);

                target.AddOrUpdate(localShow, externalEntity.Show, deleteImageEntities, downloadImageEntities);

                localEntity.ShowId = localShow.Id;
                localEntity.Show = localShow;

                var localProductionTeam = target.ProductionTeams.Include(e => e.EmployBridges).FirstOrDefault(e => e.Id == externalEntity.ProductionTeamId);

                localProductionTeam = target.AddOrUpdate(localProductionTeam, externalEntity.ProductionTeam, deleteImageEntities, downloadImageEntities);

                localEntity.ProductionTeamId = localProductionTeam.Id;
                localEntity.ProductionTeam = localProductionTeam;

                target.Presentations.Update(localEntity);

                unhandledList?.Remove(localEntity);
            }
            else
            {
                var localShow = target.Shows.FirstOrDefault(e => e.Id == externalEntity.ShowId);

                target.AddOrUpdate(localShow, externalEntity.Show, deleteImageEntities, downloadImageEntities);

                var localProductionTeam = target.ProductionTeams.FirstOrDefault(e => e.Id == externalEntity.ProductionTeamId);

                localProductionTeam = target.AddOrUpdate(localProductionTeam, externalEntity.ProductionTeam, deleteImageEntities, downloadImageEntities);

                if (!target.Presentations.Local.Any(e => e.Id == externalEntity.Id))
                {
                    target.Presentations.Add(externalEntity);
                }

                localEntity = externalEntity;
            }

            return localEntity;
        }
    }
}
