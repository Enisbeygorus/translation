﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class AssertViewModelTestHelper
    {

        public static void AssertRequiredInput(InputModel input)
        {
            Assert.IsTrue(input.IsRequired);
            Assert.IsNotEmpty(input.Name);
            Assert.IsNotEmpty(input.LabelKey);
        }

        public static void AssertInput(InputModel input)
        {
            Assert.IsNotEmpty(input.Name);
            Assert.IsNotEmpty(input.LabelKey);
        }

        public static void AssertInputErrorMessagesOfView<T>(IActionResult result, T model) where T : BaseModel
        {
            AssertViewWithModel<T>(result);

            model.InputErrorMessages.Clear();
            model.IsValid();

            var messages = ((T)((ViewResult)result).Model).InputErrorMessages;
            messages.Count.ShouldBe(model.InputErrorMessages.Count);

            foreach (var message in messages)
            {
                model.InputErrorMessages.ShouldContain(message);
            }
        }

        public static void AssertInputErrorMessagesForInvalidOrFailedResponse<T>(IActionResult result) where T : BaseModel
        {
            var messages = ((T)((ViewResult)result).Model).InputErrorMessages;
            messages.Any(x => x == StringOne).ShouldBeTrue();
        }

        public void AssertErrorMessagesForInvalidOrFailedResponse<T>(IActionResult result) where T : BaseModel
        {
            AssertViewWithModel<T>(result);

            var messages = ((T)((ViewResult)result).Model).ErrorMessages;
            messages.Any(x => x == StringOne).ShouldBeTrue();
        }

        public static void AssertViewWithModel<T>(IActionResult result)
        {
            result.ShouldNotBeNull();
            var viewResult = ((ViewResult)result);
            viewResult.ViewName.ShouldBeNull();
            viewResult.Model.ShouldNotBeNull();
            viewResult.Model.ShouldBeAssignableTo<T>();
        }

        public void AssertView<T>(ViewResult result)
        {
            result.ShouldNotBeNull();
            result.ViewName.ShouldBeNull();
            result.Model.ShouldNotBeNull();
            result.Model.ShouldBeAssignableTo<T>();
        }

        public void AssertView<T>(IActionResult result)
        {
            result.ShouldNotBeNull();
        }

        public void AssertView<T>(Task<IActionResult> result)
        {
            result.ShouldNotBeNull();
        }

        public void AssertView<T>(Task<RedirectResult> result)
        {
            result.ShouldNotBeNull();
        }

        public void AssertView<T>(JsonResult result)
        {
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeAssignableTo<T>();
        }

        public void AssertView<T>(FileResult result)
        {
            result.ShouldNotBeNull();
            result.ContentType.ShouldNotBeNull();
            result.FileDownloadName.ShouldNotBeNull();
        }

        public void AssertView<T>(RedirectResult result)
        {
            result.ShouldNotBeNull();
        }

        // todo:
        //public void AssertViewAccessDenied<T>(RedirectResult result)
        //{
        //    var controller = new BaseServiceController<OrganizationDto, UserDto>(null);
        //    var redirectAccessDenied = controller.RedirectToAccessDenied();

        //    result.ShouldNotBeNull();
        //    result.Url.ShouldBe(redirectAccessDenied.Url);
        //}

        // todo:
        //public void AssertViewAccessDenied(IActionResult result)
        //{
        //    var controller = new BaseServiceController<OrganizationDto, UserDto>(null);
        //    var redirectAccessDenied = controller.RedirectToAccessDenied();

        //    result.ShouldNotBeNull();
        //    ((RedirectResult)result).Url.ShouldBe(redirectAccessDenied.Url);
        //}

        //public void AssertViewRedirectToHome<T>(RedirectResult result)
        //{
        //    var controller = new BaseServiceController<OrganizationDto, UserDto>(null);
        //    var redirectToHome = controller.RedirectToHome();

        //    result.ShouldNotBeNull();
        //    result.Url.ShouldBe(redirectToHome.Url);
        //}

        //public void AssertViewRedirectToHome(IActionResult result)
        //{
        //    var controller = new BaseServiceController<OrganizationDto, UserDto>(null);
        //    var redirectToHome = controller.RedirectToHome();

        //    result.ShouldNotBeNull();
        //    ((RedirectResult)result).Url.ShouldBe(redirectToHome.Url);
        //}

        //public void AssertViewForbid<T>(ForbidResult result)
        //{
        //    var controller = new BaseServiceController<OrganizationDto, UserDto>(null);
        //    var forbid = controller.Forbid();

        //    result.ShouldNotBeNull();
        //    result.ShouldBeAssignableTo(forbid.GetType());
        //}

        //public void AssertViewNotFound<T>(NotFoundResult result)
        //{
        //    var controller = new BaseServiceController<OrganizationDto, UserDto>(null);
        //    var notFound = controller.NotFound();

        //    result.ShouldNotBeNull();
        //    result.ShouldBeAssignableTo(notFound.GetType());
        //}

        public void AssertView<T>(RedirectToActionResult result, string actionName)
        {
            result.ShouldNotBeNull();
            result.ActionName.ShouldBe(actionName);
        }

        public void AssertReturnType<T>(T result)
        {
            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<T>();
        }
    }
}