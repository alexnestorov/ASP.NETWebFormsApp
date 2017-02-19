﻿using NUnit.Framework;

using SalaryCalculator.Mvp.EventsArguments;

namespace SalaryCalculator.Tests.Mvp.EventArguments.SelfEmploymentEventArgsTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [TestCase(800, 0, false)]
        public void SetAllProperties_WhenAllParametersArePassedCorrectly(decimal num1, decimal num2, bool state)
        {
            var eventArgs = new SelfEmploymentEventArgs(num1, num2, state);

            Assert.AreEqual(num1, eventArgs.SocialSecurityIncome);
            Assert.AreEqual(num2, eventArgs.AdditionalSocialSecurityIncome);
            Assert.AreEqual(state, eventArgs.IsInsuredForGeneralDiseaseAndMaternity);
        }
    }
}
