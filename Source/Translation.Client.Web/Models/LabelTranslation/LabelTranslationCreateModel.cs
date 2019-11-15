﻿using System;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.LabelTranslation
{
    public sealed class LabelTranslationCreateModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }

        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }
        public Guid ProjectLanguageUid { get; set; }

        public Guid LabelUid { get; set; }
        public string LabelKey { get; set; }

        public Guid LanguageUid { get; set; }
        public string LanguageName { get; set; }

        public string LabelTranslation { get; set; }

        public HiddenInputModel OrganizationInput { get; }

        public HiddenInputModel ProjectInput { get; }
        public HiddenInputModel ProjectNameInput { get; }
        public HiddenInputModel ProjectLanguageInput { get; }

        public HiddenInputModel LabelInput { get; }
        public HiddenInputModel LabelKeyInput { get; }

        public SelectInputModel LanguageInput { get; }
        public LongInputModel LabelTranslationInput { get; }

        public LabelTranslationCreateModel()
        {
            Title = "label_translation_create_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");

            ProjectInput = new HiddenInputModel("ProjectUid");
            ProjectNameInput = new HiddenInputModel("ProjectName");
            ProjectLanguageInput = new HiddenInputModel("ProjectLanguageUid");

            LabelInput = new HiddenInputModel("LabelUid");
            LabelKeyInput = new HiddenInputModel("LabelKey");

            LanguageInput = new SelectInputModel("Language", "language", "/Language/SelectData");
            LanguageInput.IsOptionTypeContent = true;
            LabelTranslationInput = new LongInputModel("LabelTranslation", "label_translation", true);
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();

            ProjectInput.Value = ProjectUid.ToUidString();
            ProjectNameInput.Value = ProjectName;
            ProjectLanguageInput.Value = ProjectLanguageUid.ToUidString();

            LabelInput.Value = LabelUid.ToUidString();
            LabelKeyInput.Value = LabelKey;

            if (LanguageUid.IsNotEmptyGuid())
            {
                LanguageInput.Value = LanguageUid.ToUidString();
                LanguageInput.Text = LanguageName;
            }

            LabelTranslationInput.Value = LabelTranslation;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
            }

            if (ProjectUid.IsEmptyGuid())
            {
                ErrorMessages.Add("project_uid_not_valid");
            }

            ProjectName = ProjectName.TrimOrDefault();
            if (ProjectName.IsEmpty())
            {
                ErrorMessages.Add("project_name_not_valid");
            }

            if (LabelUid.IsEmptyGuid())
            {
                ErrorMessages.Add("label_uid_not_valid");
            }

            LabelKey = LabelKey.TrimOrDefault();
            if (LabelKey.IsEmpty())
            {
                ErrorMessages.Add("label_key_not_valid");
            }

            if (LanguageUid.IsEmptyGuid())
            {
                LanguageInput.ErrorMessage.Add("language_uid_not_valid");
                InputErrorMessages.AddRange(LanguageInput.ErrorMessage);
            }

            LabelTranslation = LabelTranslation.TrimOrDefault();
            if (LabelTranslation.IsEmpty())
            {
                LabelTranslationInput.ErrorMessage.Add("label_translation_required_error_message");
                InputErrorMessages.AddRange(LabelTranslationInput.ErrorMessage);
            }
        }
    }
}
