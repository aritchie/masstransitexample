using System;
using ChatHub.Services;
using FluentAssertions;
using Xunit;


namespace ChatHub.Tests.Services
{
    public class BadWordsMessageCensorTests
    {
        [Fact]
        public void I_Dont_Like_Shit_Test() => new BadWordsMessageCensor().Scrub("shit").Contains("shit").Should().BeFalse();


        [Fact]
        public void I_Dont_Like_Poop_Test() => new BadWordsMessageCensor().Scrub("poop").Contains("poop").Should().BeFalse();
    }
}
