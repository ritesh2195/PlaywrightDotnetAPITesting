namespace APITesting.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class UserTest : BaseTest
    {
        [Test]
        public async Task GetUserTest()
        {
            var getUserResponse = await userService.GetUserDetailsAsync("5fab8a6ab45b2e0074a9616e");

            Assert.That(getUserResponse.Status, Is.EqualTo(200));
        }
    }
}
