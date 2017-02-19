﻿using System;
using System.Web.ModelBinding;

using Moq;

using NUnit.Framework;

using SalaryCalculator.Data.Models;
using SalaryCalculator.Data.Services.Contracts;
using SalaryCalculator.Mvp.EventsArguments;
using SalaryCalculator.Mvp.Presenters.Settings;
using SalaryCalculator.Mvp.Views.Settings;

namespace SalaryCalculator.Tests.Mvp.Presenters.SettingsEmployeesPresenterTests
{
    [TestFixture]
    public class View_UpdateEmployee_Should
    {
        [Test]
        public void AddModelError_WhenItemIsNotFound()
        {
            var view = new Mock<ISettingsEmployeesView>();
            view.Setup(v => v.ModelState).Returns(new ModelStateDictionary());
            string errorKey = string.Empty;
            int employeeId = 1;
            string expectedError = String.Format("Employee with id {0} was not found", employeeId);
            var employeeService = new Mock<IEmployeeService>();
            employeeService.Setup(c => c.GetById(employeeId)).Returns<Employee>(null);

            ISettingsEmployeesPresenter presenter = new SettingsEmployeesPresenter
                (view.Object, employeeService.Object);

            view.Raise(v => v.UpdateModel += null, new ModelIdEventArgs(employeeId));

            Assert.AreEqual(1, view.Object.ModelState[errorKey].Errors.Count);
            StringAssert.AreEqualIgnoringCase(expectedError, view.Object.ModelState[errorKey].Errors[0].ErrorMessage);
        }
    }
}
