﻿using System;

using Moq;

using NUnit.Framework;

using SalaryCalculator.Data.Services.Contracts;
using SalaryCalculator.Mvp.Presenters.Reports;
using SalaryCalculator.Mvp.Views.Reports;

namespace SalaryCalculator.Tests.Mvp.Presenters.ReportLaborPresenterTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenParameterServiceIsNull()
        {
            var view = new Mock<IReportLaborView>();
            var service = new Mock<IEmployeePaycheckService>();

            Assert.That(() => new ReportLaborPresenter(view.Object, null), Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("The argument is null"));
        }

        [Test]
        public void Constructor_ShouldCreateInstanceCorrectly_WhenAllParametersArePassed()
        {
            var view = new Mock<IReportLaborView>();
            var service = new Mock<IEmployeePaycheckService>();

            Assert.IsInstanceOf<IReportLaborPresenter>(new ReportLaborPresenter(view.Object, service.Object));
        }
    }
}
