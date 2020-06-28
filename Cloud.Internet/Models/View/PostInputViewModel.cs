using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class PostInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Post, string>
    {
        public PostInputViewModel()
        {
            AvailableCategorySelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            AvailableEmploySelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            SelectableTags = new System.Collections.Generic.List<SAPRESSC.Core.Common.Models.View.SelectionItemInputViewModel<string>>();
            PostedAt = System.DateTime.Now;
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Summary")]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "PostedAt")]
        public System.DateTime PostedAt { get; set; }

        [Display(Name = "Author")]
        public System.Guid AuthorId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableEmploySelectListItems { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCategorySelectListItems { get; set; }

        [Display(Name = "Tags")]
        public System.Collections.Generic.List<SAPRESSC.Core.Common.Models.View.SelectionItemInputViewModel<string>> SelectableTags { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 1048576, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "Poster")]
        public Microsoft.AspNetCore.Http.IFormFile PosterFile { get; set; }

        public bool ResetPoster { get; set; }

        public System.Guid PosterId { get; set; }

        public string PosterExtension { get; set; }

        public Post Export()
        {
            var slugHelper = new Slugify.SlugHelper();

            var entity = new Post
            {
                Id = slugHelper.GenerateSlug(Title)
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Post entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Summary = entity.Summary;
            Content = entity.Content;
            PostedAt = entity.PostedAt;
            AuthorId = entity.AuthorId;
            CategoryId = entity.Category?.Id;
            PosterId = entity.PosterId ?? System.Guid.Empty;

            if (PosterId != System.Guid.Empty)
            {
                PosterExtension = entity.Poster.ContentType.ContentTypeToExtension();
            }
        }

        public void Mergue(Post entity)
        {
            entity.Title = Title;
            entity.Summary = Summary;
            entity.Content = Content;
            entity.PostedAt = PostedAt;
            entity.AuthorId = AuthorId;
        }
    }
}
