using System;
using Xunit;

namespace Regex
{
    using System.Text.RegularExpressions;

    public class IdMatchTest
    {
        [Theory]
        [InlineData("contoso://device-id-0", true, 0)]
        [InlineData("contoso://device-id-80", true, 80)]
        [InlineData("contoso://device-id-123", true, 123)]
        [InlineData("contoso://device-id0", false, 0)]
        public void IdMatch(string id, bool matches, int extracted)
        {
            var regex = new Regex(".*device-id-(\\d+)");
            var match = regex.Match(id);
            
            Assert.Equal(match.Success, matches);
            if (match.Success) {
                Assert.Equal(extracted.ToString(), match.Groups[1].Value);
            }
        }
        
        [Theory]
        [InlineData("contoso://device-id-0", "CO2", "device-id-0-co2")]
        [InlineData("contoso://device-id-80", "TEMP", "device-id-0-temp")]
        [InlineData("contoso://device-id-2", "CO2", "device-id-2-co2")]
        [InlineData("contoso://device-id-124", "TEMP", "device-id-4-temp")]
        public void ToTwinIdTest(string id, string deviceType, string expected)
        {
            Assert.Equal(ToTwinId(id, deviceType), expected);
        }
        
        private static string ToTwinId(string deviceId, string deviceType)
        {
            var regex = new Regex(".*device-id-(\\d+)");
            var match = regex.Match(deviceId);
            if (!match.Success || match.Groups.Count <= 1)
            {
                throw new ArgumentException($"Invalid deviceId: {deviceId}");
            }

            var idNumber = int.Parse(match.Groups[1].Value);
            return $"device-id-{idNumber % 5}-{deviceType.ToLower()}"; // distribute devices among 5 twins
        }
    }
}