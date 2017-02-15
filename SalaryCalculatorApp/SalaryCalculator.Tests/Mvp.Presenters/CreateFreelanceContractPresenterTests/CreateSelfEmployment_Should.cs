﻿using System;

using Moq;

using NUnit.Framework;

using SalaryCalculator.Data.Models;
using SalaryCalculator.Data.Services.Contracts;
using SalaryCalculator.Mvp.EventsArguments;
using SalaryCalculator.Mvp.Presenters;
using SalaryCalculator.Mvp.Views;
using SalaryCalculator.Tests.Mocks;

namespace SalaryCalculator.Tests.Mvp.Presenters.CreateFreelanceContractPresenterTests
{
    [TestFixture]
    public class CreateSelfEmployment_Should
    {
        [Test]
        public void CreateSelfEmployment_ShouldPassWithoutError()
        {
            var view = new Mock<ICreateFreelanceContractView>();
            var service = new Mock<ISelfEmploymentService>();
            var calculate = new FakePayroll();

            var presenter = new CreateFreelanceContractPresenter(view.Object, service.Object, calculate);
            var obj = new object { };
            var salary = new decimal();
            var e = new Mock<SelfEmploymentEventArgs>(salary, 0m, false);
            var insurance = new FakeSelfEmployment();
            view.SetupGet(x => x.Model.SelfEmployment).Returns(insurance);

            presenter.CreateSelfEmployment(obj, e.Object);

            service.Verify(x => x.Create(insurance), Times.Once);
        }

        [Test]
        public void CreateSelfEmployment_ShouldThrowArgumentNullException()
        {
            var view = new Mock<ICreateFreelanceContractView>();
            var service = new Mock<ISelfEmploymentService>();
            var calculate = new FakePayroll();

            var presenter = new CreateFreelanceContractPresenter(view.Object, service.Object, calculate);
            var obj = new object { };
            var salary = new decimal();
            var e = new Mock<SelfEmploymentEventArgs>(salary, 0m, false);
            SelfEmployment insurance = null;
            view.SetupGet(x => x.Model.SelfEmployment).Returns(insurance);

            Assert.Throws<ArgumentNullException>(() => presenter.CreateSelfEmployment(obj, e.Object));
        }
    }
}