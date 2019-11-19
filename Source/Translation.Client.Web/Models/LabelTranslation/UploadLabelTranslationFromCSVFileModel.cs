﻿using System;

using Microsoft.AspNetCore.Http;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.LabelTranslation
{
    public sealed class UploadLabelTranslationFromCSVFileModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid LabelUid { get; set; }
        public string LabelKey { get; set; }
        public IFormFile CSVFile { get; set; }
        public bool UpdateExistedTranslations { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel LabelInput { get; }
        public HiddenInputModel LabelKeyInput { get; }

        public FileInputModel CSVFileInput { get; }

        public CheckboxInputModel UpdateExistedTranslationsInput { get; set; }

        public UploadLabelTranslationFromCSVFileModel()
        {
            Title = "upload_labels_translation_from_csv_file_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            LabelInput = new HiddenInputModel("LabelUid");
            LabelKeyInput = new HiddenInputModel("LabelKey");

            CSVFileInput = new FileInputModel("CSVFile", "csv_file", true);

            UpdateExistedTranslationsInput = new CheckboxInputModel("UpdateExistedTranslations", "update_existed_translations");
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();

            LabelInput.Value = LabelUid.ToUidString();
            LabelKeyInput.Value = LabelKey;
            UpdateExistedTranslationsInput.Value = UpdateExistedTranslations;

            InfoMessages.Clear();
            InfoMessages.Add("the_file_must_be_UTF-8_encoded");
            InfoMessages.Add("you_update_label_translation_previously_added_that_have_same_language");
            InfoMessages.Add("if_you_add_multiple_translation_for_same_language_accepts_the_first_one");
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
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

            if (CSVFile == null)
            {
                CSVFileInput.ErrorMessage.Add("csv_required_error_message");
                InputErrorMessages.AddRange(CSVFileInput.ErrorMessage);
            }
        }
    }
}
