﻿using System;
using System.Web.ModelBinding;

using Moq;

using NUnit.Framework;

using SalaryCalculator.Data.Models;
using SalaryCalculator.Data.Services.Contracts;
using SalaryCalculator.Mvp.EventsArguments;
using SalaryCalculator.Mvp.Presenters.Settings;
using SalaryCalculator.Mvp.Views.Settings;
using SalaryCalculator.Tests.Mocks;

namespace SalaryCalculator.Tests.Mvp.Presenters.SettingsNonLaborContractsPresenterTests
{
    [TestFixture]
    public class View_UpdateRemunerationBill_Should
    {
        [Test]
        public void AddModelError_WhenItemIsNotFound()
        {
            var view = new Mock<ISettingsNonLaborContractsView>();
            view.Setup(v => v.ModelState).Returns(new ModelStateDictionary());
            string errorKey = string.Empty;
            int billId = 1;
            string expectedError = String.Format("RemunerationBill with id {0} was not found", billId);
            var billService = new Mock<IRemunerationBillService>();
            billService.Setup(c => c.GetById(billId)).Returns<RemunerationBill>(null);

            ISettingsNonLaborContractsPresenter presenter = new SettingsNonLaborContractsPresenter
                (view.Object, billService.Object);

            view.Raise(v => v.UpdateModel += null, new ModelIdEventArgs(billId));

            Assert.AreEqual(1, view.Object.ModelState[errorKey].Errors.Count);
            StringAssert.AreEqualIgnoringCase(expectedError, view.Object.ModelState[errorKey].Errors[0].ErrorMessage);
        }

        [Test]
        public void TryUpdateModelIsNotCalled_WhenRemunerationBillIsNotFound()
        {
            var view = new Mock<ISettingsNonLaborContractsView>();
            view.Setup(v => v.ModelState).Returns(new ModelStateDictionary());
            string errorKey = string.Empty;
            int billId = 1;
            var billService = new Mock<IRemunerationBillService>();
            billService.Setup(c => c.GetById(billId)).Returns<RemunerationBill>(null);

            ISettingsNonLaborContractsPresenter presenter = new SettingsNonLaborContractsPresenter
                (view.Object, billService.Object);

            view.Raise(v => v.UpdateModel += null, new ModelIdEventArgs(billId));

            view.Verify(v => v.TryUpdateModel(It.IsAny<RemunerationBill>()), Times.Never());
        }

        [Test]
        public void UpdateByIdIsCalled_WhenRemunerationBillIsFoundAndIsInValidState()
        {
            var view = new Mock<ISettingsNonLaborContractsView>();
            view.Setup(v => v.ModelState).Returns(new ModelStateDictionary());

            int billId = 1;
            var billService = new Mock<IRemunerationBillService>();
            var bill = new FakeRemunerationBill() { Id = billId };
            billService.Setup(c => c.GetById(billId)).Returns(bill);

            ISettingsNonLaborContractsPresenter presenter = new SettingsNonLaborContractsPresenter
                (view.Object, billService.Object);

            view.Raise(v => v.UpdateModel += null, new ModelIdEventArgs(billId));

            billService.Verify(c => c.UpdateById(billId, bill), Times.Once());
        }
    }
}
