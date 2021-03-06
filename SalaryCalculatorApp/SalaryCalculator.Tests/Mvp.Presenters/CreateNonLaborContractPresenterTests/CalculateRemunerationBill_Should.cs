﻿using System;

using Moq;

using NUnit.Framework;

using SalaryCalculator.Data.Services.Contracts;
using SalaryCalculator.Factories;
using SalaryCalculator.Mvp.EventsArguments;
using SalaryCalculator.Mvp.Presenters.JobContracts;
using SalaryCalculator.Mvp.Views.JobContracts;
using SalaryCalculator.Tests.Mocks;
using SalaryCalculator.Utilities.Constants;

namespace SalaryCalculator.Tests.Mvp.Presenters.CreateNonLaborContractPresenterTests
{
    [TestFixture]
    public class CalculateRemunerationBill_Should
    {
        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(-0.05)]
        public void CalculateWage_ShouldThrowException_WhenEventArgsParamIsLessThanZero(decimal obj1)
        {
            var view = new Mock<ICreateNonLaborContractView>();
            var billService = new Mock<IRemunerationBillService>();
            var employeeService = new Mock<IEmployeeService>();
            var modelFactory = new Mock<ISalaryCalculatorModelFactory>();
            var calculate = new FakePayroll();
            var e = new Mock<IRemunerationBillEventArgs>();

            e.Setup(x => x.GrossSalary).Returns(obj1);

            var presenter = new CreateNonLaborContractPresenter(view.Object, billService.Object, employeeService.Object, modelFactory.Object, calculate);

            Assert.Throws<ArgumentOutOfRangeException>(() => presenter.CalculateRemunerationBill(new object { }, e.Object));
        }

        [TestCase(2000)]
        public void CalculateWage_ShouldSetPaycheckSocialSecurityIncomeCorrectly_WhenEventArgsParamIsPassedCorrectly(decimal obj1)
        {
            var view = new Mock<ICreateNonLaborContractView>();
            var billService = new Mock<IRemunerationBillService>();
            var employeeService = new Mock<IEmployeeService>();
            var modelFactory = new Mock<ISalaryCalculatorModelFactory>();
            var calculate = new FakePayroll();
            var e = new Mock<IRemunerationBillEventArgs>();

            view.SetupProperty(x => x.Model.Employee, new FakeEmployee() { Id = 10 });
            view.SetupProperty(x => x.Model.RemunerationBill, new FakeRemunerationBill());
            e.Setup(x => x.GrossSalary).Returns(obj1);
            modelFactory.Setup(x => x.GetRemunerationBill()).Returns(new FakeRemunerationBill());

            var presenter = new CreateNonLaborContractPresenter(view.Object, billService.Object, employeeService.Object, modelFactory.Object, calculate);

            presenter.CalculateRemunerationBill(new object { }, e.Object);

            Assert.AreEqual(obj1, view.Object.Model.RemunerationBill.SocialSecurityIncome);
        }

        [TestCase(2600)]
        [TestCase(2599.99)]
        [TestCase(2100)]
        [TestCase(500)]
        public void CalculateWage_ShouldSetPaycheckSocialSecurityEqualToGrossSalary_WhenEventArgsParamISPassedWithValueLessThan2600(decimal obj1)
        {
            var view = new Mock<ICreateNonLaborContractView>();
            var billService = new Mock<IRemunerationBillService>();
            var employeeService = new Mock<IEmployeeService>();
            var modelFactory = new Mock<ISalaryCalculatorModelFactory>();
            var calculate = new FakePayroll();
            var e = new Mock<IRemunerationBillEventArgs>();

            view.SetupProperty(x => x.Model.Employee, new FakeEmployee() { Id = 10 });
            view.SetupProperty(x => x.Model.RemunerationBill, new FakeRemunerationBill());
            e.Setup(x => x.GrossSalary).Returns(obj1);
            modelFactory.Setup(x => x.GetRemunerationBill()).Returns(new FakeRemunerationBill());

            var presenter = new CreateNonLaborContractPresenter(view.Object, billService.Object, employeeService.Object, modelFactory.Object, calculate);

            presenter.CalculateRemunerationBill(new object { }, e.Object);
            var expectedGrossSalary = obj1;

            Assert.AreEqual(expectedGrossSalary, view.Object.Model.RemunerationBill.SocialSecurityIncome);
        }

        [TestCase(3000)]
        [TestCase(2600.01)]
        [TestCase(2601)]
        public void CalculateRemunerationBill_ShouldSetBillSocialSecurityEqualTo2600_WhenGrossSalaryEventArgsParamIsPassedWithValueMoreThan2600(decimal obj1)
        {
            var view = new Mock<ICreateNonLaborContractView>();
            var billService = new Mock<IRemunerationBillService>();
            var employeeService = new Mock<IEmployeeService>();
            var modelFactory = new Mock<ISalaryCalculatorModelFactory>();
            var calculate = new FakePayroll();
            var e = new Mock<IRemunerationBillEventArgs>();

            view.SetupProperty(x => x.Model.Employee, new FakeEmployee() { Id = 10 });
            view.SetupProperty(x => x.Model.RemunerationBill, new FakeRemunerationBill());
            modelFactory.Setup(x => x.GetRemunerationBill()).Returns(new FakeRemunerationBill());
            e.Setup(x => x.GrossSalary).Returns(obj1);

            var presenter = new CreateNonLaborContractPresenter(view.Object, billService.Object, employeeService.Object, modelFactory.Object, calculate);

            presenter.CalculateRemunerationBill(new object { }, e.Object);

            Assert.AreEqual(ValidationConstants.MaxSocialSecurityIncome, view.Object.Model.RemunerationBill.SocialSecurityIncome);
        }
    }
}
