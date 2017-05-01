using System;
using ChatHub.Services;
using FluentAssertions;
using Xunit;


namespace ChatHub.Tests.Services
{
    public class BadWordsMessageFilterTests
    {
        [Fact]
        public void I_Dont_Like_Shit_Test() => new BadWordsMessageFilter().IsValid(" shit ").Should().BeFalse();


        [Fact]
        public void I_Dont_Like_Shitting_Test() => new BadWordsMessageFilter().IsValid(" shiting ").Should().BeFalse();
    }
}
