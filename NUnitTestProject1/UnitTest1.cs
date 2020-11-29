using Home.Shared;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestJwtExpired()
        {
            string expiredToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3BlbmNlcmRvdG5ldEBnbWFpbC5jb20iLCJleHAiOjE2MDY1Mzk3MzUsImlzcyI6ImhvbWUiLCJhdWQiOiJob21lIn0.uXr_2N5molMxWTdxpb_Y1xdYuaFZruvqsbcgK92wwt8";


            bool result = PasswordHash.JwtIsExpired(expiredToken);

            Assert.IsTrue(result);
        }
    }
}