using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [ChangeUserPasswordRequiredAction]
    [CatchChangeUserPasswordRequiredException]
    [Route("management/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[controller]")]
    public class InstitutionInfoController : SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<InstitutionInfoNomenclator, Persistence.Enumerators.InstitutionInfoKind> institutionInfoNomenclatorRepository;
        private readonly IEntityRepository<InstitutionInfoUpdateRecord, long> institutionInfoUpdateRecordRepository;

        public InstitutionInfoController(IEntityRepository<InstitutionInfoUpdateRecord, long> institutionInfoUpdateRecordRepository, IEntityRepository<InstitutionInfoNomenclator, Persistence.Enumerators.InstitutionInfoKind> institutionInfoNomenclatorRepository, Microsoft.Extensions.Localization.IStringLocalizer<InstitutionInfoController> localizer, ILogger<InstitutionInfoController> logger) : base(localizer, logger)
        {
            this.institutionInfoNomenclatorRepository = institutionInfoNomenclatorRepository;
            this.institutionInfoUpdateRecordRepository = institutionInfoUpdateRecordRepository;
        }

        [HttpGet]
        [Route("")]
        public virtual IActionResult Index()
        {
            var keyValuePairs = new System.Collections.Generic.Dictionary<Persistence.Enumerators.InstitutionInfoKind, string>();

            var nomenclators = institutionInfoNomenclatorRepository.GetAll().ToArray();

            foreach (var nomenclator in nomenclators)
            {
                keyValuePairs.Add(nomenclator.Id, nomenclator.Value);
            }

            var displayModel = new Models.View.InstitutionInfoDisplayModel();

            displayModel.Import(keyValuePairs);

            return View(displayModel);
        }

        [HttpGet]
        [Route("[action]")]
        public virtual IActionResult Edit()
        {
            var keyValuePairs = new System.Collections.Generic.Dictionary<Persistence.Enumerators.InstitutionInfoKind, string>();

            var nomenclators = institutionInfoNomenclatorRepository.GetAll().ToArray();

            foreach (var nomenclator in nomenclators)
            {
                keyValuePairs.Add(nomenclator.Id, nomenclator.Value);
            }

            var modelInput = new Models.View.InstitutionInfoInputModel();

            modelInput.Import(keyValuePairs);

            return View(modelInput);
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(Models.View.InstitutionInfoInputModel modelInput)
        {
            if (ModelState.IsValid)
            {
                var nomenclators = institutionInfoNomenclatorRepository.GetAll().ToArray();

                var updatedFlag = false;

                foreach (var nomenclator in nomenclators)
                {
                    if (nomenclator.Id == Persistence.Enumerators.InstitutionInfoKind.AboutUs && modelInput.AboutUs != nomenclator.Value)
                    {
                        nomenclator.Value = modelInput.AboutUs;

                        institutionInfoNomenclatorRepository.Update(nomenclator, User.Identity.Name, false);

                        updatedFlag = true;
                    }

                    if (nomenclator.Id == Persistence.Enumerators.InstitutionInfoKind.History && modelInput.History != nomenclator.Value)
                    {
                        nomenclator.Value = modelInput.History;

                        institutionInfoNomenclatorRepository.Update(nomenclator, User.Identity.Name, false);

                        updatedFlag = true;
                    }

                    if (nomenclator.Id == Persistence.Enumerators.InstitutionInfoKind.ContactPhoneNumber && modelInput.ContactPhoneNumber != nomenclator.Value)
                    {
                        nomenclator.Value = modelInput.ContactPhoneNumber;

                        institutionInfoNomenclatorRepository.Update(nomenclator, User.Identity.Name, false);

                        updatedFlag = true;
                    }

                    if (nomenclator.Id == Persistence.Enumerators.InstitutionInfoKind.ContactEmailAddress && modelInput.ContactEmailAddress != nomenclator.Value)
                    {
                        nomenclator.Value = modelInput.ContactEmailAddress;

                        institutionInfoNomenclatorRepository.Update(nomenclator, User.Identity.Name, false);

                        updatedFlag = true;
                    }

                    if (nomenclator.Id == Persistence.Enumerators.InstitutionInfoKind.ContactPostalAddress && modelInput.ContactPostalAddress != nomenclator.Value)
                    {
                        nomenclator.Value = modelInput.ContactPostalAddress;

                        institutionInfoNomenclatorRepository.Update(nomenclator, User.Identity.Name, false);

                        updatedFlag = true;
                    }

                    if (nomenclator.Id == Persistence.Enumerators.InstitutionInfoKind.ContactMapLatitude && modelInput.ContactMapLatitude.ToString() != nomenclator.Value)
                    {
                        nomenclator.Value = modelInput.ContactMapLatitude.ToString();

                        institutionInfoNomenclatorRepository.Update(nomenclator, User.Identity.Name, false);

                        updatedFlag = true;
                    }

                    if (nomenclator.Id == Persistence.Enumerators.InstitutionInfoKind.ContactMapLongitude && modelInput.ContactMapLongitude.ToString() != nomenclator.Value)
                    {
                        nomenclator.Value = modelInput.ContactMapLongitude.ToString();

                        institutionInfoNomenclatorRepository.Update(nomenclator, User.Identity.Name, false);

                        updatedFlag = true;
                    }
                }

                if (updatedFlag)
                {
                    institutionInfoUpdateRecordRepository.Add(new InstitutionInfoUpdateRecord(), User.Identity.Name);
                }

                AddNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, "EditionSuccessMessage");

                return RedirectToAction("index");
            }

            AddNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, "InvalidMessage");

            return View(modelInput);
        }
    }
}
